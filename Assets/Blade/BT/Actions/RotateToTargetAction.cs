using System;
using Blade.Enemies;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Blade.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "RotateToTarget", story: "[Movement] rotate to [Target] ", category: "Action", id: "5afdef7a248e1fc964de452a417d9330")]
    public partial class RotateToTargetAction : Action
    {
        //NavMovement를 받아서 Target을 향해서 회전을 하다가 회전이 다 끝나면 나가도록 
        //그러면 이제 Self대신 NavMovement 그리고 Second는 이제 필요없다. 
        //회전은 대략 5f 도 정도 안으로 들어오면 완료로 처리한다.
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Transform> Self;

        protected override Status OnUpdate()
        {
            if(LookTargetSmoothly()) 
                return Status.Success;
            
            return Status.Running;
        }

        private bool LookTargetSmoothly()
        {
            Quaternion targetRot = Movement.Value.LookAtTarget(Target.Value.position); //회전을 하겠지.
            const float angleThreshold = 5f;
            return Quaternion.Angle(targetRot, Self.Value.rotation) < angleThreshold;
        }

    }
}

