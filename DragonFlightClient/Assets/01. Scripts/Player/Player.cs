using System.Collections;
using System.Collections.Generic;
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

    private WeaponHandler weaponHandler = null;
    public WeaponHandler WeaponHandler {
        get {
            if(weaponHandler == null)
                weaponHandler = GetComponent<WeaponHandler>();
            return weaponHandler;
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

    private RideDragon rideDragon = null;
    public RideDragon RideDragon {
        get {
            if(rideDragon == null)
                rideDragon = GetComponent<RideDragon>();
            return rideDragon;
        }
    }
    
    #endregion

    [SerializeField] List<Renderer> renderers = new List<Renderer>();
    [SerializeField] float dissolveTime = 1f;

    public override void Reset()
    {
        foreach (Renderer r in renderers)
            r.material.SetFloat("_Amount", -1);

        PlayerMovement.StartSendData();

        RideDragon.Init();
        PlayerHealth.Init();
    }

    public void OnFinish()
    {
        PlayerMovement.Active = false;
        playerMovement.StopSending();

        WeaponHandler.Active = false;
        SniperRifle sniperRifle = weaponHandler.CurrentWeapon as SniperRifle;
        sniperRifle.ZoomOut();
        StartCoroutine(DissolveCoroutine());
        //디졸브 시작
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
