using Players;
using TMPro;
using UnityEngine;

public class GoongDamager : MonoBehaviour
{
    [SerializeField] private PlayerInputSO input;
    [SerializeField] private SkillSO skillData;
    private SummonSkill skill;

    private void Awake()
    {
        skill = GetComponent<SummonSkill>();
    }
    
    private void OnEnable()
    {
        skill.Active(skillData, Vector3.zero, input.GetWorldPosition() + new Vector3(0,3.5f,0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, skillData.Range);
    }
}
