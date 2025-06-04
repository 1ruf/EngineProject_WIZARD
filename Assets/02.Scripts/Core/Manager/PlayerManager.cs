using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

[DefaultExecutionOrder(-1)]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerFinderSO playerFinder;

    private void Awake()
    {
        playerFinder.SetTarget(player);
    }
}