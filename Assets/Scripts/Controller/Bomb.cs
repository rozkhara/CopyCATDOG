using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D bombCollider;

    // The size of the box area to check for player colliders
    public Vector2 InnerBox;
    public Vector2 BoxCenter;

    private void Start()
    {
        bombCollider = GetComponent<Collider2D>();
        bombCollider.enabled = true; // Disable bomb collider when spawned
        BoxCenter = (Vector2)transform.position + bombCollider.offset;
        InnerBox = Vector2.Scale(((BoxCollider2D)bombCollider).size, transform.lossyScale);
    }

    private void OnDrawGizmos()
    {
        // Draw the box area in the Scene view for visual reference
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(BoxCenter, InnerBox);
    }
}
