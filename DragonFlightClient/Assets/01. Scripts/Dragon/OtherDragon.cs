using System.Linq.Expressions;
using UnityEngine;

public class OtherDragon : PoolableMono, IDamageable
{
    public float MaxHp => throw new System.NotImplementedException();

    public float CurrentHp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Transform playerRidePosition;

    public Animator animator = null;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDamage(float damage)
    {
        DamagePacket damagePacket = new DamagePacket("Dragon", damage);
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.Damage, damagePacket);
    }

    public void DoMove(Vector3 position, Vector3 rotation, float animValue)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
        animator.SetFloat("Move", animValue);
    }

    public override void Reset()
    {
        
    }
}