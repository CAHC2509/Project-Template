using UnityEngine;
using DG.Tweening;

public class SlideTransition : MonoBehaviour
{
    public enum SlideDirection { Left, Right }

    [SerializeField] private SlideDirection direction = SlideDirection.Left;
    [SerializeField] private float distance = 500f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private RectTransform target;
    private Vector2 originalPosition;

    private void Awake()
    {
        target = GetComponent<RectTransform>();
        originalPosition = target.anchoredPosition;
    }

    private void OnEnable() => Play();

    private void Play()
    {
        Vector2 startPos = originalPosition;

        if (direction == SlideDirection.Left)
            startPos.x -= distance;
        else
            startPos.x += distance;

        target.anchoredPosition = startPos;
        target.DOAnchorPos(originalPosition, duration).SetEase(ease);
    }
}
