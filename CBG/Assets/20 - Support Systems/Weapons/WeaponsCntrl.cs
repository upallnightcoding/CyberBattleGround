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

    // Start is called before the first frame update
    void Start()
    {
        numberRounds = weapons.numberRounds;
        muzzleFlash = weapons.muzzleFlashPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * FireWeapon() - 
     */
    public void FireWeapon()
    {
        if (!inCoolDown)
        {
            Shoot();
        }
    }

    /**
     * Shoot() - 
     */
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 30.0f, ForceMode.Impulse);
        Destroy(bullet, 3.0f);

        if (muzzleFlash)
        {
            GameObject flash = Instantiate(muzzleFlash, bullet.transform);
        }

        if (--numberRounds <= 0)
        {
            inCoolDown = true;
            Invoke("ResetCoolDown", weapons.coolDownPeriodSec);
        }

        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
    }

    /**
     * ResetCoolDown() - 
     */
    private void ResetCoolDown()
    {
        numberRounds = weapons.numberRounds;
        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
        inCoolDown = false;
    }
}
