using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Flow_Execute : MonoBehaviour
{
    private float StartTime;
    private float CurrentTime;

    private Vector2 WBpos;
    private Vector2 FlowposXp;
    private Vector2 FlowposXm;
    private Vector2 FlowposYp;
    private Vector2 FlowposYm;

    private float FlowLength = 10;

    private bool React = false;

    void Awake()
    {
        WBpos = new Vector2(transform.position.x, transform.position.y);
    }

    void Start()
    {
        StartTime = Time.time;
        FlowPosSetting();
    }

    // Update is called once per frame
    void Update()
    {
        WBBurst();
    }

    void FlowPosSetting()
    {
        FlowposYp = new Vector2(WBpos.x, WBpos.y + 0.4f);
        FlowposYm = new Vector2(WBpos.x, WBpos.y - 0.4f);

        FlowposXp = new Vector2(WBpos.x + 0.4f, WBpos.y);
        FlowposXm = new Vector2(WBpos.x - 0.4f, WBpos.y);
    }

    void WBBurst()
    {
        CurrentTime = Time.time - StartTime;

        if (Physics2D.Raycast(FlowposYp, new Vector2(1, 0), FlowLength, 384)
            || Physics2D.Raycast(FlowposYm, new Vector2(1, 0), FlowLength, 384)
            || Physics2D.Raycast(FlowposYp, new Vector2(-1, 0), FlowLength, 384)
            || Physics2D.Raycast(FlowposYm, new Vector2(-1, 0), FlowLength, 384)
            || Physics2D.Raycast(FlowposXp, new Vector2(0, 1), FlowLength, 384)
            || Physics2D.Raycast(FlowposXm, new Vector2(0, 1), FlowLength, 384)
            || Physics2D.Raycast(FlowposXp, new Vector2(0, -1), FlowLength, 384)
            || Physics2D.Raycast(FlowposXm, new Vector2(0, -1), FlowLength, 384))
            React = true;

        if(React && CurrentTime <= 3)
                Debug.Log(CurrentTime);
    }
}
