using UnityEditor;
using UnityEngine;

public class TWindow
{
    [MenuItem("What??/Is??/This??/Hello, World!")]
    private static void WhatIsThis()
    {
        Debug.Log("Hello, World!");
        Debug.LogWarning("Hello, World!");
        Debug.LogError("Hello, World!");
    }
}
