using Players;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private PlayerInputSO _input;
    public void Initialize(Player player)
    {
        _player = player;
        _input = player.Input;
    }
    public void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(_player);
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
