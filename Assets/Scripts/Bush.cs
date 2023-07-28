using UnityEngine;

public class Bush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider's GameObject has a SpriteRenderer component
        SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Disable the sprite renderer if it has one
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider's GameObject has a SpriteRenderer component
        SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Enable the sprite renderer if it has one
            spriteRenderer.enabled = true;
        }
    }
}
