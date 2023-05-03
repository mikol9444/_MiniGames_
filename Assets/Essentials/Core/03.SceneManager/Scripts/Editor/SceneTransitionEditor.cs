using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SceneTransitionAnimator))]
public class SceneTransitionEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start Transition"))
        {
            SceneTransitionAnimator.StartScene();
        }
        if (GUILayout.Button("End Transition"))
        {
            SceneTransitionAnimator.EndScene();
        }
        GUILayout.EndHorizontal();
    }
}
