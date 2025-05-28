using Players;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior.GraphFramework;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerInputSO Input { get; private set; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public bool CanMove { get; set; } = true;

    public Dictionary<Type, IPlayerComponent> _components;

    public Transform camTarget;
    public SkillSO CurrentSkill;
    private void Awake()
    {
        _components = new Dictionary<Type, IPlayerComponent>();
        InitComponents();
    }

    private void InitComponents()
    {
        foreach (Transform compo in transform)
        {
            if(compo.TryGetComponent(out IPlayerComponent playerCompo))
            {
                playerCompo.Initialize(this);
            }
        }
    }
    public virtual void InitializeComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }
    public T GetCompo<T>() where T : IPlayerComponent
           => (T)_components.GetValueOrDefault(typeof(T));

    public IPlayerComponent GetCompo(Type type)
        => _components.GetValueOrDefault(type);
}
