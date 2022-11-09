using System.Collections;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] bool active = false;
    public bool Active {
        get  => active;
        set {
            characterController.enabled = value;
            active = value;

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
    [SerializeField] float flyingSpeed = 15f;

    [Header("Factor")]
    [SerializeField] float rotationFactor = 0.5f;
    [SerializeField] float speedIncreaseFactor = 3f;
    [SerializeField] float jumpFactor = 2f;
    [SerializeField] float rayDistance = 0.5f;
    [SerializeField] float dashFactor = 5f;
    
    [Header("Tranfrom")] 
    public Transform playerRidePosition = null;
    public Transform cameraFollow = null;

    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();
    private float animBlend = 0f;
    private float currentSpeed;

    private bool onFlying = false;
    public bool OnFlying => onFlying;
    [SerializeField] private bool onDash = false;
    public bool OnDash { get => onDash; set => onDash = value; }

    private bool onGround = false;

    private Animator anim = null;
    private CharacterController characterController = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if(active)
        {
            Move();
            Fly();
            Rotate();

            animBlend = Mathf.Lerp(0, currentSpeed, Mathf.Abs(input.z));
            anim.SetFloat("Move", animBlend);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Fly()
    {
        // Do Fly
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            onFlying = true;
            SetFlyAnimation(onFlying);

            characterController.Move(Vector3.up * flyingSpeed * jumpFactor * Time.deltaTime);

            Vector3 rotate = transform.eulerAngles;
            rotate.x = cameraFollow.eulerAngles.x;
            transform.rotation = Quaternion.Euler(rotate);

            rotate = cameraFollow.localEulerAngles;
            rotate.x = 0f;
            cameraFollow.localRotation = Quaternion.Euler(rotate);
            
            currentSpeed = flyingSpeed;
        }

        // Do Grounding
        if(onFlying && onGround)
        {
            onFlying = false;
            SetFlyAnimation(onFlying);

            currentSpeed = walkSpeed;

            Vector3 rotate = transform.eulerAngles;
            rotate.x = 0f;
            transform.rotation = Quaternion.Euler(rotate);
        }

        onGround = CheckGround();
    }

    private void SetFlyAnimation(bool value)
    {
        anim.SetBool("OnFly", onFlying);
        BoolAnimPacket boolAnimPacket = new BoolAnimPacket("Dragon", "OnFly", onFlying);
        Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.BoolAnim, boolAnimPacket);
    }

    private void Rotate()
    {
        Vector3 rotate = transform.eulerAngles;
        float xFactor = Input.GetAxis("Mouse Y") * rotationFactor;
        float yFactor = Input.GetAxis("Mouse X") * rotationFactor;

        rotate.y += yFactor;

        if (onFlying)
        {
            rotate.x -= xFactor;
            if(rotate.x >= 90f)
                rotate.x -= 360f;

            rotate.x = Mathf.Clamp(rotate.x, -85f, 85f);
        }
        else
        {
            Vector3 headRotate = cameraFollow.localEulerAngles;
            headRotate.x -= xFactor;
            if (headRotate.x >= 90f)
                headRotate.x -= 360f;

            headRotate.x = Mathf.Clamp(headRotate.x, -85f, 20f);
            cameraFollow.localRotation = Quaternion.Euler(headRotate);
        }

        transform.rotation = Quaternion.Euler(rotate);
    }

    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxis("Vertical");

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

        dir = (input.z * transform.forward + input.x * transform.right);
        if(!onFlying)
            dir.y += DEFINE.GravityScale * Time.deltaTime;

        characterController.Move(dir * (onDash ? currentSpeed * dashFactor : currentSpeed) * Time.deltaTime);
    }

    private bool CheckGround()
    {
        if (onGround) return true;

        return Physics.Raycast(characterController.bounds.center, Vector3.down, rayDistance, DEFINE.GroundLayer);
    }

    public void StartSendData() => StartCoroutine(SendData());

    private IEnumerator SendData()
    {
        yield return new WaitUntil(() => DEFINE.Ready2Start);

        Vector3 lastPos = new Vector3();
        Vector3 lastRotate = new Vector3();
        float lastBlend = 0f;
        while(true)
        {
            if (lastPos != transform.position || lastRotate != transform.eulerAngles || lastBlend != animBlend)
            {
                lastPos = transform.position;
                lastRotate = transform.eulerAngles;
                lastBlend = animBlend;

                MovePacket movePacket = new MovePacket(lastPos, lastRotate, lastBlend);
                Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.DragonMove, movePacket);
            }

            yield return new WaitForSeconds(1f/20f);
        }
    }
}
