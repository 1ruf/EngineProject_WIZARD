namespace Core.Events
{
    public class SceneEvent
    {
        public static readonly SceneChangeEvent SceneChangeEvent = new();
    }
    public class SceneChangeEvent : GameEvent
    {
        public string SceneName;    
    }
}