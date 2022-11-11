using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextPrefab : PoolableMono
{
    [SerializeField] Vector3 noticePosition = new Vector3();
    [SerializeField] float trailDuration = 0.5f;
    [SerializeField] float trailDistance = 10f;
    private TextMeshProUGUI tmp;
    private Sequence seq;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public override void Reset()
    {
        seq.Kill();
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

    public void DoNoticeTrail()
    {
        seq = DOTween.Sequence();
        transform.localPosition = noticePosition;
        seq.Append(tmp.DOFade(0, trailDuration));
        seq.Join(transform.DOLocalMoveY(noticePosition.y + trailDistance, trailDuration));
        seq.AppendCallback(() => {
            PoolManager.Instance.Push(this);
        });
    }

    // public static implicit operator TextMeshProUGUI(TextPrefab txt) => txt.tmp;
}
