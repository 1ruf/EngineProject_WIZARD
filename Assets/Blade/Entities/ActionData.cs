using UnityEngine;

namespace Blade.Entities
{
    public class ActionData : MonoBehaviour, IEntityComponent
    {
        public Vector3 HitPoint { get; set; }
        public Vector3 HitNormal { get; set; }

        private Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}