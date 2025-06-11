using UnityEngine;

public abstract class BarUI : MonoBehaviour
{
    public abstract void DecreaseValue(float value,float maxValue);
    public abstract void IncreaseValue(float value, float maxValue);
}
