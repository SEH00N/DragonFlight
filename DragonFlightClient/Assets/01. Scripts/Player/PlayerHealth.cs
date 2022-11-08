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
            Debug.Log("쥬금");
        else
            player.Animator.SetTrigger("OnDamage");
    }

}
