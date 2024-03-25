using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCntrl : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Movement = context.ReadValue<Vector2>();
            Debug.Log($"Movement: {Movement}");
        }
    }
}
