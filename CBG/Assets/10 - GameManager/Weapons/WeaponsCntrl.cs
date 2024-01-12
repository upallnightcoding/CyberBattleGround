using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsSO weapon;
    [SerializeField] private Transform muzzlePoint;

    private bool shooting;
    private int numberRounds;

    private WeaponsCntrlState currentState;

    // Start is called before the first frame update
    void Awake()
    {
        numberRounds = weapon.numberRounds;
        shooting = false;
        currentState = WeaponsCntrlState.WAITING_TO_SHOOT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireWeapon()
    {
        switch(currentState)
        {
            case WeaponsCntrlState.WAITING_TO_SHOOT:
                currentState = WeaponsCntrlState.SHOOTING;
                break;
            case WeaponsCntrlState.SHOOTING:
                break;
            case WeaponsCntrlState.WAITING_TO_STOP_SHOOTING:
                break;
            case WeaponsCntrlState.RELOAD:
                break;
        }
    }

    private WeaponsCntrlState WaitingToShootState()
    {
        WeaponsCntrlState nextState = WeaponsCntrlState.SHOOTING;

        return (WeaponsCntrlState.SHOOTING);
    }

    public void FireWeapon1()
    {
        if (!shooting)
        {
            shooting = true;
            Instantiate(weapon.muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
            GameObject round = Instantiate(weapon.round, muzzlePoint.position, Quaternion.identity);
            round.transform.forward = muzzlePoint.transform.forward;
            round.GetComponent<Rigidbody>().AddForce(muzzlePoint.forward * weapon.roundSpeed, ForceMode.Impulse);
            Invoke("FinishedShooting", weapon.SecBetweenRounds());

            if (--numberRounds == 0)
            {

            }
        }
    }

    public void FinishedShooting()
    {
        shooting = false;
    }
}

public enum WeaponsCntrlState
{
    WAITING_TO_SHOOT,
    SHOOTING,
    WAITING_TO_STOP_SHOOTING,
    RELOAD
}
