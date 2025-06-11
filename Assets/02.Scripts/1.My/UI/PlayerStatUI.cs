using Core.Events;
using Core;
using UnityEngine;
using static UnityEngine.UI.ContentSizeFitter;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private EventChannelSO uiEvent;

    [SerializeField] private BarUI healthBar;
    [SerializeField] private BarUI manaBar;

    private float _maxHp;
    private float _maxMp;

    private float _curHp;
    private float _curMp;

    private void Awake()
    {
        uiEvent.AddListener<PlayerStatEvent>(HandlePlayerStat);
    }
    private void OnDestroy()
    {
        uiEvent.RemoveListener<PlayerStatEvent>(HandlePlayerStat);
    }
    private void HandlePlayerStat(PlayerStatEvent callback)
    {
        float hp = callback.Hp;
        float mp = callback.Mp;

        _maxHp = callback.MaxHp;
        _maxMp = callback.MaxMp;

        if (hp < _curMp) SetHp(hp);
        else SetHp(hp);
        if (mp < _curMp) SetMp(mp);
        else SetMp(mp);

        _curHp = hp;
        _curMp = mp;
    }

    private void SetMp(float mp,bool setMode = false)
    {   
        if(setMode)
        {
            manaBar.IncreaseValue(mp, _maxMp);
            return;
        }
        manaBar.DecreaseValue(mp, _maxMp);
    }
    private void SetHp(float hp, bool setMode = false)
    {
        if (setMode)
        {
            manaBar.IncreaseValue(hp, _maxMp);
            return;
        }
        healthBar.DecreaseValue(hp,_maxHp);
    }
}
