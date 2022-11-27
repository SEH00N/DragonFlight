using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using Cinemachine;
using UnityEngine;

public class SniperRifle : Weapon
{
    [Header("DefaultProperty")]
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] float fireDistance = 500f;
    [SerializeField] float damage = 10f;

    [Header("ZoomProperty")]
    [SerializeField] float zoomFov = 30f;
    [SerializeField] Transform zoomCamFollow;
    [SerializeField] Transform camLookAt;

    [Header("ShakeProperty")]
    [SerializeField] float frequency = 1f;
    [SerializeField] float power = 1f;
    [SerializeField] float shakeDelay = 1f;
    [SerializeField] float reboundAmount = 10f;
    [SerializeField] float reboundIncFactor = 5f;

    [Header("Effect")]
    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] AudioSource fireSoundPlayer;
    [SerializeField] AudioSource zoomSoundPlayer;

    private float defaultFov = 60f;

    private GameObject zoomPanel = null;
    public GameObject ZoomPanel {
        get {
            if(zoomPanel == null)
                zoomPanel = DEFINE.MainCanvas.Find("ZoomPanel").gameObject;

            return zoomPanel;
        }
    }

    private Player player = null;

    private Transform defaultCamFollow;
    private Transform camParent;
    private Vector3 hitPos;

    private CinemachineVirtualCamera cmMainCam = null;
    private CinemachineBasicMultiChannelPerlin perlin = null;

    private LineRenderer lineRenderer = null;

    private GameObject blockPanel = null;
    public GameObject BlockPanel {
        get {
            if(blockPanel == null)
                blockPanel = DEFINE.MainCanvas.Find("BlockPanel").gameObject;

            return blockPanel;
        }
    }

    private void Awake()
    {
        zoomPanel = DEFINE.MainCanvas.Find("ZoomPanel").gameObject;
        blockPanel = DEFINE.MainCanvas.Find("BlockPanel").gameObject;

        cmMainCam = DEFINE.CmMainCam;
        perlin = cmMainCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        camParent = cmMainCam.transform.parent;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        player = DEFINE.Player;
    }

    public override void ActiveWeapon()
    {
        if(TryTargeting(out Collider enemy))
            if(enemy.transform.root.TryGetComponent<IDamageable>(out IDamageable id))
                id.OnDamage(damage);

        FireEffect();
    }

    private void Update()
    {
        if(BlockPanel.activeSelf || !player.WeaponHandler.Active)
            return;

        if(Input.GetKeyDown(zoomKey))
            ZoomIn();
        if(Input.GetKeyUp(zoomKey))
            ZoomOut();
    }

    private void ZoomIn()
    {
        AudioManager.Instance.PlayAudio("Zoom", zoomSoundPlayer);

        ZoomPanel.SetActive(true);
        cmMainCam.m_Lens.FieldOfView = zoomFov;
        
        defaultCamFollow = cmMainCam.m_Follow;
        cmMainCam.m_Follow = zoomCamFollow;
    }

    private void ZoomOut()
    {
        AudioManager.Instance.PlayAudio("ZoomOut", zoomSoundPlayer);

        ZoomPanel.SetActive(false);
        cmMainCam.m_Lens.FieldOfView = defaultFov;
        
        if(cmMainCam.m_Follow == zoomCamFollow)
            cmMainCam.m_Follow = defaultCamFollow;
    }

    //해줘
    private bool TryTargeting(out Collider other)
    {
        bool returnValue = Physics.Raycast(cmMainCam.transform.position, cmMainCam.transform.forward, out RaycastHit hit, fireDistance, DEFINE.EnemyDragonLayer | DEFINE.EnemyPlayerLayer);
        // Debug.DrawRay(cmMainCam.transform.position, cmMainCam.transform.forward * fireDistance, Color.red, 1f);
        other = returnValue ? hit.collider : null;
        hitPos = returnValue ? hit.point : cmMainCam.transform.forward * fireDistance + fireParticle.transform.position;

        if(returnValue)
        {
            SpawnPacket spawnPacket = new SpawnPacket("HitEffect", hit.point, Vector3.zero);
            Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.Spawn, spawnPacket);
        }
        

        return returnValue;
    }

    //해줘
    private void FireEffect()
    {
        //빵야
        //파티클
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.Fire, "");
        fireParticle.Stop();
        fireParticle.Play();

        StartCoroutine(LineCoroutine());

        //피융
        StartCoroutine(ReboundCoroutine());
        AudioManager.Instance.PlayAudio("RifleFire", fireSoundPlayer);    

        //shake cam
        StartCoroutine(ShakeCamCoroutine());
    }

    private IEnumerator LineCoroutine()
    {
        float timer = 0f;

        lineRenderer.SetPosition(0, fireParticle.transform.position);
        lineRenderer.SetPosition(1, hitPos);
        lineRenderer.enabled = true;

        while(timer <= 0.1f)
        {
            lineRenderer.SetPosition(0, fireParticle.transform.position);
            timer += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
    }

    private IEnumerator ReboundCoroutine()
    {
        float reboundAdder = 0f;

        while(reboundAdder < reboundAmount)
        {
            Vector3 rotate = camParent.eulerAngles;
            reboundAdder += Time.deltaTime * reboundIncFactor;
            rotate.x -= Time.deltaTime * reboundIncFactor;
            camParent.eulerAngles = rotate;
            yield return null;
        }
    }

    private IEnumerator ShakeCamCoroutine()
    {
        perlin.m_FrequencyGain = frequency;
        perlin.m_AmplitudeGain = power;

        yield return new WaitForSeconds(shakeDelay);

        perlin.m_FrequencyGain = 0;
        perlin.m_AmplitudeGain = 0;
    }
}
