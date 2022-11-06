using Newtonsoft.Json;
using WebSocketSharp;
using UnityEngine;
using System;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    #region singleton
    private static Client instance = null;
    public static Client Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<Client>();

            return instance;
        }
    }
    #endregion

    [SerializeField] string ip = "localhost";
    [SerializeField] string port = "3030";
    private WebSocket server;
    private object locker = new object();

    private Queue<Action> handlerActions = new Queue<Action>();

    private Handler[] handlers = new Handler[(int)Types.Last];

    private void Awake()
    {
        server = new WebSocket($"ws://{ip}:{port}");

        server.OnMessage += GetMessages;

        server.ConnectAsync();
    }

    private void Update()
    {
        while(handlerActions.Count > 0)
            handlerActions.Dequeue()?.Invoke();
    }

    private void GetMessages(object sender, MessageEventArgs msg)
    {
        lock(locker)
        {
            Packet packet = JsonConvert.DeserializeObject<Packet>(msg.Data);

            handlerActions.Enqueue(() => {
                handlers[packet.type][packet.eventType]?.Invoke(packet);
            });
        }
    }

    public void SendMessages(int type, int eventType, string value)
    {
        try {
            Packet packet = new Packet(type, eventType, value);
            server.Send(JsonConvert.SerializeObject(packet));
        } catch (Exception err) {
            Debug.Log(err.Message);
        }
    }

    public void SendMessages(int type, int eventType, object value)
    {
        try {
            Packet packet = new Packet(type, eventType, JsonConvert.SerializeObject(value));
            server.Send(JsonConvert.SerializeObject(packet));
        } catch (Exception err) {
            Debug.Log(err.Message);
        }
    }

    private void HandlerInit()
    {
        handlers[(int)Types.RoomEvent] = gameObject.AddComponent<RoomHandler>();
        handlers[(int)Types.GameEvent] = gameObject.AddComponent<GameHandler>();

        foreach(Handler handler in handlers)
            handler.CreateHandler();
    }
}
