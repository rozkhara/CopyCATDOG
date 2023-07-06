using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    private RaycastHit2D HitR;
    private RaycastHit2D HitL;
    private RaycastHit2D HitU;
    private RaycastHit2D HitD;

    private float FlowR;
    private float FlowL;
    private float FlowU;
    private float FlowD;

    private GameObject FlowedPlayer;

    void Awake()
    {
        WBpos = new Vector2(transform.position.x, transform.position.y);
    }

    void Start()
    {
        StartTime = Time.time;
        FlowPosSetting();
        WBBurstBlock();
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
        if (CurrentTime <= 0.5f)
        {
            WBBurstPlayer();
            Destroy(gameObject);
        }
    }

    void WBBurstPlayer()
    {
        List<string> HitPlayer = new List<string>();

        RaycastHit2D[] HitinfoR1 = Physics2D.RaycastAll(FlowposYp, new Vector2(1, 0), FlowR, 128);
        RaycastHit2D[] HitinfoR2 = Physics2D.RaycastAll(FlowposYm, new Vector2(1, 0), FlowR, 128);
        RaycastHit2D[] HitinfoL1 = Physics2D.RaycastAll(FlowposYp, new Vector2(-1, 0), FlowL, 128);
        RaycastHit2D[] HitinfoL2 = Physics2D.RaycastAll(FlowposYm, new Vector2(-1, 0), FlowL, 128);

        RaycastHit2D[] HitinfoU1 = Physics2D.RaycastAll(FlowposXm, new Vector2(0, 1), FlowU, 128);
        RaycastHit2D[] HitinfoU2 = Physics2D.RaycastAll(FlowposXp, new Vector2(0, 1), FlowU, 128);
        RaycastHit2D[] HitinfoD1 = Physics2D.RaycastAll(FlowposXm, new Vector2(0, -1), FlowD, 128);
        RaycastHit2D[] HitinfoD2 = Physics2D.RaycastAll(FlowposXp, new Vector2(0, -1), FlowD, 128);

        foreach (RaycastHit2D player in HitinfoR1)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoR2)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoL1)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoL2)
            HitPlayer.Add(player.collider.name);

        foreach (RaycastHit2D player in HitinfoU1)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoU2)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoD1)
            HitPlayer.Add(player.collider.name);
        foreach (RaycastHit2D player in HitinfoD2)
            HitPlayer.Add(player.collider.name);

        HitPlayer.Distinct();
        for (int i = 0; i < HitPlayer.Count; i++)
        {
            FlowedPlayer = GameObject.Find(HitPlayer[i]);
            Player FlowedPlayerInfo = FlowedPlayer.GetComponent<Player>();

            if (FlowedPlayerInfo.Flowed == false)
            {
                FlowedPlayerInfo.Velocity = 1;
                FlowedPlayerInfo.Flowed = true;
            }
        }


    }

    void WBBurstBlock()
    {
        FlowR = FlowLength;
        FlowL = FlowLength;
        FlowU = FlowLength;
        FlowD = FlowLength;

        HitR = Physics2D.Raycast(WBpos, new Vector2(1, 0), FlowLength, 256);
        HitL = Physics2D.Raycast(WBpos, new Vector2(-1, 0), FlowLength, 256);
        HitU = Physics2D.Raycast(WBpos, new Vector2(0, 1), FlowLength, 256);
        HitD = Physics2D.Raycast(WBpos, new Vector2(0, -1), FlowLength, 256);

        if (HitR == true)
        {
            Destroy(HitR.collider.gameObject);
            FlowR = HitR.distance;
        }
        if (HitL == true)
        {
            Destroy(HitL.collider.gameObject);
            FlowL = HitL.distance;
        }
        if (HitU == true)
        {
            Destroy(HitU.collider.gameObject);
            FlowU = HitU.distance;
        }
        if (HitD == true)
        {
            Destroy(HitD.collider.gameObject);
            FlowD = HitD.distance;
        }
    }
}
