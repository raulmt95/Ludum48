using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    public float MaxVerticalSpeed;
    public ParticleSystem PopParticles;

    [Header("Soul")]
    public SpriteRenderer SoulRenderer;
    public Animator SoulAnimator;
    public float EndingFadeTime;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool _endingTriggered = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        _endingTriggered = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_endingTriggered && IntroController.Instance.GameStarted)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (!_endingTriggered)
        {
            CheckMovement();
        }
    }


    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(MaxSpeed, _rb.velocity.y);

            _renderer.flipX = false;
            _animator.SetBool("IsWalking", true);

            SoulRenderer.flipX = false;
            SoulAnimator.SetBool("IsWalking", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-MaxSpeed, _rb.velocity.y);

            _renderer.flipX = true;
            _animator.SetBool("IsWalking", true);

            SoulRenderer.flipX = true;
            SoulAnimator.SetBool("IsWalking", true);
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);

            _animator.SetBool("IsWalking", false);
            SoulAnimator.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Die();
        }
        else if (collision.CompareTag("Ending"))
        {
            TriggerEnding();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            AudioManager.Instance.PlayStepSound();
        }
    }

    private void Die()
    {
        AudioManager.Instance.PlayDeathSound();

        Instantiate(PopParticles, transform.position, Quaternion.identity);
        CameraFollower.Instance.StopFollow();
        CameraFollower.Instance.DoShake();
        GameManager.Instance.InvokeRespawn();

        gameObject.SetActive(false);
    }

    private void TriggerEnding()
    {
        AudioManager.Instance.PlayEndingMusic();

        _endingTriggered = true;
        _rb.velocity = new Vector2(0, _rb.velocity.y);

        _animator.SetBool("IsWalking", false);
        SoulAnimator.SetBool("IsWalking", false);

        GetComponent<BallLauncher>().SetEndingTriggered();

        SoulRenderer.DOColor(new Color(1, 1, 1, 0.1f), EndingFadeTime)
                    .SetDelay(2.5f)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => GameManager.Instance.InvokeCredits())
                    .Play();
    }

    private void OnEnable()
    {
        if (IntroController.Instance.GameStarted)
        {
            _animator.SetTrigger("GameStart");
        }
    }
}
