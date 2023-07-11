using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{
    public GameObject selectBar1p;
    public GameObject selectBar2p;


    private int current1pPosition = 1;
    private int current2pPosition = 1;
    private float current1pPosx = -17f;
    private float current2pPosx = -17f;
    private bool ready1p = false;
    private bool ready2p = false;



    private void Update()
    {
        /*if (Input.GetButton("1PReady"))
        {
            ready1p = true;
            Debug.Log("1P ready");
        }

        if (Input.GetButton("2PReady"))
        {
            ready2p = true;
            Debug.Log("2P ready");
        }*/

        if (!ready1p)
        {
            if (current1pPosition < 3 && Input.GetKeyDown(KeyCode.D))
            {
                current1pPosx = selectBar1p.transform.position.x;
                selectBar1p.transform.position = new Vector3(current1pPosx + 7, 0, 0);
                current1pPosition++;
            }

            if (current1pPosition > 1 && Input.GetKeyDown(KeyCode.A))
            {
                current1pPosx = selectBar1p.transform.position.x;
                selectBar1p.transform.position = new Vector3(current1pPosx - 7, 0, 0);
                current1pPosition--;
            }
        }

        if (!ready2p)
        {
            if (current2pPosition < 3 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                current2pPosx = selectBar2p.transform.position.x;
                selectBar2p.transform.position = new Vector3(current2pPosx + 7, 0, 0);
                current2pPosition++;
            }

            if (current2pPosition > 1 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current2pPosx = selectBar2p.transform.position.x;
                selectBar2p.transform.position = new Vector3(current2pPosx - 7, 0, 0);
                current2pPosition--;
            }
        }
    }

    public void Is1pReady()
    {
        ready1p = true;
        Debug.Log("1P ready: " + current1pPosition + " selected.");
    }

    public void Is2pReady()
    {
        ready2p = true;
        Debug.Log("2P ready: " + current2pPosition + " selected.");
    }
}


