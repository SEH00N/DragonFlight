using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    private Slider slider = null;
    private TMP_InputField inputField = null;
    private string lastText;

    private void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
        inputField = transform.GetChild(1).GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        DEFINE.MouseSensitivity = DataManager.Instance.userSetting.mouseSensitivity;
        slider.value = DEFINE.MouseSensitivity;
        inputField.text = DEFINE.MouseSensitivity.ToString("F");
        lastText = inputField.text;
    }

    private void OnDisable()
    {
        DataManager.Instance.userSetting.mouseSensitivity = DEFINE.MouseSensitivity;
    }

    public void SetSensitivity()
    {
        DEFINE.MouseSensitivity = slider.value;
        inputField.text = $"{slider.value.ToString("F")}";
    }

    public void EndOfTyping()
    {
        if(float.TryParse(inputField.text, out float sens))
        {
            sens = Mathf.Clamp(sens, slider.minValue, slider.maxValue);
            lastText = sens.ToString("F");
            DEFINE.MouseSensitivity = sens;
            slider.value = sens;
        }

        inputField.text = lastText;
    }
}
