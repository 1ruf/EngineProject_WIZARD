using System;
using UnityEngine;

public class PlayerOrb : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject defaultOrbPrefab;
    private Transform orbHandler;
    private Player _player;
    private PlayerAnimatorTrigger _trigger;

    private GameObject _defOrb;

    private bool _isDefaultOrb;
    public void Initialize(Player player)
    {
        _player = player;
        _trigger = _player.GetCompo<PlayerAnimatorTrigger>();
        orbHandler = _player.OrbHandler;

        _trigger.OnDefaultOrbSpawn += HandleDefOrb;
        _trigger.OnDefaultOrbRemove += HandleDefOrbRemove;
    }
    private void OnDestroy()
    {
        _trigger.OnDefaultOrbSpawn -= HandleDefOrb;
        _trigger.OnDefaultOrbRemove -= HandleDefOrbRemove;
    }
    private void HandleDefOrbRemove()
    {
        Destroy(_defOrb);
        _isDefaultOrb = false;
    }
    private void HandleDefOrb()
    {
        if(_isDefaultOrb == true) return;

        _isDefaultOrb = true;
        _defOrb = SpawnOrb(defaultOrbPrefab);
    }

    public GameObject SpawnOrb(GameObject obj)
    {
        GameObject orb = Instantiate(obj,orbHandler);
        orb.transform.position = orbHandler.transform.position;
        return orb;
    }
}
