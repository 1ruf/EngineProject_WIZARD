using NUnit.Framework;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject _targetSkillObj;
    [SerializeField] private float attackRange = 5f;

    [SerializeField] private List<Skill> skills;

    private Player _player;
    private PlayerInputSO _input;

    private bool _isSkillReady;
    private SkillSO _skillSO;

    private Skill _currentSkillAtt;

    private bool _isCooltime;

    public void Initialize(Player player)
    {
        _player= player;
        _input = player.Input;

        AddEvents();

        //테스트코드
        _isSkillReady = true;
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void Update()
    {
        if (_isSkillReady)
        {
            if (_targetSkillObj.activeSelf == false) _targetSkillObj.SetActive(true);
            Vector3 atkPos = _input.GetWorldPosition();
            SetTargetSkill(atkPos, 3);
        }
    }
    private void AddEvents()
    {
        _input.OnAttackPressed += HandleAttackPress;
    }
    private void RemoveEvents()
    {
        _input.OnAttackPressed -= HandleAttackPress;
    }

    private void SetTargetSkill(Vector3 targetPos,float radius)
    {
        _targetSkillObj.transform.localScale = new Vector3(radius, 1, radius);
        _targetSkillObj.transform.position = targetPos;
    }

    private void HandleAttackPress()
    {
        Vector3 AttackPosition = _input.GetWorldPosition();
        if ((_isCooltime == true) && (Vector3.Distance(AttackPosition, transform.position) > (int)_skillSO.SkillRange.Range)) return;
        _isCooltime = true;

        _skillSO = _player.CurrentSkill;

        
        _player.CanMove = false;
        print("공격");
        _isSkillReady = false;
        _targetSkillObj.SetActive(false);
        //대충 애니메이션
        SkillActive(AttackPosition,_skillSO);
    }
    private void SkillActive(Vector3 targetPos,SkillSO skill)
    {
        switch (skill.SkillType.Type)
        {
            case SKILL_TYPE.Summon:
                _currentSkillAtt = skills[0] as SummonSkill;
                break;
            case SKILL_TYPE.Throw:
                _currentSkillAtt = skills[1] as ThrowSkill;
                break;
            case SKILL_TYPE.Defence:
                _currentSkillAtt = skills[2] as DefenceSkill;
                break;
        }
        ExcuteSkill(targetPos,skill);
    }
    private void ExcuteSkill(Vector3 targetPos, SkillSO skill)
    {
        _currentSkillAtt.Excute(targetPos,skill);
        Instantiate(skill.skillEffect,null).transform.position = targetPos;
        StartCoroutine(Reset(3f));
    }
    private IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        _isCooltime = false;
        _player.CanMove = true;






        _isSkillReady = true;
    }
}
