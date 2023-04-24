using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D CreateTexture(Color baseColor, int gridSize, int colorCount, float range)
    {
        int width = gridSize * colorCount;
        Texture2D texture2D = new Texture2D(width, width);
        Color[] pixels = new Color[width * width];

        float hueStep = range / colorCount; // calculate the step size for hue shift

        float h, s, v;
        //baseColor += new Color(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        Color.RGBToHSV(baseColor, out h, out s, out v); // convert baseColor to HSV color space

        for (int row = 0; row < width; row++)
        {
            int colorIndex = row / gridSize; // calculate the current color index based on the row

            for (int col = 0; col < width; col++)
            {
                // calculate the current pixel's color
                float hue = (colorIndex * hueStep + col / gridSize * hueStep) % range;
                Color currentColor = Color.HSVToRGB(hue, s, v);

                // Set the color of the current pixel
                pixels[row * width + col] = currentColor;
            }

        }

        texture2D.SetPixels(pixels);
        texture2D.Apply();
        return texture2D;
    }
    public static Color RandomColor(float value) => new Color(Random.Range(-value, value), Random.Range(-value, value), Random.Range(-value, value));


}
