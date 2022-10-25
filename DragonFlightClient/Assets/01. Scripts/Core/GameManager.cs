using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool cursorVisible = false;

    private void Update()
    {
        if(Cursor.visible != cursorVisible)
            Cursor.visible = cursorVisible;
    }
}
