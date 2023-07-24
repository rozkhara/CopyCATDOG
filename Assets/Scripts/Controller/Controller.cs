using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject bombPrefab;
    public GameObject WaterBomb;

    public Player1 player1Script;
    public Player2 player2Script;
    private Vector3 player1InitialPos;
    private Vector3 player2InitialPos;

    public GameObject MG;

    private float timer = 0;
    private int delayTime = 3;

    private void Start()
    {
        SpawnPlayers();
    }
    private void FixedUpdate()
    {
        if (timer > delayTime)
        {
            HandlePlayerMovement();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayTime)
        {
            HandleBombSpawn();
        }
    }

    private void SpawnPlayers()
    {
        GameObject player1 = Instantiate(player1Prefab, new Vector3(-7f, -4.3f, 0f), Quaternion.identity);
        GameObject player2 = Instantiate(player2Prefab, new Vector3(2.8f, 4.1f, 0f), Quaternion.identity);

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

        player1Script.transform.Translate(player1Movement * player1Script.PlayerSpeed * Time.deltaTime);

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

        player2Script.transform.Translate(player2Movement * player2Script.PlayerSpeed * Time.fixedDeltaTime);
    }

    private void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player1Bomb") && player1Script.CurrentBombs < player1Script.maxBomb)
        {
            if (CanPlaceBomb(player1Script.transform.position))
            {
                SpawnBomb(player1Script, player2Script);
                player1Script.CurrentBombs++;
            }
            else
            {
                Debug.Log("Cannot place bomb at the current position.");
            }
        }

        if (Input.GetButtonDown("Player2Bomb") && player2Script.CurrentBombs < player2Script.maxBomb)
        {
            if (CanPlaceBomb(player2Script.transform.position))
            {
                SpawnBomb(player2Script, player1Script);
                player2Script.CurrentBombs++;
            }
            else
            {
                Debug.Log("Cannot place bomb at the current position.");
            }
        }
    }
    private bool CanPlaceBomb(Vector3 bombPosition)
    {
        // Check if there is already a bomb at the target position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bombPosition, 0.5f); // Adjust the radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bomb"))
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnBomb(Player1 player, Player2 otherPlayer)
    {
        player1InitialPos = player.transform.position;
        player2InitialPos = otherPlayer.transform.position;
        GameObject bomb = Instantiate(WaterBomb, player.transform.position, Quaternion.identity);
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();
        SnapBomb(bomb);
        bombCollider.isTrigger = true;

        bomb.GetComponent<Bomb>().EnablePlayerCollision(bombCollider, player.GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), player);
        bomb.GetComponent<Bomb>().OnBombDestroyed += () => { player.CurrentBombs--; };
    }

    private void SpawnBomb(Player2 player, Player1 otherPlayer)
    {
        player1InitialPos = player.transform.position;
        player2InitialPos = otherPlayer.transform.position;
        GameObject bomb = Instantiate(WaterBomb, player.transform.position, Quaternion.identity);
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();
        SnapBomb(bomb);
        bombCollider.isTrigger = true;

        bomb.GetComponent<Bomb>().EnablePlayerCollision(bombCollider, player.GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), player);
        bomb.GetComponent<Bomb>().OnBombDestroyed += () => { player.CurrentBombs--; };
    }

    public Vector2 FindWBSpawnPoint(GameObject targetObject)
    {
        int xIndex = (int)Mathf.Round((float)(targetObject.transform.position.x + 7f) / 0.7f);
        int yIndex = (int)Mathf.Round((float)(targetObject.transform.position.y + 4.3f) / 0.7f);
        return new Vector2(xIndex * 0.7f - 7f, yIndex * 0.7f - 4.3f);
    }

    private void SnapBomb(GameObject targetObject)
    {
        targetObject.transform.position = FindWBSpawnPoint(targetObject);
    }
}

