using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDZTreeView : MonoBehaviour
{
    public GameObject DZTrack1;
    public GameObject DZTrack2;
    public GameObject DZTrack3;
    public GameObject ZBstage;
    public GameObject SSstage;
    public GameObject CSstage;
   
    List<string> ZBStageDZText1 = new List<string>();
    List<string> SSStageDZText1 = new List<string>();
    List<string> CSStageDZText1 = new List<string>();
    List<string> ZBStageDZText2 = new List<string>();
    List<string> SSStageDZText2 = new List<string>();
    List<string> CSStageDZText2 = new List<string>();
    List<string> ZBStageDZText3 = new List<string>();
    List<string> SSStageDZText3 = new List<string>();
    List<string> CSStageDZText3 = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnDZChange()//将动作层对应阶段的动作存入相应数组，赋值给treeview
    {
        ZBStageDZText1.Clear();
        SSStageDZText1.Clear();
        CSStageDZText1.Clear();
        ZBStageDZText2.Clear();
        SSStageDZText2.Clear();
        CSStageDZText2.Clear();
        ZBStageDZText3.Clear();
        SSStageDZText3.Clear();
        CSStageDZText3.Clear();
        int j = 0; int k = 0; int m = 0;
        for (int i = 0; i < DZTrack1.transform.childCount; i++)//DZTrack1
        {
           
            Transform TrackChildi = DZTrack1.transform.GetChild(i);
            if (TrackChildi.name != "Image Empty(Clone)")
            {
                DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                if (DZStageTag.DZStageTag == "准备阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    ZBStageDZText1.Add(DZText);
                    ZBstage.transform.GetChild(0).transform.GetChild(j).name = ZBStageDZText1[j];
                    j = j + 1;
                }
                else if (DZStageTag.DZStageTag == "实施阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    SSStageDZText1.Add(DZText);
                    SSstage.transform.GetChild(0).transform.GetChild(k).name = SSStageDZText1[k];
                    k = k + 1;
                }
                else if (DZStageTag.DZStageTag == "测试阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    CSStageDZText1.Add(DZText);
                    CSstage.transform.GetChild(0).transform.GetChild(m).name = CSStageDZText1[m];
                    m = m + 1;
                }
            }
        }
        j = 0; k = 0; m = 0;
        for (int i = 0; i < DZTrack2.transform.childCount; i++)//DZTrack2
        {
            
            Transform TrackChildi = DZTrack2.transform.GetChild(i);
            if (TrackChildi.name != "Image Empty(Clone)")
            {
                DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                if (DZStageTag.DZStageTag == "准备阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    ZBStageDZText2.Add(DZText);
                    ZBstage.transform.GetChild(1).transform.GetChild(j).name = ZBStageDZText2[j];
                    j = j + 1;
                    print("准备阶段动作数量：" + j);
                }
                else if (DZStageTag.DZStageTag == "实施阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    SSStageDZText2.Add(DZText);
                    SSstage.transform.GetChild(1).transform.GetChild(k).name = SSStageDZText2[k];
                    k = k + 1;
                }
                else if (DZStageTag.DZStageTag == "测试阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    CSStageDZText2.Add(DZText);
                    CSstage.transform.GetChild(1).transform.GetChild(m).name = CSStageDZText2[m];
                    m = m + 1;
                }
            }
        }
        j = 0;  k = 0;  m = 0;
        for (int i = 0; i < DZTrack3.transform.childCount; i++)//DZTrack3
        {
            
            Transform TrackChildi = DZTrack3.transform.GetChild(i);
            if (TrackChildi.name != "Image Empty(Clone)")
            {
                DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                if (DZStageTag.DZStageTag == "准备阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    ZBStageDZText3.Add(DZText);
                    ZBstage.transform.GetChild(2).transform.GetChild(j).name = ZBStageDZText3[j];
                    j = j + 1;
                    
                }
                else if (DZStageTag.DZStageTag == "实施阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    SSStageDZText3.Add(DZText);
                    SSstage.transform.GetChild(2).transform.GetChild(k).name = SSStageDZText3[k];
                    k = k + 1;
                }
                else if (DZStageTag.DZStageTag == "测试阶段")
                {
                    string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                    CSStageDZText3.Add(DZText);
                    CSstage.transform.GetChild(2).transform.GetChild(m).name = CSStageDZText3[m];
                    m = m + 1;
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        OnDZChange();
    }
}
