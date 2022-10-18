using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float animBelnd = 0f;
    private float speed;

    private Rigidbody rb = null;
    private Animator anim = null;

    private bool onGround = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        speed = walkSpeed;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift) && speed < runSpeed)
            speed += Time.deltaTime * 3f;
        else if(!Input.GetKey(KeyCode.LeftShift) && speed > walkSpeed)
            speed -= Time.deltaTime * 3f;

        // transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * speed);
        Vector3 moveDir = (x * transform.right + z * transform.forward) * speed;
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;

        animBelnd = Mathf.Lerp(0, speed, Mathf.Abs(z));
        anim.SetFloat("Move", animBelnd);
    }
}
