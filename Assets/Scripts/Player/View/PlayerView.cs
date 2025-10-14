using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : ViewBase, IPlayerView
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
