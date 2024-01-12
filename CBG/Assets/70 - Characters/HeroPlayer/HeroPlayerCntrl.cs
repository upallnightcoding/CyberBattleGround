using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroPlayerCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float gamePadRotateSmoothing = 1000.0f;

    [SerializeField] private WeaponsSO weaponSO;
    private GameObject weapon;

    [SerializeField] bool isGamePad;

    private CharacterController charCntrl;

    private Vector2 movement;
    private Vector2 aimScreen;
    private OnFireState fireState;

    private Animator animator;

    //private Vector3 playerVelocity;

    private Vector3 charCntrlMove;

    private Vector3 aimTarget;

    private Vector3 aimDirection;

    private Vector3 cameraForward;
    //private Vector3 move;
    //private Vector3 moveInput;
    private Transform cam;

    float forwardAmount;
    float turnAmount;

    WeaponsCntrl control;

    private void Awake()
    {
        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        aimTarget = Vector3.zero;
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon = Instantiate(weaponSO.weapon, new Vector3(0.618f, 1.123f, 1.452f), Quaternion.identity);
        control = weapon.GetComponent<WeaponsCntrl>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = inputCntrl.Movement;
        aimScreen = inputCntrl.AimScreen;
        fireState = inputCntrl.FireState;

        MovePlayer(Time.deltaTime);
        AimPlayer();
        TriggerOnFire();
    }

    private void TriggerOnFire()
    {
        switch(fireState)
        {
            case OnFireState.NOT_FIRING:
                break;
            case OnFireState.START_FIRING:
                FireWeapon();
                break;
            case OnFireState.END_FIRING:
                break;
        }
    }

    private void FireWeapon()
    {
        control.FireWeapon();
    }

    private void FinishedShooting()
    {
        control.FinishedShooting();
    }

    private void MovePlayer(float dt)
    {
        charCntrlMove.x = movement.x; // Horizontal
        charCntrlMove.y = 0.0f;
        charCntrlMove.z = movement.y; // Vertical

        cameraForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraMove = movement.y * cameraForward + movement.x * cam.right;
        cameraMove.Normalize();

        Vector3 localMove = transform.InverseTransformDirection(cameraMove);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;

        if (charCntrlMove != Vector3.zero)
        {
            charCntrl.Move(charCntrlMove * playerSpeed * dt);

            animator.SetFloat("Horizontal", turnAmount, 0.1f, dt);
            animator.SetFloat("Vertical", forwardAmount, 0.1f, dt);
        }
    }

    private void AimPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimScreen);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aimTarget.x = hit.point.x;
            aimTarget.y = transform.position.y;
            aimTarget.z = hit.point.z;
        }

        Vector3 targetAim = aimTarget - transform.position;
        targetAim.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(targetAim);

        aimDirection.x = aimTarget.x; 
        aimDirection.y = 0.0f;
        aimDirection.z = aimTarget.z;

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5.0f);
        }
    }
}
