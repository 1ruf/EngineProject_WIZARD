using System;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour, IPlayerComponent
{
    public event Action OnAnimationEnd;

    private void AnimationOver()
    {
        OnAnimationEnd?.Invoke();
        print("애니메이션 종료");
    }

    public void Initialize(Player player) { }
}
