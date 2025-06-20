using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp;

    private void Update()
    {
        Camera mainCam = Camera.main;
        transform.rotation = Quaternion.LookRotation(mainCam.transform.forward);
    }

    public void SetText(string text, Action endAction = null)
    {
        tmp.text = text;
        tmp.rectTransform.DOScale(new Vector3(2f, 2f, 2f), 0.15f).OnComplete(() => tmp.rectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.15f));
        tmp.rectTransform.DOAnchorPosY(tmp.rectTransform.anchoredPosition.y + 1f, 2f);
        StartCoroutine(TextDestroy(1f, endAction));
    }

    private IEnumerator TextDestroy(float time, Action endAction)
    {
        yield return new WaitForSeconds(time);
        tmp.DOFade(0, 1f).OnComplete(() => endAction?.Invoke());
    }
}
