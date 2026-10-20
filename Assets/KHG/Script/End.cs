using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed;
   

    private void Update()
    {
        rectTransform.Translate(Vector2.up * speed * Time.deltaTime);

        if (rectTransform.position.y > 10500f)
        {
            SceneManager.LoadScene("MainUI");
        }
    }
}
