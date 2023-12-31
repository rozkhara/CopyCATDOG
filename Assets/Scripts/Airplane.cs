using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Airplane : MonoBehaviour
{
    public GameObject[] canDestroyBlock;
    public GameObject[] cannnotDestroyBlock;
    public List<GameObject> BlockList = new List<GameObject>();
    public SpawnMap spawnMap;

    private bool Initialized = false;
    private bool AirplaneCoroutineRunning = false;
    private bool[,] isFull = new bool[15, 13];

    public GameObject AirplanePrefab;
    private GameObject Airplaneobj;
    private bool IsAirplaneSpawn = false;
    private Vector2 AirplaneTarget = new Vector2(12, 4);

    public GameObject SpeedBoost;
    public GameObject RangeBoost;
    public GameObject MaxWBBoost;
    private float[] ItemPercent = new float[] { 35,35,30 };

    private Vector2 randomFreePos;
    private int xRandom;
    private int yRandom;
    private bool IsItemDropped = false;

    private void Update()
    {
        if (spawnMap.SpawnComplete && !Initialized)
        {
            canDestroyBlock = GameObject.FindGameObjectsWithTag("CanDestroy");
            cannnotDestroyBlock = GameObject.FindGameObjectsWithTag("CannotDestroy");
            List<GameObject> boxList1 = canDestroyBlock.ToList();
            List<GameObject> boxList2 = cannnotDestroyBlock.ToList();

            BlockList.AddRange(boxList1);
            BlockList.AddRange(boxList2);
            
            for (int i = 0; i < BlockList.Count; i++)
            {
                int xIndex = (int)Mathf.Round((float)(BlockList[i].transform.position.x + 7f) / 0.7f);
                int yIndex = (int)Mathf.Round((float)(BlockList[i].transform.position.y + 4.3f) / 0.7f);
                isFull[xIndex, yIndex] = true;
            }

            StartCoroutine(ExecuteAirplane());
            Initialized = true;
        }

        if (IsAirplaneSpawn)
        {
            if (!IsItemDropped)
            {
                StartCoroutine(AirplaneDrop());
            }
            if (!AirplaneCoroutineRunning)
            {
                StartCoroutine(MoveAirPlane());
            }
        }
    }

    private IEnumerator MoveAirPlane()
    {
        AirplaneCoroutineRunning = true;
        SoundManager.Instance.PlaySFXSound("Airplane", 0.1f);
        while (true)
        {
            Airplaneobj.transform.position = Vector2.MoveTowards(Airplaneobj.transform.position, AirplaneTarget, 0.03f);
            
            if (Airplaneobj.transform.position.x == AirplaneTarget.x)
            {
                IsAirplaneSpawn = false;
                Destroy(Airplaneobj);
                AirplaneCoroutineRunning = false;
                IsItemDropped = false;
                yield return new WaitForEndOfFrame();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void MakeEmpty(Transform transform)
    {
        int xIndex = (int)Mathf.Round((float)(transform.position.x + 7f) / 0.7f);
        int yIndex = (int)Mathf.Round((float)(transform.position.y + 4.3f) / 0.7f);
        isFull[xIndex, yIndex] = false;
    }

    private IEnumerator ExecuteAirplane()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            Airplaneobj = Instantiate(AirplanePrefab, new Vector2(-12, 4), Quaternion.identity);
            IsAirplaneSpawn = true;
        }
    }

    private IEnumerator AirplaneDrop()
    {
        if (!IsItemDropped)
        {
            float RandomPoint = Random.value * 100;
            for (int i = 0; i < ItemPercent.Length; i++)
            {
                if (RandomPoint < ItemPercent[i])
                {
                    randomFreePos = RandomPosition();
                    IsItemDropped = true;
                    yield return new WaitForSeconds(2f);
                    DropItem(randomFreePos, i);
                    break;
                }
                else
                {
                    RandomPoint -= ItemPercent[i];
                }
            }
        }

        yield break;
    }

    private void DropItem(Vector2 FreePosition, int ItemNumber)
    {
        if (ItemNumber == 0)
        {
            Instantiate(SpeedBoost, FreePosition, Quaternion.identity);
        }
        if (ItemNumber == 1)
        {
            Instantiate(RangeBoost, FreePosition, Quaternion.identity);
        }
        if (ItemNumber == 2)
        {
            Instantiate(MaxWBBoost, FreePosition, Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        do
        {
            xRandom = Random.Range(0, 14);
            yRandom = Random.Range(0, 12);
        } while (isFull[xRandom, yRandom]);
        isFull[xRandom, yRandom] = true;
        Debug.Log(xRandom + "," + yRandom + " is now full");
        return new Vector2(xRandom * 0.7f - 7f, yRandom * 0.7f - 4.3f);
    }

}