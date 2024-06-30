using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using CNCC.Panels;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

public class Frame : MonoBehaviour
{
    public GameObject frame1;
    public Camera up;
    public Camera left;
    public Camera zhu;
    public Material mat;
    public Camera screen_up;
    public Camera screen_left;
    private Vector3 gap;

    private float huamnTargetDistance = 0.1f;
    private float eyeHeightZuo = 1.15f;
    private float shoulderHeightZuo = 1.0f;
    private float stretch0 = 0.614f;
    private float shoulderHeightZhan = 1.6f;
    private float eyeHeightZhan = 1.75f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void frame(GameObject target1, GameObject human, GameObject eye, string choose)
    {
        huamnTargetDistance = Mathf.Abs(Vector3.ProjectOnPlane(target1.transform.position - human.transform.position, new Vector3(1, 0, 1)).magnitude - 1.2f);
       
        frame1.SetActive(true);
        if (target1.name.Contains("屏幕"))
        {
            screen1(human);

            showJPG(System.Environment.CurrentDirectory + @"\Output\screen.jpg");
            //showJPG(System.Environment.CurrentDirectory + @"\Output\screen2.jpg");
            frame1.SetActive(false);
            return;
        }
        if (target1.name.Contains("BUP")|| target1.name.Contains("ECP"))
        {
            getDataZhan(choose, human);
            print(eyeHeightZhan);
            BUPframeReachable(target1, human);
            BUPframeVisual(target1, human);
            //\Output\BUP1.jpg
            showJPG(System.Environment.CurrentDirectory + @"\Output\BUP1.jpg");
            showJPG(System.Environment.CurrentDirectory + @"\Output\BUP2.jpg");
        }
        else
        {
            getDataZuo(choose, human);
            left.orthographicSize = 1.0f;
            up.orthographicSize = 1.0f;
            zhu.orthographicSize = 1.0f;
            GameObject target = Instantiate(target1, target1.transform.position, target1.transform.rotation);
            target.GetComponent<Transform>().position = new Vector3(-26.6f, 1.539f, -2.31f);
            target.GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, 0);
            gap = target.GetComponent<Transform>().position - target1.GetComponent<Transform>().position;
            //target.GetComponent<MeshRenderer>().material = mat;
            if (target.GetComponent<Panel>())
            {
                target.GetComponent<Panel>().ChangeMesh(mat);
            }
            target.layer = 8;
            
            //创建俯视图
            string filename1 = System.Environment.CurrentDirectory + @"\Output\up.jpg";
            RenderTexture rt1 = new RenderTexture(Screen.width, Screen.height, 16);
            up.GetComponent<Camera>().targetTexture = rt1;
            up.GetComponent<Camera>().Render();
            RenderTexture.active = rt1;
            Texture2D t1 = new Texture2D(Screen.width, Screen.height);
            t1.ReadPixels(new Rect(0, 0, t1.width, t1.height), 0, 0);
            int a = (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min+gap).x;

            for (int i = 100; i < a; i++)
            {
                    t1.SetPixel(i, (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y , UnityEngine.Color.black);
                    t1.SetPixel(i, (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y, UnityEngine.Color.black);


            }
            for (int i = (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y; i < (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y; i++)
            {
                t1.SetPixel(100, i, UnityEngine.Color.black);
            }
            t1.Apply();
            string path1 = filename1;
            File.WriteAllBytes(path1, t1.EncodeToJPG());
            if (File.Exists(path1))
            {
                FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                Bitmap img = new Bitmap(ms);

                int height = Screen.height;
                int local = ((int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y + (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y) / 2;
                local = height - local;

                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
                String str = target.GetComponent<BoxCollider>().bounds.size.z.ToString("0.00") + "m";
                System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
                SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
                g.DrawString(str, font, sbrush, new PointF(100, local));
                img.Save(path1, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            //创建左视图
            left1(target);
            left2(target, human);
            left3(target, human);

            //创建主视图
            string filename3 = System.Environment.CurrentDirectory + @"\Output\zhu.jpg";
            RenderTexture rt3 = new RenderTexture(Screen.width, Screen.height, 16);
            zhu.GetComponent<Camera>().targetTexture = rt3;
            zhu.GetComponent<Camera>().Render();
            RenderTexture.active = rt3;
            Texture2D t3 = new Texture2D(Screen.width, Screen.height);
            t3.ReadPixels(new Rect(0, 0, t3.width, t3.height), 0, 0);
            int c = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x;
            int d = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y;
            for (int i = 100; i < c; i++)
            {
                    t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y, UnityEngine.Color.black);
                    t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y, UnityEngine.Color.black);


            }
            for (int i = 100; i < d; i++)
            {

                    t3.SetPixel((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x , i, UnityEngine.Color.black);
                    t3.SetPixel((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).x , i, UnityEngine.Color.black);

            }
            for (int i = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y; i < (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y; i++)
            {
                t3.SetPixel(100, i, UnityEngine.Color.black);
            }
            for (int i = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x; i < (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).x; i++)
            {
                t3.SetPixel(i, 100, UnityEngine.Color.black);
            }
            t3.Apply();
            string path3 = filename3;
            File.WriteAllBytes(path3, t3.EncodeToJPG());
            if (File.Exists(path3))
            {
                FileStream fs = new FileStream(path3, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                Bitmap img = new Bitmap(ms);

                int height = Screen.height;
                int local = ((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y + (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y) / 2;
                local = height - local;

                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
                String str1 = target.GetComponent<BoxCollider>().bounds.size.y.ToString("0.00") + "m";
                String str2 = target.GetComponent<BoxCollider>().bounds.size.x.ToString("0.00") + "m";
                System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
                SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
                g.DrawString(str1, font, sbrush, new PointF(100, local));
                g.DrawString(str2, font, sbrush, new PointF((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.center + gap).x, height - 100));
                img.Save(path3, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            Destroy(target);
            showJPG(System.Environment.CurrentDirectory + @"\Output\left2.jpg");
            showJPG(System.Environment.CurrentDirectory + @"\Output\left3.jpg");
        }

        //创建大屏幕可视性评估
        //screen1(human);
        frame1.SetActive(false);
    }

    //大屏幕可视性评估（俯视）
    private void screen1(GameObject human)
    {
        string filename = System.Environment.CurrentDirectory + @"\Output\screen.jpg";
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 16);
        screen_up.GetComponent<Camera>().targetTexture = rt;
        screen_up.GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        Texture2D t = new Texture2D(Screen.width, Screen.height);
        t.ReadPixels(new Rect(0, 0, t.width, t.height), 0, 0);

        //11
        GameObject left = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
        GameObject right = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
        int n = Panel.AllPanels.Count;
        int cnt = 0;
        for (int i = 0; i < n; i++)
        {
            if (Panel.AllPanels[i].name.Contains("大屏幕45度拼角台"))
            {
                if (cnt == 0)
                {
                    left = Panel.AllPanels[i].gameObject;
                    cnt++;
                }
                if (cnt == 1)
                {
                    right = Panel.AllPanels[i].gameObject;
                }
            }
        }

        float disLeft = Vector3.Distance(left.transform.position, human.transform.position);
        float disRight = Vector3.Distance(right.transform.position, human.transform.position);
        Vector3 target;
        float distance;
        if (disLeft > disRight)
        {
            distance = disLeft-0.5f;
        }
        else
        {
            distance = disRight-0.5f;
        }
        
        Vector3 dis = new Vector3(human.GetComponent<BoxCollider>().bounds.center.x + distance, human.GetComponent<BoxCollider>().bounds.center.y, human.GetComponent<BoxCollider>().bounds.center.z);
        Vector3 human_screen = screen_up.WorldToScreenPoint(human.GetComponent<BoxCollider>().bounds.center);
        Vector3 distance_screen = screen_up.WorldToScreenPoint(dis);

        float text_x = 0;
        float text_y = 0;
        bool flag = true;
        for (double i = 0; i < 150; i+=0.1)
        {
            double b = ((i + 15) * (Math.PI)) / 180;
            int x2 = (int)((distance_screen.x - human_screen.x) * Math.Cos(b) + human_screen.x);
            int y2 = (int)((distance_screen.x - human_screen.x) * Math.Sin(b) + human_screen.y);
            t.SetPixel(x2, y2, UnityEngine.Color.black);
            if (i > 120 && flag)
            {
                float k1 = (y2 - human_screen.y) / (x2 - human_screen.x);
                float b1 = human_screen.y - k1 * human_screen.x;
                for(int j = x2; j<= human_screen.x; j++)
                {
                    t.SetPixel(j, (int)(k1*j+b1), UnityEngine.Color.black);
                }
                text_x = (x2 + human_screen.x) / 2;
                text_y = (y2 + human_screen.y) / 2;
                flag = false;
            }
        }

        t.Apply();
        string path = filename;
        File.WriteAllBytes(path, t.EncodeToJPG());

        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            String str1 = distance.ToString("0.00");
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            g.DrawString(str1, font, sbrush, new PointF(text_x, height-text_y));
            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    //大屏幕可视性（左视图）
    private void screen2(GameObject target,GameObject eye, GameObject human)
    {
        string filename2 = System.Environment.CurrentDirectory + @"\Output\screen2.jpg";
        RenderTexture rt2 = new RenderTexture(Screen.width, Screen.height, 16);
        screen_left.GetComponent<Camera>().targetTexture = rt2;
        screen_left.GetComponent<Camera>().Render();
        RenderTexture.active = rt2;
        Texture2D t2 = new Texture2D(Screen.width, Screen.height);
        t2.ReadPixels(new Rect(0, 0, t2.width, t2.height), 0, 0);
        //添加人体尺寸
        float dis = human.GetComponent<Dis>().dis;//TODO
        float eye_h = 1.15f;
        float screen_dis = 6.41f;

        try {
            GameObject left = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
            GameObject right = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
            float disLeft = Vector3.Distance(left.transform.position, eye.transform.position);
            float disRight = Vector3.Distance(right.transform.position, eye.transform.position);
            screen_dis = Math.Min(disLeft, disRight);
        }
        catch
        {
            Debug.Log("0");
        }
        

        float screen_h = 2.0f;
        //int a = (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min).x;
        Vector3 distance0 = new Vector3(target.GetComponent<BoxCollider>().bounds.min.x, target.GetComponent<BoxCollider>().bounds.min.y, target.GetComponent<BoxCollider>().bounds.min.z - dis);
        Vector3 eye_height0 = new Vector3(distance0.x, distance0.y + eye_h, distance0.z);
        Vector3 screen0 = new Vector3(distance0.x, distance0.y, distance0.z+screen_dis);
        Vector3 screen1 = new Vector3(distance0.x, distance0.y + screen_h, distance0.z + screen_dis);
        Vector3 distance = screen_left.WorldToScreenPoint(distance0);
        Vector3 eye_height = screen_left.WorldToScreenPoint(eye_height0);
        Vector3 scr = screen_left.WorldToScreenPoint(screen0);
        Vector3 scr_h = screen_left.WorldToScreenPoint(screen1);

        for (int i = (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min).x; i < distance.x; i++)
        {
            t2.SetPixel(i, (int)distance.y, UnityEngine.Color.black);
        }
        for (int i = (int)distance.y; i < (int)eye_height.y; i++)
        {
            t2.SetPixel((int)distance.x, i, UnityEngine.Color.black);
        }
        for(int i = (int)scr.x; i<= distance.x; i++)
        {
            t2.SetPixel(i, (int)distance.y, UnityEngine.Color.black);
        }
        for (int i = (int)scr.y; i <= scr_h.y; i++)
        {
            t2.SetPixel((int)scr.x, i, UnityEngine.Color.black);
        }
        float k1 = (eye_height.y - scr_h.y) / (eye_height.x - scr_h.x);
        float b1 = eye_height.y - k1 * eye_height.x;
        for (int i = (int)scr_h.x; i < eye_height.x; i++)
        {
            t2.SetPixel(i, (int)(k1 * i + b1), UnityEngine.Color.black);
        }
        for (int i = (int)eye_height.x-200; i < (int)eye_height.x; i++)
        {
            t2.SetPixel(i, (int)eye_height.y, UnityEngine.Color.black);
        }

        t2.Apply();
        string path2 = filename2;
        File.WriteAllBytes(path2, t2.EncodeToJPG());
        //添加标注
        if (File.Exists(path2))
        {
            FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            String str1 =  screen_dis.ToString("0.00") + "m";
            if (screen_dis > 10) {
                str1 = strAdd(str1);
            }
            String str2 = eye_h.ToString("0.00") + "m";
            String str3 = screen_h.ToString("0.00") + "m";
            double ang = Angle(new Point((int)eye_height.x, (int)eye_height.y), new Point((int)scr_h.x, (int)scr_h.y), new Point((int)(eye_height.x - 200), (int)eye_height.y));
            String str4 = ang.ToString("0.00") + "°";
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            g.DrawString(str1, font, sbrush, new PointF((scr.x+distance.x)/ 2, height - distance.y));
            g.DrawString(str2, font, sbrush, new PointF(distance.x, height - (distance.y + eye_height.y) / 2));
            g.DrawString(str3, font, sbrush, new PointF(scr.x, height - (scr.y + scr_h.y) / 2));
            g.DrawString(str4, font, sbrush, new PointF(eye_height.x, height - eye_height.y));
            img.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    //创建左视图(操作台高度)
    private void left1(GameObject target)
    {
        string filename2 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
        RenderTexture rt2 = new RenderTexture(Screen.width, Screen.height, 16);
        left.GetComponent<Camera>().targetTexture = rt2;
        left.GetComponent<Camera>().Render();
        RenderTexture.active = rt2;
        Texture2D t2 = new Texture2D(Screen.width, Screen.height);
        t2.ReadPixels(new Rect(0, 0, t2.width, t2.height), 0, 0);
        int b = (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).x;
        for (int i = 200; i < b; i++)
        {
                t2.SetPixel(i, (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y , UnityEngine.Color.black);
                t2.SetPixel(i, (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y, UnityEngine.Color.black);


        }
        for (int i = (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y; i < (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y; i++)
        {
            t2.SetPixel(200, i, UnityEngine.Color.black);
        }
        Vector3 inside = left.WorldToScreenPoint(target.transform.GetChild(3).position);
        Vector3 outside = left.WorldToScreenPoint(target.transform.GetChild(2).position);
        for(int i=0;i<target.transform.childCount; i++)
        {
            if (target.transform.GetChild(i).name.Contains("前沿"))
            {
                Transform cur = target.transform.GetChild(i);
                for (int j = 0; j < cur.childCount; j++)
                {
                    if(cur.GetChild(j).name.Equals("b"))
                    outside = cur.GetChild(j).position;
                }
                    
                break;
            }
        }
        for (int i = (int)inside.x; i < (int)outside.x; i++)
        {
            t2.SetPixel(i, ((int)inside.y+ (int)outside.y) /2, UnityEngine.Color.black);
        }
        for (int i = (int)inside.y; i < (int)outside.y; i++)
        {
            t2.SetPixel((int)outside.x, i, UnityEngine.Color.black);
        }
        Vector3 down = left.WorldToScreenPoint(target.transform.GetChild(1).position);
        Vector3 out_up = left.WorldToScreenPoint(target.transform.GetChild(4).position);
        for (int i = (int)down.x; i < (int)out_up.x+200; i++)
        {
            t2.SetPixel(i, (int)down.y, UnityEngine.Color.black);
        }
        for (int i = (int)out_up.x; i < (int)out_up.x + 200; i++)
        {
            t2.SetPixel(i, (int)out_up.y, UnityEngine.Color.black);
        }
        for (int i = (int)inside.y; i < (int)down.y; i++)
        {
            t2.SetPixel((int)out_up.x + 180, i, UnityEngine.Color.black);
        }

        t2.Apply();
        string path2 = filename2;
        File.WriteAllBytes(path2, t2.EncodeToJPG());
        //添加标注
        if (File.Exists(path2))
        {
            FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;
            int local = ((int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y + (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y) / 2;
            local = height - local;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            String str = target.GetComponent<BoxCollider>().bounds.size.y.ToString("0.00") + "m";
            //10
            String str1 = (Math.Abs(target.transform.GetChild(3).localPosition.z- target.transform.GetChild(2).localPosition.z)).ToString("0.00") + "m";
            if(Math.Abs(target.transform.GetChild(3).localPosition.z - target.transform.GetChild(2).localPosition.z)  < 0.5)
            {
                str1 = strAdd(str1);
            }
            String str2 = (Math.Abs(target.transform.GetChild(3).localPosition.y - target.transform.GetChild(2).localPosition.y)).ToString("0.00") + "m";
            if(Math.Abs(target.transform.GetChild(3).localPosition.y - target.transform.GetChild(2).localPosition.y)  < 0.5)
            {
                str2 = strAdd(str2);
            }
            String str3 = (Math.Abs(target.transform.GetChild(1).localPosition.y - target.transform.GetChild(4).localPosition.y) ).ToString("0.00") + "m";
            String str4 = (Math.Abs(target.transform.GetChild(3).localPosition.y - target.transform.GetChild(4).localPosition.y)).ToString("0.00") + "m";
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush1 = new SolidBrush(System.Drawing.Color.Red);
            if (str.Contains("*"))
            {
                g.DrawString(str, font, sbrush1, new PointF(150, local));
            }
            else
            {
                g.DrawString(str, font, sbrush, new PointF(150, local));
            }
            if (str1.Contains("*"))
            {
                g.DrawString(str1, font, sbrush1, new PointF(((int)inside.x + (int)outside.x) / 2 - 50, height - ((int)inside.y + (int)outside.y) / 2));
            }
            else
            {
                g.DrawString(str1, font, sbrush, new PointF(((int)inside.x + (int)outside.x) / 2 - 50, height - ((int)inside.y + (int)outside.y) / 2));
            }
            if (str2.Contains("*"))
            {
                g.DrawString(str2, font, sbrush1, new PointF(((int)inside.x + (int)outside.x) / 2 + 50, height - ((int)inside.y + (int)outside.y) / 2 - 100));
            }
            else
            {
                g.DrawString(str2, font, sbrush, new PointF(((int)inside.x + (int)outside.x) / 2 + 50, height - ((int)inside.y + (int)outside.y) / 2 - 100));
            }
            if (str3.Contains("*"))
            {
                g.DrawString(str3, font, sbrush1, new PointF((int)out_up.x + 200, height - (int)(down.y + out_up.y) / 2));
            }
            else
            {
                g.DrawString(str3, font, sbrush, new PointF((int)out_up.x + 200, height - (int)(down.y + out_up.y) / 2));
            }
            if (str4.Contains("*"))
            {
                g.DrawString(str4, font, sbrush1, new PointF((int)out_up.x + 200, height - (int)(inside.y + out_up.y) / 2));
            }
            else
            {
                g.DrawString(str4, font, sbrush, new PointF((int)out_up.x + 200, height - (int)(inside.y + out_up.y) / 2));
            }
            img.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }


    //创建左视图(可触及范围)
    private void left2(GameObject target, GameObject human)
    {
        
        string filename2 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
        RenderTexture rt2 = new RenderTexture(Screen.width, Screen.height, 16);
        left.GetComponent<Camera>().targetTexture = rt2;
        left.GetComponent<Camera>().Render();
        RenderTexture.active = rt2;
        Texture2D t2 = new Texture2D(Screen.width, Screen.height);
        t2.ReadPixels(new Rect(0, 0, t2.width, t2.height), 0, 0);
        //添加人体尺寸
        float dis = huamnTargetDistance;
        float shoulder_h = shoulderHeightZuo;
        float shoulder_dis = stretch0;
        //int a = (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min).x;
        Vector3 distance0 = new Vector3((target.GetComponent<BoxCollider>().bounds.min + gap).x, (target.GetComponent<BoxCollider>().bounds.min + gap).y, (target.GetComponent<BoxCollider>().bounds.min + gap).z - dis);
        Vector3 shoulder_height0 = new Vector3(distance0.x, distance0.y + shoulder_h, distance0.z);
        Vector3 distance = left.WorldToScreenPoint(distance0);
        Vector3 shoulder_height = left.WorldToScreenPoint(shoulder_height0);
        for (int i = (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x; i < distance.x; i++)
        {
            t2.SetPixel(i, (int)distance.y, UnityEngine.Color.black);
        }
        for (int i = (int)distance.y; i < (int)shoulder_height.y; i++)
        {
            t2.SetPixel((int)distance.x, i, UnityEngine.Color.black);
        }
        Vector3 s = left.WorldToScreenPoint(new Vector3(shoulder_height0.x, shoulder_height0.y, shoulder_height0.z- shoulder_dis));
        for (float i = 0; i < 110; i+=0.1f)
        {
            double b = ((i + 120) * (Math.PI)) / 180;
            int x2 = (int)((s.x - shoulder_height.x) * Math.Cos(b) + shoulder_height.x);
            int y2 = (int)((s.x - shoulder_height.x) * Math.Sin(b) + shoulder_height.y);
            t2.SetPixel(x2, y2, UnityEngine.Color.black);
        }

        t2.Apply();
        string path2 = filename2;
        File.WriteAllBytes(path2, t2.EncodeToJPG());
        //添加标注
        if (File.Exists(path2))
        {
            FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            String str1 = dis.ToString("0.00") + "m";
            String str2 = shoulder_h.ToString("0.00") + "m";
            String str3 = stretch0.ToString("0.00") + "m";
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush1 = new SolidBrush(System.Drawing.Color.Red);
 

            if (str1.Contains("*"))
            {
                g.DrawString(str1, font, sbrush1, new PointF(left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x, height - 250));
            }
            else
            {
                g.DrawString(str1, font, sbrush, new PointF(left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x, height - 250));
            }
            if (str2.Contains("*"))
            {
                g.DrawString(str2, font, sbrush1, new PointF(distance.x, height - (distance.y + shoulder_height.y) / 2));
            }
            else
            {
                g.DrawString(str2, font, sbrush, new PointF(distance.x, height - (distance.y + shoulder_height.y) / 2));
            }
            if (str3.Contains("*"))
            {
                g.DrawString(str3, font, sbrush1, new PointF(shoulder_height.x - 200, height - shoulder_height.y - 100));
            }
            else
            {
                g.DrawString(str3, font, sbrush, new PointF(shoulder_height.x - 200, height - shoulder_height.y - 100));
            }

            img.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    //创建左视图(可视性)
    private void left3(GameObject target, GameObject human)
    {
        string filename2 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
        RenderTexture rt2 = new RenderTexture(Screen.width, Screen.height, 16);
        left.GetComponent<Camera>().targetTexture = rt2;
        left.GetComponent<Camera>().Render();
        RenderTexture.active = rt2;
        Texture2D t2 = new Texture2D(Screen.width, Screen.height);
        t2.ReadPixels(new Rect(0, 0, t2.width, t2.height), 0, 0);
        //添加人体尺寸
        float dis = huamnTargetDistance;
        float eye_h = eyeHeightZuo;
        //int a = (int)up.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min).x;
        Vector3 distance0 = new Vector3((target.GetComponent<BoxCollider>().bounds.min + gap).x, (target.GetComponent<BoxCollider>().bounds.min + gap).y, (target.GetComponent<BoxCollider>().bounds.min + gap).z - dis);
        Vector3 eye_height0 = new Vector3(distance0.x, distance0.y + eye_h, distance0.z);
        Vector3 distance = left.WorldToScreenPoint(distance0);
        Vector3 eye_height = left.WorldToScreenPoint(eye_height0);
        for (int i = (int)left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x; i < distance.x; i++)
        {
            t2.SetPixel(i, (int)distance.y, UnityEngine.Color.black);
        }
        for (int i = (int)distance.y; i < (int)eye_height.y; i++)
        {
            t2.SetPixel((int)distance.x, i, UnityEngine.Color.black);
        }
        Vector3 up = left.WorldToScreenPoint(target.transform.GetChild(0).position);
        Vector3 down = left.WorldToScreenPoint(target.transform.GetChild(1).position);
        float k1 = (eye_height.y - up.y) / (eye_height.x - up.x);
        float b1 = eye_height.y - k1 * eye_height.x;
        float k2 = (eye_height.y - down.y) / (eye_height.x - down.x);
        float b2 = eye_height.y - k2 * eye_height.x;
        for (int i = (int)up.x; i < eye_height.x; i++)
        {
            t2.SetPixel(i, (int)(k1*i+b1), UnityEngine.Color.black);
        }
        for (int i = (int)down.x; i < eye_height.x; i++)
        {
            t2.SetPixel(i, (int)(k2 * i + b2), UnityEngine.Color.black);
        }
        //添加角度
        double angle1 = Angle(new Point((int)up.x, (int)up.y), new Point((int)down.x, (int)down.y), new Point((int)eye_height.x, (int)eye_height.y));
        double angle2 = Angle(new Point((int)down.x, (int)down.y), new Point((int)up.x, (int)up.y), new Point((int)eye_height.x, (int)eye_height.y));
        double angle3 = Angle(new Point((int)eye_height.x, (int)eye_height.y), new Point((int)down.x, (int)down.y), new Point((int)eye_height.x-200, (int)eye_height.y));
        for (int i = (int)eye_height.x-200; i < (int)eye_height.x; i++)
        {
            t2.SetPixel(i, (int)eye_height.y, UnityEngine.Color.black);
        }
        for(float i = 0; i < (int)angle3; i+=0.1f)
        {
            double b = ((i + 180) * (Math.PI)) / 180;
            int x2 = (int)(50 * Math.Cos(b) + eye_height.x);
            int y2 = (int)(50 * Math.Sin(b) + eye_height.y);
            t2.SetPixel(x2, y2, UnityEngine.Color.black);
        }

        t2.Apply();
        string path2 = filename2;
        File.WriteAllBytes(path2, t2.EncodeToJPG());
        //添加标注
        if (File.Exists(path2))
        {
            FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            String str1 = dis.ToString("0.00") + "m";
            String str2 = eye_h.ToString("0.00") + "m";
            Vector3 eye = new Vector3(target.transform.GetChild(0).position.x, target.transform.GetChild(3).position.y+1.4f, target.transform.GetChild(2).position.z + 0.3f);
            String str3 = Vector3.Distance(eye, target.transform.GetChild(0).position).ToString("0.00") + "m";
            String str4 = Vector3.Distance(eye, target.transform.GetChild(1).position).ToString("0.00") + "m";
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush3 = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush4 = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush5 = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush6 = new SolidBrush(System.Drawing.Color.Black);
            SolidBrush sbrush7 = new SolidBrush(System.Drawing.Color.Black);
            if (Vector3.Distance(eye, target.transform.GetChild(0).position) > 0.5)
            {
                str3 = strAdd(str3);
                sbrush3 = new SolidBrush(System.Drawing.Color.Red);
            }
            if (Vector3.Distance(eye, target.transform.GetChild(1).position)>0.5)
            {
                str4 = strAdd(str4);
                sbrush4 = new SolidBrush(System.Drawing.Color.Red);
            }
            String str5 = angle1.ToString("0") + "°";
            if (angle1 > 95 || angle1 < 60)
            {
                str5 = strAdd(str5);
                sbrush5 = new SolidBrush(System.Drawing.Color.Red);
            }
            String str6 = angle2.ToString("0") + "°";
            if (angle2 > 95 || angle2 < 60)
            {
                str6 = strAdd(str6);
                sbrush6 = new SolidBrush(System.Drawing.Color.Red);
            }
            String str7 = angle3.ToString("0") + "°";
            if (angle3 > 45)
            {
                str7 = strAdd(str7);
                sbrush7 = new SolidBrush(System.Drawing.Color.Red);
            }
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            
            g.DrawString(str1, font, sbrush, new PointF(left.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x, height - 250));
            g.DrawString(str2, font, sbrush, new PointF(distance.x, height - (distance.y + eye_height.y) / 2));
            g.DrawString(str3, font, sbrush3, new PointF((up.x+ eye_height.x)/2, height - (up.y + eye_height.y) / 2));
            g.DrawString(str4, font, sbrush4, new PointF((down.x + eye_height.x) / 2, height - (down.y + eye_height.y) / 2));
            g.DrawString(str5, font, sbrush5, new PointF(up.x+10, height-up.y));
            g.DrawString(str6, font, sbrush6, new PointF(down.x, height - down.y));
            g.DrawString(str7, font, sbrush7, new PointF(eye_height.x, height - eye_height.y));
            img.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
            
        }
    }

    private double Angle(Point cen, Point first, Point second)
    {
        const double M_PI = 3.1415926535897;

        double ma_x = first.X - cen.X;
        double ma_y = first.Y - cen.Y;
        double mb_x = second.X - cen.X;
        double mb_y = second.Y - cen.Y;
        double v1 = (ma_x * mb_x) + (ma_y * mb_y);
        double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
        double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
        double cosM = v1 / (ma_val * mb_val);
        double angleAMB = Math.Acos(cosM) * 180 / M_PI;

        return angleAMB;
    }
    //BUP可达性
    public void BUPframeReachable(GameObject target1, GameObject human)
    {
        up.orthographicSize = 1.5f;
        zhu.orthographicSize = 1.5f;
        left.orthographicSize = 1.5f;
        GameObject target = Instantiate(target1, target1.transform.position, target1.transform.rotation);
        target.GetComponent<Transform>().localPosition = new Vector3(-26.6f, 1.539f, -2.31f);
        target.GetComponent<Transform>().localEulerAngles = new Vector3(0, -90f, 0);
        //target.GetComponent<MeshRenderer>().material = mat;
        if (target.GetComponent<Panel>())
        {
            target.GetComponent<Panel>().ChangeMesh(mat);
        }
        target.layer = 8;
        target.AddComponent<BoxCollider>();

        //创建主视图
        string filename3 = System.Environment.CurrentDirectory + @"\Output\BUP1.jpg";
        RenderTexture rt3 = new RenderTexture(Screen.width, Screen.height, 16);
        zhu.GetComponent<Camera>().targetTexture = rt3;
        zhu.GetComponent<Camera>().Render();
        RenderTexture.active = rt3;
        Texture2D t3 = new Texture2D(Screen.width, Screen.height);
        t3.ReadPixels(new Rect(0, 0, t3.width, t3.height), 0, 0);

        Vector3 up1 = zhu.WorldToScreenPoint(target.transform.GetChild(0).position);
        Vector3 down = zhu.WorldToScreenPoint(target.transform.GetChild(1).position);
        Vector3 outside = zhu.WorldToScreenPoint(target.transform.GetChild(2).position);
        Vector3 inside = zhu.WorldToScreenPoint(target.transform.GetChild(3).position);
        for (int i = 0; i < target.transform.childCount; i++)
        {
            if (target.transform.GetChild(i).name.Contains("前沿"))
            {
                Transform cur = target.transform.GetChild(i);
                for (int j = 0; j < cur.childCount; j++)
                {
                    if (cur.GetChild(j).name.Equals("b"))
                        outside = zhu.WorldToScreenPoint(cur.GetChild(j).position);
                }

                break;
            }
        }
        //盘台高度
        gap = target.GetComponent<Transform>().position - target1.GetComponent<Transform>().position;
        int c = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min+gap).x;
        int d = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y;
        for (int i = 500; i < c; i++)
        {
            t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y, UnityEngine.Color.black);
            t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y, UnityEngine.Color.black);

        }

        for (int i = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y; i < (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y; i++)
        {
            t3.SetPixel(500, i, UnityEngine.Color.black);
        }

        //获取人体数据
        float dis0 = huamnTargetDistance;
        float shoulder0 = shoulderHeightZhan;
        float reach0 = stretch0;

        Vector3 dis = zhu.WorldToScreenPoint(new Vector3(target.transform.GetChild(2).position.x+dis0, target.transform.GetChild(3).position.y, target.transform.GetChild(3).position.z));
        Vector3 shoulder = zhu.WorldToScreenPoint(new Vector3(target.transform.GetChild(2).position.x + dis0, target.transform.GetChild(3).position.y + shoulder0, target.transform.GetChild(3).position.z));
        Vector3 reach = zhu.WorldToScreenPoint(new Vector3(target.transform.GetChild(2).position.x + dis0 + reach0, target.transform.GetChild(3).position.y + shoulder0, target.transform.GetChild(3).position.z));
        for(int i = (int)outside.x; i <= dis.x; i++)
        {
            t3.SetPixel(i, (int)dis.y, UnityEngine.Color.black);
        }
        for (int i = (int)dis.y; i <= shoulder.y; i++)
        {
            t3.SetPixel((int)shoulder.x, i, UnityEngine.Color.black);
        }
        for (float i = 0; i < 120; i+=0.1f)
        {
            double b = ((i + 120) * (Math.PI)) / 180;
            int x2 = (int)((reach.x - shoulder.x) * Math.Cos(b) + shoulder.x);
            int y2 = (int)((reach.x - shoulder.x) * Math.Sin(b) + shoulder.y);
            t3.SetPixel(x2, y2, UnityEngine.Color.black);
        }
        t3.Apply();
        string path3 = filename3;
        File.WriteAllBytes(path3, t3.EncodeToJPG());
        if (File.Exists(path3))
        {
            FileStream fs = new FileStream(path3, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;
            int local = ((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min+gap).y + (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max+gap).y) / 2;
            local = height - local;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            String str1 = target.GetComponent<BoxCollider>().bounds.size.y.ToString("0.00") + "m";
            if (target.GetComponent<BoxCollider>().bounds.size.y > 2.7)
            {
                str1 = strAdd(str1);
            }
            String str2 = dis0.ToString("0.00") + "m";
            String str3 = "肩高"+shoulder0.ToString("0.00") + "m";
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            g.DrawString(stretch0.ToString("0.00") + "m", font, sbrush, new PointF(shoulder.x, height-shoulder.y));
            g.DrawString(str1, font, sbrush, new PointF(500, local));
            g.DrawString(str2, font, sbrush, new PointF(outside.x, height - inside.y));
            g.DrawString(str3, font, sbrush, new PointF(shoulder.x, height - (shoulder.y + dis.y) / 2));
            img.Save(path3, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        Destroy(target);
    }

    //BUP可视性
    public void BUPframeVisual(GameObject target1, GameObject human)
    {
        zhu.orthographicSize = 1.5f;
        up.orthographicSize = 1.5f;
        left.orthographicSize = 1.5f;
        GameObject target = Instantiate(target1, target1.transform.position, target1.transform.rotation);
        target.GetComponent<Transform>().localPosition = new Vector3(-26.6f, 1.539f, -2.31f);
        target.GetComponent<Transform>().localEulerAngles = new Vector3(0, -90f, 0);
        //target.GetComponent<MeshRenderer>().material = mat;
        if (target.GetComponent<Panel>())
        {
            target.GetComponent<Panel>().ChangeMesh(mat);
        }
        target.layer = 8;
        target.AddComponent<BoxCollider>();

        //创建主视图
        string filename3 = System.Environment.CurrentDirectory + @"\Output\BUP2.jpg";
        RenderTexture rt3 = new RenderTexture(Screen.width, Screen.height, 16);
        zhu.GetComponent<Camera>().targetTexture = rt3;
        zhu.GetComponent<Camera>().Render();
        RenderTexture.active = rt3;
        Texture2D t3 = new Texture2D(Screen.width, Screen.height);
        t3.ReadPixels(new Rect(0, 0, t3.width, t3.height), 0, 0);
        
        Vector3 up1 = zhu.WorldToScreenPoint(target.transform.GetChild(0).position);
        Vector3 down = zhu.WorldToScreenPoint(target.transform.GetChild(1).position);
        Vector3 outside = zhu.WorldToScreenPoint(target.transform.GetChild(2).position);
        Vector3 inside = zhu.WorldToScreenPoint(target.transform.GetChild(3).position);
        for (int i = 0; i < target.transform.childCount; i++)
        {
            if (target.transform.GetChild(i).name.Contains("前沿"))
            {
                Transform cur = target.transform.GetChild(i);
                for (int j = 0; j < cur.childCount; j++)
                {
                    if (cur.GetChild(j).name.Equals("b"))
                        outside = cur.GetChild(j).position;
                }

                break;
            }
        }
        //盘台高度
        gap = target.GetComponent<Transform>().position - target1.GetComponent<Transform>().position;
        int c = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).x;
        int d = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y;
        for (int i = 500; i < c; i++)
        {
            t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y, UnityEngine.Color.black);
            t3.SetPixel(i, (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y, UnityEngine.Color.black);

        }

        for (int i = (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min + gap).y; i < (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max + gap).y; i++)
        {
            t3.SetPixel(500, i, UnityEngine.Color.black);
        }

        //获取人体数据
        float dis0 = huamnTargetDistance;
        float eye0 = eyeHeightZhan;

        Vector3 dis = zhu.WorldToScreenPoint(new Vector3(target.transform.GetChild(2).position.x + dis0, target.transform.GetChild(3).position.y, target.transform.GetChild(3).position.z));
        Vector3 eye = zhu.WorldToScreenPoint(new Vector3(target.transform.GetChild(2).position.x + dis0, target.transform.GetChild(3).position.y + eye0, target.transform.GetChild(3).position.z));

        for (int i = (int)outside.x; i <= dis.x; i++)
        {
            t3.SetPixel(i, (int)dis.y, UnityEngine.Color.black);
        }
        for (int i = (int)dis.y; i <= eye.y; i++)
        {
            t3.SetPixel((int)eye.x, i, UnityEngine.Color.black);
        }
        float k1 = (eye.y - up1.y) / (eye.x - up1.x);
        float b1 = eye.y - k1 * eye.x;
        float k2 = (eye.y - down.y) / (eye.x - down.x);
        float b2 = eye.y - k2 * eye.x;
        for (int i = (int)up1.x; i < eye.x; i++)
        {
            t3.SetPixel(i, (int)(k1 * i + b1), UnityEngine.Color.black);
        }
        for (int i = (int)down.x; i < eye.x; i++)
        {
            t3.SetPixel(i, (int)(k2 * i + b2), UnityEngine.Color.black);
        }
        double angle1 = Angle(new Point((int)up1.x, (int)up1.y), new Point((int)down.x, (int)down.y), new Point((int)eye.x, (int)eye.y));
        double angle2 = Angle(new Point((int)down.x, (int)down.y), new Point((int)up1.x, (int)up1.y), new Point((int)eye.x, (int)eye.y));
        t3.Apply();
        string path3 = filename3;
        File.WriteAllBytes(path3, t3.EncodeToJPG());
        if (File.Exists(path3))
        {
            FileStream fs = new FileStream(path3, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = new Bitmap(ms);

            int height = Screen.height;
            int local = ((int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.min+gap).y + (int)zhu.WorldToScreenPoint(target.GetComponent<BoxCollider>().bounds.max+gap).y) / 2;
            local = height - local;

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            String str1 = target.GetComponent<BoxCollider>().bounds.size.y.ToString("0.00") + "m";
            if (target.GetComponent<BoxCollider>().bounds.size.y > 2.7)
            {
                str1 = strAdd(str1);
            }
            String str2 = angle1.ToString("0.0") + "°";
            if (angle1 < 40)
            {
                str2 = strAdd(str2);
            }

            String str3 = angle2.ToString("0.0") + "°";
            if (angle2 < 40)
            {
                str3 = strAdd(str3);
            }
            String str4 = dis0.ToString("0.0") + "m";
            String str5 = "视高"+eye0.ToString("0.0") + "m";
            System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(System.Drawing.Color.Black);
            g.DrawString(str1, font, sbrush, new PointF(500, local));
            g.DrawString(str2, font, sbrush, new PointF(up1.x, height-up1.y));
            g.DrawString(str3, font, sbrush, new PointF(down.x, height - down.y));
            g.DrawString(str4, font, sbrush, new PointF(outside.x, height - inside.y));
            g.DrawString(str5, font, sbrush, new PointF(eye.x, height - (eye.y+dis.y)/2));
            img.Save(path3, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        Destroy(target);
    }

    public static string strAdd(string str)
    {
        return "*" + str + "*";
    }

    public void showJPG(string path)
    {
        //建立新的系统进程    
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        //设置文件名，此处为图片的真实路径+文件名    
        process.StartInfo.FileName = path;
        //此为关键部分。设置进程运行参数，此时为最大化窗口显示图片。    
        process.StartInfo.Arguments = "rundll32.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
        //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true    
        process.StartInfo.UseShellExecute = true;
        //此处可以更改进程所打开窗体的显示样式，可以不设    
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.Start();
    }

    float getEyeHeiht(string choose, string table)
    {
        string sql;
        float res = 0f;
        SqliteAssist sqliteAssist = new SqliteAssist();
        if (table.Equals("zuozi"))
        {
            sql = "select seegleHeight from zuozi where percentage = '" + choose + "'";

            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["seegleHeight"].ToString());
            }
        }
        else
        {
            sql = "select seegle from zhanzi where percentage = '" + choose + "'";
            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["seegle"].ToString());
            }
        }
        print(res);
        return res / 100f;
    }

    float getSoulderHeiht(string choose, string table)
    {
        string sql;
        float res = 0f;
        SqliteAssist sqliteAssist = new SqliteAssist();
        if (table.Equals("zuozi"))
        {
            sql = "select shoulderHeight from zuozi where percentage = '" + choose + "'";
            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["shoulderHeight"].ToString());
            }
        }
        else
        {
            sql = "select shoulder from zhanzi where percentage = '" + choose + "'";
            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["shoulder"].ToString());
            }
        }
        return res / 100f;
    }

    float getStrech(string choose, string table)
    {
        string sql;
        float res = 0f;
        SqliteAssist sqliteAssist = new SqliteAssist();
        if (table.Equals("zuozi"))
        {
            sql = "select stretch from zuozi where percentage = '" + choose + "'";
            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["stretch"].ToString());
            }
        }
        else
        {
            sql = "select stretch from zhanzi where percentage = '" + choose + "'";
            SQLiteDataReader dr = sqliteAssist.GetQueryResult(sql);
            while (dr.Read())
            {
                res = float.Parse(dr["stretch"].ToString());
            }
        }
        return res/100f;
    }

    void getDataZhan(string choose, GameObject human)
    {
        if ("场景".Equals(choose))
        {
            eyeHeightZhan = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y + 0.05f;
            shoulderHeightZhan = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y - 0.1f;
            stretch0 = 0.792f;
        }
        else
        {
            
            eyeHeightZhan = getEyeHeiht(choose, "zhanzi");
            shoulderHeightZhan = getSoulderHeiht(choose, "zhanzi");
            stretch0 = getStrech(choose, "zhanzi");
        }
        
    }

    void getDataZuo(string choose, GameObject human)
    {
        if ("场景".Equals(choose))
        {
            eyeHeightZuo = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y+0.05f;
            shoulderHeightZuo = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y - 0.1f;
            stretch0 = 0.792f;
        }
        else
        {
            eyeHeightZuo = getEyeHeiht(choose, "zuozi");
            shoulderHeightZuo = getSoulderHeiht(choose, "zuozi");
            stretch0 = getStrech(choose, "zuozi");
        }
    }
}
