using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicToggleInputCallback : ToggleInputCallback
{
    protected override void CallbackEvent()
    {
        
    }

    public void ActiveToggle(bool active)
    {
        OnA = active;
    }
}
