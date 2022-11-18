using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private GameObject panel;
    private TextMeshProUGUI finishText = null;
    [SerializeField] string winText, loseText;

    private void Awake()
    {
        panel = transform.GetChild(0).gameObject;
        finishText = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        DEFINE.GameOver = false;
    }

    public void GameOver(bool win)
    {
        panel.SetActive(true);
        finishText.text = win ? winText : loseText;
    }
}
