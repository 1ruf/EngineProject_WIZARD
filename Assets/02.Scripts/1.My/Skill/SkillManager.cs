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
        SummonSkill(callback.Skill,callback.StartPosition, callback.TargetPosition);
    }

    private void SummonSkill(SkillSO skill,Vector3 originPos, Vector3 targetPos)
    {
        if (CheckDistance(targetPos, (float)skill.SkillRange.Range) == false) return;

        GameObject effect = Instantiate(skill.SkillAttribute.SkillEffect,transform);
        if (effect.TryGetComponent(out Skill skillScr) == false)
        {
            Destroy(effect);
            return;
        };

        effect.transform.position = targetPos;
        effect.SetActive(true);
        skillScr.UseSkill(originPos,targetPos, skill,(int)skill.SkillType.Type);
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
