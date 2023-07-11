using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int maxBomb = 3;
    public int CurrentBombs = 0;
    public float playerSpeed = 5f;

    // This script inherits properties from the MainScript

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Debug.Log("Player 1 collided with a bomb.");
        }
    }
}