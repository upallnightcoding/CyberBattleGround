using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsSO weapons;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzlePoint;

    private int numberRounds = 0;

    private bool inCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        numberRounds = weapons.numberRounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireWeapon()
    {
        if (!inCoolDown)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 30.0f, ForceMode.Impulse);
        Destroy(bullet, 3.0f);

        if (--numberRounds <= 0)
        {
            inCoolDown = true;
            Invoke("ResetCoolDown", weapons.coolDownPeriodSec);
        }

        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
    }

    private void ResetCoolDown()
    {
        numberRounds = weapons.numberRounds;
        EventCntrl.Instance.InvokeOnUpdateNumberRounds(numberRounds);
        inCoolDown = false;
    }
}
