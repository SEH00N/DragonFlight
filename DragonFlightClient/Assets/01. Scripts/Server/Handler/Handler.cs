using System;
using UnityEngine;

public abstract class Handler : MonoBehaviour
{
    public abstract int HandlersSize { get; }

    public Action<Packet>[] handlers;

    public Action<Packet> this[int index] {
        get => handlers[index];
        set => handlers[index] = value;
    } 

    public virtual void CreateHandler()
    {
        handlers = new Action<Packet>[HandlersSize];
    }
}
