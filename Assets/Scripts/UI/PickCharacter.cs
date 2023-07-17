using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCharacter : MonoBehaviour
{
    public GameObject selectBar1p;
    public GameObject selectBar2p;

    private int current1pCharacter = 1; //character number
    private int current2pCharacter = 1;

    private float current1pPosx = -17f;
    private float current2pPosx = -17f;
    private Vector3 current1pPos = Vector3.zero;
    private Vector3 current2pPos = Vector3.zero;
    private Vector3 next1pPos = Vector3.zero;
    private Vector3 next2pPos = Vector3.zero;

    private bool ready1p = false;
    private bool ready2p = false;

    private void Start()
    {
        current1pPos = selectBar1p.transform.position;
        current2pPos = selectBar2p.transform.position;
        StartCoroutine(SmoothMove());
    }


    IEnumerator SmoothMove()
    {
        if (!ready1p)
        {
            if (current1pCharacter < 3 && Input.GetKeyDown(KeyCode.D))
            {

                current1pPosx = current1pPos.x;
                /*selectBar1p.transform.position = new Vector3(current1pPosx + 7, 0, 0);*/

                next1pPos = new Vector3(current1pPosx + 7, 0, 0);
                selectBar1p.transform.position = Vector3.Lerp(current1pPos, new Vector3(-17,0,-900), Time.deltaTime);
                yield return new WaitForSeconds(1f);
                current1pPos = next1pPos;
                current1pCharacter++;
            }

            if (current1pCharacter > 1 && Input.GetKeyDown(KeyCode.A))
            {
                current1pPosx = current1pPos.x;
                /*selectBar1p.transform.position = new Vector3(current1pPosx - 7, 0, 0);*/

                next1pPos = new Vector3(current1pPosx - 7, 0, 0);
                selectBar1p.transform.position = Vector3.Lerp(current1pPos, next1pPos, Time.deltaTime);
                current1pPos = next1pPos;
                current1pCharacter--;
            }
        }

        if (!ready2p)
        {
            if (current2pCharacter < 3 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                current2pPosx = current2pPos.x;
                /*selectBar2p.transform.position = new Vector3(current2pPosx + 7, 0, 0);*/

                next2pPos = new Vector3(current2pPosx + 7, 0, 0);
                selectBar2p.transform.position = Vector3.Lerp(current2pPos, next2pPos, 1f);
                current2pPos = next2pPos;
                current2pCharacter++;
            }

            if (current2pCharacter > 1 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current2pPosx = current2pPos.x;
                /*selectBar2p.transform.position = new Vector3(current2pPosx - 7, 0, 0);*/

                next2pPos = new Vector3(current2pPosx - 7, 0, 0);
                selectBar2p.transform.position = Vector3.Lerp(current2pPos, next2pPos, 1f);
                current2pPos = next2pPos;
                current2pCharacter--;
            }
        }
    }

    public void Is1pReady()
    {
        ready1p = true;
        Debug.Log("1P ready: " + current1pCharacter + " selected.");
    }

    public void Is2pReady()
    {
        ready2p = true;
        Debug.Log("2P ready: " + current2pCharacter + " selected.");
    }
}


