using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "SO/piece/Type")]
public class SkillTypeSO : ScriptableObject
{
    public string Name;
    public SKILL_TYPE Type;

    [Header("Spawn")]
    public int SpawnCount = 1;
    public float SpawnSpeed;
}
public enum SKILL_TYPE
{
    FocusRanged = 0,
    MiddleRanged = 3,
    WideRanged = 5
}