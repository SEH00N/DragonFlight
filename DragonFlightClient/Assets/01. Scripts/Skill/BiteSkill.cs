using System.Collections.Generic;
using UnityEngine;

public class BiteSkill : Skill
{
    [SerializeField] Collider biteRange = null;
    [SerializeField] float damage = 10f;
    private Dragon dragon;

    private void Awake()
    {
        dragon = GetComponentInParent<Dragon>();
    }

    public override void ActiveSkill()
    {
        dragon.Animator.SetTrigger("OnBite");
        TriggerAnimPacket triggerAnimPacket = new TriggerAnimPacket("Dragon", "OnBite");
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.TriggerAnim, triggerAnimPacket);

        Collider[] enemies = Physics.OverlapBox(biteRange.transform.position, biteRange.bounds.size / 2f, biteRange.transform.rotation, DEFINE.EnemyDragonLayer | DEFINE.EnemyPlayerLayer);
        List<IDamageable> ids = new List<IDamageable>();

        foreach(Collider enemy in enemies)
            if(enemy.transform.root.TryGetComponent<IDamageable>(out IDamageable id))
            {
                if(!ids.Contains(id))
                {
                    ParticlePrefab effect = PoolManager.Instance.Pop("HitEffect") as ParticlePrefab;
                    effect.Init(enemy.transform.position);
                    ids.Add(id);
                }
            }

        if(ids.Count <= 0)
            return;

        foreach(IDamageable id in ids)
            id.OnDamage(damage);

        skillCoolTimer = 0f;
    }
}
