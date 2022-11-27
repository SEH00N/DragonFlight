using UnityEngine;

public class ParticlePrefab : PoolableMono
{
    [SerializeField] bool isDisappearable = true;
    [SerializeField] float lifeTime = 10f;
    private float timer = 0f;

    public override void Reset()
    {
        timer = 0f;
    }

    public virtual void Init(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public virtual void Init(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void Init(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void Update()
    {
        if(!isDisappearable)
            return;

        timer += Time.deltaTime;
        if(timer > lifeTime)
            PoolManager.Instance.Push(this);
    }
}
