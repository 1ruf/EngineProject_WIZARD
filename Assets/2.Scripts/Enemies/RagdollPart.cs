using Blade.Combat;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Blade.Enemies
{
    public class RagdollPart : MonoBehaviour
    {
        private Rigidbody _rbCompo;
        private Collider _colliderCompo;

        public void Initialize()
        {
            _rbCompo = GetComponent<Rigidbody>();
            _colliderCompo = GetComponent<Collider>();
        }

        public void SetRagDollActive(bool isActive)
        {
            _rbCompo.isKinematic = !isActive;
        }

        public void SetCollider(bool isActive)
        {
            _colliderCompo.enabled = isActive;
        }

        public void KnockBack(Vector3 force, Vector3 point)
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                _rbCompo.AddForceAtPosition(force, point, ForceMode.Impulse);
            });
        }
    }
}
