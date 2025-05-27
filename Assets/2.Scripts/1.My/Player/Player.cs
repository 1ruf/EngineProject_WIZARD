using Players;
using System;
using Unity.Behavior.GraphFramework;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerInputSO Input { get; private set; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public bool CanMove { get; set; } = true;

    public Transform camTarget;
    private void Awake()
    {
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
}
