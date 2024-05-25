using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTS_Controller
{
    public class Player_TSS_Controller : MonoBehaviour
    {
        private Animator animator;
        private Vector3 v3_movement;
        private Vector3 v3_aim;
        private float rotation_speed = 1000.0f;

        private Vector2 leftControl, rightControl;

        private Vector3 preTargetPoint = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            //Update_TSS_Controller();
            Update_TSS_KeyBoard();
        }

        void Update_TSS_KeyBoard()
        {
            //Vector2 rightControl = Gamepad.current.rightStick.value;

            Vector3 camera_rotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);

            v3_movement = new Vector3(leftControl.x, 0.0f, leftControl.y);
            float movement_amount = Mathf.Abs(v3_movement.x) + Mathf.Abs(v3_movement.z);

            //v3_aim = new Vector3(rightControl.x, 0.0f, rightControl.y);
            //float aim_amount = Mathf.Abs(v3_aim.x) + Mathf.Abs(v3_aim.z);

            //Vector2 targetPoint = Mouse.current.position.ReadValue();
            Vector2 targetPoint = rightControl;
            Debug.Log($"Target Point: {targetPoint}");
            Vector3 hitPoint = Vector3.zero;
            float aim_amount = 0.0f;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(targetPoint), out RaycastHit hit, 100.0f))
            {
                hitPoint = hit.point;
                v3_aim = hitPoint - transform.position;
                aim_amount = Vector3.Distance(hitPoint, preTargetPoint);
                Debug.Log($"hitPoint/preTargetPoint: {hitPoint}/{preTargetPoint}");
            }

            if (aim_amount > 0.0)
            {
                Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_aim);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);

                Vector3 walk_aim_direction = Quaternion.LookRotation(new Vector3(-v3_aim.x, 0.0f, v3_aim.z)) * v3_movement;
                animator.SetFloat("aim_x", walk_aim_direction.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", walk_aim_direction.z, 0.15f, Time.deltaTime);

                preTargetPoint = hitPoint;
            }
            else
            {
                if (movement_amount > 0)
                {
                    Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_movement);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);
                }
            }

            if (aim_amount > 0)
            {
            }

            Debug.Log($"Aim Amount/Aim Amount: {v3_aim}/{aim_amount}");
            animator.SetFloat("movement_amount", movement_amount);
            animator.SetBool("is_aiming", aim_amount > 0.0 ? true : false);
            //animator.SetBool("is_aiming", true);

        }

        void Update_TSS_Cntrl()
        {
            Vector2 rightControl = Gamepad.current.rightStick.value;

            Vector3 camera_rotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);

            v3_movement = new Vector3(leftControl.x, 0.0f, leftControl.y);
            v3_aim = new Vector3(rightControl.x, 0.0f, rightControl.y);

            float movement_amount = Mathf.Abs(v3_movement.x) + Mathf.Abs(v3_movement.z);
            float aim_amount = Mathf.Abs(v3_aim.x) + Mathf.Abs(v3_aim.z);

            if (aim_amount > 0.3)
            {
                Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_aim);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);

                Vector3 walk_aim_direction = Quaternion.LookRotation(new Vector3(-v3_aim.x, 0.0f, v3_aim.z)) * v3_movement;
                animator.SetFloat("aim_x", walk_aim_direction.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", walk_aim_direction.z, 0.15f, Time.deltaTime);
            }
            else
            {
                if (movement_amount > 0)
                {
                    Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_movement);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);
                }
            }

            if(aim_amount > 0)
            {
                Debug.Log($"Aim Amount/Aim Amount: {v3_aim}/{aim_amount}");
            }

            animator.SetFloat("movement_amount", movement_amount);
            animator.SetBool("is_aiming", aim_amount > 0.3 ? true : false);

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log($"OnMove Performed ... {context.ReadValue<Vector2>()}");
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
                //Debug.Log($"OnAim Performed ... {context.ReadValue<Vector2>()}");
                rightControl = context.ReadValue<Vector2>();
            }
        }

        private void xxOnAnimatorMove()
        {
            Vector3 velocity = animator.deltaPosition;

            //charCntrl.Move(velocity);
        }

        // ==================================================================
        void Update_TSS_Controller()
        {
            var controller = Gamepad.current;
            if (controller == null) return;

            v3_movement = new Vector3(controller.leftStick.x.value, 0.0f, controller.leftStick.y.value);
            v3_aim = new Vector3(controller.rightStick.x.value, 0.0f, controller.rightStick.y.value);

            Debug.Log($"Left Stick: {controller.leftStick.x.value}/{controller.leftStick.y.value}");
            Debug.Log($"Right Stick: {controller.rightStick.x.value}/{controller.rightStick.y.value}");

            float movement_amount = Mathf.Abs(v3_movement.x) + Mathf.Abs(v3_movement.z);
            float aim_amount = Mathf.Abs(v3_aim.x) + Mathf.Abs(v3_aim.z);
            Vector3 camera_rotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);

            if (aim_amount > 0)
            {
                Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_aim);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);

                Vector3 walk_aim_direction = Quaternion.LookRotation(new Vector3(-v3_aim.x, 0.0f, v3_aim.z)) * v3_movement;
                animator.SetFloat("aim_x", walk_aim_direction.x, 0.15f, Time.deltaTime);
                animator.SetFloat("aim_y", walk_aim_direction.z, 0.15f, Time.deltaTime);
            } else
            {
                if (movement_amount > 0)
                {
                    Quaternion target_rotation = Quaternion.LookRotation(Quaternion.LookRotation(camera_rotation) * v3_movement);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);
                }
            }

            animator.SetFloat("movement_amount", movement_amount);
            animator.SetBool("is_aiming", aim_amount > 0 ? true : false);

        }
    }
}


