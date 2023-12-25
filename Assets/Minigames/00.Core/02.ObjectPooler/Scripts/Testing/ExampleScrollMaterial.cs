using UnityEngine;

public class ExampleScrollMaterial : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // adjust the scroll speed in the inspector
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed; // calculate the new offset based on time and scroll speed
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0)); // set the new offset on the material
    }
}
