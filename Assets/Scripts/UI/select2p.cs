using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select2p : MonoBehaviour
{
    public int CurState = 0;
    public int NextXState = 0;
    public int NextYState = 0;

    public int IsRunning = 0;

    public bool ready2p = false;

    private Color Color2p = new Color32(184, 200, 255, 255);

    private Vector2 ReferencePosition = new Vector2(4.8f, 3.3f);
    private Vector2 GapX = new Vector2(8f, 0f);
    private Vector2 GapY = new Vector2(0f, -6f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Set1pReady();

        }
        if (!ready2p)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && NextXState < 2)
            {
                NextXState++;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && NextXState > 0)
            {
                NextXState--;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && NextYState > 0)
            {
                NextYState--;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && NextYState < 2)
            {
                NextYState++;
                StartCoroutine(BarTranslate());
            }

        }

    }

    private IEnumerator BarTranslate()
    {
        IsRunning++;
        if (IsRunning > 1)
        {
            yield return new WaitForEndOfFrame();
        }
        float elapsedTime = 0f;
        Vector3 startPos = this.transform.position;
        while (elapsedTime < 0.5f)
        {
            if (IsRunning > 1)
            {
                //CurState = NextState;
                IsRunning--;
                yield break;
            }
            this.transform.position = Vector3.Lerp(startPos, ReferencePosition + GapX * NextXState + GapY * NextYState, elapsedTime / 0.2f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CurState = NextXState + NextYState * 3;
        IsRunning--;
        yield return null;
    }

    public void Set1pReady()
    {
        ready2p = !ready2p;
        if (ready2p)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color2p;
        }

    }
}
