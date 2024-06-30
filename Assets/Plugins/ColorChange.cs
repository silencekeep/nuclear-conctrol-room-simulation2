using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.InteropServices;
using System.Text;

public class ColorChange : MonoBehaviour
{
    private Color color;
    private int a;
    private string colorStr;
    public GameObject plane;
    string path = Environment.CurrentDirectory;
    // Use this for initialization
    void Start()
    {

        Debug.Log(path);

        //读取文本里的RGB值
        using (FileStream file = new FileStream(path + @"\1.txt", FileMode.OpenOrCreate, FileAccess.Read))
        {
            byte[] buffer = new byte[1024 * 1024 * 2];
            int i = file.Read(buffer, 0, buffer.Length);
            string s = Encoding.UTF8.GetString(buffer, 0, i);

            string[] sarry = s.Split(',');

            float bb;
            float.TryParse(sarry[a], out bb);
            color.r = bb;
            color.g = float.Parse(sarry[a + 1]);
            color.b = float.Parse(sarry[a + 2].ToString());
            Debug.Log(sarry[a] + "\n");


            Debug.Log(color.r + "\n" + color.g + "\n" + color.b);
            // plane.GetComponent<Renderer>().material.color = new Color(color.r / 255f, color.g / 255f, color.b / 255f);//unity的RGB值要除以255
            plane.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b);
        }



    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

    public class CHOOSECOLOR
    {
        public Int32 lStructSize;
        public Int32 hwndOwner;
        public Int32 hInstance;
        public Int32 rgbResult;
        public IntPtr lpCustColors;
        public Int32 Flags;
        public Int32 lCustData;
        public Int32 lpfnHook;
        public Int32 lpTemplateName;
    }

    public class DllTest
    {
        [DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChooseColorA(CHOOSECOLOR pChoosecolor);//对应的win32API
        public static bool ChooseColorA1(CHOOSECOLOR pChoosecolor)
        {
            return ChooseColorA(pChoosecolor);
        }
    }


    private void OnGUI()
    {

        if (GUI.Button(new Rect(-506, -15, 40, 30), "颜色"))
        {
            CHOOSECOLOR choosecolor = new CHOOSECOLOR();
            choosecolor.lStructSize = Marshal.SizeOf(choosecolor);
            choosecolor.hwndOwner = 0;
            choosecolor.rgbResult = 0x808080;//颜色转成int型
            choosecolor.lpCustColors = Marshal.AllocCoTaskMem(64);
            choosecolor.Flags = 0x00000002 | 0x00000001;
            if (DllTest.ChooseColorA1(choosecolor))
            {
                a = choosecolor.rgbResult;//获取int型颜色值  rgba由这个值组成
                colorStr = Convert.ToString(a, 16);//十进制转化十六进制  每两个字符代表一个颜色值  顺序从左到右依次为RGB
                color.b = Convert.ToInt32(colorStr.Substring(0, 2), 16) / 255f;
                color.g = Convert.ToInt32(colorStr.Substring(2, 2), 16) / 255f;
                color.r = Convert.ToInt32(colorStr.Substring(4, 2), 16) / 255f;
            }

            //储存颜色的rgb值到文本里
            using (FileStream writecolor = new FileStream(path + @"\1.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                string save = color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString();

                byte[] savesome = new byte[1024 * 1024 * 2];

                savesome = Encoding.Default.GetBytes(save);

                writecolor.Write(savesome, 0, save.Length);
            }
            //改变plane的颜色
            plane.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b);




        }



    }

}
