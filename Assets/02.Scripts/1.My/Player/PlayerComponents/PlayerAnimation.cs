using System.Collections;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour, IPlayerComponent
{
    private PlayerAnimatorTrigger _trigger;
    private Animator _animator;
    private AnimationState _currentState;

    private bool _isCanChange;
    public void Initialize(Player player)
    {
        _animator = GetComponent<Animator>();
        _trigger = player.GetCompo<PlayerAnimatorTrigger>();

        _currentState = AnimationState.Idle;
        SetAnimatorParams(AnimationState.Idle);
        _isCanChange = true;

        _trigger.OnAnimationEnd += OnAttackAnimationEnd;
        player.OnPlayerDeath += PlayerDeath;
    }
    private void PlayerDeath()
    {
        _animator.enabled = false;
    }
    private void OnDestroy()
    {
        _trigger.OnAnimationEnd -= OnAttackAnimationEnd;
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
        if (_animator == null || !_isCanChange)
            return;

        _animator.SetBool("IDLE", state == AnimationState.Idle);
        _animator.SetBool("WALK", state == AnimationState.Walk);
        _animator.SetBool("RUN", state == AnimationState.Run);

        if (state == AnimationState.Attack)
        {
            _isCanChange = false;
            StartCoroutine(SetAttackDelayed(0.25f));
        }
        else
        {
            _animator.SetBool("ATTACK", false);
        }
    }
    private IEnumerator SetAttackDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool("ATTACK", true);
    }

    private void OnAttackAnimationEnd()
    {
        _animator.SetBool("ATTACK", false); // ¸®¼Â
        _isCanChange = true;
    }
}

