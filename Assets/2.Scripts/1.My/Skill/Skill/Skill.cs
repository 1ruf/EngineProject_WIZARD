using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public void UseSkill(Vector3 tP)
    {
        Active(Vector3.zero,tP);
    }

    public void UseSkill(Vector3 oP, Vector3 tP)
    {
        Active(oP, tP);
    }
    protected abstract void Active(Vector3 OriginPos, Vector3 targetPos);
}
