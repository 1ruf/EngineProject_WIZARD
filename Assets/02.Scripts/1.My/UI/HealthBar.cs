using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BarUI
{
    [SerializeField] private Image healthBar1;
    [SerializeField] private Image healthBar2;

    public override void DecreaseValue(float value, float maxValue)
    {
        float fillV = value / maxValue;
        healthBar1.fillAmount = fillV;
        healthBar2.DOFillAmount(fillV, 1f);
    }
}
