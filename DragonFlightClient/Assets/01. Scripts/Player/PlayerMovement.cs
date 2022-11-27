using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool active = false;
    public bool Active {
        get  => active;
        set {
            characterController.enabled = value;
            active = value;
            if(value == false)
                walkSoundPlayer.Pause();

            if(!value)
            {
                anim.SetFloat("Move", 0f);
                currentSpeed = 0f;
            }
        }
    }

    [Header("Speed")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] private float currentSpeed = 0f;

    [Header("Factor")]
    [SerializeField] float speedIncreaseFactor = 5f;
    [SerializeField] float rayDistance = 3f;
    
    [Header("Transform")]
    public Transform cameraFollow = null;

    [Header("SoundPlayer")]
    [SerializeField] AudioSource walkSoundPlayer = null;

    private CharacterController characterController = null;

    private float animBlend = 0f;
    private Animator anim = null;

    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();

    private float currentGravity = 0f;

    public bool rotationable = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Active)
        {
            Move();
            
            if(characterController.isGrounded)
                currentGravity = 0f;

            if(rotationable)
                Rotate();

            animBlend = Mathf.Lerp(0, currentSpeed, Mathf.Abs(Mathf.Abs(input.x) > Mathf.Abs(input.z) ? input.x : input.z));
            anim.SetFloat("Move", animBlend);
        }
    }

    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        if(input != Vector3.zero)
        {
            if(Input.GetKey(KeyCode.LeftShift) && currentSpeed < runSpeed)
                currentSpeed += Time.deltaTime * speedIncreaseFactor;
            else if(currentSpeed < walkSpeed)
                currentSpeed += Time.deltaTime * speedIncreaseFactor;
            else if(currentSpeed > walkSpeed)
                currentSpeed -= Time.deltaTime * speedIncreaseFactor;    
        }
        else if(currentSpeed > 0.1f)
            currentSpeed -= Time.deltaTime * speedIncreaseFactor;

        currentSpeed = Mathf.Max(0f, currentSpeed);

        dir = (input.x * transform.right + input.z * transform.forward);

        currentGravity += DEFINE.GravityScale * Time.deltaTime;
        dir.y += currentGravity;

        if((dir.x != 0 || dir.z != 0) && IsGround()) {
            if(!walkSoundPlayer.isPlaying)
                AudioManager.Instance.PlayAudio("PlayerWalk", walkSoundPlayer);
        } else
            walkSoundPlayer.Pause();

        characterController.Move(dir * currentSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 rotate = transform.eulerAngles;
        float xFactor = Input.GetAxis("Mouse Y") * DataManager.Instance.userSetting.mouseSensitivity;
        float yFactor = Input.GetAxis("Mouse X") * DataManager.Instance.userSetting.mouseSensitivity;

        rotate.y += yFactor;
        transform.rotation = Quaternion.Euler(rotate);

        Vector3 headRotate = cameraFollow.localEulerAngles;
        headRotate.x = headRotate.x >= 180f ? headRotate.x - 360f : headRotate.x;
        headRotate.x -= xFactor;

        headRotate.x = Mathf.Clamp(headRotate.x, -85f, 60f);
        cameraFollow.localRotation = Quaternion.Euler(headRotate);
    }

    private bool IsGround() => Physics.Raycast(walkSoundPlayer.transform.position, Vector3.down, rayDistance, DEFINE.GroundLayer);

    public void StartSendData() => StartCoroutine(SendData());
    public void StopSending() => StopAllCoroutines();

    private IEnumerator SendData()
    {
        yield return new WaitUntil(() => DEFINE.Ready2Start);

        Vector3 lastPos = new Vector3();
        Vector3 lastRotate = new Vector3();
        float lastBlend = 0f;
        while (true)
        {
            if(Active)
            {
                if (lastPos != transform.position || lastRotate != transform.localEulerAngles || lastBlend != animBlend)
                {
                    lastPos = transform.position;
                    lastRotate = transform.localEulerAngles;
                    lastBlend = animBlend;

                    MovePacket movePacket = new MovePacket(lastPos, lastRotate, lastBlend);
                    Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.PlayerMove, movePacket);
                }
            }

            yield return new WaitForSeconds(1f/20f);
        }
    }
}
