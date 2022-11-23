using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp = 50f;
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

    private Player player = null;
    private Image hpBar = null;

    private void Awake()
    {
        player = GetComponent<Player>();
        hpBar = DEFINE.MainCanvas.Find("HP/PlayerHPBar").GetChild(0).GetChild(0).GetComponent<Image>();
        CurrentHp = maxHp;
    }

    public void OnDamage(float damage)
    {
        if (CurrentHp <= 0f)
            return;

        CurrentHp -= damage;

        if (CurrentHp <= 0f)
        {
            Debug.Log("쥬금");
            OnDie();
            return;
        }

        player.Animator.SetTrigger("OnDamage");
    }

    private void OnDie()
    {
        TriggerAnimPacket triggerAnimPacket = new TriggerAnimPacket("Player", "OnDie");
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.TriggerAnim, triggerAnimPacket);

        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.Finish, "");
    }
}
