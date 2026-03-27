using UnityEngine;
using System.Collections;

// Generates textures from color maps and height maps for use in terrain generation. The textures are created with specific filtering and wrapping settings to ensure they display correctly in the scene.
public static class TextureGenerator
{
    // Generates a Texture2D from a given color map, width, and height. The texture is set to use point filtering and clamp wrapping mode.
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    // Generates a Texture2D from a given height map. The height values are normalized and mapped to a grayscale color range (black to white) to create the texture.
    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x,y]);
            }
        }
        return TextureFromColourMap(colourMap, width, height);
    }
}
