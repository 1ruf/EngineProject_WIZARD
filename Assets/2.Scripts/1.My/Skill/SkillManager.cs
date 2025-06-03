using Core;
using Core.Events;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO skillChannel;
    [SerializeField] private PlayerFinderSO playerFinder;

    private void Awake()
    {
        AddListeners();
    }
    private void OnDestroy()
    {
        RemoveListenders();
    }
    private void HandleSkillSpawn(SpawnSkillEvent callback)
    {
        SKILL_TYPE sType = callback.Skill.SkillType.Type;
        switch (sType)
        {
            case SKILL_TYPE.Summon:
                SummonSkill(callback.Skill, callback.TargetPosition);
                break;
            case SKILL_TYPE.Throw:
                ThrowSkill(callback.Skill, callback.TargetPosition);
                break;
        }
    }

    private void SummonSkill(SkillSO skill, Vector3 targetPos)
    {
        if (CheckDistance(targetPos, (float)skill.SkillRange.Range) == false) return;

        GameObject effect = Instantiate(skill.skillEffect,transform);
        if (effect.TryGetComponent(out Skill skillScr) == false) return;
        effect.transform.position = targetPos;
        effect.SetActive(true);
        skillScr.UseSkill(targetPos, skill);
    }
    private void ThrowSkill(SkillSO skill, Vector3 targetPos)
    {
        if (CheckDistance(targetPos, (float)skill.SkillRange.Range) == false) return;

        GameObject effect = Instantiate(skill.skillEffect, transform);
        if (effect.TryGetComponent(out Skill skillScr) == false) return;
        effect.transform.position = playerFinder.Target.OrbHandler.position;
        effect.SetActive(true);
        skillScr.UseSkill(targetPos, skill);
    }
    private bool CheckDistance(Vector3 targetPos, float targetDistance)
    {
        return targetDistance > Vector3.Distance(playerFinder.Target.transform.position, targetPos);
    }

    #region AddEvents
    private void AddListeners()
    {
        skillChannel.AddListener<SpawnSkillEvent>(HandleSkillSpawn);
    } 
    private void RemoveListenders()
    {
        skillChannel.RemoveListener<SpawnSkillEvent>(HandleSkillSpawn);
    }
    #endregion
}
