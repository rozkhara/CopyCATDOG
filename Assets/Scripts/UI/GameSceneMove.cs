using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public void ShowTutorial()
    {
        tutorialCanvas.SetActive(true);
    }
    
}
