using System;
using Blade.Core.StatSystem;
using Blade.Entities;
using UnityEngine;

namespace Blade.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInitialize
    {
        private Entity _entity;
        private ActionData _actionData;
        private EntityStatCompo _statCompo;
        private TextComponent _dmgText;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<ActionData>();
            _statCompo = entity.GetCompo<EntityStatCompo>();
            _dmgText = entity.GetCompo<TextComponent>();
        }

        public void AfterInitialize()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValueChanged += HandleMaxHPChanged;
            currentHealth = maxHealth = target.Value;
        }

        private void OnDestroy()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValueChanged -= HandleMaxHPChanged;
        }

        private void HandleMaxHPChanged(StatSO stat, float currentvalue, float previousvalue)
        {
            float changed = currentvalue - previousvalue;
            maxHealth = currentvalue;
            if(changed > 0)
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
            else
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        public void ApplyDamage(DamageData damage, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            _actionData.HitNormal = hitNormal;
            _actionData.HitPoint = hitPoint;

            //넉백은 나중에 처리한다.
            //데미지도 나중에 처리한다.

            _dmgText.Damage((int)damage.damage);

            currentHealth = Mathf.Clamp(currentHealth - damage.damage, 0, maxHealth);
            if(currentHealth<= 0)
            {
                _entity.OnDeathEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke(); //이벤트만 발행한다.
        }


    }
}