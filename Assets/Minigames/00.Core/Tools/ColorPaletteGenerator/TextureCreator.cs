using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct TextureStruct
{
    public Texture2D texture;
    public int cellsOnEdgeCount;
    public float minHue;
    public float maxHue;
    public float minSaturation;
    public float maxSaturation;
    public float minValue;
    public float maxValue;

    public TextureStruct(Texture2D texture, int cellsOnEdgeCount, float minHue, float maxHue, float minSaturation, float maxSaturation, float minValue, float maxValue)
    {
        this.texture = texture;
        this.cellsOnEdgeCount = cellsOnEdgeCount;
        this.minHue = minHue;
        this.maxHue = maxHue;
        this.minSaturation = minSaturation;
        this.maxSaturation = maxSaturation;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
}
public static class TextureCreator
{

    public static Texture2D GenerateColorPalette(TextureStruct txtStruct)
    {
        int cellAmount = txtStruct.cellsOnEdgeCount * txtStruct.cellsOnEdgeCount;
        int cellSize = txtStruct.texture.width / txtStruct.cellsOnEdgeCount;
        Debug.Log($"Cells Amount = {cellAmount}");
        Color[] colors = new Color[cellAmount];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Random.ColorHSV(txtStruct.minHue, txtStruct.maxHue, txtStruct.minSaturation, txtStruct.maxSaturation, txtStruct.minValue, txtStruct.maxValue);
        }
        for (int i = 0, k = 0; i < txtStruct.cellsOnEdgeCount; i++)
        {
            for (int j = 0; j < txtStruct.cellsOnEdgeCount; j++, k++)
            {
                int startX = i * cellSize;
                int startY = j * cellSize;
                FillArea(txtStruct.texture, colors[k], startX, startY, startX + cellSize, startY + cellSize);
            }
        }
        return txtStruct.texture;
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
