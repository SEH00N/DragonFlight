using System.Globalization;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] private float currentSpeed = 0f;

    [Header("Factor")]
    [SerializeField] float speedIncreaseFactor = 5f;
    [SerializeField] float rotationFactor = 4f;
    
    private CharacterController characterController = null;

    private Animator anim = null;
    [SerializeField, Space(10f)] private float animBlend = 0f;

    private Vector3 input = new Vector3();
    private Vector3 dir = new Vector3();
    

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Rotate();

        animBlend = Mathf.Lerp(0, currentSpeed, Mathf.Abs(Mathf.Abs(input.x) > Mathf.Abs(input.z) ? input.x : input.z));
        anim.SetFloat("Move", animBlend);
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

        dir.y += DEFINE.GravityScale * Time.deltaTime;

        characterController.Move(dir * currentSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 rotate = transform.eulerAngles;

        rotate.x -= Input.GetAxis("Mouse Y") * rotationFactor;
        rotate.y += Input.GetAxis("Mouse X") * rotationFactor;

        transform.rotation = Quaternion.Euler(rotate);
    }
}
