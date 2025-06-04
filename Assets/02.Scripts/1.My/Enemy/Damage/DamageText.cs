using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp;

    private void Update()
    {
        Camera mainCam = Camera.main;
        transform.rotation = Quaternion.LookRotation(mainCam.transform.forward);
    }

    public void SetText(string text,Action endAction = null)
    {
        tmp.text = text;
        StartCoroutine(TextDestroy(1f,endAction));
    }

    private IEnumerator TextDestroy(float time, Action endAction)
    {
        yield return new WaitForSeconds(time);
        tmp.DOFade(0, 1f).OnComplete(() => endAction?.Invoke());
    }
}
