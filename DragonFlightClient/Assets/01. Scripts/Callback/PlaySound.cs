using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void DoPlaySystem(string clipName) => AudioManager.Instance.PlaySystem(clipName);
    public void DoPlayBGM(string clipName) => AudioManager.Instance.PlayBGM(clipName);
}
