using UnityEngine;

namespace Core.Events
{
    public class SystemEvent
    {
        public static readonly EnemySpawnEvent EnemySpawnEvent = new();
    }
    public class EnemySpawnEvent : GameEvent
    {

    }
}