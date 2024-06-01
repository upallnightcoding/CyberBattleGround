using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCntrl : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;

    private int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bulletTrail, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDamage() => damage;
}
