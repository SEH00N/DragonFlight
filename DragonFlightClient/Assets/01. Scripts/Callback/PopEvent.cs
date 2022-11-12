using DG.Tweening;
using UnityEngine;

public class PopEvent : MonoBehaviour
{
    [SerializeField] RectTransform rect = null;
    [SerializeField] float duration = 0.5f;
    private bool onTweening = false;

    public void DoPopUp()
    {
        if(onTweening) return;

        onTweening = true;

        rect.DOScale(Vector3.one, duration).SetEase(Ease.Linear).OnComplete(() => onTweening = false );
    }

    public void DoPopDown()
    {
        if(onTweening) return;

        onTweening = true;

        rect.DOScale(Vector3.zero, duration).SetEase(Ease.Linear).OnComplete(() => onTweening = false );
    }
}
