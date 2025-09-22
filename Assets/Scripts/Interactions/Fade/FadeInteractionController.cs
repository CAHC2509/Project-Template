using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInteractionController : InteractionBaseController
{
    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public float FadeDuration => fadeDuration;

    public override void Initialize()
    {
        fadeCanvas.SetActive(false);
    }

    public override void BeginInteraction()
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
        fadeCanvas.SetActive(true);

        fadeImage.DOFade(1f, fadeDuration);
    }

    public override void FinishInteraction()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        fadeImage.DOFade(0f, fadeDuration).OnComplete(Conclude);
    }

    public override void Conclude()
    {
        fadeCanvas.SetActive(false);
    }
}
