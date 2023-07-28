using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject SpeedBoost;
    public GameObject RangeBoost;
    public GameObject MaxWBBoost;
    private float[] ItemPercent = new float[] { 10, 10, 10, 70 };

    public GameObject Flow_Condition;
    private GameObject FlowBackground;

    public GameObject airplane;

    public GameObject FlowCenterPrefab;
    public GameObject FlowRightPrefab;
    public GameObject FlowLeftPrefab;
    public GameObject FlowUpPrefab;
    public GameObject FlowDownPrefab;

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
        yield return new WaitForEndOfFrame();
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

        Instantiate(FlowCenterPrefab, WBpos, Quaternion.identity);

        for (int i = 0; i < 4; i++)
        {
            WBBurstBlockandItem(direction[i], ref FlowAll[i], i);
        }
        
        
    }


    private void WBBurstBlockandItem(Vector2 direction, ref float Flow, int p)
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
                        break;
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

        FlowSprite(Flow, p);
    }

    private void BurstItem(float Flow, Vector2 direction)
    {
        RaycastHit2D[] ItemHit = Physics2D.RaycastAll(WBpos, direction, Flow, ItemLayer);

        foreach (RaycastHit2D Item in ItemHit)
        {
            Destroy(Item.collider.gameObject);
        }
    }
    private void DropItem(Vector2 BlockPosition, int ItemNumber)
    {
        if (ItemNumber == 0)
        {
            Instantiate(SpeedBoost, BlockPosition, Quaternion.identity);
        }
        if (ItemNumber == 1)
        {
            Instantiate(RangeBoost, BlockPosition, Quaternion.identity);
        }
        if (ItemNumber == 2)
        {
            Instantiate(MaxWBBoost, BlockPosition, Quaternion.identity);
        }
    }

    private void FlowSprite(float length, int i)
    {
        int SpriteNumber = Mathf.RoundToInt((length - 0.35f) / 0.7f);

        if(i == 0)
        {
            for(int k = 1; k <= SpriteNumber; k++)
            {
                Vector2 SpritePos = new Vector2((float)(WBpos.x + 0.7 * k), WBpos.y);
                Instantiate(FlowRightPrefab, SpritePos, Quaternion.identity);
            }
        }
        else if (i == 1)
        {
            for (int k = 1; k <= SpriteNumber; k++)
            {
                Vector2 SpritePos = new Vector2((float)(WBpos.x - 0.7 * k), WBpos.y);
                Instantiate(FlowLeftPrefab, SpritePos, Quaternion.identity);
            }
        }
        else if (i == 2)
        {
            for (int k = 1; k <= SpriteNumber; k++)
            {
                Vector2 SpritePos = new Vector2(WBpos.x, (float)(WBpos.y + 0.7 * k));
                Instantiate(FlowUpPrefab, SpritePos, Quaternion.identity);
            }
        }
        else if (i == 3)
        {
            for (int k = 1; k <= SpriteNumber; k++)
            {
                Vector2 SpritePos = new Vector2(WBpos.x, (float)(WBpos.y - 0.7 * k));
                Instantiate(FlowDownPrefab, SpritePos, Quaternion.identity);
            }
        }
    }


    private void WBBurstPlayer()
    {
        List<Collider2D> PlayerHit = new();

        RaycastHit2D[] HitinfoR = Physics2D.BoxCastAll(WBpos, new Vector2(0.64f, 0.64f), 0f, direction[0], FlowAll[0], PlayerLayer);
        RaycastHit2D[] HitinfoL = Physics2D.BoxCastAll(WBpos, new Vector2(0.64f, 0.64f), 0f, direction[1], FlowAll[1], PlayerLayer);
        RaycastHit2D[] HitinfoU = Physics2D.BoxCastAll(WBpos, new Vector2(0.64f, 0.64f), 0f, direction[2], FlowAll[2], PlayerLayer);
        RaycastHit2D[] HitinfoD = Physics2D.BoxCastAll(WBpos, new Vector2(0.64f, 0.64f), 0f, direction[3], FlowAll[3], PlayerLayer);

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
            Collider2D TargetPlayer = PlayerHit[i];

            Controller FlowedPlayer1Info = TargetPlayer.GetComponent<Player1>();
            Controller FlowedPlayer2Info = TargetPlayer.GetComponent<Player2>();

            if (PlayerHit[i].CompareTag("Player1") && FlowedPlayer1Info.Flowed != true)
            {
                MakePlayerFlowed(FlowedPlayer1Info, TargetPlayer);
            }
            else if (PlayerHit[i].CompareTag("Player2") && FlowedPlayer2Info.Flowed != true)
            {
                MakePlayerFlowed(FlowedPlayer2Info, TargetPlayer);
            }
        }
    }

    private void MakePlayerFlowed(Controller FlowedPlayerInfo, Collider2D TargetPlayer)
    {
        FlowedPlayerInfo.Flowed = true;

        float PlayerX = FlowedPlayerInfo.transform.position.x;
        float PlayerY = FlowedPlayerInfo.transform.position.y;
        Vector2 PlayerPos = new Vector2(PlayerX, PlayerY);

        FlowBackground = Instantiate(Flow_Condition, PlayerPos, transform.rotation);
        FlowBackground.transform.parent = TargetPlayer.transform;

        FlowedPlayerInfo.CurrentSpeed = 1;
    }
}
