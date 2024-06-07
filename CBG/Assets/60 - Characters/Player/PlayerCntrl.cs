using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour, InputCntrlDrag
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform gunPoint;

    private Animator animator;

    private InputCntrl inputCntrl;

    private CharacterController charCntrl;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private Vector3 targetPoint;
    private bool reachedTargetPoint = true;

    private WeaponsCntrl weaponsCntrl = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputCntrl = GetComponent<InputCntrl>();
        charCntrl = GetComponent<CharacterController>();

        inputCntrl.SetDrag(this);

        animator.SetFloat("speed", 0.0f);

        GameObject weapon = Instantiate(weaponPrefab, gunPoint);
        weaponsCntrl = weapon.GetComponent<WeaponsCntrl>();
    }

    private void Update()
    {
        if (reachedTargetPoint)
        {
            animator.SetFloat("speed", 0.0f);
        } else
        {
            reachedTargetPoint = (Vector3.Distance(transform.position, targetPoint) < 0.1f);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;

        charCntrl.Move(velocity);
    }

    /**
     * FireWeapon()
     */
    public void FireWeapon()
    {
        //weaponsCntrl.FireWeapon();
    }

    /**
     * MovePlayer()
     */
    public void LeftMouseButton(InputAction.CallbackContext context)
    {
        Debug.Log($"OnLeftMouseButton Performed ...");

        MoveTo(Mouse.current.position.ReadValue());

        StartCoroutine(DragUpdate(context));
    }

    /**
     * MovePlayer()
     */
    private void MoveTo(Vector2 mousePosition)
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

    /**
     * DragUpdate() 
     */
    private IEnumerator DragUpdate(InputAction.CallbackContext context)
    {
        while (context.ReadValue<float>() != 0)
        {
            MoveTo(Mouse.current.position.ReadValue());
            yield return waitForFixedUpdate;
        }
    }
}
