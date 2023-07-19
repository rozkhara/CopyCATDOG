using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickCharacter : MonoBehaviour
{
    public GameObject selectBar1p;
    public GameObject selectBar2p;

    private int current1pCharacter = 1; //character number
    private int current2pCharacter = 1;

    private Vector3 current1pPos;
    private Vector3 current2pPos;
    private Vector3 next1pPos;
    private Vector3 next2pPos;

    private bool ready1p = false;
    private bool ready2p = false;

    public IEnumerator Bar1pMove(Vector3 target)
    {
        while (Vector3.Distance(selectBar1p.transform.position, target) > 0.05f)
        {
            selectBar1p.transform.position = Vector3.Lerp(selectBar1p.transform.position, target, 10 * Time.deltaTime);
            yield return null;
        }
        selectBar1p.transform.position = target;
        yield break;
    }

    public IEnumerator Bar2pMove(Vector3 target)
    {
        while (Vector3.Distance(selectBar2p.transform.position, target) > 0.05f)
        {
            selectBar2p.transform.position = Vector3.Lerp(selectBar2p.transform.position, target, 10 * Time.deltaTime);
            yield return null;
        }
        selectBar2p.transform.position = target;
        yield break;
    }

    private void Start()
    {
        current1pPos = selectBar1p.transform.position;
        current2pPos = selectBar2p.transform.position;
        next1pPos = current1pPos;
        next2pPos = current2pPos;
        Debug.Log(current1pPos);
        Debug.Log(current2pPos);
    }


    private void Update()
    {
        if (!ready1p && Vector3.Distance(selectBar1p.transform.position, next1pPos) <= 0.05f)
        {
            if (current1pCharacter < 3 && Input.GetKeyDown(KeyCode.D))
            {
                current1pPos = selectBar1p.transform.position;
                next1pPos = current1pPos + new Vector3(7, 0, 0);
                StartCoroutine(Bar1pMove(next1pPos));
                current1pCharacter++;

            }

            if (current1pCharacter > 1 && Input.GetKeyDown(KeyCode.A))
            {
                current1pPos = selectBar1p.transform.position;
                next1pPos = current1pPos + new Vector3(-7, 0, 0);
                StartCoroutine(Bar1pMove(next1pPos));
                current1pCharacter--;
            }
        }

        if (!ready2p && Vector3.Distance(selectBar2p.transform.position, next2pPos) <= 0.05f)
        {
            if (current2pCharacter < 3 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                current2pPos = selectBar2p.transform.position;
                next2pPos = current2pPos + new Vector3(7, 0, 0);
                StartCoroutine(Bar2pMove(next2pPos));
                current2pCharacter++;
            }

            if (current2pCharacter > 1 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current2pPos = selectBar2p.transform.position;
                next2pPos = current2pPos + new Vector3(-7, 0, 0);
                StartCoroutine(Bar2pMove(next2pPos));
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
