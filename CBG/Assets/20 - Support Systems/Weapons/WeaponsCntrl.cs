using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsSO weapons;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzlePoint;

    private int numberRounds = 0;
    private GameObject muzzleFlash;

    private bool inCoolDown = false;
    private bool isReadyToPullTrigger = true;
    private float coolDownPeriodSec;
    private float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        numberRounds = weapons.numberRounds;
        muzzleFlash = weapons.muzzleFlashPrefab;
        coolDownPeriodSec = weapons.coolDownPeriodSec;
        timeBetweenShots = weapons.timeBetweenShots;

        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * FireWeapon() - 
     */
    public void FireWeapon(bool trigger, Vector2 mousePosition)
    {
        if (!inCoolDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Shoot(hit.point);
            }
        }
    }

    /**
     * Shoot() - 
     */
    private void Shoot(Vector3 target)
    {
        if (numberRounds > 0)
        {
            PullTrigger(target);

            EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);

            if (numberRounds == 0)
            {
                inCoolDown = true;
                StartCoroutine(ResetCoolDown(weapons.coolDownPeriodSec));
            }
        } 
    }

    private void PullTrigger(Vector3 target)
    {
        if (isReadyToPullTrigger)
        {
            Vector3 direction = (target - gameObject.transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(direction * 30.0f, ForceMode.Impulse);
            Destroy(bullet, 3.0f);

            if (muzzleFlash)
            {
                GameObject flash = Instantiate(muzzleFlash, bullet.transform);
            }

            numberRounds--;
            isReadyToPullTrigger = false;
            Invoke("ResetTriggerPull", weapons.timeBetweenShots);
        }
    }

    private void ResetTriggerPull()
    {
        isReadyToPullTrigger = true;
    }

    /**
     * ResetCoolDown() - 
     */
    private IEnumerator ResetCoolDown(float coolDownPeriodSec)
    {
        float seconds = 0.0f;

        while (seconds < coolDownPeriodSec)
        {
            yield return null;
            seconds += Time.deltaTime;
            EventCntrl.Instance.InvokeOnCoolDownUpdate(seconds / coolDownPeriodSec);
        }

        numberRounds = weapons.numberRounds;
        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
        inCoolDown = false;
    }
}
