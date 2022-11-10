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

    public bool targetable = true;

    private void Start()
    {
        //임시코드
        Reset();
    }

    public override void Reset()
    {
        DragonHealth.CurrentHp = DragonHealth.MaxHp;

        DragonMovement.StartSendData();
    }

    public void OnFinish()
    {
        DragonMovement.Active = false;
        DragonMovement.StopSending();
    }
}
