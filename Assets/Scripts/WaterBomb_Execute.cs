using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb_Execute : MonoBehaviour
{
    private Vector2 WBpos;

    public float FlowLength;
    private float FlowLengthRaw;

    private LayerMask PlayerLayer = 1 << 3;
    private LayerMask BlockLayer = 1 << 8;
    private LayerMask ItemLayer = 1 << 9;

    private Vector2[] direction = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    private float[] FlowAll;

    public GameObject ItemPrefab;
    private float[] ItemPercent = new float[] { 10, 20, 30, 70 };

    public GameObject Flow_Condition;
    private GameObject FlowBackground;

    public GameObject airplane;

    private void Start()
    {
        StartCoroutine(WBExceed());
    }

    private IEnumerator WBExceed()
    {
        StartCoroutine(GetWBPos());
        yield return new WaitForSeconds(3f);
        StartCoroutine(WBBurst());
    }

    private IEnumerator GetWBPos()
    {
        yield return new WaitForSeconds(0.3f);
        WBpos = new Vector2(transform.position.x, transform.position.y);
        FlowLengthRaw = FlowLength * 0.7f;
        yield break;
    }

    private IEnumerator WBBurst()
    {
        FlowSetting();
        WBBurstPlayer();
        Destroy(gameObject);
        yield break;
    }

    private void FlowSetting()
    {
        FlowAll = new float[4];

        for (int i = 0; i < 4; i++)
        {
            WBBurstBlockandItem(direction[i], ref FlowAll[i]);
        }
    }


    private void WBBurstBlockandItem(Vector2 direction, ref float Flow)
    {
        RaycastHit2D isHit = Physics2D.Raycast(WBpos, direction, FlowLengthRaw, BlockLayer);

        if (isHit == true)
        {
            Flow = isHit.distance;
            BurstItem(Flow, direction);

            if (isHit.collider.gameObject.CompareTag("CanDestroy"))
            {
                airplane.GetComponent<Airplane>().MakeEmpty(isHit.collider.gameObject.transform);
                Destroy(isHit.collider.gameObject);

                float RandomPoint = Random.value * 100;

                for (int i = 0; i < ItemPercent.Length; i++)
                {
                    if (RandomPoint < ItemPercent[i])
                    {
                        DropItem(isHit.collider.transform.position, i);
                    }
                    else
                    {
                        RandomPoint -= ItemPercent[i];
                    }
                }
            }
        }
        else
        {
            Flow = FlowLengthRaw;
            BurstItem(Flow, direction);
        }
    }

    private void BurstItem(float Flow, Vector2 direction)
    {
        //TODO: raycast로 변경
        RaycastHit2D[] ItemHit = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, direction, Flow, ItemLayer);

        foreach (RaycastHit2D Item in ItemHit)
        {
            Destroy(Item.collider.gameObject);
        }
    }
    private void DropItem(Vector2 BlockPosition, int ItemNumber)
    {
        if (ItemNumber == 2)
        {
            Instantiate(ItemPrefab, BlockPosition, Quaternion.identity);
        }
    }
    private void WBBurstPlayer()
    {
        List<Collider2D> PlayerHit = new();

        RaycastHit2D[] HitinfoR = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, direction[0], FlowAll[0], PlayerLayer);
        RaycastHit2D[] HitinfoL = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, direction[1], FlowAll[1], PlayerLayer);
        RaycastHit2D[] HitinfoU = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, direction[2], FlowAll[2], PlayerLayer);
        RaycastHit2D[] HitinfoD = Physics2D.BoxCastAll(WBpos, new Vector2(0.7f, 0.7f), 0f, direction[3], FlowAll[3], PlayerLayer);

        foreach (RaycastHit2D player in HitinfoR)
            PlayerHit.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoL)
            PlayerHit.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoU)
            PlayerHit.Add(player.collider);
        foreach (RaycastHit2D player in HitinfoD)
            PlayerHit.Add(player.collider);

        for (int i = 0; i < PlayerHit.Count; i++)
        {
            Controller FlowedPlayerInfo = PlayerHit[i].GetComponent<Controller>();

            if (FlowedPlayerInfo.Flowed != true)
            {
                FlowedPlayerInfo.Flowed = true;

                float PlayerX = FlowedPlayerInfo.transform.position.x;
                float PlayerY = FlowedPlayerInfo.transform.position.y;
                Vector2 PlayerPos = new Vector2(PlayerX, PlayerY);

                FlowBackground = Instantiate(Flow_Condition, PlayerPos, transform.rotation);
                FlowBackground.transform.parent = PlayerHit[i].transform;

                FlowedPlayerInfo.CurrentSpeed = 1;
            }
        }
    }
}
