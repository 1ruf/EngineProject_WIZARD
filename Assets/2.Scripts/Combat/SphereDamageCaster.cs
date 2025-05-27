using System;
using UnityEngine;

namespace Blade.Combat
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField, Range(0.5f, 3f)] private float castRadius = 1f;
        [SerializeField, Range(0, 1f)] private float castInterpolation = 0.5f;
        [SerializeField, Range(0, 3f)] private float castingRange = 1f;
        
        public override void CastDamage(DamageData damageData, Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            Vector3 startPos = position + direction * -castInterpolation * 2; //- 붙어있음.
            
            bool isHit = Physics.SphereCast(
                startPos, castRadius, 
                transform.forward, 
                out RaycastHit hit, 
                castingRange,
                whatIsEnemy);

            if (isHit)
            {
                Debug.Log($"<color=red>Hit</color> {hit.collider.name}");
                if(hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    float damage = 5f; //스탯시스템 만들고 수정한다.
                    damageable.ApplyDamage(damageData, hit.point, hit.normal, attackData, _owner);
                }
                if (hit.collider.TryGetComponent(out IKnockBackable kb))
                {
                    Vector3 force = transform.forward * attackData.knockBackForce;
                    kb.KnockBack(force, attackData.knockBackDuration);
                }
            }
            else
            {
                Debug.Log("Not hit");
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 startPos = transform.position + transform.forward * -castInterpolation * 2;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPos, castRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startPos + transform.forward*castingRange, castRadius);
            
        }
#endif
    }
}