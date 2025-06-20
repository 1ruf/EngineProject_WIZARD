using System;
using UnityEngine;

namespace Blade.Combat
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/Combat/AttackData", order = 0)]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public float movementPower;
        public float damageMultiplier = 1f; //증가 뎀
        public float damageIncrease = 0;  //추가 뎀
        public bool isPowerAttack;
        public float knockBackForce;
        public float knockBackDuration;
        private void OnEnable()
        {
            attackName = this.name; //파일 이름으로 공격 이름을 설정한다.
        }
    }
}