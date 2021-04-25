using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public float OpenTime;

    private Tween _activeTween;

    public void Open()
    {
        AudioManager.Instance.PlayDoorOpenSound();

        _activeTween.Kill();

        _activeTween = transform.DOScaleX(0, OpenTime)
                                .SetEase(Ease.InQuad)
                                .OnComplete(() => gameObject.SetActive(false))
                                .Play();
    }

    public void Close()
    {
        AudioManager.Instance.PlayDoorCloseSound();

        _activeTween.Kill();

        _activeTween = transform.DOScaleX(1, OpenTime)
                                .SetEase(Ease.OutQuad)
                                .Play();
    }
}
