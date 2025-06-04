using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public void UseSkill(Vector3 oP, Vector3 tP, SkillSO skill)
    {
        Active(skill,oP, tP);
    }
    protected abstract void Active(SkillSO skill,Vector3 OriginPos, Vector3 targetPos);
}
