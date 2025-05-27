using System;
using Blade.Combat;
using Blade.Core.StatSystem;
using Blade.Entities;
using UnityEngine;

namespace Blade.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {

        [Header("attack datas"), SerializeField] private AttackDataSO[] attackDataList;

        [SerializeField] private StatSO attackSpeedStat;
        [SerializeField] private StatSO physicalDamageStat;
        [SerializeField] private float comboWindow;
        private Entity _entity;
        private EntityAnimator _entityAnimator;
        private EntityVFX _vfxCompo;
        private EntityAnimatorTrigger _animatorTrigger;
        private EntityStatCompo _statCompo;
        private DamageCompo _damageCompo;

        private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");
        private readonly int _comboCounterHash = Animator.StringToHash("COMBO_COUNTER");

        private float _attackSpeed = 1f;
        private float _lastAttackTime;

        public bool useMouseDirection = false;

        public int ComboCounter { get; set; } = 0;

        [SerializeField] private DamageCaster damageCaster;
        public float AttackSpeed
        {
            get => _attackSpeed;
            set
            {
                _attackSpeed = value;
                _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
            }
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _entityAnimator = entity.GetCompo<EntityAnimator>();
            _vfxCompo = entity.GetCompo<EntityVFX>();
            _animatorTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _statCompo = entity.GetCompo<EntityStatCompo>();
            _damageCompo = entity.GetCompo<DamageCompo>();
        }

        public void AfterInitialize()
        {
            _animatorTrigger.OnAttackVFXTrigger += HandleAttackVFXTrigger;
            _animatorTrigger.OnDamageCastTrigger += HandleDamageCasterTrigger;

            StatSO target = _statCompo.GetStat(attackSpeedStat);
            Debug.Assert(target != null, $"{attackSpeedStat.statName} does not exist");
            target.OnValueChanged += HandleAttackSpeedChange;
            AttackSpeed = target.Value;
        }

        private void OnDestroy()
        {
            _animatorTrigger.OnAttackVFXTrigger -= HandleAttackVFXTrigger;
            _animatorTrigger.OnDamageCastTrigger -= HandleDamageCasterTrigger;
            StatSO target = _statCompo.GetStat(attackSpeedStat);
            Debug.Assert(target != null, $"{attackSpeedStat.statName} does not exist");
            target.OnValueChanged -= HandleAttackSpeedChange;
        }

        private void HandleAttackSpeedChange(StatSO stat, float currentvalue, float previousvalue)
        {
            AttackSpeed = currentvalue;
        }

        private void HandleDamageCasterTrigger()
        {
            AttackDataSO attackData = GetCurrentAttackData();
            DamageData damageData = _damageCompo.CalculateDamage(physicalDamageStat, attackData);

            Vector3 position = damageCaster.transform.position;
            damageCaster.CastDamage(damageData, position, _entity.transform.forward, attackData);
        }

        private void HandleAttackVFXTrigger()
        {
            _vfxCompo.PlayVfx($"Blade{ComboCounter}", Vector3.zero, Quaternion.identity);
        }

        public void Attack()
        {
            bool comboCounterOver = ComboCounter > 2;
            bool comboWindowExhaust = Time.time >= _lastAttackTime + comboWindow;
            if (comboCounterOver || comboWindowExhaust)
            {
                ComboCounter = 0;
            }
            _entityAnimator.SetParam(_comboCounterHash, ComboCounter);

        }

        public void EndAttack()
        {
            ComboCounter++;
            _lastAttackTime = Time.time;
        }

        public AttackDataSO GetCurrentAttackData()
        {
            Debug.Assert(attackDataList.Length > ComboCounter, "Combo counter is out of range");
            return attackDataList[ComboCounter];
        }


    }
}