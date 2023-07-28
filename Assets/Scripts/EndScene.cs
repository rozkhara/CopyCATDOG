using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public Text WinPlayerText;
    public Text LosePlayerText;

    private string WinPlayer;
    private string LosePlayer;

    public void WinandLose(string WinName, string LoseName)
    {
        WinPlayer = WinName;
        LosePlayer = LoseName;
    }

    private void Start()
    {
    }
}
