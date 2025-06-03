using Care.Event;
using Core;
using System.Collections;
using System.Runtime;
using UnityEngine;

public class ThrowSkill : Skill
{
    [SerializeField] private EventChannelSO cameraChannel;
    [Header("Shake")]
    [SerializeField] private float impactTime = 0f;
    [SerializeField] private float power = 1f;
    [SerializeField] private float throwPower = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected override void Active(SkillSO skill, Vector3 OriginPos, Vector3 targetPos)
    {
        ApplyDamage(skill.Range, skill.Damage);
        gameObject.SetActive(true);
        Throw(targetPos - OriginPos);
        //transform.position = targetPos;
        StartCoroutine(ImpactShake(impactTime));
    }
    private IEnumerator ImpactShake(float time)
    {
        yield return new WaitForSeconds(time);

        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = power;
        cameraChannel.InvokeEvent(evt);
    }

    private void ApplyDamage(float attackRange, int damage)
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

    private void Throw(Vector3 direction)
    {
        rb.AddForce(direction * throwPower,ForceMode.Force);
    }
}
