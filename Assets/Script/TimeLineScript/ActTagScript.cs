
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActTagScript : MonoBehaviour
{
    public int ActTag;
    public float ActForce;
    public string ActType;
    public string ActTools;
    public float ActTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ActWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        ActTime = ActWidth/ 10;
        ActTime =(float)Math.Round(ActTime, 3);
    }
}
