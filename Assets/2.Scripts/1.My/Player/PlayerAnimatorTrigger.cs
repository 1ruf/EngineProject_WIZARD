using System;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour, IPlayerComponent
{
    public event Action OnAnimationEnd;

    private void AnimationOver()
    {
        OnAnimationEnd?.Invoke();
        print("�ִϸ��̼� ����");
    }

    public void Initialize(Player player) { }
}
