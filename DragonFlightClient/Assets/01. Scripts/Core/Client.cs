using Newtonsoft.Json;
using WebSocketSharp;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] string ip = "localhost";
    [SerializeField] string port = "3030";
    private WebSocket server;

    private void Awake()
    {
        server = new WebSocket($"ws://{ip}:{port}");

        server.ConnectAsync();
    }


}
