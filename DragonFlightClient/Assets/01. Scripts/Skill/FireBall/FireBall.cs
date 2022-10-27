using UnityEngine;

public class FireBall : PoolableMono
{
    [Header("Factor")]
    [SerializeField] float speed = 3f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damage = 10f;

    private float currentTimer = 0f;
    private Rigidbody rb = null;
    
    public override void Reset()
    {
        currentTimer = 0f;

        if(rb == null)
            rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        currentTimer += Time.deltaTime;

        if(currentTimer > lifeTime)
            PoolManager.Instance.Push(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        PoolManager.Instance.Pop("FireBallHitEffect");
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable id))
            id.OnDamage(damage);
    }

    //터지는 거
    private void Bomb()
    {

    }

    public void Init(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Init(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.localRotation = rotation;
    }
}
