using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Flow_Condition : MonoBehaviour
{
    private string ParentTag;
    private Vector2 ParentPosition;

    // Start is called before the first frame update
    void Awake()
    {
        ParentTag = transform.GetComponentInParent<Transform>().tag;
        ParentPosition = transform.GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ParentPosition;
        Debug.Log(ParentTag);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 128)
        {
            if (!collision.CompareTag(ParentTag))
                Debug.Log("Die!");
        }
    }
}

