using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Hide the tutorial canvas
            SoundManager.Instance.PlayBGMSound(0.1f);
            SceneManager.LoadScene("Start");
        }
    }
}
