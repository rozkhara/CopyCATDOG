using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public int[][] MapInfo = new int[13][];
    public int[][] BlockInfo = new int[13][];

    public bool SpawnComplete = false;

    public Vector2 SpawnReferencePoint = new Vector2(-7f, 4.1f);
    public Vector2 GapBetweenBlocks = new Vector2(0.7f, 0.7f);

    public List<GameObject> Prefabs = new List<GameObject>();

    public enum tileNumber
    {
        GreenFloor = 0,
        RedBrick = 1,
        YellowBrick = 2,
        BlueBrick = 3,
        RedHouse = 4,
        BlueHouse = 5,
        Box = 6,
        Bush = 7,
        Road1 = 8,
        Road2 = 9,
        Road3 = 10,
        Road4 = 11,
        Road5 = 12,
        Empty = 13,
        Tree = 14,
        Count
    }

    private void Start()
    {
        MapInfo[0] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[1] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[2] = new int[15] { 0, 0, 0, 0, 0, 0, 8, 8, 8, 0, 0, 0, 0, 0, 0 };
        MapInfo[3] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[4] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[5] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[6] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[7] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[8] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[9] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[10] = new int[15] { 0, 0, 0, 0, 0, 0, 8, 8, 8, 0, 0, 0, 0, 0, 0 };
        MapInfo[11] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };
        MapInfo[12] = new int[15] { 0, 0, 0, 0, 0, 0, 12, 11, 12, 0, 0, 0, 0, 0, 0 };

        BlockInfo[0] = new int[15] { 13, 2, 1, 2, 1, 7, 13, 13, 6, 7, 5, 1, 5, 13, 5 };
        BlockInfo[1] = new int[15] { 13, 4, 6, 4, 6, 14, 6, 13, 13, 14, 1, 2, 13, 13, 13 };
        BlockInfo[2] = new int[15] { 13, 13, 2, 1, 2, 7, 13, 6, 6, 7, 5, 6, 5, 6, 5 };
        BlockInfo[3] = new int[15] { 6, 4, 6, 4, 6, 14, 6, 13, 13, 14, 2, 1, 2, 1, 2 };
        BlockInfo[4] = new int[15] { 1, 2, 1, 2, 1, 7, 13, 13, 6, 7, 5, 6, 5, 6, 5 };
        BlockInfo[5] = new int[15] { 2, 4, 2, 4, 2, 14, 6, 6, 13, 13, 1, 2, 1, 2, 1 };
        BlockInfo[6] = new int[15] { 14, 7, 14, 7, 14, 7, 13, 13, 6, 7, 14, 7, 14, 7, 14 };
        BlockInfo[7] = new int[15] { 1, 2, 1, 2, 1, 13, 6, 13, 13, 14, 1, 4, 1, 4, 1 };
        BlockInfo[8] = new int[15] { 5, 6, 5, 6, 5, 7, 13, 6, 6, 7, 2, 1, 2, 1, 2 };
        BlockInfo[9] = new int[15] { 2, 1, 2, 1, 2, 14, 6, 13, 13, 14, 6, 4, 6, 4, 6 };
        BlockInfo[10] = new int[15] { 5, 13, 5, 6, 5, 7, 13, 13, 6, 7, 1, 2, 1, 2, 13 };
        BlockInfo[11] = new int[15] { 13, 13, 1, 2, 1, 14, 6, 6, 13, 14, 6, 4, 6, 4, 13 };
        BlockInfo[12] = new int[15] { 5, 13, 5, 1, 5, 7, 13, 13, 6, 7, 2, 1, 2, 13, 13 };

        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (MapInfo[i][j] < 13)
                {
                    Instantiate(Prefabs[MapInfo[i][j]], new Vector2(SpawnReferencePoint.x + GapBetweenBlocks.x * j, SpawnReferencePoint.y - GapBetweenBlocks.y * i), Quaternion.identity);
                }
                if (BlockInfo[i][j] < 13)
                {
                    Instantiate(Prefabs[BlockInfo[i][j]], new Vector2(SpawnReferencePoint.x + GapBetweenBlocks.x * j, SpawnReferencePoint.y - GapBetweenBlocks.y * i), Quaternion.identity);
                }
            }
        }
        SpawnComplete = true;
    }

}
