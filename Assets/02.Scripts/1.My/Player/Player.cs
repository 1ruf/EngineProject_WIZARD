using Core;
using Core.Events;
using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int maxMp;
    [field: SerializeField] public PlayerInputSO Input { get; private set; }
    [field: SerializeField] public Transform OrbHandler { get; private set; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public bool CanMove { get; set; } = true;

    public Dictionary<Type, IPlayerComponent> Components;

    public Transform camTarget;

    [Header("Channel")]
    public EventChannelSO UiChannel;
    public EventChannelSO SkillChannel;
    public EventChannelSO CameraChannel;
    private void Awake()
    {
        Components = new Dictionary<Type, IPlayerComponent>();
        InitStat();
        AddComponents();
        InitializeComponents(); 
    }
    //private void Update()
    //{
    //    Mp += (int)Time.time * 1;
    //    SetStatUI();        
    //}
    private void InitStat()
    {
        Hp = maxHp;
        Mp = maxMp;
    }

    public void SetPlayerStat(int hp, int mp)
    {
        this.Hp = hp;
        this.Mp = mp;
        SetStatUI();
    }

    private void SetStatUI()
    {
        PlayerStatEvent evt = UIEvent.PlayerStatEvent;

        evt.MaxHp = maxHp;
        evt.MaxMp = maxMp;

        evt.Hp = this.Hp;
        evt.Mp = this.Mp;

        UiChannel.InvokeEvent(evt);
    }

    protected virtual void AddComponents()
    {
        GetComponentsInChildren<IPlayerComponent>().ToList()
            .ForEach(component => Components.Add(component.GetType(), component));
    }

    protected virtual void InitializeComponents()
    {
        Components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    public T GetCompo<T>() where T : IPlayerComponent
           => (T)Components.GetValueOrDefault(typeof(T));

    public IPlayerComponent GetCompo(Type type)
        => Components.GetValueOrDefault(type);
}
public enum AnimationState
{
    Idle,
    Walk,
    Run,
    Attack
}