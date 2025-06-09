using Core;
using Core.Events;
using Players;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScroll : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private SkillSO currentSkillSO;
    [SerializeField] private EventChannelSO gameChannel;
    private Player _player;
    private PlayerInputSO _input;
    private PlayerMovement _movement;
    private EventChannelSO _uiChannel;

    private BuildSkillEvent _buildEvt => SkillEvent.BuildSkillEvent;

    private Volume _slowTimeVolume;
    private Skills _skills;
    private Action _beforeAction;

    private bool isSkillActivated;

    private bool _isCreating;
    private bool _skillUsing;

    private int _pressKey;

    private int _stackCnt;
    private int _skillCnt;

    private void Awake()
    {
        _slowTimeVolume = GetComponentInChildren<Volume>();
        _skills = GetComponent<Skills>();
    }
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;
        _uiChannel = player.UiChannel;

        _player.GetCompo<PlayerAnimatorTrigger>().OnAnimationEnd += HandleSkillUsed;
        _input.OnSkillCreatePressed += HandleCreatePressed;
    }
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            _pressKey = 0;
            SetSkillCount(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            _pressKey = 1;
            SetSkillCount(1);

        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            _pressKey = 2;
            SetSkillCount(2);

        }
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
        _stackCnt = 0;
        SetTimeSlow(true);
        SetBuildCount();
        AddSpaceListener(HandleCreatePressed, S1);
    }

    private void BuildComplete()
    {
        SkillSO skill = _buildEvt.SkillSO;

        AddSpaceListener(BuildComplete, HandleCreatePressed);
        _stackCnt = 4;
        SetBuildCount();
        SetTimeSlow(false);
        SetSkill(skill);
    }

    private void S1()
    {
        _stackCnt = 1;
        SetBuildCount();
        SkillRangeSO range = new();
        range = _skills.GetSkill<SkillRangeSO>(_pressKey);
        print(range);
        _buildEvt.SkillSO.SkillRange = range;
        AddSpaceListener(S1, S2);
    }
    private void S2()
    {
        _stackCnt = 2;
        SetBuildCount();
        SkillAttributeSO attribute = new();
        attribute = _skills.GetSkill<SkillAttributeSO>(_pressKey);
        print(attribute);
        _buildEvt.SkillSO.SkillAttribute = attribute;
        AddSpaceListener(S2, S3);
    }
    private void S3()
    {
        _stackCnt = 3;
        SetBuildCount();
        SkillTypeSO type = new();
        type = _skills.GetSkill<SkillTypeSO>(_pressKey);
        print(type);
        _buildEvt.SkillSO.SkillType = type;
        AddSpaceListener(S3, BuildComplete);
    }

    private void SetTimeSlow(bool value)
    {
        if (value)
        {
            _slowTimeVolume.weight = 1f;
            Time.timeScale = 0.3f;
            return;
        }
        _slowTimeVolume.weight = 0f;
        Time.timeScale = 1f;
    }

    private void AddSpaceListener(Action beforeAction, Action nexAction)
    {
        _beforeAction = beforeAction;
        _input.OnSkillCreatePressed -= beforeAction;
        _input.OnSkillCreatePressed += nexAction;
    }

    private void SetBuildCount()
    {
        SkillBuildUiEvent evt = UIEvent.SkillBuildUiEvent;
        evt.StackCount = _stackCnt;
        evt.SkillNum = _skillCnt;
        _uiChannel.InvokeEvent(evt);
    }
    private void SetSkillCount(int index)
    {
        _skillCnt = index;
        SkillBuildUiEvent evt = UIEvent.SkillBuildUiEvent;
        evt.SkillNum = _skillCnt;
        _uiChannel.InvokeEvent(evt);
    }
}
