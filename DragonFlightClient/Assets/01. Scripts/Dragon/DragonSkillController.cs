using UnityEngine;
using System.Collections.Generic;

public class DragonSkillController : MonoBehaviour
{
    [SerializeField] List<Skill> skills = new List<Skill>();
    private DragonMovement dragonMovement = null;

    private void Awake()
    {
        dragonMovement = GetComponent<DragonMovement>();
    }

    private void Update()
    {
        if(!dragonMovement.Active)
            return;

        if(Input.anyKeyDown)
            skills.ForEach(skill => {
                if(Input.GetKeyDown(skill.ActiveKey) && skill.skillCoolTimer > skill.skillCoolTime)
                    skill.ActiveSkill();
            });
    }
}
