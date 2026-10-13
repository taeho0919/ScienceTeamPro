using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSO", menuName = "KTH/SceneSO")]
public class SceneSO : ScriptableObject
{
    public SceneAsset[] scene;
}
