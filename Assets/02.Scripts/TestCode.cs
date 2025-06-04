using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCode : MonoBehaviour
{
    [SerializeField] private List<SkillSO> skillList;
    [SerializeField] private PlayerScroll scroll;
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[0]);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[1]);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[2]);
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[3]);
        }
        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[4]);
        }
        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[5]);
        }
        if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[6]);
        }
        if (Keyboard.current.digit8Key.wasPressedThisFrame) 
        {
            scroll.SetSkill(skillList[7]);
        }
        if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            scroll.SetSkill(skillList[8]);
        }
    }
}
