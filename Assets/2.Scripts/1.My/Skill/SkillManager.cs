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

    }
    private void ThrowSkill()
    {

    }
    private void DefenseSkill()
    {

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
