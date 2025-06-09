using UnityEngine;

namespace Blade.Entities
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/EntityFinder", order = 0)]
    public class EntityFinderSO : ScriptableObject
    {
        public Player Target { get; private set; }

        public void SetTarget(Player target)
        {
            Target = target;
        }
    }
}