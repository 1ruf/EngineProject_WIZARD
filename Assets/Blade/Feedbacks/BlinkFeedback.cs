using DG.Tweening;
using UnityEngine;

namespace Blade.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private float blinkDuration = 0.15f;
        [SerializeField] private float blinkIntensity = 0.15f;

        private readonly int _blinkHash = Shader.PropertyToID("_BlinkValue");

        public override void CreateFeedback()
        {
            meshRenderer.material.SetFloat(_blinkHash, blinkIntensity);
            DOVirtual.DelayedCall(blinkDuration, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (meshRenderer != null) //중간에 캐릭터가 Destroy되면 이걸 실행할 때 에러가 날 수 있으니 검사해야해.
            {
                meshRenderer.material.SetFloat(_blinkHash, 0);
            }
        }
    }
}