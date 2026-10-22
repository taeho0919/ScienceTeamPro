using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class KTH_SceneManager : MonoBehaviour
{
    [SerializeField] private SceneSO ss;
    private int i = 0;
    public static KTH_SceneManager Instance;
    
    private void Awake()
    {
        
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private bool isLoading;

    public void NextSceneGo()
    {
        if (isLoading) return;

        int current = System.Array.IndexOf(ss.sceneNames, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        i = current >= 0 ? current + 1 : i;
        if (i >= ss.sceneNames.Length) return;

        string sceneName = ss.sceneNames[i];
        StartCoroutine(LoadWithFade(sceneName));
    }

    public void LoadSceneWithFade(string sceneName)
    {
        if (isLoading) return;
        StartCoroutine(LoadWithFade(sceneName));
    }

    private IEnumerator LoadWithFade(string sceneName)
    {
        isLoading = true;

        if (FadeManager.Instance != null)
        {
            yield return FadeManager.Instance.FadeIn().WaitForCompletion();
        }

        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.FadeOut();
        }

        isLoading = false;
    }
}
