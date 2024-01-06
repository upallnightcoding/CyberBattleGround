using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroPlayerCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Mouse Move: {context.ReadValue<Vector2>()}");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Aiming: {context.ReadValue<Vector2>()}");
        }
    }
}
