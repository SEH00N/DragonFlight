using UnityEngine;

public class Player : PoolableMono
{
    #region Property
    private PlayerHealth playerHealth = null;
    public PlayerHealth PlayerHealth {
        get {
            if(playerHealth == null)
                playerHealth = GetComponent<PlayerHealth>();

            return playerHealth;
        }
    }

    private PlayerMovement playerMovement = null;
    public PlayerMovement PlayerMovement {
        get {
            if(playerMovement == null)
                playerMovement = GetComponent<PlayerMovement>();

            return playerMovement;
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

    private void Start()
    {
        Reset();
    }

    public override void Reset()
    {
        PlayerHealth.CurrentHp = PlayerHealth.MaxHp;

        PlayerMovement.StartSendData();
    }
}