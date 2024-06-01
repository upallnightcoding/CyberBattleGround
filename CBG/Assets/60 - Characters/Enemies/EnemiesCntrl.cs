using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private int maxDamage = 100;
    private float rotationSpeed = 20.0f;
    private float speed = 0.5f;

    private int health = 0;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * rotationSpeed;
        Vector3 direction = player.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter ...");

        if (collision.gameObject.TryGetComponent(out BulletCntrl bulletCntrl))
        {
            TakeDamage(bulletCntrl.GetDamage());
        }
    }
}
