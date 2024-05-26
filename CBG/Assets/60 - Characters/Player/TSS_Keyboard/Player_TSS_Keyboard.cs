using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_TSS_Keyboard
{
    public class Player_TSS_Keyboard : MonoBehaviour
    {
        private float speed = 4.0f;

        private Vector2 leftControl, rightControl;

        private CharacterController charCntrl;

        private Vector3 lookPos;

        private Animator animator;

        private Transform cam;
        private Vector3 camForward, move;
        private float forwardAmount, turnAmount;

        // Start is called before the first frame update
        void Start()
        {
            charCntrl = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            cam = Camera.main.transform;
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(rightControl);

            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                lookPos = hit.point;
            }

            Vector3 lookDir = lookPos - transform.position;
            lookDir.y = 0.0f;

            transform.LookAt(transform.position + lookDir, Vector3.up);

            float horizontal = leftControl.x;
            float vertical = leftControl.y;

            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * camForward + horizontal * cam.right;
            move.Normalize();

            Vector3 localMove = transform.InverseTransformDirection(move);
            turnAmount = localMove.x;
            forwardAmount = localMove.z;

            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);

            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            //charCntrl.Move(movement * speed * Time.deltaTime);
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
    }
}


