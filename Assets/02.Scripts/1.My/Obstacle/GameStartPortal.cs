using Core;
using Core.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPortal : MonoBehaviour, IInteractable
{
    [SerializeField] private EventChannelSO sceneChannel;
    public void Interact(Player player)
    {
        player.CanMove = false;
        player.GetCompo<PlayerCamera>().SceneChange(true);
        StartCoroutine(SceneChange());
    }
    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(0.3f);

        SceneChangeEvent evt = SceneEvent.SceneChangeEvent;
        evt.SceneName = "GameScene";
        sceneChannel.InvokeEvent(evt);
    }
}
