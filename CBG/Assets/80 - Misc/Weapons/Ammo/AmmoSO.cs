using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "CBG/Ammo")]
public class AmmoSO : ScriptableObject
{
    public int damage;
    public GameObject HitBlast;
}
