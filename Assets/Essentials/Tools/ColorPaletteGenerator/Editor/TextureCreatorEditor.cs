using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TextureCreatorEditor : EditorWindow
{
    private string fileName = "myTexture.png";
    private int resolution = 64;
    private int cellCount = 4;
    private float minHue = 0f;
    private float maxHue = 1f;
    private float minSaturation = 0.5f;
    private float maxSaturation = 1f;
    private float minValue = 0.5f;
    private float maxValue = 1f;
    private bool autoUpdate = false;
    private Texture2D previewTexture;

    public TextureStruct textureStruct;

    [MenuItem("Tools/Palette Generator")]
    public static void ShowWindow()
    {
        GetWindow<TextureCreatorEditor>("Palette Generator");
    }
    // void OnInspectorUpdate()
    // {
    //     if (updateWindow)
    //     {
    //         previewTexture = TextureCreator.CreateEmpty(edgeLength);
    //         Repaint();
    //     }
    // }
    private static GUIStyle CreateLabelStyle(int fontSize = default, Color color = default, TextAnchor alignment = default, FontStyle fontStyle = default)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = fontSize;
        style.normal.textColor = color;
        style.alignment = alignment;
        style.fontStyle = fontStyle;
        return style;
    }
    private void OnGUI()
    {
        CheckValue();
        // Define custom styles for the sliders
        GUIStyle sliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
        sliderStyle.fixedHeight = 10f;
        sliderStyle.fixedWidth = 200f;
        sliderStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1f);
        sliderStyle.alignment = TextAnchor.MiddleCenter;
        GUIStyle thumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
        thumbStyle.fixedWidth = 20f;
        thumbStyle.fixedHeight = 20f;
        thumbStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        Color greenColor = new Color(0.5f, 1f, 0.5f);
        Color redColor = new Color(1f, 0.5f, 0.5f);
        if (autoUpdate)
        {
            buttonStyle.normal.textColor = greenColor;
            buttonStyle.active.textColor = greenColor;
        }
        else
        {
            buttonStyle.normal.textColor = redColor;
            buttonStyle.active.textColor = redColor;
        }

        // Set up custom label style
        GUIStyle titleStyle = CreateLabelStyle(16, Color.cyan, TextAnchor.LowerCenter, FontStyle.BoldAndItalic);
        GUIStyle labelStyle = CreateLabelStyle(13, new Color(0f, 1f, 0.5f, 1f), TextAnchor.MiddleLeft, FontStyle.Bold);
        labelStyle.fixedWidth = 150f;

        GUILayout.Label("Palette Generator", titleStyle);
        GUILayout.Space(20);


        fileName = EditorGUILayout.TextField("File Name", fileName);
        resolution = EditorGUILayout.IntSlider(new GUIContent("Resolution", "The resolution of the output texture"), resolution, 1, 1024);

        cellCount = EditorGUILayout.IntSlider(new GUIContent("CellCount", "The number of cells in the output texture"), cellCount, 1, 32);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Min Hue: " + minHue.ToString("F2"), labelStyle);
        minHue = GUILayout.HorizontalSlider(minHue, 0f, maxHue, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Hue: " + maxHue.ToString("F2"), labelStyle);
        maxHue = GUILayout.HorizontalSlider(maxHue, 0f, 1f, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Min Saturation: " + minSaturation.ToString("F2"), labelStyle);
        minSaturation = GUILayout.HorizontalSlider(minSaturation, 0f, 1f, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Saturation: " + maxSaturation.ToString("F2"), labelStyle);
        maxSaturation = GUILayout.HorizontalSlider(maxSaturation, 0f, 1f, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Min Value: " + minValue.ToString("F2"), labelStyle);
        minValue = GUILayout.HorizontalSlider(minValue, 0f, 1f, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Value: " + maxValue.ToString("F2"), labelStyle);
        maxValue = GUILayout.HorizontalSlider(maxValue, 0f, 1f, sliderStyle, thumbStyle);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("AutoUpdate", GUILayout.Width(80f));

        if (GUILayout.Button(autoUpdate ? "On" : "Off", buttonStyle, GUILayout.Width(60f)))
        {
            autoUpdate = !autoUpdate;
        }

        GUILayout.EndHorizontal();
        if (GUILayout.Button("Generate Palette"))
        {
            GeneratePalette();
        }

        if (GUILayout.Button("Save Palette"))
        {
            string directory = EditorUtility.SaveFolderPanel("Save Palette", "Assets/Essentials/Tools/Generated", "");
            if (!string.IsNullOrEmpty(directory))
            {
                string filePath = directory + "/" + fileName;
                SaveTextureAsPNG(previewTexture, filePath);
            }
        }


        GUILayout.Space(25f);

        GUILayout.Label("Preview", EditorStyles.boldLabel);

        if (previewTexture != null)
        {
            Rect previewRect = GUILayoutUtility.GetRect(position.width, position.width);
            previewRect.x += (position.width - previewRect.width) / 2f;
            GUI.DrawTexture(previewRect, previewTexture, ScaleMode.ScaleToFit);
        }

        if (GUI.changed && autoUpdate)
        {
            GeneratePalette();
        }


    }
    private void GeneratePalette()
    {
        previewTexture = new Texture2D(resolution, resolution);
        textureStruct = new TextureStruct(previewTexture, cellCount, minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
        previewTexture = TextureCreator.GenerateColorPalette(textureStruct);
    }




    private void SaveTextureAsPNG(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();

        File.WriteAllBytes(filePath, bytes);
        AssetDatabase.Refresh();
        Debug.Log("Saved texture to " + filePath);
    }

    private void CheckValue()
    {
        resolution = Mathf.ClosestPowerOfTwo(resolution);
        cellCount = Mathf.ClosestPowerOfTwo(cellCount);
        if (minHue > maxHue) minHue = maxHue;
        if (minSaturation > maxSaturation) minSaturation = maxSaturation;
        if (minValue > maxValue) minValue = maxValue;
        // 
    }
}
