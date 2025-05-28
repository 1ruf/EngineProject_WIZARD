using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public virtual void Excute(Vector3 targetPos, SkillSO skill) { }
}
