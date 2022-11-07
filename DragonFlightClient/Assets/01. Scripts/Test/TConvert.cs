using Newtonsoft.Json;
using UnityEngine;

public class TConvert : MonoBehaviour
{
    [SerializeField] string value;

    public Packet p;
    public MovePacket pos;

    private void Awake()
    {
        value = JsonUtility.ToJson(new Packet(0, 2, JsonConvert.SerializeObject(pos)));
    }
}
