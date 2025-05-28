using UnityEngine;

public class SummonSkill : Skill
{
    public override void Excute(Vector3 targetPos, SkillSO skill)
    {
        base.Excute(targetPos, skill);
        Collider[] collision = Physics.OverlapSphere(targetPos, skill.Range);
        foreach (Collider collider in collision)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"summon ��ų �ߵ�! : �����:{skill.Damage}");
                damageable.Damage(skill.Damage);
            }
        }
    }
}
