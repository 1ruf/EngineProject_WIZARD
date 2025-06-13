using Core;
using Core.Events;
using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EventChannelSO cameraChannel;
    private void OnEnable()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.1f);
        cameraChannel.InvokeEvent(new CameraShakeEvent() { Power = 2, Speed = 30});
        Destroy(gameObject);
    }
}
