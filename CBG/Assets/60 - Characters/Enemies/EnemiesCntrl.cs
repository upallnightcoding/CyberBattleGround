using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private int maxDamage = 100;
    private float rotationSpeed = 20.0f;
    private float speed = 0.5f;

    private FiniteStateMachine fsm = null;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        fsm = new FiniteStateMachine();
        fsm.Add(new RobotStateIdle(this));

        animator.SetFloat("speed", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Move(player.position);

        fsm.OnUpdate(Time.deltaTime);
    }

    public void Move(Vector3 position)
    {
        float step = speed * rotationSpeed;
        Vector3 direction = position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }
}
