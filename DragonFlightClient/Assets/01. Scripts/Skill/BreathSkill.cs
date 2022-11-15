using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathSkill : Skill
{
    public override void ActiveSkill()
    {
        //애니메이터 세팅
        
        Targeting();

        Effect();

        skillCoolTimer = 0f;
    }

    private void Effect()
    {

    }

    private Collider[] Targeting()
    {
        return new Collider[3];
    }
}
