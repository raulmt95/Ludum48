using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public float Lifetime;
    public float TimeToVanish;
    public float MaxRotationSpeed;
    public ParticleSystem PopParticles;
    public SpriteRenderer SoulRenderer;

    private Rigidbody2D _rb;
    private float _rotationSpeed = 0;
    private Tween _vanishTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke(nameof(Vanish), Lifetime);
        SoulRenderer.color = new Color(1, 1, 1, DarknessController.Instance.CurrentDarknessLevel);
    }

    private void Update()
    {
        _rotationSpeed = MaxRotationSpeed * _rb.velocity.x / 5;
        transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
    }

    private void Vanish()
    {
        _vanishTween = transform.DOScale(0, TimeToVanish)
                                .SetEase(Ease.InQuad)
                                .OnComplete(() => Destroy(gameObject))
                                .Play();
    }

    private void Pop()
    {
        AudioManager.Instance.PlayBallExplosionSound();

        Instantiate(PopParticles, transform.position, Quaternion.identity);
        CancelInvoke(nameof(Vanish));
        _vanishTween.Kill();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike") || collision.CompareTag("Failsafe"))
        {
            Pop();
        }
    }

    public void ApplyForce(Vector3 direction, float strength)
    {
        _rb.AddForce(direction * strength, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BallExitTrigger"))
        {
            gameObject.layer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Spike") && !collision.collider.CompareTag("Ball"))
        {
            AudioManager.Instance.PlayBounceSound();
        }
    }
}
