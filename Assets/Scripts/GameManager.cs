using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player;
    public float RespawnTime;
    public Transform CurrentActiveCheckpoint;
    public GameObject EndingPanel;

    public void SetActiveCheckpoint(Transform checkpoint)
    {
        CurrentActiveCheckpoint = checkpoint;
    }

    private void Respawn()
    {
        AudioManager.Instance.PlaySpawnSound();
        CameraFollower.Instance.FollowSpeed = 15;

        Player.transform.position = CurrentActiveCheckpoint.position;

        Player.SetActive(true);

        CameraFollower.Instance.StartFollow();
    }

    public void InvokeRespawn()
    {
        Invoke(nameof(Respawn), RespawnTime);
    }

    public void InvokeCredits()
    {
        Invoke(nameof(ShowCredits), 1.2f);
    }

    private void ShowCredits()
    {
        EndingPanel.SetActive(true);
    }
}
