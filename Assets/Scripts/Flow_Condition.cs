using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flow_Condition : MonoBehaviour
{
    private int PlayerLayer = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                EndSetting(other.gameObject, transform.parent.gameObject);
            }
            else
            {
                EndSetting(transform.parent.gameObject, other.gameObject);
            }

            StartCoroutine(MovetoEndScene());
        }
    }

    private void EndSetting(GameObject Player1, GameObject Player2)
    {
        Controller Player1Info = Player1.GetComponent<Player1>();
        Controller Player2Info = Player2.GetComponent<Player2>();

        Player1Info.MaxBomb = 0;
        Player1Info.CurrentSpeed = 0;
        Player2Info.MaxBomb = 0;
        Player2Info.CurrentSpeed = 0;
    }

    private IEnumerator MovetoEndScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("End");
        yield break;
    }
}

