using Core;
using Core.Events;
using NUnit.Framework;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject _targetSkillObj;

    public bool IsSkillReady {get; private set;}

    private Player _player;
    private PlayerInputSO _input;

    private SkillSO _skillSO;
    private EventChannelSO _skillChannel;

    private bool _isCooltime;
    private bool _isAttackable;

    public void SetSkillReady(bool value)
    {
        IsSkillReady = value;
        _isAttackable = value;
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

    private void Update()
    {
        if (IsSkillReady)
        {
            CheckSkill();

            Vector3 atkPos = _input.GetWorldPosition();
            SetTargetSkill(atkPos, _skillSO.Range, CheckDistance(atkPos,transform.position)?Color.green:Color.red);
        }
        else
        {
            if (_targetSkillObj.activeSelf == true) _targetSkillObj.SetActive(false);
        }
    }
    private void CheckSkill()
    {
        if (_skillSO == null) _skillSO = _player.CurrentSkill;
        if (_targetSkillObj.activeSelf == false) _targetSkillObj.SetActive(true);
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
        Vector3 AttackPosition = _input.GetWorldPosition();
        if (_isCooltime && !_isAttackable) return;
        if (!CheckDistance(AttackPosition, transform.position)) return;

        ActiveSkill(AttackPosition);

        _isCooltime = true;

        _skillSO = _player.CurrentSkill;

        
        _player.CanMove = false;
        IsSkillReady = false;
        _targetSkillObj.SetActive(false);
        //대충 애니메이션
    }
}
