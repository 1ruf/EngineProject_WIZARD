using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Core.Events
{
    public class UIEvent
    {
        public static readonly PlayerStatEvent PlayerStatEvent = new();
        public static readonly SkillBuildUiEvent SkillBuildUiEvent = new();
    }
    public class PlayerStatEvent : GameEvent
    {
        public float MaxHp;
        public float MaxMp;

        public float Hp;
        public float Mp;
    }
    public class SkillBuildUiEvent : GameEvent
    {
        public int StackCount;
        public int SkillNum;
    }
}