using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject bombPrefab;

    private Player1 player1Script;
    private Player2 player2Script;
    private Vector3 player1InitialPos;
    private Vector3 player2InitialPos;


    private void Start()
    {
        SpawnPlayers();
    }

    private void Update()
    {
        HandlePlayerMovement();
        HandleBombSpawn();
    }

    private void SpawnPlayers()
    {
        GameObject player1 = Instantiate(player1Prefab, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        GameObject player2 = Instantiate(player2Prefab, new Vector3(5f, 0f, 0f), Quaternion.identity);

        player1Script = player1.GetComponent<Player1>();
        player2Script = player2.GetComponent<Player2>();
    }

    private void HandlePlayerMovement()
    {
        // Player 1 movement
        float player1MoveX = Input.GetAxisRaw("Player1Horizontal");
        float player1MoveY = Input.GetAxisRaw("Player1Vertical");
        Vector3 player1Movement = new Vector3(player1MoveX, player1MoveY, 0f).normalized;

        // Restrict movement to four directions for player 1
        if (Mathf.Abs(player1Movement.x) > Mathf.Abs(player1Movement.y))
        {
            player1Movement.y = 0f;
        }
        else
        {
            player1Movement.x = 0f;
        }

        player1Script.transform.Translate(player1Movement * player1Script.playerSpeed * Time.deltaTime);

        // Player 2 movement
        float player2MoveX = Input.GetAxisRaw("Player2Horizontal");
        float player2MoveY = Input.GetAxisRaw("Player2Vertical");
        Vector3 player2Movement = new Vector3(player2MoveX, player2MoveY, 0f).normalized;

        // Restrict movement to four directions for player 2
        if (Mathf.Abs(player2Movement.x) > Mathf.Abs(player2Movement.y))
        {
            player2Movement.y = 0f;
        }
        else
        {
            player2Movement.x = 0f;
        }

        player2Script.transform.Translate(player2Movement * player2Script.playerSpeed * Time.deltaTime);
    }


    private void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player1Bomb"))
        {
            if (player1Script.CurrentBombs < player1Script.maxBomb)
            {
                if (!IsBombInsideCollider(player1Script.GetComponent<Collider2D>()))
                {
                    SpawnBomb(player1Script, player2Script);
                    player1Script.CurrentBombs++;
                }
                else
                {
                    Debug.Log("A bomb is already inside Player 1's collider");
                }
            }
            else
            {
                Debug.Log("Max bomb for player 1 reached");
            }
        }

        if (Input.GetButtonDown("Player2Bomb"))
        {
            if (player2Script.CurrentBombs < player2Script.maxBomb)
            {
                if (!IsBombInsideCollider(player2Script.GetComponent<Collider2D>()))
                {
                    SpawnBomb(player2Script, player1Script);
                    player2Script.CurrentBombs++;
                }
                else
                {
                    Debug.Log("A bomb is already inside Player 2's collider");
                }
            }
            else
            {
                Debug.Log("Max bomb for player 2 reached");
            }
        }
    }

    private bool IsBombInsideCollider(Collider2D playerCollider)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerCollider.bounds.center, playerCollider.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bomb") && collider != playerCollider && collider.bounds.Intersects(playerCollider.bounds))
            {
                return true;
            }
        }
        return false;
    }




    private void SpawnBomb(Player1 player, Player2 otherPlayer)
    {
        player1InitialPos = player.transform.position;
        player2InitialPos = otherPlayer.transform.position;
        GameObject bomb = Instantiate(bombPrefab, player.transform.position, Quaternion.identity);
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();
        bombCollider.isTrigger = true;  // Make the bomb a trigger initially

        bomb.GetComponent<  Bomb>().EnablePlayerCollision(bombCollider, player.GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), player);
    }

    private void SpawnBomb(Player2 player, Player1 otherPlayer)
    {
        player1InitialPos = player.transform.position;
        player2InitialPos = otherPlayer.transform.position;
        GameObject bomb = Instantiate(bombPrefab, player.transform.position, Quaternion.identity);
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();
        bombCollider.isTrigger = true;  // Make the bomb a trigger initially

        bomb.GetComponent<Bomb>().EnablePlayerCollision(bombCollider, player.GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), player);
    }

}
