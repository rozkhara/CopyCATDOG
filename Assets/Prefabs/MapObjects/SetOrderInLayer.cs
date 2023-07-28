using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrderInLayer : MonoBehaviour
{
    SpriteRenderer SR;
    private bool isBlock = false;
    private bool isPlayer = false;
    private bool isShadow = false;
    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        isBlock = name.Contains("Block");
        isPlayer = name.Contains("Player");
        isShadow = name.Contains("Shadow");
    }
    private void Update()
    {
        SR.sortingOrder = isBlock ? (int)(transform.position.y * -10f - 5f) : -50;
        if (isPlayer)
        {
            SR.sortingOrder = (int)(transform.position.y * -10f);
        }
        if (isShadow)
        {
            SR.sortingOrder = (int)(transform.position.y * -10f - 3f);
        }
    }
}
