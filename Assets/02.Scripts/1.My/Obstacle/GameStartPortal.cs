using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPortal : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene("GameScene");
    }
}
