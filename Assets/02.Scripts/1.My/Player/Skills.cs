using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public List<SkillRangeSO> Ranges;
    public List<SkillAttributeSO> Attributes;
    public List<SkillTypeSO> Types;

    public T GetSkill<T>(int num) where T : class
    {
        if (typeof(T) == typeof(SkillRangeSO))
        {
            if (num >= 0 && num < Ranges.Count)
                return Ranges[num] as T;
        }
        else if (typeof(T) == typeof(SkillAttributeSO))
        {
            if (num >= 0 && num < Attributes.Count)
                return Attributes[num] as T;
        }
        else if (typeof(T) == typeof(SkillTypeSO))
        {
            if (num >= 0 && num < Types.Count)
                return Types[num] as T;
        }
        return default;
    }
}
