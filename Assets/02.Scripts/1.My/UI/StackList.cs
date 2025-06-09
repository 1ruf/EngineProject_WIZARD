using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackList : MonoBehaviour
{
    [SerializeField] private List<Transform> outlines;

    private void OnEnable()
    {
        foreach (Transform childTrm in outlines)
        {
            SetUi(childTrm, false);
        }
    }
    public void SetEnable(int index)
    {
        SetUi(outlines[index], true);
        foreach (Transform childTrm in outlines)
        { 
            if (childTrm != outlines[index])
            {
                SetUi(childTrm, false);
            }
        }
    }
    private void SetUi(Transform trm,bool value)
    {
        if (value)
        {
            trm.localScale = new Vector3(1.2f,1.2f,1.2f);
            trm.GetComponent<Image>().DOColor(Color.green, 0.05f);
        }
        else
        {
            trm.localScale = Vector3.one;
            trm.GetComponent<Image>().DOColor(Color.white, 0.05f);
        }
    }
}
