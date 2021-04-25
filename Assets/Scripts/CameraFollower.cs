using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : Singleton<CameraFollower>
{
    public PlayerController Player;
    public float Offset;
    public float FollowSpeed;
    public float ShakeDuration;
    public float ShakeStrength;

    private Vector3 _targetPosition;
    private bool _canFollow = false;
    private bool _respawning = false;
    private float _deltaDistance;

    private void Awake()
    {
        _canFollow = false;
        _respawning = false;
    }

    private void Update()
    {
        if (_canFollow)
        {
            _targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + Offset, transform.position.z);

            if (_respawning)
            {
                _deltaDistance = Mathf.Abs(transform.position.x - _targetPosition.x) + Mathf.Abs(transform.position.y - _targetPosition.y);
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, FollowSpeed * Time.deltaTime);

                if (_deltaDistance < 0.1f)
                {
                    _respawning = false;
                }
            }
            else
            {
                transform.position = _targetPosition;
            }
        }
    }

    public void StartFollow()
    {
        _respawning = true;
        _canFollow = true;
    }

    public void StopFollow()
    {
        _canFollow = false;
    }

    public void DoShake()
    {
        Camera.main.DOShakePosition(ShakeDuration, ShakeStrength).Play();
    }
}
