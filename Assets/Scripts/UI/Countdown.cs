using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float playTime = 10.0f;
    private int startTime = 3;

    [SerializeField]
    private Text countdownText;

    [SerializeField]
    private Text startCountdownText;

    [SerializeField]
    private GameObject indicator;

    private void Start()
    {
        StartCoroutine(CountDownNew());
        countdownText.text = playTime.ToString();
    }

    /*IEnumerator CountdownToStart()
    {
        while (startTime > 0)
        {
            startCountdownText.text = startTime.ToString();

            yield return new WaitForSeconds(1f);

            startTime--;
        }

        startCountdownText.text = "GO!";

        yield return new WaitForSeconds(0.5f);

        startCountdownText.gameObject.SetActive(false);
        yield return null;
    }*/

    private IEnumerator CountDownNew()
    {

            Time.timeScale = 0f;
            startCountdownText.text = startTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            startTime--;
            startCountdownText.text = startTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            startTime--;
            startCountdownText.text = startTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            startCountdownText.text = "GO!";
            startTime--;
            Time.timeScale = 1f;
            indicator.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
            startCountdownText.gameObject.SetActive(false);
            yield break;
    }

    private void FixedUpdate()
    {
        if (playTime > 0 && startTime <= 0)
        {
            playTime -= Time.deltaTime;
            countdownText.text = Mathf.Round(playTime).ToString();
        }
        else if (playTime <= 0)
        {
            playTime = 0;
            countdownText.text = playTime.ToString();
        }
    }
}
