using System.Collections;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float flyingSpeed = 15f;
    [SerializeField] float rotationFactor = 0.5f;
    [SerializeField] float rayDistance = 0.5f;
    
    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();
    private float animBelnd = 0f;
    private float currentSpeed;

    [SerializeField] private bool onFlying = false;
    [SerializeField] private bool onGround = false;

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
        Move();
        Fly();
        Rotate();

        animBelnd = Mathf.Lerp(0, currentSpeed, Mathf.Abs(input.z));
        anim.SetFloat("Move", animBelnd);
        anim.SetBool("OnFly", onFlying);
    }

    private void Fly()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            onFlying = true;
            characterController.Move(Vector3.up * flyingSpeed * Time.deltaTime);

            currentSpeed = flyingSpeed;
        }

        if(onFlying && onGround)
        {
            onFlying = false;
            currentSpeed = walkSpeed;
        }

        onGround = CheckGround();
    }

    private void Rotate()
    {
        Vector3 rotate = transform.eulerAngles;

        if(onFlying)
        {
            rotate.x -= Input.GetAxis("Mouse Y") * rotationFactor;
            rotate.y += Input.GetAxis("Mouse X") * rotationFactor;
        }
        else
        {
            rotate.y += input.x * rotationFactor;
            rotate.x = 0;
        }
        
        transform.rotation = Quaternion.Euler(rotate);
    }

    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift) && currentSpeed < runSpeed)
            currentSpeed += Time.deltaTime * 3f;
        else if(!Input.GetKey(KeyCode.LeftShift) && currentSpeed > walkSpeed)
            currentSpeed -= Time.deltaTime * 3f;

        dir = (input.z * transform.forward);
        if(!onFlying)
            dir.y += DEFINE.GravityScale * Time.deltaTime;

        characterController.Move(dir * currentSpeed * Time.deltaTime);
    }

    private bool CheckGround()
    {
        if (onGround) return true;

        return Physics.Raycast(characterController.bounds.center, Vector3.down, rayDistance, DEFINE.GroundLayer);
    }
}
