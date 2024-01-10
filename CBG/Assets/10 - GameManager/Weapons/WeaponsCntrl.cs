using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsSO weapon;
    [SerializeField] private Transform muzzlePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire()
    {
        Instantiate(weapon.muzzleBlast, muzzlePoint.position, muzzlePoint.rotation);
    }
}
