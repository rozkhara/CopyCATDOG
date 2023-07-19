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

    public float FlowLength;

    private RaycastHit2D HitR;
    private RaycastHit2D HitL;
    private RaycastHit2D HitU;
    private RaycastHit2D HitD;

    private float FlowR;
    private float FlowL;
    private float FlowU;
    private float FlowD;

    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine("WBExceed");
    }

    IEnumerator WBExceed()
    {
        StartCoroutine("GetWBPos");
        yield return new WaitForSeconds(3f);
        StartCoroutine("WBBurst");
    }

    IEnumerator GetWBPos()
    {
        yield return new WaitForSeconds(0.3f);
        WBpos = new Vector2(transform.position.x, transform.position.y);
        yield break;
    }

    IEnumerator WBBurst()
    {
        FlowSetting();
        WBBurstBlock();
        WBBurstPlayer();
        Destroy(gameObject);
        yield break;
    }
    
    void FlowSetting()
    {
        FlowR = FlowLength * 0.7f;
        FlowL = FlowLength * 0.7f;
        FlowU = FlowLength * 0.7f;
        FlowD = FlowLength * 0.7f;

        HitR = Physics2D.Raycast(WBpos, new Vector2(1, 0), FlowLength * 0.7f, 256);
        HitL = Physics2D.Raycast(WBpos, new Vector2(-1, 0), FlowLength * 0.7f, 256);
        HitU = Physics2D.Raycast(WBpos, new Vector2(0, 1), FlowLength * 0.7f, 256);
        HitD = Physics2D.Raycast(WBpos, new Vector2(0, -1), FlowLength * 0.7f, 256);

        if (HitR == true)
            FlowR = HitR.distance;
        if (HitL == true)
            FlowL = HitL.distance;
        if (HitU == true)
            FlowU = HitU.distance;
        if (HitD == true)
            FlowD = HitD.distance;
    }

    void WBBurstBlock()
    {
        if (HitR == true)
            if (HitR.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitR.collider.gameObject);

        if (HitL == true)
            if (HitL.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitL.collider.gameObject);

        if (HitU == true)
            if (HitU.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitU.collider.gameObject);

        if (HitD == true)
            if (HitD.collider.gameObject.CompareTag("CanDestroy"))
                Destroy(HitD.collider.gameObject);
    }

    void WBBurstPlayer()
    {
        List<Collider2D> HitPlayer = new List<Collider2D>();

        RaycastHit2D[] HitinfoR = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, new Vector2(1, 0), FlowR, 8);
        RaycastHit2D[] HitinfoL = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, new Vector2(-1, 0), FlowL, 8);
        RaycastHit2D[] HitinfoU = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, new Vector2(0, 1), FlowU, 8);
        RaycastHit2D[] HitinfoD = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, new Vector2(0, -1), FlowD, 8); 

        foreach (RaycastHit2D player in HitinfoR)
            HitPlayer.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoL)
            HitPlayer.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoU)
            HitPlayer.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoD)
            HitPlayer.Add(player.collider);

        for (int i = 0; i < HitPlayer.Count; i++)
        {
            Player FlowedPlayerInfo = HitPlayer[i].GetComponent<Player>();

            if (FlowedPlayerInfo.Flowed == false)
                FlowedPlayerInfo.Flowed = true;
        }
    }
}
