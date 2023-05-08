using UnityEngine;
using UnityEditor;


//[CustomEditor(typeof(PopUpManager))]
public class PopUpManagerEditor : Editor
{
    //PopUpManager man;
    //private SerializedProperty popupText;
    //private void OnEnable()
    //{
    //    man = (PopUpManager)target;
    //    popupText = serializedObject.FindProperty("popupText");
    //}

    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();
    //    GUILayout.BeginHorizontal();
    //    man.popupText = EditorGUILayout.TextField("My String Field", man.popupText);

    //    if (GUILayout.Button("Trigger First Popup"))
    //    {
    //        man.TogglePopup("gasgas");
    //    }
    //    GUILayout.EndHorizontal();
    //    serializedObject.ApplyModifiedProperties();
    //}
}
