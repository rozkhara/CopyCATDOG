using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flow_Condition : MonoBehaviour
{
    private int PlayerLayer = 3;

    private Animator anim;
    private Controller parentController;

    private bool Isplayer1;

    private void Start()
    {
        if (transform.parent.CompareTag("Player1"))
        {
            parentController = transform.parent.gameObject.GetComponent<Player1>();
            Isplayer1 = true;
        }
        else
        {
            parentController = transform.parent.gameObject.GetComponent<Player2>();
            Isplayer1 = false;
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
        parentController.BombCount = 0;
    }

    private IEnumerator MovetoEndScene()
    {
        yield return new WaitForSeconds(1.5f);
        if (Isplayer1)
        {
            SoundManager.Instance.StopBGMSound();
            SoundManager.Instance.PlaySFXSound("Win", 0.5f);
            SceneManager.LoadScene("End2");
        }
        else
        {
            SoundManager.Instance.StopBGMSound();
            SoundManager.Instance.PlaySFXSound("Win", 0.5f);
            SceneManager.LoadScene("End1");
        }
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

