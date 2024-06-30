//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using NPOI.SS.UserModel;
//using System.Data.SqlClient;
//using System.Data;

//public class ReportOut : MonoBehaviour
//{
//    public Toggle open;
//    public Dropdown format;
//    int formatSelected;
//    bool ifOpen;
//    private int hour;
//    private int minute;
//    private int second;
//    private int year;
//    private int month;
//    private int day;
//    private string filePath;
//    private FileStream fs;
//    public InputField name, renyuan, chanpin;
//    private string na, reny, chanp;
//    public text pengzhuang;

//    // Start is called before the first frame update
//    void Start()
//    {
//        var button = GetComponent<Button>();
//        button.onClick.AddListener(OnClick);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void OnClick()
//    {
//        if (open != null)
//        {
//            ifOpen = open.isOn;
//        }
//        if (format != null)
//        {
//            //获取当前时间
//            hour = DateTime.Now.Hour;
//            minute = DateTime.Now.Minute;
//            second = DateTime.Now.Second;
//            year = DateTime.Now.Year;
//            month = DateTime.Now.Month;
//            day = DateTime.Now.Day;

//            na = name.text;
//            reny = renyuan.text;
//            chanp = chanpin.name;

//            //value = 1是doc，= 2是txt
//            formatSelected = format.value;
//            if (formatSelected == 0)
//            {
//                format_excel();
//            }
//            else
//            {
//                format_txt();
//            }
//        }
//        if (ifOpen)
//        {
//            System.Diagnostics.Process.Start(filePath);
//        }
//    }

//    public void format_txt()
//    {
//        filePath = "";
//        string path = EditorUtility.SaveFilePanel("Output Log", Application.dataPath, "Report" + year + month + day + hour + minute, "txt");
//        string info1 = "测试名称：" + na + "\n" + "测试人员: " + reny + " \n" + "测试产品: " + chanp + " \n" + "日期：" + year + month + day + hour + minute + "\n";
//        string info2 = "测试内容" + "\n" + "可视性分析: " + "视锥角度：40 " + "\n";
//        string info3 = "可达性分析: " + " 未设置待检测物体 " + "\n";
//        string info4 = "操作空间分析:" + "" + "\n";
//        string info5 = "舒适性分析: " + " 关节平均得分： 3.4  " + "\n";
//        string info6 = "辅助装置布局分析：" + " 脚限最优距离：40-100 " + "\n";
//        string information = info1 + info2 + info3 + info4 + info5 + info6;

//        if (path.Length != 0)
//        {
//            FileStream aFile = new FileStream(@"" + path, FileMode.OpenOrCreate);
//            StreamWriter sw = new StreamWriter(aFile);
//            sw.Write(information);
//            sw.Close();
//            sw.Dispose();

//        }
//        filePath = path;
//        Debug.Log("导出文本文档成功" + path);
//    }

//    public void format_excel()
//    {
//        filePath = "";
//        string path = EditorUtility.SaveFilePanel("Output Log", Application.dataPath, "Report" + year + month + day + hour + minute, "xls");

//        NPOI.HSSF.UserModel.HSSFWorkbook wk = new NPOI.HSSF.UserModel.HSSFWorkbook();
//        ISheet sheet = wk.CreateSheet(na);//excel表格的sheet名称，可自行更改
//        int columns = 1;
//        int rows = 1;
//        //int columns = ds.Tables[0].Columns.Count;
//        //int rows = ds.Tables[0].Rows.Count;
//        IRow rowHead = sheet.CreateRow(0);
//        for (int j = 0; j < columns; j++)
//        {
//            rowHead.CreateCell(j, CellType.String).SetCellValue("test");
//            //rowHead.CreateCell(j, CellType.String).SetCellValue(ds.Tables[0].Columns[j].ToString());
//        }

//        for (int i = 0; i < rows; i++)
//        {
//            IRow row = sheet.CreateRow(i + 1);
//            for (int j = 0; j < columns; j++)
//            {
//                row.CreateCell(j).SetCellValue("1");
//            }
//        }
//        fs = File.Create(path);//文件保存路径和名称，可自行更改
//        wk.Write(fs);
//        fs.Close();
//        fs.Dispose();

//        filePath = path;
//        Debug.Log("导出Excel成功");

//    }
//}
