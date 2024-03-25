using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float pathUpdateDelay = 0.2f;
    [SerializeField] private GameObject weapon;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private float shootingDistance;
    private float pathUpdateDeadLine = 0.0f;

    private FiniteStateMachine fsm = null;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        fsm = new FiniteStateMachine();
        fsm.Add(new StateEnemyIdle(this));
    }

    // Start is called before the first frame update
    void Start()
    {
        shootingDistance = navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        fsm.OnUpdate(Time.deltaTime);
    }

    public void Speed()
    {
        animator.SetFloat("speed", navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    // Update is called once per frame
    void xxxUpdate()
    {
        bool inRange = Vector3.Distance(transform.position, player.position) <= shootingDistance;

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
        weapon.GetComponent<WeaponsCntrl>().FireWeapon();
        Debug.Log("Shooting ...");
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            pathUpdateDeadLine = Time.time + pathUpdateDelay;
            navMeshAgent.SetDestination(player.position);
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
