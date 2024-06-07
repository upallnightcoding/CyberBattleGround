using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTS_Controller
{
    public class Player_TSS_Controller : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private Transform weaponPosition;

        private Animator animator;
        private Vector3 moveCntrl, aimCntrl;
        private float rotationSpeed = 1000.0f;

        private WeaponsCntrl weaponCntrl;

        private bool aimLock = false;

        private Vector2 leftControl, rightControl;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            GameObject weapon = Instantiate(weaponPrefab, weaponPosition);
            weaponCntrl = weapon.GetComponent<WeaponsCntrl>();
            weapon.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, -90.0f);
        }

        void Update()
        {
            Update_TSS_Controller();
        }

        void Update_TSS_Controller()
        {
            Gamepad controller = Gamepad.current;
            if (controller == null) return;

            Vector3 cameraRotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);

            moveCntrl = new Vector3(leftControl.x, 0.0f, leftControl.y);
            aimCntrl = new Vector3(rightControl.x, 0.0f, rightControl.y);

            float moveAmt = Mathf.Abs(moveCntrl.x) + Mathf.Abs(moveCntrl.z);
            float aimAmt = Mathf.Abs(aimCntrl.x) + Mathf.Abs(aimCntrl.z);

            aimCntrl.Normalize();

            if (IsAming(aimAmt))
            {
                RotatePlayer(cameraRotation, aimCntrl);

                Vector3 walkAimDirection = Quaternion.LookRotation(new Vector3(-aimCntrl.x, 0.0f, aimCntrl.z)) * moveCntrl;
                animator.SetFloat("aim_x", walkAimDirection.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", walkAimDirection.z, 0.15f, Time.deltaTime);
            }
            else
            {
                if (IsMoving(moveAmt))
                {
                    RotatePlayer(cameraRotation, moveCntrl);
                }
            }

            animator.SetFloat("movement_amount", moveAmt);
            animator.SetBool("is_aiming", IsAming(aimAmt));
        }

        private void RotatePlayer(Vector3 cameraRotation, Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Quaternion.LookRotation(cameraRotation) * direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void Fire() => weaponCntrl.FireWeapon(true, Vector3.zero);

        private bool IsAming(float aimAmt) => (aimAmt > 0.3f) || aimLock;

        private bool IsMoving(float moveAmt) => (moveAmt > 0.0f);

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                leftControl = context.ReadValue<Vector2>();
            }

            if (context.canceled)
            {
                leftControl = Vector2.zero;
            }

        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                rightControl = context.ReadValue<Vector2>();
                Debug.Log($"rightControl: {rightControl}");
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Fire();
            }
        }

        public void OnAimLock(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                aimLock = !aimLock;
            }
        }


    }
}


