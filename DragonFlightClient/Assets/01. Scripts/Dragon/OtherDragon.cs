using System.Linq.Expressions;
using UnityEngine;

public class OtherDragon : PoolableMono, IDamageable
{
    public float MaxHp => throw new System.NotImplementedException();

    public float CurrentHp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private Animator animator = null;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDamage(float damage)
    {
        DamagePacket damagePacket = new DamagePacket("Dragon", damage);
        Client.Instance.SendMessages((int)Types.GameEvent, (int)GameEvents.Damage, damagePacket);
    }

    public void DoMove(Vector3 position, Quaternion rotation, float animValue)
    {
        transform.position = position;
        transform.rotation = rotation;
        animator.SetFloat("Move", animValue);
    }

    public override void Reset()
    {
        
    }
}
