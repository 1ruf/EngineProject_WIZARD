using Blade.Effects;
using Blade.Entities;
using DG.Tweening;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Blade.Feedbacks
{
    public class SlashImpactFeedback : Feedback
    {
        [Inject] private PoolManagerMono _poolManager;

        [SerializeField] private float effectPlayTime = 0.5f;
        [SerializeField] private ActionData actionData;
        [SerializeField] private PoolingItemSO slashEffect;


        private PoolingEffect _currentEffect;

        public override void CreateFeedback()
        {
            _currentEffect = _poolManager.Pop<PoolingEffect>(slashEffect);
            _currentEffect.PlayVFX(actionData.HitPoint, Quaternion.identity);

            DOVirtual.DelayedCall(effectPlayTime, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (_currentEffect == null) return;
            _poolManager.Push(_currentEffect);
        }
    }
}
