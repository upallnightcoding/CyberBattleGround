using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCntrl : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private GameObject bulletSparks;

    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bulletTrail, gameObject.transform);
    }

    public void RemoveBullet(Transform parent, Vector3 contactPoint)
    {
        Instantiate(bulletSparks, contactPoint, transform.rotation, parent);
        Destroy(this);
    }

    public int GetDamage() => damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageCntrl damageCntrl))
        {
            damageCntrl.TakeDamage(damage);

            Instantiate(bulletSparks, collision.GetContact(0).point, Quaternion.identity, collision.gameObject.transform);

            Destroy(gameObject);
        }
    }
}
