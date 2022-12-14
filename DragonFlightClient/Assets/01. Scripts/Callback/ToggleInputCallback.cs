using UnityEngine;
using UnityEngine.Events;

public abstract class ToggleInputCallback : MonoBehaviour
{
    public KeyCode inputKey;
    public UnityEvent ACallbackAction;
    public UnityEvent BCallbackAction;

    public bool OnA;

    protected virtual void Update()
    {
        if(Input.GetKeyDown(inputKey))
        {
            UnityEvent temp = OnA ? ACallbackAction : BCallbackAction;
            temp?.Invoke();
            OnA = !OnA;
            
            CallbackEvent();
        }
    }

    protected abstract void CallbackEvent();
}
