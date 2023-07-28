using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
    public void GameScenesCtrl()
    {
        SceneManager.LoadScene("Pick_New");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
