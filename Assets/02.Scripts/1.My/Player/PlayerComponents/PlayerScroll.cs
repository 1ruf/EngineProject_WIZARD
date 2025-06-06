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
    [SerializeField] private SkillSO _currentSkillSO;
    private Player _player;
    private PlayerInputSO _input;
    private PlayerMovement _movement;

    private Volume _slowTimeVolume;

    private bool isSkillActivated;

    private bool _isCreating;
    private bool _skillUsing;

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
        _input.OnSkillCreatePressed -= HandleCreatePressed;
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
        return _currentSkillSO;
    }

    private void BuildSkill()
    {
        SetTimeSlow(true);
        AddSpaceListener(()=>HandleCreatePressed(), ()=>S1());
    }

    private void BuildComplete()
    {
        SkillSO skill = new SkillSO()
        {

        };

        SetTimeSlow(false);
        SetSkill(skill);

        AddSpaceListener(()=>BuildComplete(),() => HandleCreatePressed());
    }

    private void S1()
    {
        print("build1");
        AddSpaceListener(() => S1(), () => S2());
    }
    private void S2()
    {
        AddSpaceListener(() => S2(), () => S3());
        print("build2");
    }
    private void S3()
    {
        AddSpaceListener(() => S3(), () => BuildComplete());
        print("build3");
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
        _input.OnSkillCreatePressed -= beforeAction;
        _input.OnSkillCreatePressed += nexAction;
    }
}
