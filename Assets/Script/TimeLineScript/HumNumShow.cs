using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumNumShow : MonoBehaviour
{
    public GameObject DSHumanText2;
    public GameObject DSHumanText3;

    public GameObject DSHumanTimeText2;
    public GameObject DSHumanTimeText3;
   
    public GameObject DSTrack2;
    public GameObject DSTrack3;

    /// <summary>
    /// 改变数字人数量时改变动素层UI
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        if (HumanNumber.humanNum == 1)
        {
            DSHumanText2.SetActive(false);
            DSTrack2.SetActive(false);
            DSHumanTimeText2.SetActive(false);

            DSHumanText3.SetActive(false);
            DSTrack3.SetActive(false);            
            DSHumanTimeText3.SetActive(false);

        }
        else if (HumanNumber.humanNum == 2)
        {
            DSHumanText3.SetActive(false);
            DSTrack3.SetActive(false);
            DSHumanTimeText3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
