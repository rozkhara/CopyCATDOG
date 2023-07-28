using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Hide the tutorial canvas
            SceneManager.LoadScene("Main Map");
        }
    }
}
