using UnityEngine;

public class FireBallBombPrefab : ParticlePrefab
{
    [SerializeField] AudioSource bombSoundPlayer = null;

    public override void Init(Vector3 position, Quaternion rotation)
    {
        base.Init(position, rotation);

        AudioManager.Instance.PlayAudio("FireBallBomb", bombSoundPlayer);
    }

    public override void Init(Vector3 position)
    {
        base.Init(position);
        
        AudioManager.Instance.PlayAudio("FireBallBomb", bombSoundPlayer);
    }

    public override void Init(Vector3 position, Vector3 rotation)
    {
        base.Init(position, rotation);
        
        AudioManager.Instance.PlayAudio("FireBallBomb", bombSoundPlayer);
    }
}
