using Core;
using Core.Events;
using Players;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class PlayerScroll : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private SkillSO currentSkillSO;
    [SerializeField] private EventChannelSO gameChannel;
    private Player _player;
    private PlayerInputSO _input;
    private PlayerMovement _movement;

    private BuildSkillEvent _buildEvt => SkillEvent.BuildSkillEvent;

    private Volume _slowTimeVolume;
    private Action _beforeAction;

    private bool isSkillActivated;

    private bool _isCreating;
    private bool _skillUsing;


    [SerializeField] private SkillRangeSO sampleRange;
    [SerializeField] private SkillAttributeSO sampleAttri;
    [SerializeField] private SkillTypeSO sampleType;

    private void Awake()
    {
        _slowTimeVolume = GetComponentInChildren<Volume>();
    }
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;

        _player.GetCompo<PlayerAnimatorTrigger>().OnAnimationEnd += HandleSkillUsed;
        _input.OnSkillCreatePressed += HandleCreatePressed;
    }

    private void OnDestroy()
    {
        _input.OnSkillCreatePressed -= _beforeAction;
    }

    private void HandleSkillUsed()
    {
        _skillUsing = false;
        _player.CanMove = true;
    }

    private void HandleCreatePressed()
    {
        if (isSkillActivated == true) return;
        _isCreating = true;
        BuildSkill();
    }
    public void SetSkillActive(bool value)
    {
        isSkillActivated = value;
    }
    public void SetSkill(SkillSO skill)
    {
        SetSkillActive(true);
        //대충 만들어서 넘겨주기
        _player.GetCompo<PlayerAttack>().SkillReady(skill);

        Instantiate(skill.SkillAttribute.Orb, _player.OrbHandler);
    }
    public void RemoveOrb()
    {
        foreach (Transform child in _player.OrbHandler)
        {
            Destroy(child.gameObject);
            //child.gameObject.SetActive(false);
        }
    }

    public SkillSO GetSkill()
    {
        return currentSkillSO;
    }

    private void BuildSkill()
    { 
        _buildEvt.SkillSO = new SkillSO();
        SetTimeSlow(true);
        AddSpaceListener(HandleCreatePressed, S1);
    }

    private void BuildComplete()
    {
        SkillSO skill = _buildEvt.SkillSO;

        AddSpaceListener(BuildComplete,HandleCreatePressed);
        SetTimeSlow(false);
        SetSkill(skill);
    }

    private void S1()
    {
        SkillRangeSO range = sampleRange;

        //SkillRangeSO range = new();
        _buildEvt.SkillSO.SkillRange = range;
        AddSpaceListener(S1, S2);
    }
    private void S2()
    {
        SkillAttributeSO attribute = sampleAttri;

        //SkillAttributeSO attribute = new();
        _buildEvt.SkillSO.SkillAttribute = attribute;
        AddSpaceListener( S2, S3);
    }
    private void S3()
    {
        SkillTypeSO type = sampleType;

        //SkillTypeSO type = new();
        _buildEvt.SkillSO.SkillType = type;
        AddSpaceListener(S3,BuildComplete);
    }

    private void SetTimeSlow(bool value)
    {
        if (value)
        {
            _slowTimeVolume.weight = 1f;
            Time.timeScale = 0.2f;
            return;
        }
        _slowTimeVolume.weight = 0f;
        Time.timeScale = 1f;
    }

    private void AddSpaceListener(Action beforeAction,Action nexAction)
    {
        _beforeAction = beforeAction;
        _input.OnSkillCreatePressed -= beforeAction;
        _input.OnSkillCreatePressed += nexAction;
    }
}
