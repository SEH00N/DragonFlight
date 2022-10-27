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

    private void Update()
    {
        if(!isDisappearable)
            return;

        timer += Time.deltaTime;
        if(timer > lifeTime)
            PoolManager.Instance.Push(this);
    }
}
