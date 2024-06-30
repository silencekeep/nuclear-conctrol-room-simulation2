using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using NPOI.SS.UserModel;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;

public class DaocKT3 : MonoBehaviour
{
    public InputField renyuan;
    private FileStream fs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Daochu()
    {
        //filePath = "";
        //string path = Application.dataPath + "/ZMF/Report/评价报告" + year + month + day + hour + minute + ".xls";
        string path = System.Environment.CurrentDirectory + "/shuju：" + renyuan.text + /*"_" + chanp +*/ ".xls";

        fs = new FileStream("D:/muban3.xls", FileMode.Open, FileAccess.Read);

        HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);

        HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.GetSheet("数据");

        try
        {
            //sheet1.GetRow(42).GetCell(4).SetCellValue(chanp);

            sheet1.GetRow(0).GetCell(1).SetCellValue(renyuan.text);

            //sheet1.GetRow(48).GetCell(4).SetCellValue(year + "." + month + "." + day);

            //sheet1.GetRow(53).GetCell(1).SetCellValue(month);

            //sheet1.GetRow(53).GetCell(2).SetCellValue(day);
        }
        catch (Exception e3)
        {
            Debug.Log(e3);
        }


        //Force excel to recalculate all the formulawhile open
        sheet1.ForceFormulaRecalculation = true;

        FileStream file = new FileStream(path, FileMode.Create);

        hssfworkbook.Write(file);

        file.Close();

        System.Diagnostics.Process.Start(path);
    }
}
