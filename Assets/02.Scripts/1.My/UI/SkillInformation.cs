using TMPro;
using UnityEngine;

public class SkillInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;

    public void SetInfo(string name, string description)
    {
        skillName.text = $"¿Ã∏ß:{name}";
        skillDescription.text = description;
    }
}
