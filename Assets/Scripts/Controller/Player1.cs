using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public int maxBomb = 3;
    public int CurrentBombs = 0;
    public float PlayerSpeed;
    private Player PlayerInfo;

    // This script inherits properties from the MainScript

    void Start()
    {
        PlayerInfo = transform.GetComponent<Player>();
    }

    void Update()
    {
        PlayerSpeed = PlayerInfo.Velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Debug.Log("Player 1 collided with a bomb.");
        }
    }
}