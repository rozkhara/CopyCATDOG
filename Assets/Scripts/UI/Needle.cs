using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Needle : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    private Controller player1Info;
    private Controller player2Info;

    private bool IsFind = false;

    public Text Player1Needle;
    public Text Player2Needle;

    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private void Update()
    {
        if (IsFind)
        {
            Player1Needle.text = "x" + player1Info.Needle;
            Player2Needle.text = "x" + player2Info.Needle;
        }
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.01f);

        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");

        player1Info = player1.GetComponent<Player1>();
        player2Info = player2.GetComponent<Player2>();

        IsFind = true;

        yield break;
    }
}
