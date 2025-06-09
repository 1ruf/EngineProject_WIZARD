using System;
using UnityEngine;

namespace Blade.Core.StatSystem
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSO stat;
        [SerializeField] private bool isUseOverride;
        [SerializeField] private float overrideBaseValue;

        public StatSO Stat => stat;
        public StatOverride(StatSO stat) => this.stat = stat;

        public StatSO CreateStat() //���� ������ �������̵� ���� �־ �������ش�.
        {
            StatSO newStat = stat.Clone() as StatSO;
            Debug.Assert(newStat != null, $"{nameof(newStat)} stat clone failed");

            if (isUseOverride)
            {
                newStat.BaseValue = overrideBaseValue;
            }

            return newStat;
        }
    }
}