using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : PoolableMono, IDamageable
{
    public float MaxHp => throw new System.NotImplementedException();

    public float CurrentHp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Animator animator = null;

    [SerializeField] ParticleSystem fireParticle = null;

    [SerializeField] List<Renderer> renderers = new List<Renderer>();
    [SerializeField] float dissolveTime = 1f;

    public void OnDamage(float damage)
    {
        // Debug.Log("아얏 시 발");
        animator.SetTrigger("OnDamage");
        DamagePacket damagePacket = new DamagePacket("Player", damage);
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.Damage, damagePacket);
    }

    public void DoFireEffect()
    {
        fireParticle.Stop();
        fireParticle.Play();
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
