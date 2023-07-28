using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject WaterBomb;

    protected Animator PlayerAnimator;

    //This is the timer for bomb explosion. Now it is 3 seconds according to WaterBomb_Execute script
    protected float BombTimer = 3f;

    protected int CurrentBombs = 0;

    public int PlayerSpeedInit { get; set; }
    public int CurrentSpeed { get; set; }
    public int PlayerRange { get; set; }
    public int MaxBomb { get; set; }

    public int Needle { get; protected set; }
    public bool Flowed { get; set; }

    public bool Dead { get; set; } = false;


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

        PlayerAnimator.SetFloat("Horizontal", playerMovement.x);
        PlayerAnimator.SetFloat("Vertical", playerMovement.y);
        PlayerAnimator.SetFloat("Speed", playerMovement.sqrMagnitude);
        PlayerAnimator.SetBool("Hit", Flowed);

        if (!Dead)
        {
            transform.Translate(CurrentSpeed * Time.deltaTime * playerMovement);
        }
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
        GameObject bomb = Instantiate(WaterBomb, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb_Execute>().FlowLength = (float)PlayerRange;
        SnapBomb(bomb);
        CurrentBombs++;

        SoundManager.Instance.PlaySFXSound("Bomb_Spawn", 1f);

        yield return new WaitForSeconds(BombTimer);

        CurrentBombs--; // Decrease CurrentBombs after the bomb explodes
    }

    //FIXME: have to change the code after getting DHHS implemented
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
    public void SetCharacterStats(int speed, int range, int maxBomb)
    {
        PlayerSpeedInit = speed;
        CurrentSpeed = speed;
        PlayerRange = range;
        MaxBomb = maxBomb;
        Needle = 2;
        Flowed = false;
    }
    public Vector2 FindWBSpawnPoint(GameObject bombObject)
    {
        int xIndex = (int)Mathf.Round((float)(bombObject.transform.position.x + 7f) / 0.7f);
        int yIndex = (int)Mathf.Round((float)(bombObject.transform.position.y + 4.3f) / 0.7f);
        return new Vector2(xIndex * 0.7f - 7f, yIndex * 0.7f - 4.3f);
    }

    private void SnapBomb(GameObject bombObject)
    {
        bombObject.transform.position = FindWBSpawnPoint(bombObject);
    }

    protected virtual void UseNeedle()
    {
        Needle--;
        Destroy(transform.GetChild(0).gameObject);
        PlayerAnimator.SetTrigger("Needle");
        SoundManager.Instance.PlaySFXSound("Pop", 1f);
        Flowed = false;
        CurrentSpeed = PlayerSpeedInit;
    }
}
