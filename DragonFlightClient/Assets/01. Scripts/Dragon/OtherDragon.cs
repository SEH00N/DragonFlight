using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OtherDragon : PoolableMono, IDamageable
{
    public float MaxHp => throw new System.NotImplementedException();

    public float CurrentHp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Transform playerRidePosition;

    public Animator animator = null;
    
    [SerializeField] List<Renderer> renderers = new List<Renderer>();
    [SerializeField] float dissolveTime = 1f;

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

    public void DoDissolve()
    {
        StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine()
    {
        float timer = 0f;

        while(timer < dissolveTime)
        {
            foreach (Renderer r in renderers)
                r.material.SetFloat("_Amount", Mathf.Lerp(-1, 0.7f, timer / dissolveTime));

            timer += Time.deltaTime;
            yield return null;
        }

        yield return null;

        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        foreach (Renderer r in renderers)
            r.material.SetFloat("_Amount", -1);
        
        if(animator == null)
            animator = GetComponent<Animator>();
    }
}
