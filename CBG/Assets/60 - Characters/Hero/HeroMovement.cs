using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public InputCntrl inputCntrl;

    public float flySpeed = 20.0f;
    public float yawAmount = 120.0f;

    private float yaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        float horizontalInput = inputCntrl.Movement.x;
        float verticalInput = inputCntrl.Movement.y;

        yaw += horizontalInput * yawAmount * Time.deltaTime;
        //yaw = Mathf.Clamp(yaw, -90.0f, 90.0f);

        float pitch = Mathf.Lerp(0, 45, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput) * 1.5f;
        float roll = Mathf.Lerp(0, 45, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput) * 1.5f;

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

    }
}
