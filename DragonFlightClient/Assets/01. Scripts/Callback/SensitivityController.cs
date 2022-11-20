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
        slider.value = DataManager.Instance.userSetting.mouseSensitivity;
        inputField.text = DataManager.Instance.userSetting.mouseSensitivity.ToString("F");
        lastText = inputField.text;
    }

    public void SetSensitivity()
    {
        DataManager.Instance.userSetting.mouseSensitivity = slider.value;
        inputField.text = $"{slider.value.ToString("F")}";
    }

    public void EndOfTyping()
    {
        if(float.TryParse(inputField.text, out float sens))
        {
            sens = Mathf.Clamp(sens, slider.minValue, slider.maxValue);
            lastText = sens.ToString("F");
            DataManager.Instance.userSetting.mouseSensitivity = sens;
            slider.value = sens;
        }

        inputField.text = lastText;
    }
}
