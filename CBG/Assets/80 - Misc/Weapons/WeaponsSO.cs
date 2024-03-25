using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "CBG/Weapon")]
public class WeaponsSO : ScriptableObject
{
    [Header("Weapon Attributes")]
    public string weaponName;
    public int numberRounds;
    public float roundsPerSec;
    public float reloadTimeSec;
    public bool automatic;
    public float roundSpeed;

    [Header("Weapon Model/Animation")]
    public GameObject weapon;
    public GameObject muzzleFlash;
    public GameObject round;

    public float SecBetweenRounds()
    {
        return (1.0f / roundsPerSec);
    }
}
