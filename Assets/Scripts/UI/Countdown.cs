using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float setTime = 10.0f;
    private int startTime = 3;

    [SerializeField]
    Text countdownText;


    public Text startCountdownText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
        countdownText.text = setTime.ToString();

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
    }

    void Update()
    {
        if (setTime > 0 && startTime <= 0)
            setTime -= Time.deltaTime;
        else if (setTime <= 0)
            Time.timeScale = 0.0f;

        countdownText.text = Mathf.Round(setTime).ToString();

    }
}
