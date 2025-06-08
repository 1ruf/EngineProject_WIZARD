using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextSceneName;

    [SerializeField] private TextMeshProUGUI loadingText;

    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        loadingText.text = $"Loading... {0}%";
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = false;

        float time = 0f;

        float loadingPercent = 0f;
        while (true)
        {
            Debug.Log(operation.progress);
            yield return null;
            loadingText.text = $"Loading... {loadingPercent}%";

            if (operation.progress < 0.9f)
            {
                loadingPercent = operation.progress * 100f;
            }
            else
            {
                time += Time.unscaledDeltaTime;
                loadingPercent = 99f;
                if (time >= 1f)
                {
                    operation.allowSceneActivation = true;
                    break;
                }
            }
        }
        loadingText.text = $"Loading... {100}%";
    }
}
