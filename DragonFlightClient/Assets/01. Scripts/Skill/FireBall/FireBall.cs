using System.Collections.Generic;
using UnityEngine;

public class FireBall : PoolableMono
{
    [SerializeField] float speed = 3f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damage = 10f;
    [SerializeField] Collider bombArea = null;

    private float currentTimer = 0f;
    private Rigidbody rb = null;
    
    public override void Reset()
    {
        currentTimer = 0f;

        if(rb == null)
            rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
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
        Bomb();

        PoolManager.Instance.Push(this);
        ParticlePrefab particle = PoolManager.Instance.Pop("FireBallHitEffect") as ParticlePrefab;
        particle.Init(transform.position, Quaternion.identity);
    }

    //터지는 거
    private void Bomb()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position, bombArea.bounds.size / 2f, Quaternion.identity, DEFINE.EnemyLayer);

        List<IDamageable> ids = new List<IDamageable>();

        foreach (Collider enemy in enemies)
            if (enemy.transform.root.TryGetComponent<IDamageable>(out IDamageable id))
                if (!ids.Contains(id))
                    ids.Add(id);

        Debug.Log(enemies.Length + " " + ids.Count);

        if (ids.Count <= 0)
            return;

        foreach(IDamageable id in ids)
            id.OnDamage(damage);
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
