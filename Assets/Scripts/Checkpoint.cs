using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Color ActivatedColor;
    public ParticleSystem ActivationParticles;

    private SpriteRenderer _renderer;
    private bool _active = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!_active)
            {
                SetActive();
            }
        }
    }

    private void SetActive()
    {
        AudioManager.Instance.PlayCheckpointSound();

        _active = true;

        GameManager.Instance.SetActiveCheckpoint(transform);
        _renderer.color = ActivatedColor;

        ActivationParticles.Play();
    }
}
