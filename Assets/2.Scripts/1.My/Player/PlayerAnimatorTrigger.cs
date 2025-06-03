using System;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour, IPlayerComponent
{
    public event Action OnAnimationEnd;
    public event Action OnSpellActiveeMotion;

    private void AnimationOver()
    {
        OnAnimationEnd?.Invoke();
    }

    private void SpellActive()
    {
        OnSpellActiveeMotion?.Invoke();
    }

    public void Initialize(Player player) { }
}
