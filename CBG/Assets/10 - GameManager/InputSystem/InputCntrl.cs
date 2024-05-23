using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCntrl : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 Look { get; private set; }
    public bool IsAiming { get; private set; } = false;

    private bool firstLook = true;

    private InputCntrlDrag drag = null;

    public void SetDrag(InputCntrlDrag drag) => this.drag = drag;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Movement = context.ReadValue<Vector2>();
            //Debug.Log($"OnMove Performed: {context.ReadValue<Vector2>()}");
        }

        if (context.canceled)
        {
            Movement = Vector2.zero;
            //Debug.Log($"OnMove Canceled: {context.ReadValue<Vector2>()}");
        }
    }

    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            drag.LeftMouseButton(context);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            drag.FireWeapon();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Look = context.ReadValue<Vector2>();
            //Debug.Log($"OnLook Performed: {Look}");
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"OnAim Performed ...");
            IsAiming = !IsAiming;
        }
    }
}

public interface InputCntrlDrag
{
    public void LeftMouseButton(InputAction.CallbackContext context);
    public void FireWeapon();
}
