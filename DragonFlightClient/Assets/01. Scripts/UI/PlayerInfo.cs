using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private TextMeshProUGUI readyText = null;
    private Image playerImage = null;

    private bool isReady = false;

    private void Awake()
    {
        playerImage = transform.GetChild(0).GetComponent<Image>();
        readyText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Init(string textValue)
    {
        readyText.text = textValue;
        playerImage.color = Color.gray;
    }

    public void SetReady()
    {
        isReady = !isReady;

        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.Ready, isReady ? "true" : "false");
        readyText.text = isReady ? "READY" : "READY?";
    }
}
