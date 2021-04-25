using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public float MoveTime;
    public List<Transform> PositionList;

    private int _currentPosition;

    private void Start()
    {
        _currentPosition = 0;
        MoveToPosition(PositionList[0]);
    }

    private void MoveToPosition(Transform point)
    {
        transform.DOMove(point.position, MoveTime)
                 .SetEase(Ease.InOutQuad)
                 .OnComplete(() => NextPosition())
                 .Play();
 
    }

    private void NextPosition()
    {
        if (_currentPosition < PositionList.Count - 1)
        {
            _currentPosition++;
        }
        else
        {
            _currentPosition = 0;
        }

        MoveToPosition(PositionList[_currentPosition]);
    }
}
