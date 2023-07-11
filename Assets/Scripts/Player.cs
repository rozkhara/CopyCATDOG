using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    public bool Flowed = false;
    public GameObject Flow_Condition;
    public int FlowLength = 10;
    public float Velocity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayerFlowed");
    }

    IEnumerator PlayerFlowed()
    {
        while (true)
        {
            if (Flowed == true)
            {
                float PlayerX = transform.position.x;
                float PlayerY = transform.position.y;
                Vector2 PlayerPos = new Vector2(PlayerX, PlayerY);
                Instantiate(Flow_Condition, PlayerPos, transform.rotation).transform.parent = this.transform;
                yield break;
            }
            yield return null;
        }
    }
}
