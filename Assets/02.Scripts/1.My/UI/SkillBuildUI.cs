using Core;
using Core.Events;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SkillBuildUI : MonoBehaviour
{
    [SerializeField] private GameObject skillBuildUI;

    [SerializeField] private EventChannelSO uiChannel;


    [SerializeField] private SkillInformation skillInformation;

    [SerializeField] private List<StackList> stackLists = new();

    private GameObject skillInfoObj => skillInformation.gameObject;
    private Animator _anim;

    private bool _uiEnabled;
    private int _stackCnt;
    private int _skillCnt;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        uiChannel.AddListener<SkillBuildUiEvent>(OnSkillBuildEvent);

        SetBuildUI(false);
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
            SetInfoUI(false);
            return;
        }
        if (_uiEnabled == false) SetBuildUI(true);

        SetSkillSize(stackLists[_stackCnt]);
    }

    private void SetBuildUI(bool value)
    {
        SetInfoUI(value);
        skillBuildUI.SetActive(value);
        _uiEnabled = value;
    }
    public void SetInfoUI(bool value)
    {
        skillInfoObj.SetActive(value);
    }
    public void SetInfo(string name, string description)
    {
        skillInformation.SetInfo(name, description);
        //SetInfoUI(true);
    }
    private void SetSkillSize(StackList stack)
    {
        stack.SetEnable(_skillCnt,this);
    }
}
