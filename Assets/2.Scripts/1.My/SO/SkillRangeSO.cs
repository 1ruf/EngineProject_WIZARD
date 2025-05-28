using UnityEngine;

[CreateAssetMenu(fileName = "Range", menuName = "SO/piece/Range")]
public class SkillRangeSO : ScriptableObject
{
    public int Level;
    public SKILL_RANGE Range;

    public int GetRange(SKILL_RANGE range)
    {
        return (int)range;
    }
}
public enum SKILL_RANGE
{
    CLSOE = 3,
    MIDDLE = 10,
    LONG = 20,
}