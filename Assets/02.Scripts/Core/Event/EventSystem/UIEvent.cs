using UnityEngine;

namespace Core.Events
{
    public class UIEvent
    {
        public static readonly PlayerStatEvent PlayerStatEvent = new PlayerStatEvent();
    }
    public class PlayerStatEvent : GameEvent
    {
        public float MaxHp;
        public float MaxMp;

        public float Hp;
        public float Mp;
    }
}