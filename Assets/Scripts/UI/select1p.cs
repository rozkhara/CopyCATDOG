using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select1p : MonoBehaviour
{
    public int CurState = 0;
    public int NextState = 0;

    public int IsRunning = 0;

    private bool ready1p = false;


    private Vector2 ReferencePosition = new Vector2(-19.33333f, 0f);
    private Vector2 Gap = new Vector2(7f, 0f);

    private void Update()
    {
        if (!ready1p)
        {
            if (Input.GetKeyDown(KeyCode.D) && NextState < 2)
            {
                NextState++;
                StartCoroutine(BarTranslate());
            }
            if (Input.GetKeyDown(KeyCode.A) && NextState > 0)
            {
                NextState--;
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
            this.transform.position = Vector3.Lerp(startPos, ReferencePosition + Gap * NextState, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CurState = NextState;
        IsRunning--;
        yield return null;
    }

    public void Set1pReady()
    {
        ready1p = true;
    }
}
