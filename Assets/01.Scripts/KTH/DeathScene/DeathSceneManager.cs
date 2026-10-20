using DG.Tweening;
using UnityEngine;

public class DeathSceneManager : MonoBehaviour
{
    [SerializeField] private RectTransform textTransform;
    [SerializeField] private RectTransform text2Transform;
    [SerializeField] private CanvasGroup buttonGroup;
    public KTH_PlayerAnimationEnd pae;

    [SerializeField] private float dropHeight = 1000f;
    [SerializeField] private float dropDuration = 1f;
    [SerializeField] private float textInterval = 0.3f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Ease dropEase = Ease.OutBounce;

    private Vector2 textTarget;
    private Vector2 text2Target;
    private Sequence sequence;

    private void Awake()
    {
        textTarget = textTransform.anchoredPosition;
        text2Target = text2Transform.anchoredPosition;

        textTransform.anchoredPosition = textTarget + Vector2.up * dropHeight;
        text2Transform.anchoredPosition = text2Target + Vector2.up * dropHeight;

        buttonGroup.alpha = 0f;
        buttonGroup.interactable = false;
        buttonGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        pae.OnEnd += DeathUi;
    }

    private void OnDisable()
    {
        pae.OnEnd -= DeathUi;
        sequence?.Kill();
    }

    public void DeathUi()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence()
            .Append(textTransform.DOAnchorPos(textTarget, dropDuration).SetEase(dropEase))
            .Insert(textInterval, text2Transform.DOAnchorPos(text2Target, dropDuration).SetEase(dropEase))
            .Append(buttonGroup.DOFade(1f, fadeDuration))
            .OnComplete(() =>
            {
                buttonGroup.interactable = true;
                buttonGroup.blocksRaycasts = true;
            });
    }
}
