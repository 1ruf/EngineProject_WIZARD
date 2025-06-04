using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class SpawnParent : Skill
{
    [SerializeField] private GameObject effect;
    

    private SkillSO _curSkill;

    protected override void Active(SkillSO skill, Vector3 OriginPos, Vector3 targetPos, float spwanRange)
    {
        _curSkill = skill;
        StartCoroutine(Spawn(skill, OriginPos, targetPos, spwanRange));
    }

    private IEnumerator Spawn(SkillSO skill, Vector3 OriginPos, Vector3 targetPos, float spwanRange)
    {
        for (int i = 0; i < skill.SkillType.SpawnCount; i++)
        {
            GameObject eff = Instantiate(effect,transform);
            eff.GetOrAddComponent<SummonSkill>().Active(skill,OriginPos,targetPos);
            eff.transform.position = new Vector3(GetRandom(targetPos.x,spwanRange), targetPos.y,GetRandom(targetPos.z, spwanRange));
            yield return new WaitForSeconds(skill.SkillType.SpawnSpeed);
        }
    }

    private float GetRandom(float originV, float range)
    {
        return originV + Random.Range(-range, range);
    }
}
