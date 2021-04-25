using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Sources")]
    public AudioSource PlayerAS;
    public AudioSource BounceAS;
    public AudioSource ExplosionAS;
    public AudioSource ButtonAS;
    public AudioSource DoorAS;
    public AudioSource ChargeAS;
    public AudioSource ShootAS;
    public AudioSource CheckpointAS;
    public AudioSource MusicAS;

    [Header("Clips")]
    public AudioClip StepSound;
    public AudioClip DeathSound;
    public AudioClip BounceSound;
    public AudioClip BallExplosionSound;
    public AudioClip StarButtonSound;
    public AudioClip PressureButtonSound;
    public AudioClip DoorOpenSound;
    public AudioClip DoorCloseSound;
    public AudioClip ShootSound;
    public AudioClip CheckpointSound;
    public AudioClip SpawnSound;
    public AudioClip EndingMusic;

    private void PlaySound(AudioClip soundClip, AudioSource source, float minPitch, float maxPitch)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(soundClip);
    }

    public void PlayStepSound()
    {
        PlaySound(StepSound, PlayerAS, 0.8f, 1.2f);
    }

    public void PlayDeathSound()
    {
        PlaySound(DeathSound, PlayerAS, 0.95f, 1.05f);
    }

    public void PlayBounceSound()
    {
        PlaySound(BounceSound, BounceAS, 0.8f, 1.2f);
    }

    public void PlayBallExplosionSound()
    {
        PlaySound(BallExplosionSound, ExplosionAS, 0.85f, 1.15f);
    }

    public void PlayStarButtonSound()
    {
        PlaySound(StarButtonSound, ButtonAS, 0.9f, 1.1f);
    }

    public void PlayPressureButtonSound()
    {
        PlaySound(PressureButtonSound, ButtonAS, 0.95f, 1.05f);
    }

    public void PlayDoorOpenSound()
    {
        PlaySound(DoorOpenSound, DoorAS, 0.95f, 1.05f);
    }

    public void PlayDoorCloseSound()
    {
        PlaySound(DoorCloseSound, DoorAS, 0.95f, 1.05f);
    }

    public void PlayShootSound(float pitch)
    {
        PlaySound(ShootSound, ShootAS, pitch - 0.1f, pitch + 0.1f);
    }

    public void PlayCheckpointSound()
    {
        PlaySound(CheckpointSound, CheckpointAS, 0.98f, 1.02f);
    }

    public void PlaySpawnSound()
    {
        PlaySound(SpawnSound, CheckpointAS, 0.98f, 1.02f);
    }

    public void StartChargeSound()
    {
        ChargeAS.pitch = Random.Range(0.98f, 1.02f);
        ChargeAS.Play();
    }

    public void StopChargeSound()
    {
        ChargeAS.Stop();
    }

    public void PlayEndingMusic()
    {
        MusicAS.loop = false;
        MusicAS.Stop();

        MusicAS.PlayOneShot(EndingMusic);
    }
}
