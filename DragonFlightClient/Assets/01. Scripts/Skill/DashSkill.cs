using UnityEngine;

public class DashSkill : Skill
{
    private Dragon dragon = null;

    private void Awake()
    {
        dragon = GetComponent<Dragon>();
    }

    public override void ActiveSkill()
    {
        
    }
}
