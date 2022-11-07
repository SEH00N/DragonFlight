using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : PoolableMono, IDamageable
{
    public float MaxHp => throw new System.NotImplementedException();

    public float CurrentHp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDamage(float damage)
    {
        DamagePacket damagePacket = new DamagePacket("Player", damage);
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.Damage, damagePacket);
    }

    public void DoMove(Vector3 position, Vector3 rotation, float animValue)
    {
        transform.position = position;
        transform.localRotation = Quaternion.Euler(rotation);
        animator.SetFloat("Move", animValue);
    }

    public void Riding(bool isRide)
    {
        if(isRide)
        {
            transform.position = DEFINE.OtherDragon.playerRidePosition.position;
            transform.SetParent(DEFINE.OtherDragon.playerRidePosition);
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        } else {
            transform.SetParent(null);
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public override void Reset()
    {

    }
}
