using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D bombCollider;
    private Collider2D player1Collider;
    private Collider2D player2Collider;
    private MonoBehaviour player;

    private void Start()
    {
        bombCollider = GetComponent<Collider2D>();
    }

    public void EnablePlayerCollision(Collider2D bombCollider, Collider2D player1Collider, Collider2D player2Collider, MonoBehaviour player)
    {
        this.bombCollider = bombCollider;
        this.player1Collider = player1Collider;
        this.player2Collider = player2Collider;
        this.player = player;

        bombCollider.isTrigger = false;  // Make the bomb a solid collider

        Physics2D.IgnoreCollision(bombCollider, player1Collider, true);
        Physics2D.IgnoreCollision(bombCollider, player2Collider, true);

        StartCoroutine(CheckPlayerMovement());
    }

    private IEnumerator CheckPlayerMovement()
    {
        bool player1Moved = false;
        bool player2Moved = false;
        float timer = 0f;

        Vector2 player1InitialPos = player1Collider.transform.position;
        Vector2 player2InitialPos = player2Collider.transform.position;

        while ((!player1Moved || !player2Moved) && timer < 3f)
        {
            timer += Time.deltaTime;

            if (!player1Moved && (Vector2)player1Collider.transform.position != player1InitialPos)
            {
                player1Moved = true;
                Physics2D.IgnoreCollision(bombCollider, player1Collider, false);
            }

            if (!player2Moved && (Vector2)player2Collider.transform.position != player2InitialPos)
            {
                player2Moved = true;
                Physics2D.IgnoreCollision(bombCollider, player2Collider, false);
            }

            if (player1Moved && player2Moved)
                break;

            yield return null;
        }

        if (timer >= 3f && (!player1Moved || !player2Moved))
        {
            Physics2D.IgnoreCollision(bombCollider, player1Collider, false);
            Physics2D.IgnoreCollision(bombCollider, player2Collider, false);
        }

        var controller = FindObjectOfType<Controller>();
        if (controller != null)
        {
            if (player == controller.player1Script)
                controller.canPlaceBombPlayer1 = true;
            else if (player == controller.player2Script)
                controller.canPlaceBombPlayer2 = true;
        }
    }
}
