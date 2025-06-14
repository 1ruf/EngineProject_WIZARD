using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ManaBar : BarUI
{
    [SerializeField] private Image healthBar1;

    public override void DecreaseValue(float value, float maxValue)
    {
        float fillV = value / maxValue;
        healthBar1.fillAmount = fillV;
    }

    public override void IncreaseValue(float value, float maxValue)
    {
        float fillV = value / maxValue;
        healthBar1.fillAmount = fillV;
    }
}
