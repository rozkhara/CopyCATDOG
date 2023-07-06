using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NewBehaviourScript : MonoBehaviour
{

    Rigidbody2D Movement;
    SpriteRenderer Show;
    Animator Walk;

    public GameObject WaterBomb;
    
    public Vector2 pos;
    public LayerMask ChoosedLayer;
    
    public int WBnum = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Movement = GetComponent<Rigidbody2D>();
        Show = GetComponent<SpriteRenderer>();
        Walk = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TryBomb();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 1)
        {
            Movement.velocity = new Vector2(3, Movement.velocity.y);
            Show.flipX = false;
        }
        else if (horizontal == -1)
        {
            Movement.velocity = new Vector2(-3, Movement.velocity.y);
            Show.flipX = true;
        }
        else
        {
            Movement.velocity = new Vector2(0, Movement.velocity.y);
            Show.flipX = false;
        }

        if (vertical == 1)
            Movement.velocity = new Vector2(Movement.velocity.x, 3);
        else if (vertical == -1)
            Movement.velocity = new Vector2(Movement.velocity.x, -3);
        else
            Movement.velocity = new Vector2(Movement.velocity.x, 0);

        if (horizontal == 0 && vertical == 0)
            Walk.SetBool("IsWalking", false);
        else
            Walk.SetBool("IsWalking", true);
    }

    void TryBomb()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            pos = new Vector2(Mathf.RoundToInt(posX), Mathf.RoundToInt(posY));

            if (WhetherWB() == 1)
            {
                Instantiate(WaterBomb, pos, transform.rotation);
                WBnum++;
            }
        }
    }

    int WhetherWB()
    {
        Collider2D recog = Physics2D.OverlapBox(pos, new Vector2(1, 1), 0f, ChoosedLayer);

        if (recog != null) return 0;

        return 1;
    }
}
