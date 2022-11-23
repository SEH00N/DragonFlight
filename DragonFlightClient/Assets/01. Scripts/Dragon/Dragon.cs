using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : PoolableMono
{
    #region Property
    private DragonHealth dragonHealth = null;
    public DragonHealth DragonHealth {
        get {
            if(dragonHealth == null)
                dragonHealth = GetComponent<DragonHealth>();

            return dragonHealth;
        }
    }

    private DragonMovement dragonMovement = null;
    public DragonMovement DragonMovement {
        get {
            if(dragonMovement == null)
                dragonMovement = GetComponent<DragonMovement>();

            return dragonMovement;
        }
    }

    private DragonSkillController skillController = null;
    public DragonSkillController SkillController {
        get {
            if(skillController == null)
                skillController = GetComponent<DragonSkillController>();

            return skillController;
        }
    }

    private Animator animator = null;
    public Animator Animator {
        get {
            if(animator == null)
                animator = GetComponent<Animator>();

            return animator;
        }
    }

    #endregion

    [SerializeField] List<Renderer> renderers = new List<Renderer>();
    [SerializeField] float dissolveTime = 1f;

    public bool targetable = true;

    // private void Start()
    // {
    //     //임시코드
    //     Reset();
    // }

    public override void Reset()
    {
        foreach (Renderer r in renderers)
            r.material.SetFloat("_Amount", -1);

        DragonHealth.CurrentHp = DragonHealth.MaxHp;

        DragonMovement.StartSendData();
    }

    public void OnFinish()
    {
        DragonMovement.Active = false;
        DragonMovement.StopSending();
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
}
