using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _02CloudsMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public string textureName = "_MainTex";

    private Material material;
    private Vector2 offset;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        offset = material.GetTextureOffset(textureName);
    }

    private void Update()
    {
        offset += new Vector2(Time.deltaTime * speed, 0f);
        material.SetTextureOffset(textureName, offset);
    }
}
