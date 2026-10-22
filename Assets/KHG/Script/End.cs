using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed;

    private float endY;
    private bool isEnded;

    private void Start()
    {
        float lowestTop = 0f;
        foreach (RectTransform child in rectTransform)
        {
            float top = child.localPosition.y + child.rect.yMax;
            if (top < lowestTop) lowestTop = top;
        }

        RectTransform parent = rectTransform.parent as RectTransform;
        float halfHeight = parent != null ? parent.rect.height * 0.5f : Screen.height * 0.5f;
        endY = halfHeight - lowestTop;
    }

    private void Update()
    {
        if (isEnded) return;

        rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;

        if (rectTransform.anchoredPosition.y > endY)
        {
            isEnded = true;
            SceneManager.LoadScene("MainUI");
        }
    }
}
