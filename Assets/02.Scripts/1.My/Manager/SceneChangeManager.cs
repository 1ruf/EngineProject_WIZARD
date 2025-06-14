using Core;
using Core.Events;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO sceneChannel;
    [SerializeField] private Image blocker;

    private void Awake()
    {
        Time.timeScale = 1f;
        sceneChannel.AddListener<SceneChangeEvent>(OnSceneChange);
        blocker.DOFade(0, 1f).OnComplete(()=>blocker.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        sceneChannel.RemoveListener<SceneChangeEvent>(OnSceneChange);
    }

    private void OnSceneChange(SceneChangeEvent callback)
    {
        Time.timeScale = 1f;
        blocker.gameObject.SetActive(true);
        blocker.DOFade(1, 1f).OnComplete(()=>SceneChange(callback.SceneName));
    }

    private void SceneChange(string sceneName)
    {
        LoadingSceneController.LoadScene(sceneName);
    }
}
