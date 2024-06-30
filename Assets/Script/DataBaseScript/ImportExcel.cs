using UnityEngine;
using System.Data.OleDb;
using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XWPF.UserModel;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Windows.Forms;
using System.Text;
using System.Collections;

public class ImportExcel : MonoBehaviour
{
    /// <summary>
    /// 将excel导入到datatable
    /// </summary>
    /// <param name="filePath">excel路径</param>
    /// <returns>返回datatable</returns>
    public GameObject importPanel;
    public GameObject importOkPanel;
    public InputField ImportDescriptionInputField;
    public Dropdown posSelect;
    DataTable ExcelTable = null;
    FileStream fs = null;
    DataColumn column = null;
    DataRow dataRow = null;
    IWorkbook workbook = null;
    XWPFDocument docx = null;
    ISheet sheet = null;
    IRow row = null;
    NPOI.SS.UserModel.ICell cell = null;
    int startRow = 0;
    string filePath;
    //private SqlConnection sqlCon;
    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    string height_Standing;
    string seegle_Standing;
    string shoulder_Standing;
    string fingertips_Standing;
    string stretch_Standing;
    string axisDistance_Standing;
    string centralAxisToEye_Standing;
    string eyeToSide_Standing;
    string shoulderToSide_Standing;
    string percentage_Standing;

    string belowTheKnee_Sitting;
    string heightAboveChair_Sitting;
    string seegleAboveChair_Sitting;
    string shoulderAboveChair_Sitting;
    string stretch_Sitting;
    string thigh_Sitting;
    string hipToKneeMesial_Sitting;
    string kneeHeight_Sitting;
    string axisDistance_Sitting;
    string centralAxisToEye_Sitting;
    string seegleHeight_Sitting;
    string shoulderHeight_Sitting;
    string hipToKnee_Sitting;
    string foot_Sitting;
    string bodyToToes_Sitting;
    string eyeToSide_Sitting;
    string shoulderToSide_Sitting;
    string percentage_Sitting;
    private string height;

    void Start()
    {
        DataTable dt = new DataTable();
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        posSelect.value = 0;
    }

