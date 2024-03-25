using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputCntrl_Old
{

    public class InputCntrl_Old : MonoBehaviour
    {
        public OnFireState FireState { get; private set; } = OnFireState.NOT_FIRING;

        public Vector2 Movement { get; private set; }
        public Vector2 AimScreen { get; private set; }

        public void ResetReload()
        {
            FireState = OnFireState.NOT_FIRING;
        }

        /*public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Movement = context.ReadValue<Vector2>();
            }
        }*/

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AimScreen = context.ReadValue<Vector2>();
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                FireState = OnFireState.START_FIRING;
            }

            if (context.canceled)
            {
                FireState = OnFireState.END_FIRING;
            }
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                FireState = OnFireState.RELOAD;
            }
        }
    }
}

public enum OnFireState
{
    NOT_FIRING,
    START_FIRING,
    END_FIRING,
    RELOAD
}
