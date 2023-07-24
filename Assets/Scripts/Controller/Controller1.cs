using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject WaterBomb;

    protected float BombTimer = 3f; //This is the timer for bomb explosion.Now it is 3 seconds according to WaterBomb_Execute script
    protected int CurrentBombs = 0;

    public int PlayerSpeed { get; protected set; }
    public int PlayerRange { get; protected set; }
    public int MaxBomb { get; protected set; }
    public int BombExplosionRange { get; protected set; }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    protected virtual void HandlePlayerMovement()
    {
        float MoveX = Input.GetAxisRaw("Player1Horizontal");
        float MoveY = Input.GetAxisRaw("Player1Vertical");
        Move(MoveX, MoveY);
    }

    protected virtual void Move(float x, float y)
    {
        Vector2 playerMovement = new(x, y);

        // Check if both horizontal and vertical inputs are pressed simultaneously
        if (Mathf.Abs(playerMovement.x) > 0 && Mathf.Abs(playerMovement.y) > 0)
        {
            playerMovement.x = 0f;
            playerMovement.y = 0f;
        }

        playerMovement.Normalize();

        transform.Translate(PlayerSpeed * Time.deltaTime * playerMovement);
    }

    protected virtual void HandleBombSpawn()
    {
        if (CanPlaceBomb(transform.position))
        {
            StartCoroutine(SpawnBombWithDelay(transform.position));
        }
        else
        {
            Debug.Log("Cannot place bomb at the current position.");
        }
    }

    protected virtual IEnumerator SpawnBombWithDelay(Vector2 position)
    {
        Instantiate(WaterBomb, position, Quaternion.identity);
        CurrentBombs++;

        yield return new WaitForSeconds(BombTimer);

        CurrentBombs--; // Decrease CurrentBombs after the bomb explodes
    }

    private bool CanPlaceBomb(Vector2 bombPosition)
    {
        // Check if there is already a bomb at the target position
        Physics2D.OverlapPoint(bombPosition, 64);
        //Fix the code after finding the bomb position on the grid
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bombPosition, 1.0f); // Adjust the radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bomb"))
            {
                return false;
            }
        }

        return true;
    }

    // Method to set character stats
    public void SetCharacterStats(int speed, int range, int maxBomb, int explosionRange)
    {
        PlayerSpeed = speed;
        PlayerRange = range;
        MaxBomb = maxBomb;
        BombExplosionRange = explosionRange;
    }
}