    public void ImportBtnClicked()//打开文件浏览器选择要导入的Excel文件
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "Excel文件(*.xls)\0*.xls\0Excel文件(*.xlsx)\0*.xlsx\0Word文件(*.docx;*.doc)\0*.docx;*.doc\0\0";//|Excel文件(*.xls)|*.xls，word文件（*.docx）
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            filePath = openFileName.file;
            ImportDescriptionInputField.text = filePath;

        }

        //OpenFileDialog fd = new OpenFileDialog();
        //if (fd.ShowDialog() == DialogResult.OK)
        //{
        //    string fileName = fd.FileName;
        //    filePath = Path.GetFullPath(fd.FileName);
        //}
    }

    public void ExcelToDataTable() //站姿
    {
        try
        {
           // StringBuilder sb = new StringBuilder();
            using (fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 解决版本兼容
                //07以前为xls,以后为xlsx
                if (filePath.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filePath.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else if (filePath.IndexOf(".docx") > 0)
                {
                    docx = new XWPFDocument(fs);
                }

                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet
                    ExcelTable = new DataTable();
                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum;//总行数
                        if (rowCount > 0)
                        {
                            IRow firstRow = sheet.GetRow(1);//第二行
                            int cellCount = firstRow.LastCellNum;//列数
                                                                 //创建datatable的列
                            startRow = 1;//因为第一行是中文列名所以直接从第二行开始读取
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                cell = firstRow.GetCell(i);
                                cell.SetCellType(CellType.String);
                                if (cell != null)
                                {
                                    if (cell.StringCellValue != null)
                                    {
                                        column = new DataColumn(cell.StringCellValue);
                                        ExcelTable.Columns.Add(column);
                                    }
                                }
                            }

                            //填充datatable行
                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                row = sheet.GetRow(i);
                                if (row == null) continue;

                                dataRow = ExcelTable.NewRow();
                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    cell = row.GetCell(j);
                                    if (cell == null)
                                    {
                                        dataRow[j] = "";
                                    }
                                    else
                                    {
                                        switch (cell.CellType)
                                        {
                                            case CellType.Blank:
                                                dataRow[j] = "";
                                                break;
                                            case CellType.Numeric:
                                                short format = cell.CellStyle.DataFormat;

                                                //对时间格式的处理
                                                if (format == 14 || format == 31 || format == 57 || format == 58)
                                                    dataRow[j] = cell.DateCellValue;
                                                else
                                                    dataRow[j] = cell.NumericCellValue;
                                                break;
                                            case CellType.String:
                                                dataRow[j] = cell.StringCellValue;
                                                break;
                                        }
                                    }
                                }

                                ExcelTable.Rows.Add(dataRow);

                            }
                        }
                    }
                }

                

            }
            //由于excel表在删除一张表的时候回再次读取回出现空行的原因
            //所以需要一个删除空行的方法⇣⇣⇣⇣
            if (workbook != null)
            {
                List<DataRow> removelist = new List<DataRow>();
                for (int i = 0; i < ExcelTable.Rows.Count; i++)
                {
                    bool IsNull = true;
                    for (int j = 0; j < ExcelTable.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(ExcelTable.Rows[i][j].ToString().Trim()))
                        {
                            IsNull = false;
                        }
                    }
                    if (IsNull)
                    {
                        removelist.Add(ExcelTable.Rows[i]);
                    }
                }
                for (int i = 0; i < removelist.Count; i++)
                {
                    ExcelTable.Rows.Remove(removelist[i]);
                }
                removelist.Clear();
            }


            //sqlCon = new SqlConnection(sqlAddress);
            //sqlCon.Open();
            if (workbook != null)
            {
                for (int k = 0; k < ExcelTable.Rows.Count; k++)   //将数组中的数据取出显示在下拉列表中
                {

                    height_Standing = Convert.ToString(ExcelTable.Rows[k][0]);
                    seegle_Standing = Convert.ToString(ExcelTable.Rows[k][1]);
                    shoulder_Standing = Convert.ToString(ExcelTable.Rows[k][2]);
                    fingertips_Standing = Convert.ToString(ExcelTable.Rows[k][3]);
                    stretch_Standing = Convert.ToString(ExcelTable.Rows[k][4]);
                    axisDistance_Standing = Convert.ToString(ExcelTable.Rows[k][5]);
                    centralAxisToEye_Standing = Convert.ToString(ExcelTable.Rows[k][6]);
                    eyeToSide_Standing = Convert.ToString(ExcelTable.Rows[k][7]);
                    shoulderToSide_Standing = Convert.ToString(ExcelTable.Rows[k][8]);
                    percentage_Standing = Convert.ToString(ExcelTable.Rows[k][9]);
                    string ActInsert = "insert into zhanzi (height,seegle,shoulder,fingertips,stretch,axisDistance,centralAxisToEye,eyeToSide,shoulderToSide,percentage) values ('" + height_Standing + "','" + seegle_Standing + "','" + shoulder_Standing + "','" + fingertips_Standing + "','" + stretch_Standing + "','" + axisDistance_Standing + "','" + centralAxisToEye_Standing + "','" + eyeToSide_Standing + "','" + shoulderToSide_Standing + "','" + percentage_Standing + "')";
                    sqlconnect.Instance.sqladd(ActInsert);

                    //SqlCommand sqlCommand = new SqlCommand(ActInsert, sqlCon);
                    //sqlCommand.ExecuteNonQuery();
                }
            }

            if (docx != null)
            {
                ArrayList arraylist = new ArrayList();
                //读取段落
                foreach (var para in docx.Paragraphs)
                {
                    string text = para.ParagraphText; //获得文本
                    arraylist.Add(para.ParagraphText);
                    Debug.Log(text);
                    //var runs = para.Runs;
                    ////string styleid = para.Style;
                    //for (int i = 0; i < runs.Count; i++)
                    //{
                    //    var run = runs[i];
                    //    text = run.ToString(); //获得run的文本
                    //    //sb.Append(text + ",");
                    //    Debug.Log(text);
                    //}
                }
                //height_Standing = Convert.ToString(arraylist[0]);
                //seegle_Standing = Convert.ToString(arraylist[1]);
                //shoulder_Standing = Convert.ToString(arraylist[2]);
                //fingertips_Standing = Convert.ToString(arraylist[3]);
                //stretch_Standing = Convert.ToString(arraylist[4]);
                //axisDistance_Standing = Convert.ToString(arraylist[5]);
                //centralAxisToEye_Standing = Convert.ToString(arraylist[6]);
                //eyeToSide_Standing = Convert.ToString(arraylist[7]);
                //shoulderToSide_Standing = Convert.ToString(arraylist[8]);
                //percentage_Standing = Convert.ToString(arraylist[9]);
                //string ActInsert = "insert into zhanzi(height,seegle,shoulder,fingertips,stretch,axisDistance,centralAxisToEye,eyeToSide,shoulderToSide,percentage) values ('" + height_Standing + "','" + seegle_Standing + "','" + shoulder_Standing + "','" + fingertips_Standing + "','" + stretch_Standing + "','" + axisDistance_Standing + "','" + centralAxisToEye_Standing + "','" + eyeToSide_Standing + "','" + shoulderToSide_Standing + "','" + percentage_Standing + "')";
                //SqlCommand sqlCommand = new SqlCommand(ActInsert, sqlCon);
                //sqlCommand.ExecuteNonQuery();

            }
            //sqlCon.Close();
            importOkPanel.SetActive(true);
        }
        catch (Exception ex)
        { MessageBox.Show(ex.ToString()); }
    }

    public void ExcelToDataTable2() //坐姿
    {
        try
        {

            using (fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 解决版本兼容
                //07以前为xls,以后为xlsx
                if (filePath.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filePath.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet
                    ExcelTable = new DataTable();
                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum;//总行数
                        if (rowCount > 0)
                        {
                            IRow firstRow = sheet.GetRow(1);//第二行
                            int cellCount = firstRow.LastCellNum;//列数
                                                                 //创建datatable的列
                            startRow = 1;//因为第一行是中文列名所以直接从第二行开始读取
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                cell = firstRow.GetCell(i);
                                cell.SetCellType(CellType.String);
                                if (cell != null)
                                {
                                    if (cell.StringCellValue != null)
                                    {
                                        column = new DataColumn(cell.StringCellValue);
                                        ExcelTable.Columns.Add(column);
                                    }
                                }
                            }

                            //填充datatable行
                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                row = sheet.GetRow(i);
                                if (row == null) continue;

                                dataRow = ExcelTable.NewRow();
                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    cell = row.GetCell(j);
                                    if (cell == null)
                                    {
                                        dataRow[j] = "";
                                    }
                                    else
                                    {
                                        switch (cell.CellType)
                                        {
                                            case CellType.Blank:
                                                dataRow[j] = "";
                                                break;
                                            case CellType.Numeric:
                                                short format = cell.CellStyle.DataFormat;

                                                //对时间格式的处理
                                                if (format == 14 || format == 31 || format == 57 || format == 58)
                                                    dataRow[j] = cell.DateCellValue;
                                                else
                                                    dataRow[j] = cell.NumericCellValue;
                                                break;
                                            case CellType.String:
                                                dataRow[j] = cell.StringCellValue;
                                                break;
                                        }
                                    }
                                }

                                ExcelTable.Rows.Add(dataRow);

                            }
                        }
                    }
                }
            }
            //由于excel表在删除一张表的时候回再次读取回出现空行的原因
            //所以需要一个删除空行的方法⇣⇣⇣⇣
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < ExcelTable.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < ExcelTable.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(ExcelTable.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(ExcelTable.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                ExcelTable.Rows.Remove(removelist[i]);
            }
            removelist.Clear();

            //sqlCon = new SqlConnection(sqlAddress);
            //sqlCon.Open();
            for (int k = 0; k < ExcelTable.Rows.Count; k++)   //将数组中的数据取出显示在下拉列表中
            {
                belowTheKnee_Sitting = Convert.ToString(ExcelTable.Rows[k][0]);
                heightAboveChair_Sitting = Convert.ToString(ExcelTable.Rows[k][1]);
                seegleAboveChair_Sitting = Convert.ToString(ExcelTable.Rows[k][2]);
                shoulderAboveChair_Sitting = Convert.ToString(ExcelTable.Rows[k][3]);
                stretch_Sitting = Convert.ToString(ExcelTable.Rows[k][4]);
                thigh_Sitting = Convert.ToString(ExcelTable.Rows[k][5]);
                hipToKneeMesial_Sitting = Convert.ToString(ExcelTable.Rows[k][6]);
                kneeHeight_Sitting = Convert.ToString(ExcelTable.Rows[k][7]);
                axisDistance_Sitting = Convert.ToString(ExcelTable.Rows[k][8]);
                centralAxisToEye_Sitting = Convert.ToString(ExcelTable.Rows[k][9]);
                seegleHeight_Sitting = Convert.ToString(ExcelTable.Rows[k][10]);
                shoulderHeight_Sitting = Convert.ToString(ExcelTable.Rows[k][11]);
                hipToKnee_Sitting = Convert.ToString(ExcelTable.Rows[k][12]);
                foot_Sitting = Convert.ToString(ExcelTable.Rows[k][13]);
                bodyToToes_Sitting = Convert.ToString(ExcelTable.Rows[k][14]);
                eyeToSide_Sitting = Convert.ToString(ExcelTable.Rows[k][15]);
                shoulderToSide_Sitting = Convert.ToString(ExcelTable.Rows[k][16]);
                percentage_Sitting = Convert.ToString(ExcelTable.Rows[k][17]);

                string ActInsert = "insert into zuozi (belowTheKnee,heightAboveChair,seegleAboveChair,shoulderAboveChair,stretch,thigh,hipToKneeMesial,kneeHeight,axisDistance,centralAxisToEye,seegleHeight,shoulderHeight,hipToKnee,foot,bodyToToes,eyeToSide,shoulderToSide,percentage) values ('" + belowTheKnee_Sitting + "','" + heightAboveChair_Sitting + "','" + seegleAboveChair_Sitting + "','" + shoulderAboveChair_Sitting + "','" + stretch_Sitting + "','" + thigh_Sitting + "','" + hipToKneeMesial_Sitting + "','" + kneeHeight_Sitting + "','" + axisDistance_Sitting + "','" + centralAxisToEye_Sitting + "','" + seegleHeight_Sitting + "','" + shoulderHeight_Sitting + "','" +  hipToKnee_Sitting + "','" +  foot_Sitting + "','" +  bodyToToes_Sitting + "','" +  eyeToSide_Sitting + "','" + shoulderToSide_Sitting + "','" + percentage_Sitting+"')";
                sqlconnect.Instance.sqladd(ActInsert);
                //SqlCommand sqlCommand = new SqlCommand(ActInsert, sqlCon);
                //sqlCommand.ExecuteNonQuery();
            }
            //sqlCon.Close();
            importOkPanel.SetActive(true);
        }
        catch (Exception ex)
        { MessageBox.Show(ex.ToString()); }
    }


    //将excel数据存入数据库
    public void ImportMethod()
    {
        if (posSelect.value == 0)
        {
            ExcelToDataTable();
        }
        else
        {
            if (posSelect.value == 1)
            {
                ExcelToDataTable2();
            }
        }
    }



}




