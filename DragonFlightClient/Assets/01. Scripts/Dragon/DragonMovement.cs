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
    
    [Header("Tranfrom")] 
    public Transform playerRidePosition = null;
    public Transform cameraFollow = null;

    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();
    private float animBlend = 0f;
    private float currentSpeed;

    private bool onFlying = false;
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
        transform.rotation = Quaternion.identity;
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
            anim.SetBool("OnFly", onFlying);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Fly()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            onFlying = true;
            characterController.Move(Vector3.up * flyingSpeed * jumpFactor * Time.deltaTime);

            Vector3 rotate = transform.eulerAngles;
            rotate.x = cameraFollow.eulerAngles.x;
            transform.rotation = Quaternion.Euler(rotate);

            rotate = cameraFollow.localEulerAngles;
            rotate.x = 0f;
            cameraFollow.localRotation = Quaternion.Euler(rotate);

            currentSpeed = flyingSpeed;
        }

        if(onFlying && onGround)
        {
            onFlying = false;
            currentSpeed = walkSpeed;

            Vector3 rotate = transform.eulerAngles;
            rotate.x = 0f;
            transform.rotation = Quaternion.Euler(rotate);
        }

        onGround = CheckGround();
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

        dir = (input.z * currentSpeed * transform.forward);
        if(!onFlying)
            dir.y += DEFINE.GravityScale * Time.deltaTime;

        characterController.Move(dir * currentSpeed * Time.deltaTime);
    }

    private bool CheckGround()
    {
        if (onGround) return true;

        return Physics.Raycast(characterController.bounds.center, Vector3.down, rayDistance, DEFINE.GroundLayer);
    }

    public void StartSendData()
    {
        StartCoroutine(SendData());
    }

    private IEnumerator SendData()
    {
        Vector3 lastPos = new Vector3();
        Quaternion lastRotate = new Quaternion();
        while(true)
        {
            if(lastPos != transform.position || lastRotate != transform.rotation)
            {
                lastPos = transform.position;
                lastRotate = transform.rotation;

                MovePacket movePacket = new MovePacket(lastPos, lastRotate, animBlend);
                Client.Instance.SendMessages((int)Types.GameEvent, (int)GameEvents.DragonMove, movePacket);
            }

            yield return new WaitForSeconds(1f/20f);
        }
    }
}
