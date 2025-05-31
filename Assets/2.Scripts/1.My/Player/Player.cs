using Core;
using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerInputSO Input { get; private set; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public bool CanMove { get; set; } = true;

    public Dictionary<Type, IPlayerComponent> _components;

    public Transform camTarget;
    public SkillSO CurrentSkill;
    public EventChannelSO SkillChannel;
    public EventChannelSO cameraChannel;
    private void Awake()
    {
        _components = new Dictionary<Type, IPlayerComponent>();
        AddComponents();
        InitializeComponents();
    }


    protected virtual void AddComponents()
    {
        GetComponentsInChildren<IPlayerComponent>().ToList()
            .ForEach(component => _components.Add(component.GetType(), component));
    }

    protected virtual void InitializeComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    public T GetCompo<T>() where T : IPlayerComponent
           => (T)_components.GetValueOrDefault(typeof(T));

    public IPlayerComponent GetCompo(Type type)
        => _components.GetValueOrDefault(type);
}
public enum AnimationState
{
    Idle,
    Walk,
    Run,
    Attack
}