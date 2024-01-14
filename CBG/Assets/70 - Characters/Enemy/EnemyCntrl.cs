using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I got hit ... {collision.gameObject.name}");

        if (collision.gameObject.TryGetComponent<AmmoCntrl>(out AmmoCntrl ammoCntrl))
        {
            Debug.Log($"You hit ... {ammoCntrl.GetDamage()}");
            ammoCntrl.DisplayHitBlast(collision.GetContact(0).point);
        }
    }
}
