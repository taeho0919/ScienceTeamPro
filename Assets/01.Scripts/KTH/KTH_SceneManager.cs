using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KTH_SceneManager : MonoBehaviour
{
    [SerializeField] private SceneSO ss;
    private int i = 0;
    public static KTH_SceneManager Instance;
    
    private void Awake()
    {
        
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void NextSceneGo()
    {
        StartCoroutine(NextScene());
    }
    
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1f);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name !=ss.scene[i].name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(ss.scene[i].name);
            i++;
        }
        else
        {
            i++;
            UnityEngine.SceneManagement.SceneManager.LoadScene(ss.scene[i].name);
        }
    }
}
