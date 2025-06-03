using System.Collections;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour, IPlayerComponent
{
    private Animator _animator;
    private AnimationState _currentState;

    private bool _isCanChange;
    public void Initialize(Player player)
    {
        _animator = GetComponent<Animator>();
        _currentState = AnimationState.Idle;
        SetAnimatorParams(AnimationState.Idle);

        _isCanChange = true;
    }

    public void SetState(AnimationState newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
        SetAnimatorParams(newState);
    }

    private void SetAnimatorParams(AnimationState state)
    {
        if (_animator == null && _isCanChange == false)
        {
            Debug.LogError("Animator 컴포넌트가 할당되지 않았습니다.", this);
            return;
        }
        _animator.SetBool("IDLE", state == AnimationState.Idle);
        _animator.SetBool("WALK", state == AnimationState.Walk);
        _animator.SetBool("RUN", state == AnimationState.Run);
        //_animator.SetBool("ATTACK", state == AnimationState.Attack);

        if (state == AnimationState.Attack)
            _animator.SetTrigger("ATTACK");
    }
}

