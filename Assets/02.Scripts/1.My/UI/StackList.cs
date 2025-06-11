using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackList : MonoBehaviour
{
    [SerializeField] private List<Transform> outlines;
    [SerializeField] private Skills skills;
    [SerializeField] private int idx;

    private void OnEnable()
    {
        foreach (Transform childTrm in outlines)
        {
            SetUi(childTrm, false);
        }
    }
    public void SetEnable(int index,SkillBuildUI buildUI)
    {
        SetUi(outlines[index], true);
        foreach (Transform childTrm in outlines)
        { 
            if (childTrm != outlines[index])
            {
                SetUi(childTrm, false);
                SetDesc(index, buildUI);
            }
        }
    }
    private void SetDesc(int index,SkillBuildUI buildUI)
    {
        switch (index)
        {
            case 0:
                SkillRangeSO range = skills.GetSkill<SkillRangeSO>(idx);
                buildUI.SetInfo(range.name, range.Description);
                break;
            case 1:
                SkillAttributeSO attribute = skills.GetSkill<SkillAttributeSO>(idx);
                buildUI.SetInfo(attribute.name, attribute.Description);
                break;
            case 2:
                SkillTypeSO type = skills.GetSkill<SkillTypeSO>(idx);
                buildUI.SetInfo(type.name, type.Description);
                break;
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
