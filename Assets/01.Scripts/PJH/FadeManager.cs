using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;
    
    [Header("시간")]
    [SerializeField] private float fadeDuration;

    

    public void FadeIn()
    {
        fadePanel.DOFade(1f, fadeDuration);
        fadePanel.blocksRaycasts = true;
    }

    public void FadeOut()
    {
        fadePanel.DOFade(0f, fadeDuration);
        fadePanel.blocksRaycasts = false;
    }

    private IEnumerator FadeRoutine()
    {
        FadeIn();
        yield return new WaitForSeconds(5);
        FadeOut();
    }
}
