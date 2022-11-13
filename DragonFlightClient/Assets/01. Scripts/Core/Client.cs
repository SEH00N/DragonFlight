using Newtonsoft.Json;
using WebSocketSharp;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

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
    [SerializeField] float autoConnectDelay = 3f;
    private WebSocket socket;
    private object locker = new object();

    private Queue<Action> handlerActions = new Queue<Action>();

    private Handler[] handlers = new Handler[(int)Types.Last];

    private void Awake()
    {
        if(instance != null)
            return;

        socket = new WebSocket($"ws://{ip}:{port}");

        socket.OnMessage += GetMessages;
        HandlerInit();
    }

    private void Start()
    {
        StartCoroutine(AutoConnector());
    }

    private void Update()
    {
        while(handlerActions.Count > 0)
            handlerActions.Dequeue()?.Invoke();
    }

    private void OnDisable()
    {
        StopAllCoroutines();    
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
            socket.Send(JsonConvert.SerializeObject(packet));
        } catch (Exception err) {
            Debug.LogWarning(err.Message);
        }
    }

    public void SendMessages(int type, int eventType, object value)
    {
        try {
            Packet packet = new Packet(type, eventType, JsonConvert.SerializeObject(value));
            socket.Send(JsonConvert.SerializeObject(packet));
        } catch (Exception err) {
            Debug.LogWarning(err.Message);
        }
    }

    private void HandlerInit()
    {
        handlers[(int)Types.RoomEvent] = gameObject.AddComponent<RoomHandler>();
        handlers[(int)Types.GameManagerEvent] = gameObject.AddComponent<GameManagerHandler>();
        handlers[(int)Types.InteractEvent] = gameObject.AddComponent<InteractHandler>();
        handlers[(int)Types.ErrorEvent] = gameObject.AddComponent<ErrorHandler>();

        foreach(Handler handler in handlers)
            if(handler != null)
                handler.CreateHandler();
    }

    private IEnumerator AutoConnector()
    {
        socket.ConnectAsync();
        yield return new WaitForSeconds(autoConnectDelay);
        bool lastConnect = socket.IsAlive;
        SceneLoader.Instance.Reconnect(!lastConnect);

        while(true)
        {
            if(socket.IsAlive) {
                if(!lastConnect)
                {
                    lastConnect = true;
                    SceneLoader.Instance.Reconnect(false);

                    Debug.Log("On Return of Connect");
                }
                yield return new WaitUntil(() => !socket.IsAlive);
            } else {
                if(lastConnect)
                {
                    lastConnect = false;
                    SceneLoader.Instance.Reconnect(true);

                    Debug.Log("On Reconnect");
                }
                socket.ConnectAsync();
                yield return new WaitForSeconds(autoConnectDelay);
            }
        }
    }
}
