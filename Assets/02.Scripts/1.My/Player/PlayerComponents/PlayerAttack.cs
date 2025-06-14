using Core;
using Core.Events;
using Players;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private TextMeshProUGUI _distaceTmp;
    [SerializeField] private GameObject goong;

    public bool IsSkillReady { get; private set; }

    private Player _player;
    private PlayerInputSO _input;
    private PlayerAnimatorTrigger _animTrigger;
    private PlayerScroll _scroll;
    private PlayerStat _stat;
    private SkillSO _skillSO;
    private EventChannelSO _skillChannel;


    private string _SAVED_ROUND_KEY = "SavedRound";
    private int _savedRound;
    private bool _isAttacking = false;
    private bool _secretUse;

    public void SkillReady(SkillSO skill)
    {
        _skillSO = skill;
        IsSkillReady = true;
    }

    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;
        _skillChannel = player.SkillChannel;
        _stat = player.GetCompo<PlayerStat>();

        _savedRound = PlayerPrefs.GetInt(_SAVED_ROUND_KEY, 0);
        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void AddEvents()
    {
        _input.OnAttackPressed += HandleAttackPress;
        _input.OnFKeyPressed += HandleSecretSkill;
    }
    private void RemoveEvents()
    {
        _input.OnAttackPressed -= HandleAttackPress;
        _input.OnFKeyPressed -= HandleSecretSkill;
    }


    private void HandleSecretSkill()
    {
        print($"비밀 스킬 사용 시도 : 현재조건{_savedRound}/5, 사용여부:{_secretUse}");
        if (_savedRound < 5 || _secretUse == true) return;
        print("생성");
        GameObject sSkill = Instantiate(goong, null);
        sSkill.transform.position = transform.position + new Vector3(0,2,0);
        _secretUse = true;
    }

    private void OrbDestroy()
    {
        if (_scroll == null) _scroll = _player.GetCompo<PlayerScroll>();
        _scroll.SetSkillActive(false);
        _scroll.RemoveOrb();
    }

    private void ActiveSkill()
    {
        Vector3 AttackPosition = _input.GetWorldPosition();

        OrbDestroy();

        SpawnSkillEvent evt = SkillEvent.SetSkillEvent;
        Debug.Assert(_skillSO != null, "skill이 없습니다.");
        evt.Skill = _skillSO;
        evt.StartPosition = transform.position;
        evt.TargetPosition = AttackPosition;

        _skillChannel.InvokeEvent(evt);

        _animTrigger.OnSpellActiveeMotion -= ActiveSkill;
        _isAttacking = false;
    }

    private void HandleAttackPress()
    {
        if (IsSkillReady == false || _skillSO.Mana > _player.Mp) return;
        if (_isAttacking) return;

        _stat.ManaUse(_skillSO.Mana);
        IsSkillReady = false;
        _isAttacking = true;

        if (_animTrigger == null) _animTrigger = _player.GetCompo<PlayerAnimatorTrigger>();

        _animTrigger.OnSpellActiveeMotion -= ActiveSkill;
        _animTrigger.OnSpellActiveeMotion += ActiveSkill;

        _player.CanMove = false;

        _player.GetCompo<PlayerAnimation>().SetState(AnimationState.Attack);
    }
    private void FixedUpdate()
    {
        SetDistaceUI();
    }
    private void SetDistaceUI()
    {
        if (IsSkillReady == false)
        {
            _distaceTmp.text = string.Empty;
            return;
        }
        Vector3 pos = _input.GetWorldPosition();
        float dis = Vector3.Distance(_player.transform.position, pos);

        _distaceTmp.color = (float)_skillSO.SkillRange.Range > dis ? Color.green : Color.red;
        _distaceTmp.text = $"{Vector3.Distance(_player.transform.position, pos):F1}";
    }
}
