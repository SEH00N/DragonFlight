using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float holdingTime = 3f;

    [SerializeField] float maxHp = 100f;
    public float MaxHp => MaxHp;

    private float currentHp = 0f;
    public float CurrentHp { get => currentHp; set => currentHp = value; }

    private Dragon dragon = null;
    public Image hpBar = null;

    private void Awake()
    {
        dragon = GetComponent<Dragon>();
        hpBar = DEFINE.MainCanvas.Find("HP/DragonHPBar").GetChild(0).GetChild(0).GetComponent<Image>();


        CurrentHp = maxHp;
    }

    public void OnDamage(float damage)
    {
        if(currentHp <= 0f)
            return;

        // Debug.LogWarning(damage + " " + currentHp);
        currentHp -= damage;

        if(currentHp <= 0f)
            Die();
        else
            dragon.Animator.SetTrigger("OnDamage");
    }

    private void Die()
    {
        dragon.DragonMovement.Active = false;
        dragon.Animator.SetTrigger("OnDie");
        StartCoroutine(FallingDown());
    }

    private IEnumerator FallingDown()
    {
        StopAllCoroutines();
        float timer = 0f;

        while(timer <= holdingTime)
        {
            transform.position -= Vector3.down * DEFINE.GravityScale * Time.deltaTime;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //dissolve 해야됨
    }
}
