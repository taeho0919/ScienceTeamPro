using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_DontDestroy : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField]private SpriteRenderer backGround;
    [SerializeField] private GameObject[] boss;
    private Transform player;
    private static KTH_DontDestroy instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadScene;   
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndingScene" || scene.name == "DeathScene"||scene.name=="EndCutScene")
        {
            Destroy(gameObject);
            return;
        }

        ApplyStage(scene.name);
    }

    private void Start()
    {
        ApplyStage(SceneManager.GetActiveScene().name);
    }

    private void ApplyStage(string sceneName)
    {
        int stage = -1;
        if (sceneName == "Boss1Scene") stage = 0;
        else if (sceneName == "Boss2Scene") stage = 1;
        else if (sceneName == "Boss3Scene") stage = 2;
        if (stage < 0) return;

        if (stage < backgrounds.Length)
        {
            backGround.sprite = backgrounds[stage];
        }

        for (int b = 0; b < boss.Length; b++)
        {
            boss[b].SetActive(b == stage);
        }
    }
}
