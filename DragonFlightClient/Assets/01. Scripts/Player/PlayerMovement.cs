using System.Globalization;
using System.Runtime.InteropServices;
using System;
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
    [SerializeField] float rotationFactor = 4f;
    
    [Header("Transform")]
    public Transform cameraFollow = null;

    private CharacterController characterController = null;

    private float animBlend = 0f;
    private Animator anim = null;

    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(active)
        {
            Move();
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

        dir.y += DEFINE.GravityScale;

        characterController.Move(dir * currentSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 rotate = transform.eulerAngles;
        float xFactor = Input.GetAxis("Mouse Y") * rotationFactor;
        float yFactor = Input.GetAxis("Mouse X") * rotationFactor;

        rotate.y += yFactor;
        transform.rotation = Quaternion.Euler(rotate);

        Vector3 headRotate = cameraFollow.localEulerAngles;
        headRotate.x -= xFactor;
        if(headRotate.x >= 90f)
            headRotate.x -= 360f;

        headRotate.x = Mathf.Clamp(headRotate.x, -85f, 85f);
        cameraFollow.localRotation = Quaternion.Euler(headRotate);
    }

    public void StartSendData() => StartCoroutine(SendData());

    private IEnumerator SendData()
    {
        Vector3 lastPos = new Vector3();
        Vector3 lastRotate = new Vector3();
        float lastBlend = 0f;
        while(true)
        {
            if(lastPos != transform.position || lastRotate != transform.eulerAngles || lastBlend != animBlend)
            {
                lastPos = transform.position;
                lastRotate = transform.eulerAngles;
                lastBlend = animBlend;

                MovePacket movePacket = new MovePacket(lastPos, lastRotate, lastBlend);
                Client.Instance.SendMessages((int)Types.InteractEvent, (int)InteractEvents.PlayerMove, movePacket);
            }

            yield return new WaitForSeconds(1f/20f);
        }
    }
}
