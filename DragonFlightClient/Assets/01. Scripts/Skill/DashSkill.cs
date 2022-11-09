using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    [SerializeField] float dashTime = 10f;

    private Dragon dragon = null;

    private void Awake()
    {
        dragon = GetComponentInParent<Dragon>();
    }

    public override void ActiveSkill()
    {
        if(!dragon.DragonMovement.OnFlying || dragon.DragonMovement.OnDash)
            return;
        
        // dragon.Animator.SetTrigger("OnDash");
        // TriggerAnimPacket triggerAnimPacket = new TriggerAnimPacket("Dragon", "OnDash");
        // Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.TriggerAnim, triggerAnimPacket);
        skillCoolTimer = 0f;
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        dragon.DragonMovement.OnDash = true;
        yield return new WaitForSeconds(dashTime);
        dragon.DragonMovement.OnDash = false;
    }
}
