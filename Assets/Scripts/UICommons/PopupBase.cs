using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupBase : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;
    [SerializeField]
    CanvasGroup canvas;
    [SerializeField]
    float duration = 0.2f;

    Sequence sequence;
    // Use this for initialization
    void Start()
    {
        rect.localScale = new Vector2(0.5f,0.5f);
        canvas.alpha = 0;
        sequence = DOTween.Sequence();
        sequence.Append(rect.DOScale(new Vector2(1.1f, 1.1f), duration))
            .Join(canvas.DOFade(1, duration))
            .Append(rect.DOScale(Vector2.one, duration))
            .SetAutoKill(false).SetUpdate(true);
        sequence.Play();


    }

    public void Close(Action callback)
    {

        sequence.PlayBackwards();
        Main.SetTimeout(() =>
        {
            sequence.Kill();
            callback();
            Destroy(gameObject);
        }, duration * 2);
    }
}
