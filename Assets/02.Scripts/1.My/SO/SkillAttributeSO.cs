using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName = "SO/piece/Attribute")]
public class SkillAttributeSO : ScriptableObject
{
    public string Name;
    public int ManaCost;
    public int Damage;
    public int AttackRange;

    public GameObject Orb;
    public GameObject SkillEffect;
    public SKILL_ATTRIBUTE attribute;
}
public enum SKILL_ATTRIBUTE
{
    ICE,
    WIND,
    UNIVERSE,
    HOLOGRAM
}