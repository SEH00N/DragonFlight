using UnityEngine;

public class FireBall : PoolableMono
{
    public override void Reset()
    {

    }

    public void Init(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
