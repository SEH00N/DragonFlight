using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyRoomCode : MonoBehaviour
{
    private TextMeshProUGUI tmp = null;

    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void DoCopy()
    {
        TextPrefab txt =  PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
        txt.Init("복사되었습니다.", DEFINE.StaticCanvas);
        txt.DoNoticeTrail();

        GUIUtility.systemCopyBuffer = tmp.text;
    }
}
