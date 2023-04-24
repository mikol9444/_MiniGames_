using UnityEditor;
using UnityEngine;
using System.IO;
namespace Essentials.Tools
{
    public class PaletteCreator : EditorWindow
    {
        private string fileName = "myTexture.png";
        private int textureSize = 32;
        private int gridSize = 4;
        private float randomValue;
        private float hueShift;
        private Color textureColor = Color.blue;
        private Texture2D previewTexture;
        private bool updateWindow;

        [MenuItem("Tools/PaletteCreator")]
        public static void ShowWindow()
        {
            GetWindow<PaletteCreator>("PaletteCreator");
        }
        void OnInspectorUpdate()
        {
            if (updateWindow)
            {
                previewTexture = TextureGenerator.CreateTexture(textureColor, textureSize, gridSize, hueShift);
                Repaint();
            }
        }
        private void OnGUI()
        {
            GUILayout.Label("Texture Settings", EditorStyles.boldLabel);
            // Show sliders for texture size, grid size, random value, and hue shift
            textureSize = EditorGUILayout.IntSlider("Texture Size", textureSize, 1, 128);
            gridSize = EditorGUILayout.IntSlider("Grid Size", gridSize, 1, 16);
            randomValue = EditorGUILayout.Slider("Random Value", randomValue, 0f, 1f);
            hueShift = EditorGUILayout.Slider("Hue Shift", hueShift, 0f, 1f);
            textureColor = EditorGUILayout.ColorField("Texture Color", textureColor);
            updateWindow = EditorGUILayout.Toggle("updateWindow", updateWindow);


            GUILayout.Space(20);



            GUILayout.Space(20);

            GUILayout.Label("Save Settings", EditorStyles.boldLabel);
            fileName = EditorGUILayout.TextField("File Name", fileName);

            if (GUILayout.Button("Generate Texture"))
            {
                // previewTexture = TextureGenerator.CreateTexture(textureColor, 64, 8, hueShift);
                previewTexture = Gen.CreateTexture(textureSize, textureColor);
                //previewTexture = TextureGenerator.SplitTextureInGrid(previewTexture, gridSize);

            }
            if (GUILayout.Button("Create Color Palette Static"))
            {
                previewTexture = Gen.ColorPalette(previewTexture, textureColor, gridSize);
            }
            if (GUILayout.Button("Create Color Palette with startColor"))
            {
                previewTexture = Gen.DivideTextureIntoQuadrants(previewTexture, textureColor, gridSize);
            }
            if (GUILayout.Button("Create Color Palette Random"))
            {
                previewTexture = Gen.DivideTextureIntoQuadrants(previewTexture, textureColor, gridSize, randomValue);
            }



            if (GUILayout.Button("Save Texture"))
            {
                string directory = EditorUtility.SaveFolderPanel("Save Texture", "Assets/Essentials/Tools/Generated", "");
                if (!string.IsNullOrEmpty(directory))
                {
                    string filePath = directory + "/" + fileName;
                    SaveTextureAsPNG(previewTexture, filePath);
                }
            }
            GUILayout.Label("Preview", EditorStyles.boldLabel);
            if (previewTexture != null)
            {
                GUILayout.Box(previewTexture);
            }

        }

