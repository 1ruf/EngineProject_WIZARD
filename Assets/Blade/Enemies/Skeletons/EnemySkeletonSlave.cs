using System;
using Blade.BT.Events;
using Blade.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy, IKnockBackable
    {
        public UnityEvent<Vector3, float> OnKnockBackEvent;
        private StateChange _stateChangeChannel;
        private CapsuleCollider _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CapsuleCollider>();
            OnDeathEvent.AddListener(HandleDeathEvent);
        }

        protected override void Start()
        {
            base.Start();
            _stateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
            //이벤트 채널 가져오기
        }

        private void OnDestroy()
        {
            OnDeathEvent.RemoveListener(HandleDeathEvent);
        }

        private void HandleDeathEvent()
        {
            if (IsDead) return;
            IsDead = true;
            _collider.enabled = false;
            _stateChangeChannel.SendEventMessage(EnemyState.DEAD); //코드에서 BT로 상태변경 이벤트 전송
        }

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }
    }
}