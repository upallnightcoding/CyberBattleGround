using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsSO weapon;
    [SerializeField] private Transform muzzlePoint;

    private bool shooting;
    private bool reloading;
    private int numberRounds;

    // Start is called before the first frame update
    void Awake()
    {
        numberRounds = weapon.numberRounds;
        shooting = false;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartReloading()
    {
        reloading = true;
        numberRounds = weapon.numberRounds;
        Invoke(nameof(FinishedReloading), weapon.reloadTimeSec);
    }

    private void FinishedReloading()
    {
        reloading = false;
    }

    public void FireWeapon()
    {
        if (!shooting && numberRounds-- > 0)
        {
            shooting = true;
            Instantiate(weapon.muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
            GameObject round = Instantiate(weapon.round, muzzlePoint.position, Quaternion.identity);
            round.transform.forward = muzzlePoint.forward;
            round.GetComponent<Rigidbody>().AddForce(muzzlePoint.forward * weapon.roundSpeed, ForceMode.Impulse);

            Debug.DrawLine(muzzlePoint.position, muzzlePoint.position + muzzlePoint.forward * 10.0f, Color.red, 30.0f);
            Debug.Log($"FireWeapong: {shooting}/{numberRounds}/{muzzlePoint.forward}");

            Invoke(nameof(FinishedShooting), weapon.SecBetweenRounds());
        }
    }

    public void FinishedShooting()
    {
        shooting = false;
    }
}