        private void SaveTextureAsPNG(Texture2D texture, string filePath)
        {
            byte[] bytes = texture.EncodeToPNG();

            File.WriteAllBytes(filePath, bytes);
            AssetDatabase.Refresh();
            Debug.Log("Saved texture to " + filePath);
        }
    }
    public static class Gen
    {
        public static Texture2D CreateTexture(int gridSize, Color textureColor)
        {
            Texture2D texture = new Texture2D(gridSize, gridSize);
            Color[] pixels = new Color[gridSize * gridSize];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = textureColor;
            }
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        public static Texture2D ColorPalette(Texture2D texture, Color textureColor, int gridSize)
        {
            int numQuadrants = gridSize * gridSize;
            int quadrantSize = texture.width / gridSize; // assuming texture is gridSizexgridSize quadrants

            for (int i = 0; i < numQuadrants; i++)
            {
                // Determine the quadrant position based on the index
                int x = (i % gridSize) * quadrantSize;
                int y = (i / gridSize) * quadrantSize;

                // Create a new texture for this quadrant
                Texture2D quadrant = new Texture2D(quadrantSize, quadrantSize);

                // Set the color data for this quadrant with slightly different colors
                Color[] colors = new Color[quadrantSize * quadrantSize];
                for (int j = 0; j < colors.Length; j++)
                {
                    // Vary the color of each pixel slightly using the index of the quadrant
                    colors[j] = new Color(0.5f + 0.05f * (i % gridSize), 0.5f + 0.05f * (i / gridSize), 0.5f, 0f) + new Color(textureColor.r / 2, textureColor.g / 2, textureColor.b / 2, textureColor.a);

                }
                quadrant.SetPixels(colors);
                quadrant.Apply();

                // Apply the quadrant to the input texture
                texture.SetPixels(x, y, quadrantSize, quadrantSize, colors);
                texture.Apply();

                // Cleanup
                //Destroy(quadrant);
            }
            return texture;
        }
        public static Texture2D DivideTextureIntoQuadrants(Texture2D texture, Color textureColor, int gridSize)
        {
            int numQuadrants = gridSize * gridSize;
            int quadrantSize = texture.width / gridSize;

            for (int i = 0; i < numQuadrants; i++)
            {
                // Determine the quadrant position based on the index
                int x = (i % gridSize) * quadrantSize;
                int y = (i / gridSize) * quadrantSize;

                // Generate a random color for this quadrant
                float colorRange = 0.2f;
                float r = Random.Range(0, colorRange);
                float g = Random.Range(0, colorRange);
                float b = Random.Range(0, colorRange);
                Color quadrantColor = new Color(r, g, b, 0f) + new Color(textureColor.r, textureColor.g, textureColor.b) * 0.15f;

                // Create a new texture for this quadrant
                Texture2D quadrant = new Texture2D(quadrantSize, quadrantSize);

                // Set the color data for this quadrant with slightly different shades
                Color[] colors = new Color[quadrantSize * quadrantSize];
                for (int j = 0; j < colors.Length; j++)
                {
                    // Vary the shade of each pixel slightly using the index of the quadrant and the quadrant color
                    colors[j] = new Color(quadrantColor.r + (i % gridSize) * 0.05f, quadrantColor.g + (i / gridSize) * 0.05f, quadrantColor.b, 1.0f) + textureColor;
                }
                quadrant.SetPixels(colors);
                quadrant.Apply();

                // Apply the quadrant to the input texture
                texture.SetPixels(x, y, quadrantSize, quadrantSize, colors);
                texture.Apply();

                // Cleanup
                //Destroy(quadrant);
            }
            return texture;
        }

        public static Texture2D DivideTextureIntoQuadrants(Texture2D texture, Color textureColor, int gridSize, float value = 0.1f)
        {
            int numQuadrants = gridSize * gridSize;
            int quadrantSize = texture.width / gridSize; // assuming texture is gridSizexgridSize quadrants

            for (int i = 0; i < numQuadrants; i++)
            {
                // Determine the quadrant position based on the index
                int x = (i % gridSize) * quadrantSize;
                int y = (i / gridSize) * quadrantSize;

                // Generate a random color for this quadrant
                float r = Random.Range(-value, value);
                float g = Random.Range(-value, value);
                float b = Random.Range(-value, value);
                Color quadrantColor = new Color(r, g, b, textureColor.a) + new Color(textureColor.r, textureColor.g, textureColor.b, 0f);

                // Create a new texture for this quadrant
                Texture2D quadrant = new Texture2D(quadrantSize, quadrantSize);

                // Set the color data for this quadrant
                Color[] colors = new Color[quadrantSize * quadrantSize];
                for (int j = 0; j < colors.Length; j++)
                {
                    colors[j] = quadrantColor;
                }
                quadrant.SetPixels(colors);
                quadrant.Apply();

                // Apply the quadrant to the input texture
                texture.SetPixels(x, y, quadrantSize, quadrantSize, colors);
                texture.Apply();

                // Cleanup
                //Destroy(quadrant);
            }
            return texture;
        }



    }

}
