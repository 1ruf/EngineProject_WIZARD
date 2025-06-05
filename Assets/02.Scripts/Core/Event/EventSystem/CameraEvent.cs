namespace Core.Events
{
    public class CameraEvent
    {
        public static readonly CameraShakeEvent CameraShakeEvent = new();
    }
    public class CameraShakeEvent : GameEvent
    {
        public float Power;
        public float Speed = 50f;
        public float Duration = 0f;
    }
}