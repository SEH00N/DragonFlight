using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TStringRange : MonoBehaviour
{
    [SerializeField] string s;

    private void Awake()
    {
        // for(int i = 0; i < int.MaxValue; i ++)
        // {
        //     s += "A";
        //     Debug.Log(s.Length);
        // }
    }
}
