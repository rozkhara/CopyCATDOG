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

    private bool bar1pCanMove = false;
    private bool bar2pCanMove = false;

    private Vector3 gapSize = new Vector3(7f, 0f, 0f);

    private IEnumerator BarMove(int barnum, Vector3 target)
    {
        if (barnum == 1)
        {
            while (Vector3.Distance(selectBar1p.transform.position, target) > 0.05f)
            {
                selectBar1p.transform.position = Vector3.Lerp(selectBar1p.transform.position, target, 10 * Time.deltaTime);
                yield return null;
            }
            selectBar1p.transform.position = target;
            bar1pCanMove = true;
            yield return null;
        }

        if (barnum == 2)
        {
            while (Vector3.Distance(selectBar2p.transform.position, target) > 0.05f)
            {
                selectBar2p.transform.position = Vector3.Lerp(selectBar2p.transform.position, target, 10 * Time.deltaTime);
                yield return null;
            }
            selectBar2p.transform.position = target;
            bar2pCanMove = true;
            yield return null;
        }

    }


    private void Start()
    {
        current1pPos = selectBar1p.transform.position;
        current2pPos = selectBar2p.transform.position;
        next1pPos = current1pPos;
        next2pPos = current2pPos;
        bar1pCanMove = true;
        bar2pCanMove = true;
    }


    private void Update()
    {
        if (!ready1p && bar1pCanMove)
        {
            if (current1pCharacter < 3 && Input.GetKeyDown(KeyCode.D))
            {
                current1pPos = selectBar1p.transform.position;
                next1pPos = current1pPos + gapSize;
                bar1pCanMove = false;
                StartCoroutine(BarMove(1, next1pPos));
                current1pCharacter++;
            }

            if (current1pCharacter > 1 && Input.GetKeyDown(KeyCode.A))
            {
                current1pPos = selectBar1p.transform.position;
                next1pPos = current1pPos - gapSize;
                bar1pCanMove = false;
                StartCoroutine(BarMove(1, next1pPos));
                current1pCharacter--;
            }
        }

        if (!ready2p && bar2pCanMove)
        {
            if (current2pCharacter < 3 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                current2pPos = selectBar2p.transform.position;
                next2pPos = current2pPos + gapSize;
                bar2pCanMove = false;
                StartCoroutine(BarMove(2, next2pPos));
                current2pCharacter++;
            }

            if (current2pCharacter > 1 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current2pPos = selectBar2p.transform.position;
                next2pPos = current2pPos - gapSize;
                bar2pCanMove = false;
                StartCoroutine(BarMove(2, next2pPos));
                current2pCharacter--;
            }
        }
    }

    public void Set1pReady()
    {
        ready1p = true;
    }

    public void Set2pReady()
    {
        ready2p = true;
    }

}
