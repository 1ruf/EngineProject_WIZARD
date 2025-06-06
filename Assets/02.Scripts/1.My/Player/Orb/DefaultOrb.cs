using DG.Tweening;
using UnityEngine;

public class DefaultOrb : MonoBehaviour
{
    [SerializeField] private Transform particle;
    private void OnEnable()
    {
        ScaleUp(transform);
        ScaleUp(particle);
    }

    private void ScaleUp(Transform target)
    {
        target.localScale = Vector3.zero;
        target.DOScale(1, 1);
    }
}
