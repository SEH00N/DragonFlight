using System.Globalization;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

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
}
