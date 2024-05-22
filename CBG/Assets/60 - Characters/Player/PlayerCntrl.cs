using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Animator animator;

    private InputCntrl inputCntrl;

    private CharacterController charCntrl;

    private TopDownController controller;
    //===============================
    private float rotationSpeed = 1000.0f;
    private Vector3 movement;
    private float movementAmount;
    private Vector3 aimPoint;
    private bool isAiming;

    private Vector3 targetPoint;
    private bool reachedTargetPoint = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputCntrl = GetComponent<InputCntrl>();
        charCntrl = GetComponent<CharacterController>();

        controller = new TopDownController(transform, animator);

        animator.SetFloat("speed", 0.0f);
    }

    private void Update()
    {
        //controller.Update(inputCntrl);
        if (reachedTargetPoint)
        {
            animator.SetFloat("speed", 0.0f);
        } else
        {
            reachedTargetPoint = (Vector3.Distance(transform.position, targetPoint) < 0.1f);
        }
    }

    private void TwinStickWithMouse()
    {
        //MovePlayer();
        //MovePlayer();
    }

    private void xxxMovePlayer()
    {
        float horizontal = inputCntrl.Movement.x;
        float vertical = inputCntrl.Movement.y;

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        float movementAmount = movement.normalized.magnitude;

        animator.SetFloat("speed", movementAmount);
    }

    private void MovePlayer(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        Vector3 lookPosition = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            lookPosition = hit.point;
            targetPoint = hit.point;
            animator.SetFloat("speed", 1.0f);
            reachedTargetPoint = false;
        }

        if (lookPosition != Vector3.zero)
        {
            Vector3 lookDir = lookPosition - transform.position;
            lookDir.y = 0.0f;

            transform.LookAt(transform.position + lookDir, Vector3.up);

            animator.SetFloat("speed", 1.0f);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;

        charCntrl.Move(velocity);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"OnFire Performed ...");
            //MovePlayer();
        }

        if (context.canceled)
        {
            Debug.Log($"OnFire Cancel ...");
        }
    }

    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"OnLeftMouseButton Performed ...");

            
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            MovePlayer(mousePosition);
            

            StartCoroutine(DragUpdate(context));
        }
    }

    private IEnumerator DragUpdate(InputAction.CallbackContext context)
    {
        while (context.ReadValue<float>() != 0)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            MovePlayer(mousePosition);
            yield return new WaitForFixedUpdate();
        }
    }

    //=================================================================================

    private void xxTwinStickWithMouse()
    {
        //Debug.Log($"Movement: {inputCntrl.Movement}");

        movement = new Vector3(inputCntrl.Movement.x, 0.0f, inputCntrl.Movement.y);
        isAiming = inputCntrl.IsAiming;

        Vector3 cameraRotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.y);

        if (isAiming)
        {
            Debug.Log($"Look: ${inputCntrl.Look}");

            aimPoint = new Vector3(inputCntrl.Look.x, 0.0f, inputCntrl.Look.y);

            Ray aimRay = Camera.main.ScreenPointToRay(inputCntrl.Look);

            if (Physics.Raycast(aimRay, out RaycastHit hit))
            {
                /*Vector3 aim = (hit.point - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(Quaternion.LookRotation(cameraRotation) * -aim);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                Vector3 walkAimDirection = Quaternion.LookRotation(new Vector3(-aim.x, 0.0f, aim.z)) * movement;
                animator.SetFloat("aim_x", walkAimDirection.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", walkAimDirection.z, 0.15f, Time.deltaTime);*/
                Vector3 p = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 aim = (p - transform.position);
                transform.LookAt(hit.point);
                animator.SetFloat("aim_x", aim.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", aim.z, 0.15f, Time.deltaTime);
            }

        } else
        {
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Quaternion.LookRotation(cameraRotation) * -movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                movement.Normalize();
                movementAmount = movement.magnitude;
            }
            else
            {
                movementAmount = 0.0f;
            }

            animator.SetFloat("speed", movementAmount);
        }

        animator.SetBool("isAiming", isAiming);
    }
}
