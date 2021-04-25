using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DarknessController : Singleton<DarknessController>
{
    public Image DarknessPanel;
    public SpriteRenderer SquareSoulSprite;
    public float FadeTime;
    public float CurrentDarknessLevel = 0;

    private Color _nextColor;

    public void SetDarknessLevel(float level)
    {
        CurrentDarknessLevel = level;

        _nextColor = new Color(0, 0, 0, level);

        DarknessPanel.DOColor(_nextColor, FadeTime)
                     .SetEase(Ease.InOutQuad)
                     .Play();

        _nextColor = new Color(1, 1, 1, level);

        SquareSoulSprite.DOColor(_nextColor, FadeTime)
                        .SetEase(Ease.InOutQuad)
                        .Play();
    }
}
