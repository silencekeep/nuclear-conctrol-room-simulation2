using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class buttoncreate : MonoBehaviour
{/// <summary>
/// 未使用
/// </summary>
    bool flag = false;
    bool flag2 = false;
    public Button ButtonPrefab;
    private Transform track;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatNewBotton(int k)
    {
        //前提：动态创建按钮，都需要创建一个按钮预设体ButtonPrefabs，假如，我们创建四个按钮。
        track = GameObject.Find("Timeline").transform;
        float a = 50;
        //Button obj = (Button)Instantiate(ButtonPrefab);   //实例化按钮
        Button obj = Instantiate(ButtonPrefab, transform.position, Quaternion.identity) as Button;
        obj.transform.SetParent(track);
        obj.transform.Translate(new Vector3(obj.transform.localPosition.x + (float)(k * a)-200, obj.transform.localPosition.y, obj.transform.localPosition.z)); 
        //按钮显示的位置                                                                                                                                                            
        RectTransform rtr = obj.GetComponent<RectTransform>();                                                                                                                                                           
        ////设置父级基准位置                                                                                                                                                            
        //rtr.anchorMin = new Vector2(0.5f, 0.5f);                                                                                                                                                            
        //rtr.anchorMax = new Vector2(0.5f, 0.5f);                                                                                                                                                            
        ////定义控件自身定位点位置                                                                                                                                                            
        //rtr.pivot = new Vector2(0.5f+i*50, 0.5f);                                                                                                                                                            
        ////定义控件定位点相对基准位置的偏移                                                                                                                                                            
        //rtr.anchoredPosition = new Vector2(0, 0);                                                                                                                                                             
        //定义控件大小                                                                                                                                                           
        rtr.sizeDelta = new Vector2(50, 20);                                                                                                                                                             
        //obj.onClick.AddListener(ButtonClicked2);


    }
    public void ButtonClicked()
    {
        i = i + 1;
        CreatNewBotton(i);
    }
    public void OnGUI()
    {

        if (GUI.Button(new Rect(385, 400, 100, 20), "添加动素"))
        {
            flag = !flag;

            //Debug.Log ("You clicked me");
        }
        if (flag)
        {
            GUI.Button(new Rect(385, 600, 100, 20), "1");
            flag2 = !flag2;
        }

        if (flag2)
        {
            GUI.Button(new Rect(485, 600, 100, 20), "2");

        }
    }
}
