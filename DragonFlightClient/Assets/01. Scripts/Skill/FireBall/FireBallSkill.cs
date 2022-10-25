using UnityEngine;

public class FireBallSkill : Skill
{
    [Header("Transform")]
    [SerializeField] Transform firePosition = null;
    [SerializeField] Transform xRotation = null;

    public override void ActiveSkill()
    {
        //슈웅 해야됨
        FireBall fireball = PoolManager.Instance.Pop("FireBall") as FireBall;
        fireball.Init(firePosition.position, new Vector3(xRotation.localEulerAngles.x, transform.eulerAngles.y));
    }
}
