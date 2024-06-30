using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongxiaoxue : MonoBehaviour
{
    public GameObject kekshi, keda, shushixing,caozuokognjian,fuzhuzhuangzhi,denglu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickKeshi()
    {
        kekshi.SetActive(true);
        keda.SetActive(false);
        caozuokognjian.SetActive(false);
        shushixing.SetActive(false);
        fuzhuzhuangzhi.SetActive(false);
    }

    public void Onclickkeda()
    {
        kekshi.SetActive(false);
        keda.SetActive(true);
        caozuokognjian.SetActive(false);
        shushixing.SetActive(false);
        fuzhuzhuangzhi.SetActive(false);
    }

    public void Onclickcaozuokongjian()
    {
        kekshi.SetActive(false);
        keda.SetActive(false);
        caozuokognjian.SetActive(true);
        shushixing.SetActive(false);
        fuzhuzhuangzhi.SetActive(false);
    }
    public void Onclickshushixing()
    {
        kekshi.SetActive(false);
        keda.SetActive(false);
        caozuokognjian.SetActive(false);
        shushixing.SetActive(true);
        fuzhuzhuangzhi.SetActive(false);
    }
    public void Onclickfuzhuzhuangzhi()
    {
        kekshi.SetActive(false);
        keda.SetActive(false);
        caozuokognjian.SetActive(false);
        shushixing.SetActive(false);
        fuzhuzhuangzhi.SetActive(true);
    }

    public void XiuGai()
    {
        denglu.SetActive(true);
    }
}
