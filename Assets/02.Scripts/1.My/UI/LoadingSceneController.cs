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
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }


    private void OnEnable()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("nextSceneName is null or empty. Did you forget to call LoadScene()?");
            return;
        }

        StartCoroutine(LoadSceneProcess());
    }


    private IEnumerator LoadSceneProcess()
    {
        loadingText.text = $"Loading... {0}:%";
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = false;

        float time = 0f;

        float loadingPercent = 0f;
        while (true)
        {
            yield return null;
            loadingText.text = $"Loading... {loadingPercent:F0}%";

            if (operation.progress < 0.9f)
            {
                loadingPercent = operation.progress * 100f;
            }
            else
            {
                time += Time.unscaledDeltaTime;
                loadingPercent = 98f;
                if (time >= 1f)
                {
                    operation.allowSceneActivation = true;
                    break;
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        loadingText.text = $"Loading... {100}%";
    }
}
