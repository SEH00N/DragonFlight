using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<AudioManager>();

            return instance;
        }
    }

    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipPool = new Dictionary<string, AudioClip>();

    private AudioSource bgmPlayer = null;

    private void Awake()
    {
        bgmPlayer = transform.GetComponentInChildren<AudioSource>();

        foreach(AudioClip clip in clips)
            CreateAudioPool(clip);
    }

    public void PlayBGM(string clipName)
    {
        
    }

    public void PauseBGM()
    {
        
    }

    private void CreateAudioPool(AudioClip clip)
    {
        if(clipPool.ContainsKey(clip.name))
        {
            Debug.LogWarning("Current name of audio clip is already exsit");
            return;
        }

        clipPool.Add(clip.name, clip);
    }
}
