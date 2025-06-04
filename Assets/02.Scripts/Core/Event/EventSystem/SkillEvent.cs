using UnityEngine;

namespace Core.Events
{
    public class SkillEvent
    {
        public static readonly SpawnSkillEvent SetSkillEvent = new();
    }
    public class SpawnSkillEvent : GameEvent
    {
        public Vector3 StartPosition;
        public Vector3 TargetPosition;

        public SkillSO Skill;
    }
}
