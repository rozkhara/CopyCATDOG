using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D bombCollider;
    private bool isInsideInBox;
    private bool colliderEnabled;

    // The size of the box area to check for player colliders
    public Vector2 OutterBox = new Vector2(2.0f, 2.0f);
    public Vector2 InnerBox;
    public Vector2 BoxCenter;

    private void Start()
    {
        bombCollider = GetComponent<Collider2D>();
        bombCollider.enabled = false; // Disable bomb collider when spawned
        BoxCenter = (Vector2)transform.position + bombCollider.offset;
        InnerBox = Vector2.Scale(((BoxCollider2D)bombCollider).size, transform.lossyScale);
        colliderEnabled = false;
    }

    private void FixedUpdate()
    {
        if (!colliderEnabled) // Only check if collider is not enabled yet
        {
            CheckPlayersInsideBoxes();
        }
    }

    private void CheckPlayersInsideBoxes()
    {
        // Find all colliders inside the InnerBox area
        Collider2D[] InnerBoxColliders = Physics2D.OverlapBoxAll(BoxCenter, InnerBox, 0f);
        // Find all colliders inside the OutterBox area
        Collider2D[] OutterBoxColliders = Physics2D.OverlapBoxAll(BoxCenter, OutterBox, 0f);

        bool isInsideInner = false;
        bool isInsideOutter = false;

        // Check if any of the InnerBoxColliders are tagged as players
        foreach (Collider2D collider in InnerBoxColliders)
        {
            if (collider.CompareTag("Player1") || collider.CompareTag("Player2"))
            {
                isInsideInner = true;
                break;
            }
        }

        // Check if any of the OutterBoxColliders are tagged as players
        foreach (Collider2D collider in OutterBoxColliders)
        {
            if (collider.CompareTag("Player1") || collider.CompareTag("Player2"))
            {
                isInsideOutter = true;
                break;
            }
        }

        // Update the collider state based on the presence of players inside the boxes
        if (isInsideInner && isInsideOutter)
        {
            // Disable the collider if players are inside both boxes
            Debug.Log("Inside both boxes");
        }
        else
        {
            Debug.Log("Inside outter box");
            // Enable the collider if players are only inside the OutterBox
            bombCollider.enabled = true;
            colliderEnabled = true; // Set the flag to true to stop checking
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the box area in the Scene view for visual reference
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(BoxCenter, InnerBox);
    }
}
