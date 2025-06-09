using System.Collections.Generic;
using System.Linq;
using Blade.Entities;
using UnityEngine;

namespace Blade.Core.StatSystem
{
    public class EntityStatCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private StatOverride[] statOverrides;
        //private StatSO[] _stats; //��¥ ���ȵ�
        private Dictionary<string, StatSO> _stats;
        public Entity Owner { get; private set; } //�ۿ��� ���� �����ϰ�
        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stats = statOverrides.ToDictionary(s => s.Stat.statName, s => s.CreateStat());
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, $"Stat: GetStat - stat can not be null");
            return _stats.GetValueOrDefault(stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, $"Stats: TryGetStat - stat cannot be null");
            outStat = _stats.GetValueOrDefault(stat.statName);
            return outStat != null;
        }

        public void SetBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue = value;
        public float GetBaseValue(StatSO stat) => GetStat(stat).BaseValue;
        public void IncreaseBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue += value;

        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddModifier(key, value);

        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllStatModifier()
        {
            foreach (StatSO stat in _stats.Values)
            {
                stat.ClearModifier();
            }
        }
    }
}