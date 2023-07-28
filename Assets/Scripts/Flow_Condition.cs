using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flow_Condition : MonoBehaviour
{
    private int PlayerLayer = 3;

    private Animator anim;
    private Controller parentController;

    private void Start()
    {
        if (transform.parent.CompareTag("Player1"))
        {
            parentController = transform.parent.gameObject.GetComponent<Player1>();
        }
        else
        {
            parentController = transform.parent.gameObject.GetComponent<Player2>();
        }
        anim = transform.parent.gameObject.GetComponent<Animator>();
        StartCoroutine(PlayerDie());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            parentController.Flowed = false;
            anim.SetBool("Dead", true);
            parentController.Dead = true;
            StartCoroutine(MovetoEndScene());
        }
    }

    private void EndSetting()
    {
        parentController.CurrentSpeed = 0;
        parentController.MaxBomb = 0;
    }

    private IEnumerator MovetoEndScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("End");
        yield break;
    }

    private IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(5f);
        if (parentController.Flowed)
        {
            EndSetting();
            parentController.Flowed = false;
            anim.SetBool("Dead", true);
            parentController.Dead = true;
            StartCoroutine(MovetoEndScene());
        }
    }
}

