using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    public Transform MapGrid;

    [SerializeField]
    private int mapWidth = 15;
    [SerializeField]
    private int mapHeight = 13;

    public List<GameObject> mapTiles = new List<GameObject>();

    private void Start()
    {
        GenerateMap();
    }


    private void GenerateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);

                mapTiles.Add(newTile);
                newTile.transform.SetParent(MapGrid.transform, false);
                newTile.transform.localPosition = new Vector3(x * 0.7f, y * 0.7f, 0);
                newTile.name = "MapTile" + x + " " + y;
            }
        }
    }
}
