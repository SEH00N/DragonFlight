using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : ParticlePrefab
{
    [SerializeField] AudioSource hitSoundPlayer = null;

    private void OnEnable()
    {
        AudioManager.Instance.PlayAudio("Hit", hitSoundPlayer);
    }
}
