using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private GameObject panel;
    private TextMeshProUGUI finishText = null;
    [SerializeField] string winText, loseText;
    [SerializeField] AudioSource resultSoundPlayer;

    private void Awake()
    {
        panel = transform.GetChild(1).gameObject;
        finishText = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        DEFINE.GameOver = false;
    }

    public void GameOver(bool win)
    {
        panel.SetActive(true);
        AudioManager.Instance.PlayAudio(win ? "Win" : "Lose", resultSoundPlayer);
        finishText.text = win ? winText : loseText;
    }
}
