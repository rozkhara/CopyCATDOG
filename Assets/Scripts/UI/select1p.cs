using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select1p : MonoBehaviour
{
    public static int CurrentCharacter1p { get; set; }

    public int CurState = 0;
    public int NextXState = 0;
    public int NextYState = 0;

    public int IsRunning = 0;

    public bool ready1p = false;

    private Color Color1p = new Color32(255, 184, 184, 255);

    private Vector2 ReferencePosition = new Vector2(-21f, 3.3f);
    private Vector2 GapX = new Vector2(8f, 0f);
    private Vector2 GapY = new Vector2(0f, -6f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Set1pReady();

        }
        if (!ready1p)
        {
            if (Input.GetKeyDown(KeyCode.D) && NextXState < 2)
            {
                NextXState++;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.A) && NextXState > 0)
            {
                NextXState--;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.W) && NextYState > 0)
            {
                NextYState--;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.S) && NextYState < 2)
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
        while (elapsedTime < 0.2f)
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
        ready1p = !ready1p;
        if (ready1p)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
            CurrentCharacter1p = CurState;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color1p;
        }

    }
}
