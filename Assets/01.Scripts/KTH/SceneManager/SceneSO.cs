using UnityEngine;

[CreateAssetMenu(fileName = "SceneSO", menuName = "KTH/SceneSO")]
public class SceneSO : ScriptableObject, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    public UnityEditor.SceneAsset[] scene;
#endif
    [HideInInspector] public string[] sceneNames;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (scene == null) return;
        sceneNames = new string[scene.Length];
        for (int i = 0; i < scene.Length; i++)
            sceneNames[i] = scene[i] != null ? scene[i].name : string.Empty;
#endif
    }

    public void OnAfterDeserialize() { }
}
