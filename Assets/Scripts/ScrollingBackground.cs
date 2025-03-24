using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.07f; // Adjust speed as needed
    private Renderer bgRenderer;

    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        bgRenderer.material.mainTextureOffset = new Vector2(0, -offset); // Moves texture downwards
    }
}
