using Core;
using Core.Events;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO skillChannel;

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
        }
    }

    private void SummonSkill(SkillSO skill, Vector3 targetPos)
    {
        GameObject effect = Instantiate(skill.skillEffect,transform);
        if (effect.TryGetComponent(out Skill skillScr) == false) return;
        effect.transform.position = targetPos;
        effect.SetActive(true);
        skillScr.UseSkill(targetPos);
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
