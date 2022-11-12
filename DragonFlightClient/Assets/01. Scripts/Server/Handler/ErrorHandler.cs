using System;
using UnityEngine;

public class ErrorHandler : Handler
{
    public override int HandlersSize => (int)ErrorEvents.Last;

    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)ErrorEvents.ErrorMessage] = ErroeMessageEvent;
    }

    private void ErroeMessageEvent(Packet packet)
    {
        TextPrefab txt = PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
        txt.Init(packet.value, DEFINE.StaticCanvas);
        txt.DoNoticeTrail();
    }
}
