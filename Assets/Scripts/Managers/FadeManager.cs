using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    public static FadeManager Instance;
    public bool IsFadingIn;
    public bool IsFadingOut;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        fadeImage.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        IsFadingOut = true;
        fadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
            IsFadingOut = false;
        });
    }

    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        IsFadingIn = true;
        fadeImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
            IsFadingIn = false;
        });
    }

    public void BlackOut()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
    }
}
