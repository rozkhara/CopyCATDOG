using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public GameObject[] box;
    public SpawnMap spawnMap;

    private bool Initialized = false;
    private bool AirplaneCoroutineRunning = false;
    private bool[,] isFull = new bool[15, 13];

    public GameObject AirplanePrefab;
    private GameObject Airplaneobj;
    private bool IsAirplaneSpawn = false;
    private Vector2 AirplaneTarget = new Vector2(12, 4);

    //void Start()
    //{
    //    box = GameObject.FindGameObjectsWithTag("CanDestroy");

    //    for (int i = 0; i < box.Length; i++)
    //    {
    //        int xIndex = (int)Mathf.Round((float)(box[i].transform.position.x + 7f) / 0.7f);
    //        int yIndex = (int)Mathf.Round((float)(box[i].transform.position.y + 4.3f) / 0.7f);
    //        isFull[xIndex, yIndex] = true;
    //    }

    //    StartCoroutine(ExecuteAirplane());
    //}

    private void Update()
    {
        if (spawnMap.SpawnComplete && !Initialized)
        {
            box = GameObject.FindGameObjectsWithTag("CanDestroy");

            for (int i = 0; i < box.Length; i++)
            {
                int xIndex = (int)Mathf.Round((float)(box[i].transform.position.x + 7f) / 0.7f);
                int yIndex = (int)Mathf.Round((float)(box[i].transform.position.y + 4.3f) / 0.7f);
                isFull[xIndex, yIndex] = true;
            }

            StartCoroutine(ExecuteAirplane());
            Initialized = true;
        }

        if (IsAirplaneSpawn)
        {
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
        Debug.Log(xIndex + "," + yIndex + " is now empty.");
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
}