using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Flow_Condition : MonoBehaviour
{
    private Collider2D Flow;
    private ContactFilter2D PlayerFilter;
    private LayerMask PlayerLayer = 8;
    private Collider2D[] Results = new Collider2D[2];

    private int IsOverlap;

    // Start is called before the first frame update
    void Awake()
    {
        Flow = GetComponent<Collider2D>();
        PlayerFilter.SetLayerMask(PlayerLayer);
    }

    // Update is called once per frame
    void Update()
    {
        OverlapPlayer();
    }

    private void OverlapPlayer()
    {
        IsOverlap = Physics2D.OverlapCollider(Flow, PlayerFilter, Results);

        if(IsOverlap > 1)
        {
            Debug.Log("Overlap");
        }
    }
}

