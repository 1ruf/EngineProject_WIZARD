using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private float range;


    public void DamageDealer(int damage)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range);
        foreach (Collider target in targets)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.yellow;
    }
}
