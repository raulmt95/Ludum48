using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallLauncher : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform Player;
    public float MaxCharge;
    public float MinCharge;
    public float ChargeFactor;
    public Transform TargetLineCenter;
    public Transform TargetScalePivot;

    private bool _charging = false;
    private float _currentCharge;
    private Ball _currentBall;
    private Vector3 _mousePosition;
    private Vector3 _mouseWorld;
    private Vector3 _launchDirection;
    private Tween _targetLineTween;
    private bool _endingTriggered = false;

    private void Awake()
    {
        HideTargetLine();
    }

    private void Update()
    {
        if (!_endingTriggered)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                BeginCharge();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                LaunchBall();
            }

            if (_charging)
            {
                Charge();
            }
        }
    }

    private void BeginCharge()
    {
        AudioManager.Instance.StartChargeSound();

        CancelInvoke(nameof(HideTargetLine));
        _charging = true;
        _currentCharge = MinCharge;

        TargetScalePivot.localScale = Vector2.zero;

        _targetLineTween = TargetScalePivot.DOScale(1, MaxCharge / ChargeFactor)
                                           .SetEase(Ease.Linear)
                                           .Play();
    }

    private void Charge()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(TargetLineCenter.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        TargetLineCenter.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (_currentCharge < MaxCharge)
        {
            _currentCharge += ChargeFactor * Time.deltaTime;
        }
        else
        {
            _currentCharge = MaxCharge;
        }
    }

    private void LaunchBall()
    {
        AudioManager.Instance.StopChargeSound();
        AudioManager.Instance.PlayShootSound(_currentCharge / MaxCharge + MinCharge / _currentCharge);

        _targetLineTween.Kill();
        Invoke(nameof(HideTargetLine), 1);

        _charging = false;
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorld = new Vector3(_mousePosition.x, _mousePosition.y, Player.position.z);
        _launchDirection = (_mouseWorld - Player.position).normalized;

        _currentBall = Instantiate(BallPrefab, Player.position, Quaternion.identity).GetComponent<Ball>();
        _currentBall.ApplyForce(_launchDirection, _currentCharge);
    }

    private void HideTargetLine()
    {
        TargetScalePivot.localScale = Vector2.zero;
    }

    public void SetEndingTriggered()
    {
        _endingTriggered = true;
    }
}
