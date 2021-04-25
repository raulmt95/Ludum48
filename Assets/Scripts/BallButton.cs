using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallButton : MonoBehaviour
{
    public float RotationSpeed;
    public float ActivatedRotationSpeed;
    public ParticleSystem ActivationParticles;

    public UnityEvent OnActivation;

    private SpriteRenderer _renderer;
    private bool _isActivated = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActivated && collision.CompareTag("Ball"))
        {
            Activate();
        }
    }

    private void Activate()
    {
        AudioManager.Instance.PlayStarButtonSound();

        OnActivation.Invoke();

        ActivationParticles.Play();
        _isActivated = true;
        _renderer.color = Color.yellow;
        RotationSpeed = ActivatedRotationSpeed;
    }
}
