using System;
using UnityEngine;

public class PlayerCollecter : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask collectableLayer;

    private PlayerStat _playerStat;

    public void Initialize(Player player)
    {
        _playerStat = player.GetCompo<PlayerStat>();
    }
    private void Update()
    {
        DetectCollectables();
    }

    private void DetectCollectables()
    {
        Collider[] collectables = Physics.OverlapSphere(transform.position, detectRange, collectableLayer);
        foreach (Collider collider in collectables)
        {
            if (collider.TryGetComponent(out ICollectable collectable))
            {
                SetState(collectable.Collect());
            }
        }
    }

    private void SetState(CrystalSO crystalSO)
    {
        _playerStat.Damage(-crystalSO.RefillHp,false);
        _playerStat.ManaUse(-crystalSO.RefillMana);
    }
}
