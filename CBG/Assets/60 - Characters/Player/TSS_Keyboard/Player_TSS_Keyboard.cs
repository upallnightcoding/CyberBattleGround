using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_TSS_Keyboard
{
    public class Player_TSS_Keyboard : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private Transform weaponPosition;

        private Vector2 leftControl, rightControl;

        // GameObject Components
        private Animator animator;
        private LineRenderer lineRenderer;

        private Transform gameCamera;

        private Vector3 aimingPosition;

        private WeaponsCntrl weaponCntrl;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;

            gameCamera = Camera.main.transform;

            GameObject weapon = Instantiate(weaponPrefab, weaponPosition);
            weaponCntrl = weapon.GetComponent<WeaponsCntrl>();
            weapon.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, -90.0f);
        }

        // Update is called once per frame
        void Update()
        {
            AimWeapon(rightControl);
            MovePlayer(leftControl);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, aimingPosition);
        }

        /**
         * MovePlayer() - 
         */
        private void MovePlayer(Vector2 leftControl)
        {
            float horizontal = leftControl.x;
            float vertical = leftControl.y;

            Vector3 camForward = Vector3.Scale(gameCamera.up, new Vector3(1, 0, 1)).normalized;
            Vector3 move = vertical * camForward + horizontal * gameCamera.right;
            move.Normalize();

            Vector3 localMove = transform.InverseTransformDirection(move);
            float turnAmount = localMove.x;
            float forwardAmount = localMove.z;

            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        /**
         * AimWeapon() -
         */
        private void AimWeapon(Vector2 rightControl)
        {
            Ray ray = Camera.main.ScreenPointToRay(rightControl);
            aimingPosition = Vector3.zero;

            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                aimingPosition = hit.point;
            }

            Vector3 lookDir = aimingPosition - transform.position;
            lookDir.y = 0.0f;

            transform.LookAt(transform.position + lookDir, Vector3.up);
        }

        private void FireWeapon()
        {
            weaponCntrl.FireWeapon();
        }

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

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                rightControl = context.ReadValue<Vector2>();
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                FireWeapon();
            }
        }
    }
}


