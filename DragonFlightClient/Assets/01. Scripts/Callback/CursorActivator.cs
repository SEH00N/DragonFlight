using UnityEngine;

public class CursorActivator : MonoBehaviour
{
    public void ToggleCursorActive()
    {
        GameManager.Instance.CursorActive = !GameManager.Instance.CursorActive;
    }

    public void ToggleCursorActive(bool active)
    {
        GameManager.Instance.CursorActive = active;
    }
}
