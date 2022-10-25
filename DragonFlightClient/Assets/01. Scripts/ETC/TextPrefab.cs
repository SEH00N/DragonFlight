using TMPro;
using UnityEngine;

public class TextPrefab : PoolableMono
{
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public override void Reset()
    {
        if(tmp == null)
            tmp = GetComponent<TextMeshProUGUI>();
        tmp.alpha = 1f;
    }

    public void Init(string text, Transform parent = null)
    {
        tmp.text = text;
        
        if(parent != null)
            transform.SetParent(parent);

        transform.localScale = Vector3.one;
    }
}
