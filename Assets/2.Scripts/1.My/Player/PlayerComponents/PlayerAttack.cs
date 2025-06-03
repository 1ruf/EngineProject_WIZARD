using Care.Event;
using Core;
using Core.Events;
using NUnit.Framework;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using VHierarchy.Libs;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject _targetSkillObj;

    public bool IsSkillReady {get; private set;}

    private Player _player;
    private PlayerInputSO _input;
    private PlayerAnimatorTrigger _animTrigger;
    private PlayerScroll _scroll;

    private SkillSO _skillSO;
    private EventChannelSO _skillChannel;

    public void SkillReady(SkillSO skill)
    {
        _skillSO = skill;
        IsSkillReady = true;
    }
    public void Initialize(Player player)
    {
        _player= player;
        _input = player.Input;
        _skillChannel = player.SkillChannel;

        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void AddEvents()
    {
        _input.OnAttackPressed += HandleAttackPress;
    }
    private void RemoveEvents()
    {
        _input.OnAttackPressed -= HandleAttackPress;
    }

    private void SetTargetSkill(Vector3 targetPos,float radius, Color color)
    {
        _targetSkillObj.transform.localScale = new Vector3(radius, radius, 1);
        _targetSkillObj.transform.position = targetPos + new Vector3(0,0.01f,0);

        color.a = 0.5f;

        _targetSkillObj.GetComponent<SpriteRenderer>().color = color;
    }
    private void OrbDestroy()
    {
        if(_scroll == null) _scroll = _player.GetCompo<PlayerScroll>();
        _scroll.SetSkillActive(false);
        _scroll.RemoveOrb();
    }
    private void ActiveSkill()
    {
        Vector3 AttackPosition = _input.GetWorldPosition(); //이거 수정해야될수도

        OrbDestroy();

        SpawnSkillEvent evt = SkillEvent.SetSkillEvent;
        Debug.Assert(_skillSO != null,"skill이 없습니다.");
        evt.Skill = _skillSO;
        evt.StartPosition = transform.position;
        evt.TargetPosition = AttackPosition;

        _skillChannel.InvokeEvent(evt);

        _animTrigger.OnSpellActiveeMotion -= ActiveSkill;
    }
    private bool CheckDistance(Vector3 v1,Vector3 v2)
    {
        return Vector3.Distance(v1, v2) < (int)_skillSO.SkillRange.Range;
    }

    private void HandleAttackPress()
    {
        if (!IsSkillReady) return;
        IsSkillReady = false;

        if (_animTrigger == null) _animTrigger = _player.GetCompo<PlayerAnimatorTrigger>();
        _animTrigger.OnSpellActiveeMotion += ActiveSkill;
        _player.CanMove = false;

        _player.GetCompo<PlayerAnimation>().SetState(AnimationState.Attack);////////////////////////////////////////마법 종류에 따라서 애니메이션 다르게 하기
    }
    private IEnumerator SetAttackPostition()
    {
        while (true)
        {
            yield return null;

            if (!IsSkillReady) break;

            Vector3 atkPos = _input.GetWorldPosition();
            SetTargetSkill(atkPos, _skillSO.Range, CheckDistance(atkPos, transform.position) ? Color.green : Color.red);
        }
    }
}
