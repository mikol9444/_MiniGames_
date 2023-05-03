using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SceneMan))]
public class SceneManEditor : Editor
{
    private SerializedProperty sceneName;
    SceneMan animator;
    private void OnEnable()
    {
        animator = (SceneMan)target;
        sceneName = serializedObject.FindProperty("sceneName");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reload Scene"))
        {
            SceneMan.Instance.ReloadScene();
        }
        if (GUILayout.Button("Next Scene"))
        {
            SceneMan.Instance.NextScene();
        }
        if (GUILayout.Button("Previous Scene"))
        {
            SceneMan.Instance.PreviousScene();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sceneName);
        if (GUILayout.Button("Load Scene"))
        {
            SceneMan.Instance.LoadScene(animator.sceneName);
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
