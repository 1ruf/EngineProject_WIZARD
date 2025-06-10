using Blade.Enemies;
using Blade.Entities;
using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;

public class EnemyAttackComponent : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPart;

    [SerializeField] private int testDamage = 8;
    private Entity _entity;
    public void Initialize(Entity entity)
    {
        _entity = entity;

        _entity.GetCompo<EntityAnimatorTrigger>()
            .OnDamageCastTrigger += HandleAttack;
    }
    private void OnDestroy()
    {
        if (_entity != null)
        {
            _entity.GetCompo<EntityAnimatorTrigger>()
                .OnDamageCastTrigger -= HandleAttack;
        }
        _entity = null;
    }
    private void HandleAttack()
    {
        Collider[] hits = Physics.OverlapSphere(attackPart.position, attackRange);
        foreach (Collider collider in hits)
        {
            if(collider.transform.TryGetComponent(out Player player))
            {
                player.GetCompo<PlayerStat>().Damage(testDamage);
            }
        }
    }
}
