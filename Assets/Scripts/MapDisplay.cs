using UnityEngine;

// Responsible for displaying a generated noise map as a texture in the scene.
public class MapDisplay : MonoBehaviour
{
    // Renderer component to apply the generated texture to
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    // Draws the provided noise map as a texture on the assigned renderer.
    public void DrawTexture(Texture2D texture)
    {
        // Set the texture to the renderer's material and adjust its scale
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
