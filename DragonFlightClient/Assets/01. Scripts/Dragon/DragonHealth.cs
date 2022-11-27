using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour, IDamageable
{
    [SerializeField] AudioSource damageSoundPlayer = null;
    [SerializeField] float holdingTime = 3f;

    [SerializeField] float maxHp = 100f;
    public float MaxHp => maxHp;

    private float currentHp = 0f;
    public float CurrentHp { 
        get => currentHp; 
        set {
            currentHp = value;
            if(hpBar)
                hpBar.fillAmount = currentHp / maxHp;
        }
    }
    private Dragon dragon = null;
    private Image hpBar = null;

    private void Awake()
    {
        dragon = GetComponent<Dragon>();
    }

    public void Init()
    {
        hpBar = DEFINE.MainCanvas.Find("HP/DragonHPBar").GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void OnDamage(float damage)
    {
        if(CurrentHp <= 0f)
            return;

        // Debug.LogWarning(damage + " " + CurrentHp);
        CurrentHp -= damage;

        if(CurrentHp <= 0f)
            Die();
        else
            dragon.Animator.SetTrigger("OnDamage");

        AudioManager.Instance.PlayAudio("DragonOnDamage", damageSoundPlayer);
    }

    private void Die()
    {
        dragon.DragonMovement.Active = false;
        dragon.targetable = false;
        DEFINE.Player.GetComponent<RideDragon>().DoRideOff();

        TriggerAnimPacket triggerAnimPacket = new TriggerAnimPacket("Dragon", "OnDie");
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.TriggerAnim, triggerAnimPacket);
        dragon.Animator.SetTrigger("OnDie");

        AudioManager.Instance.PlayAudio("DragonDie", damageSoundPlayer);

        dragon.DragonMovement.OnDieFallingEvent(holdingTime);
    }
}
