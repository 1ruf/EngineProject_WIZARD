using Core;
using Core.Events;
using System.Collections;
using UnityEngine;

public class SummonSkill : MonoBehaviour
{
    [SerializeField] private EventChannelSO cameraChannel;
    [Header("Shake")]
    [SerializeField] private float impactTime = 0f;
    [SerializeField] private float power = 1f;
    public void Active(SkillSO skill, Vector3 OriginPos, Vector3 targetPos)
    {
        gameObject.SetActive(true);
        StartCoroutine(ImpactShake(skill, impactTime));
    }
    private IEnumerator ImpactShake(SkillSO skill, float time)  
    {
        yield return new WaitForSeconds(time);

        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = power;
        cameraChannel.InvokeEvent(evt);
        ApplyDamage(skill.Range, skill.Damage + Random.Range(-skill.Damage/5, skill.Damage/5));
    }

    private void ApplyDamage(float attackRange, int damage)
    {
        attackRanged = attackRange;
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Blade.Combat.IDamageable damageable))
            {
                damageable.ApplyDamage(new DamageData() { damage = damage},transform.position,Vector3.zero,null,null);
            }
        }
    }

    private float attackRanged;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRanged);
    }
}
