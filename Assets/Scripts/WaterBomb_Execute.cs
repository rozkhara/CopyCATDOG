using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class WaterBomb_Execute : MonoBehaviour
{
    private Vector2 WBpos;
    private Vector2 FlowposXp;
    private Vector2 FlowposXm;
    private Vector2 FlowposYp;
    private Vector2 FlowposYm;

    public float FlowLength = 10f;

    private RaycastHit2D HitR;
    private RaycastHit2D HitL;
    private RaycastHit2D HitU;
    private RaycastHit2D HitD;

    private float FlowR;
    private float FlowL;
    private float FlowU;
    private float FlowD;

    private GameObject FlowedPlayer;

    // Start is called before the first frame update

    void Awake()
    {
        WBpos = new Vector2(transform.position.x, transform.position.y);
    }

    void Start()
    {
        StartCoroutine("WBExceed");
    }

    IEnumerator WBExceed()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("WBExceed");
        StartCoroutine("WBBurst");
    }

    IEnumerator WBBurst()
    {
        FlowPosSetting();
        WBBurstBlock();
        yield return new WaitForSeconds(0.1f);
        WBBurstPlayer();
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }

    void FlowPosSetting()
    {
        FlowposYp = new Vector2(WBpos.x, WBpos.y + 0.4f);
        FlowposYm = new Vector2(WBpos.x, WBpos.y - 0.4f);

        FlowposXp = new Vector2(WBpos.x + 0.4f, WBpos.y);
        FlowposXm = new Vector2(WBpos.x - 0.4f, WBpos.y);
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
            if (HitR.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitR.collider.gameObject);
            FlowR = HitR.distance;
        }
        if (HitL == true)
        {
            if (HitL.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitL.collider.gameObject);
            FlowL = HitL.distance;
        }
        if (HitU == true)
        {
            if (HitU.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitU.collider.gameObject);
            FlowU = HitU.distance;
        }
        if (HitD == true)
        {
            if (HitD.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitD.collider.gameObject);
            FlowD = HitD.distance;
        }
    }

    void WBBurstPlayer()
    {
        List<string> HitPlayer = new List<string>();

        RaycastHit2D[] HitinfoR1 = Physics2D.RaycastAll(FlowposYp, new Vector2(1, 0), FlowR, 8);
        RaycastHit2D[] HitinfoR2 = Physics2D.RaycastAll(FlowposYm, new Vector2(1, 0), FlowR, 8);
        RaycastHit2D[] HitinfoL1 = Physics2D.RaycastAll(FlowposYp, new Vector2(-1, 0), FlowL, 8);
        RaycastHit2D[] HitinfoL2 = Physics2D.RaycastAll(FlowposYm, new Vector2(-1, 0), FlowL, 8);

        RaycastHit2D[] HitinfoU1 = Physics2D.RaycastAll(FlowposXm, new Vector2(0, 1), FlowU, 8);
        RaycastHit2D[] HitinfoU2 = Physics2D.RaycastAll(FlowposXp, new Vector2(0, 1), FlowU, 8);
        RaycastHit2D[] HitinfoD1 = Physics2D.RaycastAll(FlowposXm, new Vector2(0, -1), FlowD, 8);
        RaycastHit2D[] HitinfoD2 = Physics2D.RaycastAll(FlowposXp, new Vector2(0, -1), FlowD, 8);

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
}
