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

    private void Start()
    {
        StartCoroutine(CountdownToStart());
        countdownText.text = playTime.ToString();

    }
    IEnumerator CountdownToStart()
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
    }

    private void FixedUpdate()
    {
        if (playTime > 0 && startTime <= 0)
        {
            playTime -= Time.deltaTime;
        }
        else if (playTime <= 0)
        {
            Time.timeScale = 0.0f;
        }

        countdownText.text = Mathf.Round(playTime).ToString();

    }
}
