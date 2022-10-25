using System.IO.Enumeration;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] KeyCode activeKey = KeyCode.Q;
    public KeyCode ActiveKey => activeKey;

    public abstract void ActiveSkill();
}
