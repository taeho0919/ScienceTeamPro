using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    [SerializeField] private GameObject settingPanel;

    private bool isPaused;

    public bool IsPaused => isPaused;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Setting Panel이 연결되지 않았습니다!");
        }
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (settingPanel == null)
            return;

        isPaused = true;
        settingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            Time.timeScale = 1f;
            instance = null;
        }
    }
}