using Care.Event;
using Core;
using System.Collections;
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
        ApplyDamage(skill.Range,skill.Damage + Random.Range(-1,1));
        StartCoroutine(ImpactShake(impactTime));
    }
    private IEnumerator ImpactShake(float time)
    {
        yield return new WaitForSeconds(time);

        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = power;
        cameraChannel.InvokeEvent(evt);
    }

    private void ApplyDamage(float attackRange,int damage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
    }
}
