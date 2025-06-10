using Blade.Entities;
using UnityEngine;

public class EnemyHitComponent : MonoBehaviour, IEntityComponent
{
    private Entity _enemy;

    public void Initialize(Entity entity)
    {
        _enemy = entity;
    }
}
