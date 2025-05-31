using Players;
using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerScroll : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float slowMotioSpeed = 0.2f;
    private Player _player;
    private SkillSO _currentSkillSO;
    private PlayerInputSO _input;

    private bool _isCreating;
    private bool _skillUsing;
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;

        _player.GetCompo<PlayerAnimatorTrigger>().OnAnimationEnd += HandleSkillUsed;
        _input.OnSkillCreatePressed += HandleCreatePressed;
    }

    private void HandleSkillUsed()
    {
        _skillUsing = false;
        _player.CanMove = true;
    }

    private void HandleCreatePressed()
    {
        SetSkill();
    }
    public void SetSkill()
    {
        //대충 만들어서 넘겨주기
        _player.GetCompo<PlayerAttack>().SkillReady(_currentSkillSO);

    }

    public SkillSO GetSkill()
    {
        return _currentSkillSO;
    }

    private void SetSkill(SkillSO skill)
    {
        _currentSkillSO = skill;
    }
}
