using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPortal : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        player.CanMove = false;
        player.GetCompo<PlayerCamera>().SceneChange(true);
        StartCoroutine(SceneChange());
    }
    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        LoadingSceneController.LoadScene("GameScene");
    }
}
