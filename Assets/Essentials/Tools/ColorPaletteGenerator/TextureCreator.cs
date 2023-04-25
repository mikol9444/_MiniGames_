using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureCreator
{
    public static Texture2D CreateEmpty(int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        Color[] pixels = new Color[resolution * resolution]; // create a new array of colors

        for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.black;
        texture.SetPixels(pixels); // set the pixel data of the texture to the color array
        texture.Apply(); // apply the changes to the texture
        return texture;
    }
    public static Texture2D CreateColorPalette(Texture2D texture, int cellsOnEdgeCount, float minHue, float maxHue, float minSaturation, float maxSaturation, float minValue, float maxValue)
    {
        int cellAmount = cellsOnEdgeCount * cellsOnEdgeCount;
        int cellSize = texture.width / cellsOnEdgeCount;
        Debug.Log($"Cells Amount = {cellAmount}");
        Color[] colors = new Color[cellAmount];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
        }
        for (int i = 0, k = 0; i < cellsOnEdgeCount; i++)
        {
            for (int j = 0; j < cellsOnEdgeCount; j++, k++)
            {
                int startX = i * cellSize;
                int startY = j * cellSize;
                FillArea(texture, colors[k], startX, startY, startX + cellSize, startY + cellSize);
            }
        }
        return texture;
    }

    public static Texture2D FillArea(Texture2D texture, Color color, int startX, int startY, int endX, int endY)
    {
        // Check if texture is null
        if (texture == null)
        {
            Debug.LogError("Texture is null.");
            return null;
        }

        // Check if startX and startY are within texture bounds
        if (startX < 0 || endX > texture.width || startY < 0 || endY > texture.height)
        {
            Debug.LogError("Start coordinates or area size are outside the bounds of the texture.");
            return texture;
        }

        int width = endX - startX;
        int height = endY - startY;

        Color[] colors = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colors[y * width + x] = color;
            }
        }
        texture.SetPixels(startX, startY, width, height, colors);
        texture.Apply();
        return texture;
    }



}
