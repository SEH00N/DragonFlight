using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool cursorActive = false;
    public bool CursorActive {
        get => cursorActive;
        set {
            cursorActive = value;
            Cursor.visible = cursorActive;
            Cursor.lockState = cursorActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void Awake()
    {
        Cursor.visible = cursorActive;
        Cursor.lockState = cursorActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
            if(Input.GetKey(KeyCode.LeftShift))
                if(Input.GetKeyDown(KeyCode.Comma))
                    CursorActive = !CursorActive;
    }
}
