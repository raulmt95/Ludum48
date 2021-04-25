using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquareButton : MonoBehaviour
{
    public Sprite ReleasedSprite;
    public Sprite PressedSprite;
    public UnityEvent OnPress;
    public UnityEvent OnRelease;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayPressureButtonSound();

            OnPress.Invoke();
            _renderer.sprite = PressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnRelease.Invoke();
            _renderer.sprite = ReleasedSprite;
        }
    }
}
