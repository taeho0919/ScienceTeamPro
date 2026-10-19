using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_SceneNext : MonoBehaviour
{
    [SerializeField]private string sceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
