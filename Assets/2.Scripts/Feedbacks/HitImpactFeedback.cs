using Blade.Effects;
using Blade.Entities;
using DG.Tweening;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Blade.Feedbacks
{
    public class HitImpactFeedback : Feedback
    {
        [SerializeField] private PoolingItemSO hitImpactItem;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private ActionData actionData;

        [Inject] private PoolManagerMono _poolManager;

        private PoolingEffect _effect;
        
        public override void CreateFeedback()
        {
            _effect = _poolManager.Pop<PoolingEffect>(hitImpactItem);
            
            Quaternion rotation = Quaternion.LookRotation(actionData.HitNormal * -1);
            _effect.PlayVFX(actionData.HitPoint, rotation);

            DOVirtual.DelayedCall(playDuration, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (_effect == null) return;
            _poolManager.Push(_effect);
        }
    }
}