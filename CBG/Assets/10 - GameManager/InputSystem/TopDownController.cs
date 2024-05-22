using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController 
{
    private float rotationSpeed = 1000.0f;
    private Vector3 movement;
    private Vector3 aim;
    private Animator animator;
    private Transform player;

    // Update is called once per frame

    public TopDownController(Transform player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    public void Update(InputCntrl inputCntrl)
    {
        movement = new Vector3(inputCntrl.Movement.x, 0.0f, inputCntrl.Movement.y);
        aim = new Vector3(inputCntrl.Look.x, 0.0f, inputCntrl.Look.y);

        float movementAmount = Mathf.Abs(movement.x) + Mathf.Abs(movement.z);
        float aimAmount = Mathf.Abs(aim.x) + Mathf.Abs(aim.z);
        Vector3 cameraRotation = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.y);

        Debug.Log($"Amount Movement/Aim: {movementAmount}/{aimAmount}");

        if (aimAmount > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Quaternion.LookRotation(cameraRotation) * -aim);
            player.rotation = Quaternion.RotateTowards(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 walkAimDirection = Quaternion.LookRotation(new Vector3(-aim.x, 0.0f, aim.z)) * movement;
            animator.SetFloat("aim_x", walkAimDirection.x, 0.15f, Time.deltaTime);
            animator.SetFloat("aim_y", walkAimDirection.z, 0.15f, Time.deltaTime);

            Debug.Log($"Aiming (x/y): {walkAimDirection.x}/{walkAimDirection.z}");
        }
        else
        {
            if (movementAmount > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Quaternion.LookRotation(cameraRotation) * -movement);
                player.rotation = Quaternion.RotateTowards(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        animator.SetFloat("speed", movementAmount);
        animator.SetBool("isAiming", aimAmount > 0.2f ? true : false);
    }
}
