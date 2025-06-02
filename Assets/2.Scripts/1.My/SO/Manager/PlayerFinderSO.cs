using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "PlayerFinder", menuName = "SO/PlayerFinder", order = 0)]
public class PlayerFinderSO : ScriptableObject
{
    public Player Target { get; private set; }

    public void SetTarget(Player target)
    {
        Target = target;
    }
}