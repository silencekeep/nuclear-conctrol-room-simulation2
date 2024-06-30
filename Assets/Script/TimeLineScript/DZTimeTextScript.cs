using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DZTimeTextScript : MonoBehaviour
{/// <summary>
/// 自动更新动作层每个数字人的动作时间总和
/// </summary>
    public GameObject Track;
    public Text Timetext;
    private float CurtimeLength;
    private float TrackWidth;
    // Start is called before the first frame update
    void Start()
    {
        TrackWidth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Track.transform.childCount; i++)
        {
            Transform TrackChildi = Track.transform.GetChild(i);
            if (TrackChildi.name != "Image Empty(Clone)")
            {
                TrackWidth = TrackWidth + TrackChildi.GetComponent<RectTransform>().sizeDelta.x;
            }
        }
        CurtimeLength = TrackWidth ;
        //print(CurtimeLength);
        Timetext.text = CurtimeLength.ToString("#0.000");
        TrackWidth = 0;
    }
}
