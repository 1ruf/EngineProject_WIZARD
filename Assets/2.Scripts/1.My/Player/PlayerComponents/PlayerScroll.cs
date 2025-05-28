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
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;

        _input.OnSkillCreatePressed += HandleCreatePressed;
    }
    private void HandleCreatePressed()
    {
        SetSkill(_isCreating = !_isCreating);
    }
    public void SetSkill(bool value)
    {
        if (value)
        {
            Time.timeScale = slowMotioSpeed;
        }
        else
        {
            Time.timeScale = 1f;
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
