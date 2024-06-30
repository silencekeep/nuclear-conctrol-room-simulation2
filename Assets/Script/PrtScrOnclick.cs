using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PrtScrOnclick : MonoBehaviour
{
    private int hour;
    private int minute;
    private int second;
    private int year;
    private int month;
    private int day;
    public GameObject vpop;
    public Image fzrgfx;
    private Texture2D zhizunbao;
    private Texture2D m_Tex;
    public GameObject pop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CaptureScreen()
    {
       
        //vpop.SetActive(false);
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;
        
        if (System.IO.Directory.Exists(System.Environment.CurrentDirectory + "/PrtScr"))
        {
            Debug.Log("文件夹已经存在");
        }
        else
        {
            Debug.Log("文件夹不存在");
            Directory.CreateDirectory(System.Environment.CurrentDirectory + "/PrtScr");
        }

        string path = System.Environment.CurrentDirectory + "/PrtScr/Screenshot" + year + month + day + hour + minute + ".png";
        ScreenCapture.CaptureScreenshot(path, 0);
        //ScreenCapture.CaptureScreenshotAsTexture(path)
        //System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\PrtScr");
        //fzrgfx.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        System.Threading.Thread.Sleep(2000);
        //加载
        LoadFromFile(path);
        //变换格式
        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
        fzrgfx.sprite = tempSprite;//赋值

    }

    private void LoadFromFile(string path)
    {
        m_Tex = new Texture2D(1, 1);
        m_Tex.LoadImage(ReadPNG(path));
    }

    private byte[] ReadPNG(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read);

        fileStream.Seek(0, SeekOrigin.Begin);

        byte[] binary = new byte[fileStream.Length]; //创建文件长度的buffer
        fileStream.Read(binary, 0, (int)fileStream.Length);

        fileStream.Close();

        fileStream.Dispose();

        fileStream = null;

        return binary;
    }

    public void kanjianPoP()
    {
        if (pop.activeInHierarchy)
        {
            pop.SetActive(false);
        }
        else if (!pop.activeInHierarchy)
        {
            pop.SetActive(true);
        }
    }

}
