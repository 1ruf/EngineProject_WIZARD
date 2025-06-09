using Blade.Entities;
using UnityEngine;

namespace Blade.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;

        protected Entity _owner;

        public virtual void InitCaster(Entity owner)
        {
            _owner = owner;
        }

        public abstract void CastDamage(DamageData damageData, Vector3 position, Vector3 direction, AttackDataSO attackData);
    }
}