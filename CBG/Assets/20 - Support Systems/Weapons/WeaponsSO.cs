using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "CBG/Weapons")]
public class WeaponsSO : ScriptableObject
{
    public string weaponName;

    public int numberRounds;

    public float coolDownPeriodSec;

    public float timeBetweenShots;

    public bool automatic;

    [Header("Weapon Prefabs")]
    public GameObject trailPreFab;
    public GameObject muzzleFlashPrefab;
}
