using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroPlayerCntrl : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float gamePadRotateSmoothing = 1000.0f;

    [SerializeField] bool isGamePad;

    private CharacterController charCntrl;

    private Vector2 movement;
    private Vector2 aim;

    private Animator animator;

    private Vector3 playerVelocity;

    private Vector3 move;

    private void Awake()
    {
        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAim();
    }

    private void HandleMovement()
    {
        float horizontal = movement.x;
        float vertical = movement.y;

        move.x = horizontal;
        move.y = 0.0f;
        move.z = vertical;

        charCntrl.Move(move * playerSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
    }

    private void HandleAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(aim);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(target);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement = context.ReadValue<Vector2>();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aim = context.ReadValue<Vector2>();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Started");
        }

        if (context.canceled)
        {
            Debug.Log("Canceled");
        }
    }
}
