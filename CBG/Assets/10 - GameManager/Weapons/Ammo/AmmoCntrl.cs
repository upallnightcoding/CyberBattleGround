using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    [SerializeField] private AmmoSO ammo;

    public int GetDamage()
    {
        return(ammo.damage);
    }

    public void DisplayHitBlast(Vector3 position)
    {
        GameObject go = Instantiate(ammo.HitBlast, position, Quaternion.identity);
        Destroy(go, 1.0f);
    }
}
