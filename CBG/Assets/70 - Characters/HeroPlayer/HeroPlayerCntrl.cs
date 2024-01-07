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

    private Vector3 playerVelocity;

    private void Awake()
    {
        charCntrl = GetComponent<CharacterController>();
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
        Vector3 move = new Vector3(movement.x, 0.0f, movement.y);
        charCntrl.Move(move * playerSpeed * Time.deltaTime);
    }

    private void HandleAim()
    {
        //aim = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 target = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(target);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement = context.ReadValue<Vector2>();
            Debug.Log($"Mouse Move: {movement}");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aim = context.ReadValue<Vector2>();
            Debug.Log($"Aiming: {aim}");
        }
    }
}
