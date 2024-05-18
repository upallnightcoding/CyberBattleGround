using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCntrl : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 Look { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Movement = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            Movement = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Look = context.ReadValue<Vector2>();
        }
    }
}
