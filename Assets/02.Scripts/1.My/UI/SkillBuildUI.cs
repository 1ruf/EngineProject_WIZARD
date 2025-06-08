using Core;
using Core.Events;
using System;
using Unity.Behavior.GraphFramework;
using UnityEngine;

public class SkillBuildUI : MonoBehaviour
{
    [SerializeField] private GameObject skillBuildUI;

    [SerializeField] private EventChannelSO uiChannel;
    private Animator _anim;

    private bool _uiEnabled;
    private int _stackCnt;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        uiChannel.AddListener<SkillBuildEvent>(OnSkillBuildEvent);

        skillBuildUI.SetActive(false);
        _uiEnabled = false;
    }
    private void OnDestroy()
    {
        uiChannel.RemoveListener<SkillBuildEvent>(OnSkillBuildEvent);
    }

    private void OnSkillBuildEvent(SkillBuildEvent callback)
    {
        callback.StackCount = _stackCnt;
        if (callback.StackCount > 3) 
        {
            SetBuildUI(false);
            return;
        }
        if (_uiEnabled == false) SetBuildUI(true);
        _anim.SetInteger("StackCount", _stackCnt);
    }

    private void SetBuildUI(bool value)
    {
        skillBuildUI.SetActive(value);
        _uiEnabled = value;
    }
}
