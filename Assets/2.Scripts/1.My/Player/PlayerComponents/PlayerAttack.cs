using Care.Event;
using Core;
using Core.Events;
using NUnit.Framework;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using VHierarchy.Libs;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject _targetSkillObj;

    public bool IsSkillReady {get; private set;}

    private Player _player;
    private PlayerInputSO _input;

    private SkillSO _skillSO;
    private EventChannelSO _skillChannel;
    private EventChannelSO _cameraChannel;

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
        _cameraChannel = player.cameraChannel;

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
    private void ActiveSkill(Vector3 pos)
    {
        SpawnSkillEvent evt = SkillEvent.SetSkillEvent;

        Debug.Assert(_skillSO == null,"skill이 없습니다.");
        evt.Skill = _skillSO;
        evt.StartPosition = transform.position;
        evt.TargetPosition = pos;

        _skillChannel.InvokeEvent(evt);
    }
    private bool CheckDistance(Vector3 v1,Vector3 v2)
    {
        return Vector3.Distance(v1, v2) < (int)_skillSO.SkillRange.Range;
    }

    private void HandleAttackPress()
    {
        if (!IsSkillReady) return;
        IsSkillReady = false;

        Vector3 AttackPosition = _input.GetWorldPosition();
        //if (_isCooltime && !_isAttackable) return;
        //if (!CheckDistance(AttackPosition, transform.position)) return;

        ActiveSkill(AttackPosition);
        _player.CanMove = false;

        _player.GetCompo<PlayerAnimation>().SetState(AnimationState.Attack);


        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = 1f;
        evt.Duration = 0f;
        _cameraChannel.InvokeEvent(evt);
        //_isCooltime = true;

        //_skillSO = _player.CurrentSkill;


        //_player.CanMove = false;
        //IsSkillReady = false;
        //_targetSkillObj.SetActive(false);
        //대충 애니메이션
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
