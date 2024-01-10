using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "CBG/Weapon")]
public class WeaponsSO : ScriptableObject
{
    [Header("Weapon Attributes")]
    public string weaponName;
    public int numberRounds;
    public float timeBetweenRoundsSec;
    public float reloadTimeSec;

    [Header("Weapon Model/Animation")]
    public GameObject weapon;
    public GameObject muzzleBlast;
    public GameObject round;
}
