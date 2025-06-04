using Unity.Behavior;
using UnityEngine;

public class SpawnParent : Skill
{
    [SerializeField] private GameObject effect;
    [Header("Spawn")]
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private float spawnRange;

    private SkillSO _curSkill;

    protected override void Active(SkillSO skill, Vector3 OriginPos, Vector3 targetPos)
    {
        _curSkill = skill;
        Spawn(skill,OriginPos,targetPos);
    }

    private void Spawn(SkillSO skill, Vector3 OriginPos, Vector3 targetPos)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject eff = Instantiate(effect,transform);
            eff.GetOrAddComponent<SummonSkill>().Active(skill,OriginPos,targetPos);
            eff.transform.position = new Vector3(GetRandom(targetPos.x,spawnRange), targetPos.y,GetRandom(targetPos.z,spawnRange));
        }
    }

    private float GetRandom(float originV, float range)
    {
        return originV + Random.Range(0, range);
    }
}
