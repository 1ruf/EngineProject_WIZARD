using UnityEngine;

[CreateAssetMenu(fileName = "Range", menuName = "SO/piece/Range")]
public class SkillRangeSO : ScriptableObject
{
    public int Level;
    public string Description;

    public int ManaCost;
    public float DamageDebuff;
    public SKILL_RANGE Range;

    public int GetRange(SKILL_RANGE range)
    {
        return (int)range;
    }
}
public enum SKILL_RANGE
{
    CLOSE = 12,
    MIDDLE = 22,
    LONG = 35,
}