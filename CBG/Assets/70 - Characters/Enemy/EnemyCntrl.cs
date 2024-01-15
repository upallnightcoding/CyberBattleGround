using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float pathUpdateDelay = 0.2f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private float shootingDistance;
    private float pathUpdateDeadLine = 0.0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shootingDistance = navMeshAgent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

        if (inRange)
        {
            LookAtTarget();
        } else
        {
            UpdatePath();
        }

        animator.SetBool("shoot", inRange);

        animator.SetFloat("speed", navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    public void Shoot()
    {
        Debug.Log("Enemy is shooting ...");
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            pathUpdateDeadLine = Time.time + pathUpdateDelay;
            navMeshAgent.SetDestination(target.position);
        }
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
