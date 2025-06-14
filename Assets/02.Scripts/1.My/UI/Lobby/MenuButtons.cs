using Blade.Entities;
using Core;
using Core.Events;
using DG.Tweening;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private EventChannelSO sceneChannel;
    [SerializeField] private EntityFinderSO playerFinder;

    [SerializeField] private RectTransform libraryUI;

    private bool _libOpen;

    public void BackButtonPressd()
    {
        playerFinder.Target.CanMove = false;

        SceneChangeEvent evt = SceneEvent.SceneChangeEvent;
        evt.SceneName = "TitleScene";

        sceneChannel.InvokeEvent(evt);
    }

    public void LibraryClicked()
    {
        _libOpen = !_libOpen;
        libraryUI.DOAnchorPosX(_libOpen ? 550 : 1500, 0.5f);
    }
}
