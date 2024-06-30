using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeShowMain : MonoBehaviour
{
    private float spendTime;
    private int hour;
    private int minute;
    private int second;
    private int milliSecond;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.fullScreen = true;
        //Screen.SetResolution(600, 400, false);
    }

    // Update is called once per frame
    void Update()
    {
        spendTime += Time.deltaTime;
        hour = (int)spendTime / 3600;
        minute = (int)(spendTime - hour * 3600) / 60;
        second = (int)(spendTime - hour * 3600 - minute * 60);
        if (second == 1)
        {
            SceneManager.LoadScene(1);
            //Screen.SetResolution(1920, 1080, false);
            //Screen.fullScreen = true;
        }
    }
}
