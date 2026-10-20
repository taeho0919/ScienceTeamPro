using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;

    [Header("시간")]
    [SerializeField] private float fadeDuration;

    public static FadeManager Instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateIfMissing()
    {
        if (Instance != null) return;

        GameObject root = new GameObject("FadeManager");
        root.SetActive(false);

        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        GameObject panel = new GameObject("FadePanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(CanvasGroup));
        panel.transform.SetParent(root.transform, false);
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        panel.GetComponent<Image>().color = Color.black;

        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.blocksRaycasts = false;

        FadeManager manager = root.AddComponent<FadeManager>();
        manager.fadePanel = group;
        manager.fadeDuration = 1f;
        root.SetActive(true);
    }

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
