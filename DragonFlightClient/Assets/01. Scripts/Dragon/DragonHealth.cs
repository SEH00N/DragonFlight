using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour, IDamageable
{
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
    }

    private void Die()
    {
        dragon.DragonMovement.Active = false;
        dragon.targetable = false;
        DEFINE.Player.GetComponent<RideDragon>().DoRideOff();

        TriggerAnimPacket triggerAnimPacket = new TriggerAnimPacket("Dragon", "OnDie");
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.TriggerAnim, triggerAnimPacket);
        dragon.Animator.SetTrigger("OnDie");

        StartCoroutine(FallingDown());
    }

    private IEnumerator FallingDown()
    {
        float timer = 0f;

        while(timer <= holdingTime)
        {
            transform.position -= Vector3.down * DEFINE.GravityScale * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        //dissolve 해야됨
        //드래곤 죽었을 때 서버에 보내기 (상대방 입장에서 디졸브)
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.DragonDie, "");
    }
}
