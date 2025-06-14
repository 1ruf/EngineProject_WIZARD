using Blade.Entities;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPart;

    [SerializeField] private int testDamage = 8;

    private string _SAVED_ROUND_KEY = "SavedRound";

    private Entity _entity;
    private int _level;
    public void Initialize(Entity entity)
    {
        _entity = entity;

        _entity.GetCompo<EntityAnimatorTrigger>()
            .OnDamageCastTrigger += HandleAttack;
        _level = PlayerPrefs.GetInt(_SAVED_ROUND_KEY, 1);
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
            if (collider.transform.TryGetComponent(out Player player))
            {
                player.GetCompo<PlayerStat>().Damage(testDamage + Random.Range(0,_level));
            }
        }
    }
}
