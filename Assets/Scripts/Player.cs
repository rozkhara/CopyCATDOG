using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    Rigidbody2D Movement;
    SpriteRenderer Show;

    public GameObject WaterBomb;

    private Vector2 pos;
    public LayerMask WBLayer;

    private int WBnum = 0;
    public int Velocity = 3;

    public bool Flowed = false;
    public GameObject Flow_Condition;
    public int FlowLength = 10;
    public int WBNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        Movement = GetComponent<Rigidbody2D>();
        Show = GetComponent<SpriteRenderer>();
        StartCoroutine("PlayerFlowed");
        
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
            Movement.velocity = new Vector2(Velocity, Movement.velocity.y);
            Show.flipX = false;
        }
        else if (horizontal == -1)
        {
            Movement.velocity = new Vector2(-Velocity, Movement.velocity.y);
            Show.flipX = true;
        }
        else
        {
            Movement.velocity = new Vector2(0, Movement.velocity.y);
            Show.flipX = false;
        }

        if (vertical == 1)
            Movement.velocity = new Vector2(Movement.velocity.x, Velocity);
        else if (vertical == -1)
            Movement.velocity = new Vector2(Movement.velocity.x, -Velocity);
        else
            Movement.velocity = new Vector2(Movement.velocity.x, 0);
    }

    void TryBomb()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            pos = new Vector2(Mathf.RoundToInt(posX), Mathf.RoundToInt(posY));

            if (WhetherWB() == true)
            {
                Instantiate(WaterBomb, pos, transform.rotation);
                WaterBomb_Execute WBInfo = WaterBomb.GetComponent<WaterBomb_Execute>();
                WBInfo.FlowLength = FlowLength;
                WBnum++;
            }
        }
    }

    bool WhetherWB()
    {
        Collider2D Recog_WB = Physics2D.OverlapBox(pos, new Vector2(1, 1), 0f, WBLayer);

        if (Recog_WB != null) return false;

        return true;
    }

    IEnumerator PlayerFlowed()
    {
        while (true)
        {
            if (Flowed == true)
            {
                float PlayerX = transform.position.x;
                float PlayerY = transform.position.y;
                Vector2 PlayerPos = new Vector2(PlayerX, PlayerY);

                Instantiate(Flow_Condition, PlayerPos, transform.rotation).transform.parent = this.transform;
                yield break;
            }
            yield return null;
        }
    }
}
