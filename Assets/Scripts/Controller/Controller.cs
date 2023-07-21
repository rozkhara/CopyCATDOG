using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject WaterBomb;
   
    protected float BombTimer = 3f;

    protected int MaxBomb = 3;
    protected int CurrentBombs = 0;
    protected float PlayerSpeed;
    protected int BombExplosionRange = 5;
    protected int PlayerRange;

    private void Start()
    {
        SpawnPlayers();
    }
    
    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void SpawnPlayers()
    {
        Instantiate(Player1Prefab, new Vector3(-7f, -4.3f, 0f), Quaternion.identity);
        Instantiate(Player2Prefab, new Vector3(3f, 4.4f, 0f), Quaternion.identity);
    }
    protected virtual void HandlePlayerMovement()
    {
        float MoveX = Input.GetAxisRaw("Player1Horizontal");
        float MoveY = Input.GetAxisRaw("Player1Vertical");
        Move(MoveX, MoveY);
    }
    protected virtual void Move(float x, float y)
    {
        Vector2 playerMovement = new Vector2(x, y);

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

    /*private void Snapping(GameObject targetObject)
    {
        mapGeneratorScript = targetObject.GetComponent<MapGenerator>();
        if (mapGeneratorScript != null)
        {
            Debug.Log("GetComponent targetObj is completed.");
            mapGeneratorScript.FindAndTransformObject(targetObject);
            Debug.Log("Snapping is completed.");
        }
        else
        {
            Debug.Log("GetComponent targetobj is failed.");
        }

    }*/

    //The function belown is commented not used
    /*private void FindAndTransformObject(GameObject targetObject)
    {
        Vector2 targetPosition = targetObject.transform.position;
        Transform nearestObject = null;

        float shortestDistance = Mathf.Infinity;
        if (mapGeneratorScript != null)
        {
            for (int y = 0; y < mapGeneratorScript.mapTiles.Count; y++)
            {
                Transform tilePosition = mapGeneratorScript.mapTiles[y].transform;
                float distance = Vector2.Distance(targetPosition, tilePosition.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestObject = tilePosition;
                }
            }
            if (nearestObject != null)
            {
                targetObject.transform.position = nearestObject.position;
            }
        }


        /*foreach (Transform otherObject in otherObjects)
        {
            float distance = Vector2.Distance(targetPosition, otherObject.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestObject = otherObject;
            }
        }*/
}


