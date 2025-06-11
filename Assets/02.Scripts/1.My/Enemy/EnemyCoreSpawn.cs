using Blade.Entities;
using UnityEngine;

public class EnemyCoreSpawn : MonoBehaviour, IEntityComponent
{
    [SerializeField] private GameObject enemyCorePrefab;

    public void Initialize(Entity entity)
    {
    }

    public void SpawnEnemyCore()
    {
        Instantiate(enemyCorePrefab, transform.position, transform.rotation,null);
    }
}
