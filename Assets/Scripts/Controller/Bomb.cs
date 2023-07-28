using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D bombCollider;

    public Vector2 bombSize = new(0.7f, 0.7f);

    private GameObject Player1;
    private GameObject Player2;

    private void Start()
    {
        bombCollider = GetComponent<Collider2D>();
        bombCollider.enabled = true;
        Player1 = GameManager.Instance.Player1Spawned;
        Player2 = GameManager.Instance.Player2Spawned;
        Physics2D.IgnoreCollision(bombCollider, Player1.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bombCollider, Player2.GetComponent<Collider2D>());
    }

    private void Update()
    {
        if (Player1.transform.position.x <= this.transform.position.x - bombSize.x || Player1.transform.position.x >= this.transform.position.x + bombSize.x ||
            Player1.transform.position.y <= this.transform.position.y - bombSize.y || Player1.transform.position.y >= this.transform.position.y + bombSize.y)
        {
            //Debug.Log("hello");
            Physics2D.IgnoreCollision(bombCollider, Player1.GetComponent<Collider2D>(), false);
        }
        if (Player2.transform.position.x <= this.transform.position.x - bombSize.x || Player2.transform.position.x >= this.transform.position.x + bombSize.x ||
            Player2.transform.position.y <= this.transform.position.y - bombSize.y || Player2.transform.position.y >= this.transform.position.y + bombSize.y)
        {
            //Debug.Log("hello");
            Physics2D.IgnoreCollision(bombCollider, Player2.GetComponent<Collider2D>(), false);
        }
    }
}
