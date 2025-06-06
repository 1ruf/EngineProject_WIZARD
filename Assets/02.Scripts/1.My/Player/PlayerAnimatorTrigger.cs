using System;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour, IPlayerComponent
{
    public event Action OnAnimationEnd;
    public event Action OnSpellActiveeMotion;
    public event Action OnDefaultOrbSpawn;
    public event Action OnDefaultOrbRemove;

    private void AnimationOver() => OnAnimationEnd?.Invoke();
    private void SpellActive() => OnSpellActiveeMotion?.Invoke();
    private void SpawnDefaultOrb() => OnDefaultOrbSpawn?.Invoke();
    private void RemoveDefaultOrb() => OnDefaultOrbRemove?.Invoke();

    public void Initialize(Player player) { }
}
