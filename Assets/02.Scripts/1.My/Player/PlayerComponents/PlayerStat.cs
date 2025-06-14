using Core;
using Core.Events;
using NUnit.Framework.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerStat : MonoBehaviour, IPlayerComponent
{
    private EventChannelSO cameraChannel;
    private Volume impactVolume;

    private Player _player;

    private float _hp;
    private float _mp;

    private void Awake()
    {
        impactVolume = GetComponentInChildren<Volume>();
    }
    public void Initialize(Player player)
    {
        cameraChannel = player.CameraChannel;
        _player = player;

        _hp = player.Hp;
        _mp = player.Mp;

        StatApply();
        player.OnPlayerDeath += PlayerDied;
    }

    public void Damage(float damage,bool useImpact = true)
    {
        _hp = _player.Hp - damage;
        if(useImpact == true) SetImpact(damage);
        StatApply();
    }
    public void ManaUse(float mana)
    {
        _mp = _player.Mp - mana;
        StatApply();
    }

    private void SetImpact(float dmg)
    {
        CameraShakeEvent evt = Core.Events.CameraEvent.CameraShakeEvent;
        evt.Power = dmg/15;
        cameraChannel.InvokeEvent(evt);

        impactVolume.weight = dmg / 20;
        StartCoroutine(DecreaseImpact());
    }

    private void StatApply()
    {
        _player.SetPlayerStat((int)_hp, (int)_mp);
    }

    private void PlayerDied()
    {
        _player.enabled = false;
    }
    private IEnumerator DecreaseImpact()
    {
        while (true)
        {
            if (impactVolume.weight < 0.035f)
            {
                impactVolume.weight = 0;
                break;
            }
            impactVolume.weight -= 0.01f;
            yield return new WaitForSeconds(0.015f);
        }
    }
}
