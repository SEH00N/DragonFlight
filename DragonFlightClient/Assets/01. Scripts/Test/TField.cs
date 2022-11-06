using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FieldInfo fInfo = typeof(DEFINE).GetField("Dragon", BindingFlags.Static | BindingFlags.GetProperty);
        IDamageable id = fInfo as IDamageable;

        id.OnDamage(10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
