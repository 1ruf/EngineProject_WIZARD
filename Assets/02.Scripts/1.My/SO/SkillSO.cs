using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "SO/Skill")]
public class SkillSO : ScriptableObject
{
    public string Name;
    public int Damage;
    public int Range;

    public SkillRangeSO SkillRange /*{ get; private set; }*/;
    public SkillAttributeSO SkillAttribute /*{ get; private set; }*/;
    public SkillTypeSO SkillType /*{get; private set;}*/;

    public GameObject skillEffect;

    public void SetSkillSO(SkillRangeSO rangeSO,SkillAttributeSO attributeSO,SkillTypeSO skillType)
    {
        this.SkillRange = rangeSO;
        this.SkillAttribute = attributeSO;
        this.SkillType = skillType;
    }
}
