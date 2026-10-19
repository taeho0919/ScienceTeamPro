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

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ss.scene[i].name)
        {
            i++;
        }
        string sceneName = ss.scene[i].name;
        i++;

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
