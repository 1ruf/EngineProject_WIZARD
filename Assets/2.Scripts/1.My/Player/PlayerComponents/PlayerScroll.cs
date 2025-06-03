using Players;
using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerScroll : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float slowMotioSpeed = 0.2f;
    [SerializeField] private SkillSO _currentSkillSO;
    [SerializeField] private Transform orbHandler;
    private Player _player;
    private PlayerInputSO _input;
    private PlayerMovement _movement;

    private bool isSkillActivated;

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
        if (isSkillActivated == true) return;
        SetSkillActive(true);
        SetSkill();
    }
    public void SetSkillActive(bool value)
    {
        isSkillActivated = value;
    }
    public void SetSkill()
    {
        //대충 만들어서 넘겨주기
        _player.GetCompo<PlayerAttack>().SkillReady(_currentSkillSO);

        Instantiate(_currentSkillSO.SkillAttribute.Orb, orbHandler);
    }
    public void RemoveOrb()
    {
        foreach (Transform child in orbHandler)
        {
            child.gameObject.SetActive(false);
        }
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
