using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "SO/piece/Type")]
public class SkillTypeSO : ScriptableObject
{
    public string Name;
    public SKILL_TYPE Type;
}
public enum SKILL_TYPE
{
    FocusRanged,
    WideRanged
}