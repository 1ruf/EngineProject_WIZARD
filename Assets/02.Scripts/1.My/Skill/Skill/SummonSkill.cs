using Care.Event;
using Core;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SummonSkill : MonoBehaviour
{
    [SerializeField] private EventChannelSO cameraChannel;
    [Header("Shake")]
    [SerializeField] private float impactTime = 0f;
    [SerializeField] private float power = 1f;
    public void Active(SkillSO skill,Vector3 OriginPos, Vector3 targetPos)
    {
        gameObject.SetActive(true);
        StartCoroutine(ImpactShake(skill,impactTime));
    }
    private IEnumerator ImpactShake(SkillSO skill,float time)
    {
        yield return new WaitForSeconds(time);

        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = power;
        ApplyDamage(skill.Range, skill.Damage + Random.Range(-1, 1));
        cameraChannel.InvokeEvent(evt);
    }

    private void ApplyDamage(float attackRange,int damage)
    {
        attackRanged = attackRange;
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            print(collider);
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
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
