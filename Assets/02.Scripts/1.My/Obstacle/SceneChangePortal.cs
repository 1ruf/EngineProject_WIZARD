using Core;
using Core.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangePortal : MonoBehaviour, IInteractable
{
    [SerializeField] private EventChannelSO sceneChannel;
    [SerializeField] private string sceneName = "GameScene";
    public void Interact(Player player)
    {
        Destroy(player);
        player.CanMove = false;
        player.GetCompo<PlayerCamera>().SceneChange(true);
        StartCoroutine(SceneChange());
    }
    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(0.3f);

        SceneChangeEvent evt = SceneEvent.SceneChangeEvent;
        evt.SceneName = sceneName;
        sceneChannel.InvokeEvent(evt);
    }
}
