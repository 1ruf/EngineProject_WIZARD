using Assets.Blade.Enemies;
using Blade.Entities;
using UnityEngine;

namespace Blade.Enemies
{
    public class RagDollCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform ragDollParentTrm;
        [SerializeField] private LayerMask whatIsBody;

        //private List<RagdollPart> _partList;
        private Collider[] _results;

        private RagdollPart _defaultPart;

        private ActionData _actionData;

        public void Initialize(Entity entity)
        {
            _actionData = entity.GetCompo<ActionData>();
            _results = new Collider[1]; //길이 하나짜리 충돌 배열
            //_partList = ragDollParentTrm.GetComponentsInChildren<RagdollPart>().ToList();
            //foreach (RagdollPart part in _partList)
            //{
            //    part.Initialize(); //각 파츠들 초기화
            //}
            //Debug.Assert(_partList.Count > 0, $"No ragdoll part found in {gameObject.name}");
            //_defaultPart = _partList[0]; //기본 파츠
            //SetRagDollActive(false);
            //SetColliderActive(false);

            entity.OnDeathEvent.AddListener(HandleDeathEvent);
        }

        private void HandleDeathEvent()
        {
            //SetColliderActive(true);
            //SetRagDollActive(true);
            const float force = 30f;
            //AddForceToRagDoll(_actionData.HitNormal * force, _actionData.HitPoint);
        }

        //private void SetRagDollActive(bool isActive)
        //{
        //    foreach (RagdollPart part in _partList)
        //    {
        //        part.SetRagDollActive(isActive);
        //    }
        //}

        //private void SetColliderActive(bool isActive)
        //{
        //    foreach (RagdollPart part in _partList)
        //    {
        //        part.SetCollider(isActive);
        //    }
        //}

        public void AddForceToRagDoll(Vector3 force, Vector3 point)
        {
            int count = Physics.OverlapSphereNonAlloc(point, 0.5f, _results, whatIsBody);
            if (count > 0)
            {
                _results[0].GetComponent<RagdollPart>().KnockBack(force, point);
            }
            else
            {
                _defaultPart.KnockBack(force, point);
            }

        }
    }
}