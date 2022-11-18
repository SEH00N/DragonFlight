using UnityEngine;
using UnityEngine.Events;

public class ToggleInputCallback : MonoBehaviour
{
    public KeyCode inputKey;
    public UnityEvent ACallbackAction;
    public UnityEvent BCallbackAction;

    public bool OnA;

    private void Update()
    {
        if(Input.GetKeyDown(inputKey))
        {
            UnityEvent temp = OnA ? ACallbackAction : BCallbackAction;
            temp?.Invoke();
            OnA = !OnA;
        }
    }
}
