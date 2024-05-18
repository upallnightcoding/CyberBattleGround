using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    private Animator animator;
    private Vector3 movement = new Vector3();
    private Vector3 aim;
    private float rotationSpeed = 1000.0f;

    private InputCntrl inputCntrl;

    private float horzInput, vertInput;
    private float movementMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputCntrl = GetComponent<InputCntrl>();
    }

    // Update is called once per frame
    void Update()
    {
        horzInput = inputCntrl.Movement.x;
        vertInput = inputCntrl.Movement.y;

        movement.x = horzInput;
        movement.y = 0.0f;
        movement.z = vertInput;

        movementMagnitude = Mathf.Clamp01(movement.magnitude);

        animator.SetFloat("speed", movementMagnitude, 0.05f, Time.deltaTime);

        Debug.Log($"Movement: {movementMagnitude}/{inputCntrl.Movement}/{inputCntrl.Look}");
    }
}
