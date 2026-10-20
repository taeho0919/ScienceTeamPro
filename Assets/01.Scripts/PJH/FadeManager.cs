using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;
    
    [Header("시간")]
    [SerializeField] private float fadeDuration;

    public static FadeManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        fadePanel.alpha = 0f;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeRoutine());
    }

    public Tween FadeIn()
    {
        fadePanel.blocksRaycasts = true;
        return fadePanel.DOFade(1f, fadeDuration);
    }

    public Tween FadeOut()
    {
        fadePanel.alpha = 1f;
        fadePanel.blocksRaycasts = false;
        return fadePanel.DOFade(0f, fadeDuration);
    }

    public Tween FadeOutFromBlack()
    {
        fadePanel.alpha = 1f;
        return FadeOut();
    }

    private IEnumerator FadeRoutine()
    {
        FadeIn();
        yield return new WaitForSeconds(3);
        FadeOut();
    }
}
