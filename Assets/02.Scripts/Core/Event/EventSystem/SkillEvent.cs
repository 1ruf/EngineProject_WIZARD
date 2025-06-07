using UnityEngine;

namespace Core.Events
{
    public class SkillEvent
    {
        public static readonly SpawnSkillEvent SetSkillEvent = new();
        public static readonly BuildSkillEvent BuildSkillEvent = new();
    }
    public class SpawnSkillEvent : GameEvent
    {
        public Vector3 StartPosition;
        public Vector3 TargetPosition;

        public SkillSO Skill;
    }
    public class BuildSkillEvent : GameEvent
    {
        public SkillSO SkillSO;
    }
}
