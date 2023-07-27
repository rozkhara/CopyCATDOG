using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public GameObject[] box;
    private bool[,] isFull = new bool[15, 13];

    void Start()
    {
        for(int i=0; i<box.Length; i++)
        {
            int xIndex = (int)Mathf.Round((float)(box[i].transform.position.x + 7f) / 0.7f);
            int yIndex = (int)Mathf.Round((float)(box[i].transform.position.y + 4.3f) / 0.7f);
            isFull[xIndex, yIndex] = true;
        }
    }

    public void MakeEmpty(Transform transform)
    {
        int xIndex = (int)Mathf.Round((float)(transform.position.x + 7f) / 0.7f);
        int yIndex = (int)Mathf.Round((float)(transform.position.y + 4.3f) / 0.7f);
        isFull[xIndex, yIndex] = false;
        Debug.Log(xIndex +","+ yIndex + " is now empty.");
    }
}
