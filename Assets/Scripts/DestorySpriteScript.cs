using System.Collections;
using UnityEngine;

public class DestroySpriteScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroySprite());
    }

    private IEnumerator DestroySprite()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        yield break;
    }
}
