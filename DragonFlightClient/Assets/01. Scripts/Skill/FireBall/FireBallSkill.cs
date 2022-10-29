using UnityEngine;

public class FireBallSkill : Skill
{
    [Header("Transform")]
    [SerializeField] Transform firePosition = null;
    [SerializeField] Transform xRotation = null;

    public override void ActiveSkill()
    {
        if(skillCoolTimer < skillCoolTime)
            return;

        //서버한테 요청 후 res로 소환하는 방식으로 수정해야됨
        skillCoolTimer = 0f;

        FireBall fireball = PoolManager.Instance.Pop("FireBall") as FireBall;
        fireball.Init(firePosition.position, new Vector3(xRotation.eulerAngles.x, transform.eulerAngles.y));
    }
}
