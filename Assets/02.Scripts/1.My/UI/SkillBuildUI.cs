using Core;
using Core.Events;
using System.Collections.Generic;
using UnityEngine;

public class SkillBuildUI : MonoBehaviour
{
    [SerializeField] private GameObject skillBuildUI;

    [SerializeField] private EventChannelSO uiChannel;

    [SerializeField] private List<StackList> stackLists = new();

    private Animator _anim;

    private bool _uiEnabled;
    private int _stackCnt;
    private int _skillCnt;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        uiChannel.AddListener<SkillBuildUiEvent>(OnSkillBuildEvent);

        skillBuildUI.SetActive(false);
        _uiEnabled = false;
    }
    private void OnDestroy()
    {
        uiChannel.RemoveListener<SkillBuildUiEvent>(OnSkillBuildEvent);
    }

    private void OnSkillBuildEvent(SkillBuildUiEvent callback)
    {
        _skillCnt = callback.SkillNum;
        _stackCnt = callback.StackCount;
        _anim.SetInteger("StackCount", _stackCnt);
        if (callback.StackCount >= 3)
        {
            SetBuildUI(false);
            return;
        }
        if (_uiEnabled == false) SetBuildUI(true);

        SetSkillSize(stackLists[_stackCnt]);
    }

    private void SetBuildUI(bool value)
    {
        skillBuildUI.SetActive(value);
        _uiEnabled = value;
    }
    private void SetSkillSize(StackList stack)
    {
        stack.SetEnable(_skillCnt);
    }
}
