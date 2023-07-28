using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToMainMap : MonoBehaviour
{
    [SerializeField]
    private GameObject player1Bar;
    [SerializeField]
    private GameObject player2Bar;

    private int startTime = 5;

    private bool running = false;

    [SerializeField]
    private Text startCountdownText;

    public GameObject HowToPlay;

    void Start()
    {
        startCountdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if(player1Bar.GetComponent<select1p>().ready1p && player2Bar.GetComponent<select2p>().ready2p)
        {
            if (!running)
            {
                startCountdownText.gameObject.SetActive(true);
                StartCoroutine(CountDownNew());
            }
        }
        else
        {
            if(running)
            {
                running = false;
            }
        }
        
    }

    private IEnumerator CountDownNew()
    {
        startTime = 5;
        float remainingTime = (float)startTime;
        running = true;
        while (running)
        {
            startCountdownText.text = startTime.ToString();
            remainingTime -= Time.unscaledDeltaTime;
            startTime = (int)Mathf.Ceil(remainingTime);
            if(startTime == 0)
            {
                startCountdownText.text = "GO!";
                yield return new WaitForSeconds(0.5f);
                HowToPlay.SetActive(true);
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        startCountdownText.gameObject.SetActive(false);
        yield return null;
    }
}
