using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneButtons : MonoBehaviour
{
    [SerializeField] private Image blocker;
    private void OnEnable()
    {
        blocker.DOFade(0, 0.5f);
    }
    public void ExitPressed()
    {
        Application.Quit();
    }

    public void StartPressed()
    {
        blocker.DOFade(1, 0.5f).OnComplete(() => SceneManager.LoadScene("LobbyScene"));
    }
}
