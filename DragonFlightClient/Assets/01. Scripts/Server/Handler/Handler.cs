using System;
using UnityEngine;

public abstract class Handler : MonoBehaviour
{
    public abstract int HandlersSize { get; }

    public Action<Packet>[] handlers;

    public Action<Packet> this[int index] => handlers[index];

    protected virtual void Awake()
    {
        handlers = new Action<Packet>[HandlersSize];
    }
}
