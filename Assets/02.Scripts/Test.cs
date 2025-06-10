using Core;
using Core.Events;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EventChannelSO sceneChannel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneChannel.InvokeEvent(new SceneChangeEvent() { SceneName = "LobbyScene" });
        }
    }
}
