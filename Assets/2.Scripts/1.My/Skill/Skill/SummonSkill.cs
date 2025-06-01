using Care.Event;
using Core;
using System.Collections;
using UnityEngine;

public class SummonSkill : Skill
{
    [SerializeField] private EventChannelSO cameraChannel;
    [Header("Shake")]
    [SerializeField] private float impactTime = 0f;
    [SerializeField] private float power = 1f;
    protected override void Active(Vector3 OriginPos, Vector3 targetPos)
    {
        gameObject.SetActive(true);
        transform.position = targetPos;
        StartCoroutine(ImpactShake(impactTime));
    }
    private IEnumerator ImpactShake(float time)
    {
        yield return new WaitForSeconds(time);

        CameraShakeEvent evt = CameraEvent.CameraShakeEvent;
        evt.Power = power;
        cameraChannel.InvokeEvent(evt);
    }
}
