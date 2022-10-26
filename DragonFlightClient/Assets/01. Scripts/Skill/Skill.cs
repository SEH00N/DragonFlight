using System.IO.Enumeration;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float skillCoolTime = 0f;
    [SerializeField] KeyCode activeKey = KeyCode.Q;
    public KeyCode ActiveKey => activeKey;

    protected float skillCoolTimer = 0f;

    public abstract void ActiveSkill();

    private void Update()
    {
        if(skillCoolTimer < skillCoolTime)
            skillCoolTimer += Time.deltaTime;
    }
}
