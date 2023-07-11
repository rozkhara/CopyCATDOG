using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D bombCollider;
    private Collider2D player1Collider;
    private Collider2D player2Collider;
    private MonoBehaviour player;

    public event Action OnBombDestroyed;

    private void Start()
    {
        bombCollider = GetComponent<Collider2D>();
        bombCollider.enabled = false; // Disable bomb collider when spawned
        StartCoroutine(DestroyAfterTime(3f));
    }

    public void EnablePlayerCollision(Collider2D bombCollider, Collider2D player1Collider, Collider2D player2Collider, MonoBehaviour player)
    {
        this.bombCollider = bombCollider;
        this.player1Collider = player1Collider;

        this.player2Collider = player2Collider;
        this.player = player;

        bombCollider.isTrigger = false; // Make the bomb a solid collider
        bombCollider.enabled = true;    // Enable the bomb collider

        StartCoroutine(CheckPlayerMovement());
        StartCoroutine(DestroyAfterTime(3f));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        OnBombDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator CheckPlayerMovement()
    {
        bool player1Moved = false;
        bool player2Moved = false;
        float timer = 0f;

        float minDistanceToPlayer1 = Vector2.Distance(transform.position, player1Collider.bounds.ClosestPoint(transform.position));
        float minDistanceToPlayer2 = Vector2.Distance(transform.position, player2Collider.bounds.ClosestPoint(transform.position));

        while ((!player1Moved || !player2Moved) && timer < 3f)
        {
            timer += Time.deltaTime;

            float currentDistanceToPlayer1 = Vector2.Distance(transform.position, player1Collider.bounds.ClosestPoint(transform.position));
            float currentDistanceToPlayer2 = Vector2.Distance(transform.position, player2Collider.bounds.ClosestPoint(transform.position));

            if (!player1Moved && currentDistanceToPlayer1 > minDistanceToPlayer1)
            {
                player1Moved = true;
                bombCollider.enabled = true; // Enable the bomb collider when player 1 moves away from the bomb
            }

            if (!player2Moved && currentDistanceToPlayer2 > minDistanceToPlayer2)
            {
                player2Moved = true;
                bombCollider.enabled = true; // Enable the bomb collider when player 2 moves away from the bomb
            }

            if (player1Moved && player2Moved)
                break;

            yield return null;
        }
    }

}