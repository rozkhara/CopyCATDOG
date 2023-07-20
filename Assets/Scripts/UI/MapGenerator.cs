using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    public Transform MapGrid;

    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;

    public List<GameObject> mapTiles = new List<GameObject>();

    private void Start()
    {
        generateMap();
    }

    private void Update()
    {
       
    }

    private void generateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);

                mapTiles.Add(newTile);
                newTile.transform.SetParent(MapGrid.transform, false);
                newTile.transform.localPosition = new Vector3(x * 0.7f, y * 0.7f, 0);
                newTile.name = "MapTile" + (x + 1) + " " + (y + 1);
            }
        }
        Debug.Log("Tile number:  " + mapTiles.Count);
    }


    /*public void FindAndTransformObject(GameObject targetObject)
    {
        Vector2 targetPosition = targetObject.transform.position;
        Transform nearestObject = null;

        float shortestDistance = Mathf.Infinity;

        for (int y = 0; y < mapTiles.Count; y++)
        {
            Transform tilePosition = mapTiles[y].transform;
            float distance = Vector2.Distance(targetPosition, tilePosition.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestObject = tilePosition;
            }
        }

        *//*foreach (Transform otherObject in otherObjects)
        {
            float distance = Vector2.Distance(targetPosition, otherObject.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestObject = otherObject;
            }
        }*//*

    }*/


}
