using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCntrl 
{
    // UI - Update Number of Rounds
    //-----------------------------
    public event Action<int> OnUpdateNumberRounds = delegate { };
    public void InvokeOnUpdateNumberRounds(int numberRounds) => 
        OnUpdateNumberRounds.Invoke(numberRounds);

    /**
     * Instance() -
     */
    public static EventCntrl Instance
    {
        get
        {
            if (aInstance == null)
            {
                aInstance = new EventCntrl();
            }

            return (aInstance);
        }
    }

    public static EventCntrl aInstance = null;
}
