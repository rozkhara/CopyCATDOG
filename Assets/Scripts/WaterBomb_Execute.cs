using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBomb_Execute : MonoBehaviour
{
    private float StartTime;
    private float CurrentTime;

    private Vector2 WBpos;
    public float FlowLength;

    public GameObject Flow_Center;

    // Start is called before the first frame update

    void Awake()
    {
        WBpos = new Vector2(transform.position.x, transform.position.y);
        Flow_Execute WBInfo = Flow_Center.GetComponent<Flow_Execute>();
        WBInfo.FlowLength = FlowLength;
    }

    void Start()
    {
        StartTime = Time.time;
        StartCoroutine("WBExceed");
    }

    IEnumerator WBExceed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            CurrentTime = Time.time - StartTime;
            if (CurrentTime >= 3)
            {
                Instantiate(Flow_Center, WBpos, transform.rotation);
                Destroy(gameObject);
                yield break;
            }
        }
    }
}
