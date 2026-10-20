using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Scene Setting")]
    [SerializeField] private string scenenName;

    private void Start()
    { 
        if (settingsPanel != null)
        {  
            settingsPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scenenName);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
         settingsPanel.SetActive(true);
        }   
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
