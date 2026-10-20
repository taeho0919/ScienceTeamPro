using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_SceneNext : MonoBehaviour
{
    [SerializeField]private string sceneName;

    public void NextScene()
    {
        if (KTH_SceneManager.Instance != null)
        {
            KTH_SceneManager.Instance.LoadSceneWithFade(sceneName);
            return;
        }

        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.StartCoroutine(LoadWithFade(sceneName));
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private static IEnumerator LoadWithFade(string target)
    {
        yield return FadeManager.Instance.FadeIn().WaitForCompletion();
        yield return SceneManager.LoadSceneAsync(target);
        FadeManager.Instance.FadeOut();
    }
}
