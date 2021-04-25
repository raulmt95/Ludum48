using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroController : Singleton<IntroController>
{
    public Animator PlayerAnimator;
    public Image TitleImage;
    public float FadeDuration;
    public bool GameStarted;

    private void Awake()
    {
        GameStarted = false;
    }

    private void Update()
    {
        if (!GameStarted)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        GameStarted = true;

        TitleImage.DOFade(0, FadeDuration)
                  .SetEase(Ease.InQuad)
                  .OnComplete(() => TitleImage.gameObject.SetActive(false))
                  .Play();

        PlayerAnimator.SetTrigger("GameStart");
        CameraFollower.Instance.StartFollow();
    }
}
