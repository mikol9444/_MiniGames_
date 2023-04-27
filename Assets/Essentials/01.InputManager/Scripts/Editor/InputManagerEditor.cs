using UnityEngine;
using UnityEditor;

namespace Essentials
{

    [CustomEditor(typeof(ExampleInputListener))]
    public class InputManagerEditor : Editor
    {
        private bool showAllFields = true;

        public override void OnInspectorGUI()
        {
            ExampleInputListener inputManager = (ExampleInputListener)target;

            showAllFields = EditorGUILayout.Foldout(showAllFields, "Input Fields");

            if (showAllFields)
            {

                DrawVector2Field("Vector", ref inputManager.movementVector);
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                DrawButton("Jumping", inputManager.jumping);
                DrawButton("Sprinting", inputManager.sprinting);
                DrawButton("Interacting", inputManager.interacting);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                DrawButton("Button 1", inputManager.button1Pressed);
                DrawButton("Button 2", inputManager.button2Pressed);
                DrawButton("Button 3", inputManager.button3Pressed);
                GUILayout.EndHorizontal();

                DrawButton("Pause", inputManager.pausePressed);


            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawButton(string label, bool value)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.white;
            style.fontStyle = value ? FontStyle.Bold : FontStyle.Normal;
            style.normal.background = value ? Texture2D.whiteTexture : GUI.skin.button.normal.background;

            if (GUILayout.Button(label, style))
            {
                value = !value;
            }
        }

        private void DrawVector2Field(string label, ref Vector2 value)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("X", GUILayout.Width(12));
            value.x = EditorGUILayout.FloatField(value.x);
            GUILayout.Label("Y", GUILayout.Width(12));
            value.y = EditorGUILayout.FloatField(value.y);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }

}