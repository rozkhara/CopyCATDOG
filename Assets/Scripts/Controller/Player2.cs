using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public int maxBomb = 5;
    public int CurrentBombs = 0;
    public float PlayerSpeed;
    private Player PlayerInfo;
    public int BombExplosionRange = 5;

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
            Debug.Log("Player 2 collided with a bomb.");
        }
    }
}
