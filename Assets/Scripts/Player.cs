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
    public float Velocity = 5f;
    private GameObject FlowBack;

    public bool Flowed_Flag = false;
    public int needle = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayerFlowed");
    }

    IEnumerator PlayerFlowed()
    {
        while (true)
        {
            if (Flowed)
            {
                if (!Flowed_Flag)
                {
                    float PlayerX = transform.position.x;
                    float PlayerY = transform.position.y;
                    Vector2 PlayerPos = new Vector2(PlayerX, PlayerY);
                    FlowBack = Instantiate(Flow_Condition, PlayerPos, transform.rotation);
                    FlowBack.transform.parent = this.transform;
                    Flowed_Flag = true;
                }
                Velocity = 1f;
            }
            else
            {
                Destroy(FlowBack);
                Velocity = 5f;
            }
            yield return null;
        }
    }
}
