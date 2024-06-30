using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using NPOI.XWPF.Extractor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static NPOI.HSSF.Util.HSSFColor;
using Aspose.Words;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CNCC.Panels;
using static UnityEngine.InputSystem.HID.HID;
using Aspose.Words.Drawing;
using NPOI.OpenXml4Net.OPC;
using System.Xml;
using TMPro;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Spreadsheet;
using System.Drawing;
using NPOI.XSSF.UserModel;
using Paragraph = iTextSharp.text.Paragraph;
using Font = iTextSharp.text.Font;
using static RootMotion.FinalIK.GrounderQuadruped;

//using Spire.Doc;
//using Spire.Doc.Documents;
//using System.Drawing;
//using System.IO;
//using System.Text;

public class AnalyseUIController : MonoBehaviour
{
    private FileStream fs;
    public GameObject tab;
    public GameObject InfoTable;
    public GameObject InfoRow_Prefab;
    public int i=0;
    public void addInfo(string target, string human, string standard, string description, string value, string result, string category)
    {
        tab.SetActive(true);
        i++;
        GameObject table = InfoTable;
        GameObject row = Instantiate(InfoRow_Prefab, table.transform.position, table.transform.rotation) as GameObject;
        row.name = "row" + i;
        row.transform.SetParent(table.transform);
        row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
        row.transform.Find("id").GetComponent<UnityEngine.UI.Text>().text = i.ToString();
        row.transform.Find("target").GetComponent<UnityEngine.UI.Text>().text = target;
        row.transform.Find("human").GetComponent<UnityEngine.UI.Text>().text = human;
        row.transform.Find("standard").GetComponent<UnityEngine.UI.Text>().text = standard;
        row.transform.Find("description").GetComponent<UnityEngine.UI.Text>().text = description;
        row.transform.Find("value").GetComponent<UnityEngine.UI.Text>().text = value;
        row.transform.Find("result").GetComponent<UnityEngine.UI.Text>().text = result;
        row.transform.Find("category").GetComponent<UnityEngine.UI.Text>().text = category;
    }
    public void refresh()
    {

        i = 0;
        if (InfoTable.transform.childCount > 0)
        {
            for (int j = 0; j < InfoTable.transform.childCount; j++)
            {
                Destroy(InfoTable.transform.GetChild(j).gameObject);
            }
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void EXCELExport()
    {
        GameObject table = InfoTable; //获取行
        int j = table.transform.childCount;//获取行内容

        System.Data.DataSet dc = new System.Data.DataSet();
        string[] id_arr = new string[j];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] target_arr = new string[j];
        string[] human_arr = new string[j];
        string[] standard_arr = new string[j];
        string[] description_arr = new string[j];
        string[] value_arr = new string[j];
        string[] result_arr = new string[j];
        string[] category_arr = new string[j];

        for (int i = 0; i < j; i++)   //将数据表中的数据放进数组中
        {
            string id = table.transform.GetChild(i).transform.Find("id").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动素序号ActID数据放进数组中
                                                                                                                    //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                                                                    //dc.Tables[0].Rows[i][7])表示取数据表中第一列所有数据
            if (id != null)
                id_arr[i] = id;

            string target = table.transform.GetChild(i).transform.Find("target").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动素类别ActType数据放进数组中
            if (target != null)
                target_arr[i] = target;

            string human = table.transform.GetChild(i).transform.Find("human").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动作类别ActGenType数据放进数组中
            if (human != null)
                human_arr[i] = human;

            string standard = table.transform.GetChild(i).transform.Find("standard").GetComponent<UnityEngine.UI.Text>().text;
            if (standard != null)
                standard_arr[i] = standard;

            string description = table.transform.GetChild(i).transform.Find("description").GetComponent<UnityEngine.UI.Text>().text;
            if (description != null)
                description_arr[i] = description;

            string value = table.transform.GetChild(i).transform.Find("value").GetComponent<UnityEngine.UI.Text>().text;
            if (value != null)
                value_arr[i] = value;

            string result = table.transform.GetChild(i).transform.Find("result").GetComponent<UnityEngine.UI.Text>().text;
            if (result != null)
                result_arr[i] = result;

            string category = table.transform.GetChild(i).transform.Find("category").GetComponent<UnityEngine.UI.Text>().text;
            if (category != null)
                category_arr[i] = category;

        }
        NPOI.HSSF.UserModel.HSSFWorkbook wk = new NPOI.HSSF.UserModel.HSSFWorkbook();
        ISheet sheet = wk.CreateSheet("控制室人体工效学设计评估");
        //ExcelFile excelFile = new ExcelFile();
        //ExcelWorksheet sheet = excelFile.Worksheets.Add("Test");
        int columns = 8;
        int rows = j;
        //row = sheet.CreateRow(rows);
        //cell = row.CreateCell(0);
        //cell.SetCellValue(i);
        sheet.SetColumnWidth(1, 25 * 256);
        sheet.SetColumnWidth(3, 25 * 256);
        sheet.SetColumnWidth(4, 35 * 256);
        sheet.SetColumnWidth(5, 35 * 256);
        sheet.SetColumnWidth(7, 15 * 256);

        ICellStyle centerAlignmentStyle = wk.CreateCellStyle();
        centerAlignmentStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        centerAlignmentStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

        IRow rowHead = sheet.CreateRow(0);
        string[] title = {"编号", "盘台","数字人","标准条例","标准描述","实际值","是否符合","分类"};

        for (int k = 0; k < columns; k++)
        {
            NPOI.SS.UserModel.ICell cell = rowHead.CreateCell(k, CellType.String);
            cell.SetCellValue(title[k]);
            cell.CellStyle = centerAlignmentStyle; // 设置单元格样式为居中对齐
            //rowHead.CreateCell(k, CellType.String).SetCellValue(title[k]);                                          
        }

        for (int i = 0; i < rows; i++)
        {
            IRow row = sheet.CreateRow(i + 1);

            row.CreateCell(0).SetCellValue(id_arr[i]);
            row.CreateCell(1).SetCellValue(target_arr[i]);
            row.CreateCell(2).SetCellValue(human_arr[i]);
            row.CreateCell(3).SetCellValue(standard_arr[i]);
            row.CreateCell(4).SetCellValue(description_arr[i]);
            row.CreateCell(5).SetCellValue(value_arr[i]);
            row.CreateCell(6).SetCellValue(result_arr[i]);
            row.CreateCell(7).SetCellValue(category_arr[i]);

            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                row.GetCell(columnIndex).CellStyle = centerAlignmentStyle;
            }
        }
        var path = System.Environment.CurrentDirectory + "/控制室人体工效学设计评估.xls";                
        if (path.Length > 0)
        {
            fs = File.Create(path);
            wk.Write(fs);
            fs.Close();
            fs.Dispose();
            Debug.Log("111111"); 
        }
    }


    public void WORDExport()
    {
        GameObject table = InfoTable; //获取行
        int j = table.transform.childCount;  //获取行内容

        System.Data.DataSet dc = new System.Data.DataSet();
        string[] id_arr = new string[j];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] target_arr = new string[j];
        string[] human_arr = new string[j];
        string[] standard_arr = new string[j];
        string[] description_arr = new string[j];
        string[] value_arr = new string[j];
        string[] result_arr = new string[j];
        string[] category_arr = new string[j];

        for (int i = 0; i < j; i++)   //将数据表中的数据放进数组中
        {
            string id = table.transform.GetChild(i).transform.Find("id").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动素序号ActID数据放进数组中
            if (id != null)
                id_arr[i] = id;

            string target = table.transform.GetChild(i).transform.Find("target").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动素类别ActType数据放进数组中
            if (target != null)
                target_arr[i] = target;

            string human = table.transform.GetChild(i).transform.Find("human").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动作类别ActGenType数据放进数组中
            if (human != null)
                human_arr[i] = human;

            string standard = table.transform.GetChild(i).transform.Find("standard").GetComponent<UnityEngine.UI.Text>().text;
            if (standard != null)
                standard_arr[i] = standard;

            string description = table.transform.GetChild(i).transform.Find("description").GetComponent<UnityEngine.UI.Text>().text;
            if (description != null)
                description_arr[i] = description;

            string value = table.transform.GetChild(i).transform.Find("value").GetComponent<UnityEngine.UI.Text>().text;
            if (value != null)
                value_arr[i] = value;

            string result = table.transform.GetChild(i).transform.Find("result").GetComponent<UnityEngine.UI.Text>().text;
            if (result != null)
                result_arr[i] = result;

            string category = table.transform.GetChild(i).transform.Find("category").GetComponent<UnityEngine.UI.Text>().text;
            if (category != null)
                category_arr[i] = category;
        }
        var uploadPath = System.Environment.CurrentDirectory;
        Debug.Log("finish word11111");

        XWPFDocument m_Doc = new XWPFDocument(); //1、初始化文档
        XWPFParagraph p0 = m_Doc.CreateParagraph();
        p0.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
        XWPFRun r0 = p0.CreateRun();
        r0.FontFamily = "宋体";
        r0.FontSize = 18;
        r0.IsBold = true;
        r0.SetText("控制室人体工效学设计评估");
       
        /*
        //创建文档中的表格对象实例
        XWPFTable firstXwpfTable = m_Doc.CreateTable(j + 1, 8);//显示的行列数rows:j+1行,cols:8列
        firstXwpfTable.Width = 4800;//设置表格宽度
        firstXwpfTable.SetColumnWidth(0, 800);
        string[] title = { "编号", "盘台", "数字人", "标准条例", "标准描述", "实际值", "是否符合", "分类" };
        //Table 表格第一行展示
        for (int i = 0; i < 8; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(0).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, title[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, true, 10));
        }
        Debug.Log("finish word33333");
        for (int i = 0; i < j; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(i + 1).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, id_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, target_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, human_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(3).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, standard_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(4).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, description_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(5).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, value_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(6).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, result_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
            firstXwpfTable.GetRow(i + 1).GetCell(7).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, category_arr[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 10));
        }
        Debug.Log("finish word44444");
        */

        XWPFParagraph p1 = m_Doc.CreateParagraph();
        XWPFRun r1 = p1.CreateRun();
        r1.FontFamily = "宋体";
        r1.FontSize = 15;
        r1.SetText("1 缩略语");

        XWPFTable firstXwpfTable = m_Doc.CreateTable(8, 2);
        firstXwpfTable.Width = 5000;
        for (int row = 0; row < firstXwpfTable.Rows.Count; row++)
        {
            for (int col = 0; col < firstXwpfTable.Rows[row].GetTableCells().Count; col++)
            {   // 获取单元格
                XWPFTableCell cell = firstXwpfTable.Rows[row].GetCell(col);
                // 设置单元格的垂直对齐方式为居中
                cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
            }
        }
        string[] title = { "缩略语", "说明" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(0).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, title[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest1 = { "BUP", "后备盘" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(1).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest1[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest2 = { "DCS", "集散控制系统（包括1层和2层设备）" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(2).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest2[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest3 = { "ECP", "紧急操作台" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(3).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest3[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest4 = { "LDP", "大屏幕" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(4).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest4[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest5 = { "OWP", "操纵员工作站" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(5).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest5[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest6 = { "RSS", "远程停堆站" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(6).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest6[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest7 = { "SVDU", "安全级显示器" };
        for (int i = 0; i < 2; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable.GetRow(7).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable, contest7[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }

        XWPFParagraph p01 = m_Doc.CreateParagraph();
        XWPFRun r01 = p01.CreateRun();
        r01.FontFamily = "宋体";
        r01.FontSize = 15;
        r01.SetText("2 控制室设计概述");

        XWPFParagraph p02 = m_Doc.CreateParagraph();
        XWPFRun r02 = p02.CreateRun();
        r02.FontFamily = "宋体";
        r02.FontSize = 13;
        r02.SetText("2.1 主控制室");

        XWPFParagraph p2 = m_Doc.CreateParagraph();
        p2.IndentationFirstLine = (int)500;
        XWPFRun r2 = p2.CreateRun();
        r2.FontFamily = "宋体";
        r2.FontSize = 13;
        r2.SetText("主控制室的主要目标是实现核电厂在所有运行和事故工况下的安全有效运行。主控制室为主控制室工作人员提供实现电厂运行目标所必需的人机接口和有关的信息和设备，此外，主控制室为主控制室工作人员提供适宜的工作环境，以利于执行任务，而无不适之感和人身危险。");

        XWPFParagraph p3 = m_Doc.CreateParagraph();
        p3.IndentationFirstLine = (int)500;
        XWPFRun r3 = p3.CreateRun();
        r3.FontFamily = "宋体";
        r3.FontSize = 13;
        r3.SetText("主控制室设计的基本目标是及时、准确和完整地向操纵员提供关于电厂设备和系统的功能状态的信息。主控制室设计必须考虑到所有运行状态（包括换料和事故工况）下使任务最佳化，并将检测与控制电厂所要求的工作量减到最小。主控制室还必须向主控制室外的其他设施提供必要的信息。主控制室设计必须提供各项功能最佳分配，以便操纵员和系统能最大限度地发挥其能力。主控制室设计的另一个目标是使电厂能有效地进行试运行、并允许修改和维护。");

        XWPFParagraph p4 = m_Doc.CreateParagraph();
        p4.IndentationFirstLine = (int)500;
        XWPFRun r4 = p4.CreateRun();
        r4.FontFamily = "宋体";
        r4.FontSize = 13;
        r4.SetText("主控制室主要包括以下盘台：");

        XWPFParagraph p5 = m_Doc.CreateParagraph();
        p5.IndentationFirstLine = (int)500;
        XWPFRun r5 = p5.CreateRun();
        r5.FontFamily = "宋体";
        r5.FontSize = 13;
        r5.SetText("操纵员控制台（OWP）");

        XWPFParagraph p6 = m_Doc.CreateParagraph();
        p6.IndentationFirstLine = (int)500;
        XWPFRun r6 = p6.CreateRun();
        r6.FontFamily = "宋体";
        r6.FontSize = 13;
        r6.SetText("紧急操作台（ECP），包括安全级部分（ECP-S）和非安全级部分（ECP-N）");

        XWPFParagraph p7 = m_Doc.CreateParagraph();
        p7.IndentationFirstLine = (int)500;
        XWPFRun r7 = p7.CreateRun();
        r7.FontFamily = "宋体";
        r7.FontSize = 13;
        r7.SetText("大屏幕（LDP）");

        XWPFParagraph p07 = m_Doc.CreateParagraph();
        p07.IndentationFirstLine = (int)500;
        XWPFRun r07 = p07.CreateRun();
        r07.FontFamily = "宋体";
        r07.FontSize = 13;
        r07.SetText("后备盘（BUP）");

        XWPFParagraph p08 = m_Doc.CreateParagraph();
        XWPFRun r08 = p08.CreateRun();
        r08.FontFamily = "宋体";
        r08.FontSize = 13;
        r08.SetText("2.2 远程停堆站");

        XWPFParagraph p09 = m_Doc.CreateParagraph();
        p09.IndentationFirstLine = (int)500;
        XWPFRun r09 = p09.CreateRun();
        r09.FontFamily = "宋体";
        r09.FontSize = 13;
        r09.SetText("当主控制室可用时，远程停堆站控制功能被闭锁。当主控制室由于某种原因（例如发生火灾）变得不可利用时，操纵员可以不需要进入主控制室，就能从远程停堆站停闭反应堆，将核电厂带入并保持在安全停堆状态。");

        XWPFParagraph p010 = m_Doc.CreateParagraph();
        p010.IndentationFirstLine = (int)500;
        XWPFRun r010 = p010.CreateRun();
        r010.FontFamily = "宋体";
        r010.FontSize = 13;
        r010.SetText("远程停堆站设有三套简化的操纵员工作站和RSS-SVDU控制台（安装有序列1、2的SVDU共2台，紧急停堆控制器，以及序列1MCR/RSS切换装置）。");

        XWPFParagraph p011 = m_Doc.CreateParagraph();
        XWPFRun r011 = p011.CreateRun();
        r011.FontFamily = "宋体";
        r011.FontSize = 15;
        r011.SetText("3 人体尺寸基础数据");

        XWPFParagraph p012 = m_Doc.CreateParagraph();
        p012.IndentationFirstLine = (int)500;
        XWPFRun r012 = p012.CreateRun();
        r012.FontFamily = "宋体";
        r012.FontSize = 13;
        r012.SetText("本报告根据GB10000（1988）《中国成年人人体尺寸》中给出的5%成年女性到95%成年男性的人体测量数据，对控制室布局及盘台盘面设计进行人体工效学评价。对于GB10000中缺少的数据采用HAF·J0055（1995）《核电厂控制室设计的人因工程原则》中提供的数据。");

        XWPFParagraph p013 = m_Doc.CreateParagraph();
        p013.IndentationFirstLine = (int)500;
        XWPFRun r013 = p013.CreateRun();
        r013.FontFamily = "宋体";
        r013.FontSize = 13;
        r013.SetText("具体采用数据如下表所示：");

        XWPFTable firstXwpfTable1 = m_Doc.CreateTable(30, 5);
        firstXwpfTable1.Width = 5000;
        for (int row = 0; row < firstXwpfTable1.Rows.Count; row++)
        {
            for (int col = 0; col < firstXwpfTable1.Rows[row].GetTableCells().Count; col++)
            {   // 获取单元格
                XWPFTableCell cell = firstXwpfTable1.Rows[row].GetCell(col);
                // 设置单元格的垂直对齐方式为居中
                cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
            }
        }
        string[] title1 = { " ", " ", "5%女（mm）", "95%男（mm）", "备注" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(0).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, title1[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        //合并单元格
        firstXwpfTable1.GetRow(1).GetCell(0).GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.restart;
        firstXwpfTable1.GetRow(1).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, "站姿", NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        for (int col = 1; col < 5; col++)
        {
            firstXwpfTable1.GetRow(1).GetCell(col).GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.@continue;
        }
        string[] contest8 = { "序号", "项目", " ", " ", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(2).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest8[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest9 = { "1", "身高", "1484", "1775", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(3).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest9[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest10 = { "2", "眼至地面高", "1371", "1664", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(4).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest10[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest11 = { "3", "肩高", "1195", "1455", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(5).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest11[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest12 = { "4", "指尖至地面高", "581", "663", "按以下公式计算：肩高-上臂-前臂-手长" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(6).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest12[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest13 = { "5", "功能伸展范围", "614", "792", "按以下公式计算：上臂+前臂+手长" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(7).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest13[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest14 = { "6", "人体轴线至台边距离", "127", "123", "按HAF·J0055" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(8).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest14[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest15 = { "7", "人体中心轴至眼距离", "76", "82", "按HAF·J0055" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(9).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest15[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest16 = { "8", "眼至盘台前沿", "51", "41", "按以下公式计算：人体轴线至台边距离-人体中心轴至眼距离" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(10).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest16[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest17 = { "9", "肩至盘台前沿", "127", "123", "人体轴线至台边距离" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(11).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest17[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        //合并单元格
        firstXwpfTable1.GetRow(12).GetCell(0).GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.restart;
        firstXwpfTable1.GetRow(12).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, "坐姿", NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        for (int col = 1; col < 5; col++)
        {
            firstXwpfTable1.GetRow(12).GetCell(col).GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.@continue;
        }
        string[] contest18 = { "1", "腿部弯曲部位的高度（膝下方）", "342", "448", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(13).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest18[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest19 = { "2", "椅面以上的身高（伸直）", "809", "958", " " };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(14).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest19[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest20 = { "3", "椅面以上的眼高（伸直）", "695", "847", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(15).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest20[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest21 = { "4", "椅面以上的肩高", "518", "641", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(16).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest21[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest22 = { "5", "功能伸展范围", "614", "792", "按以下公式计算：上臂+前臂+手长" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(17).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest22[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest23 = { "6", "大腿的净高度（膝盖处）", "113", "151", "GB 10000为大腿中部尺寸" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(18).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest23[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest24 = { "7", "臀部至膝弯内侧的距离", "401", "494", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(19).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest24[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest25 = { "8", "膝的高度", "424", "532", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(20).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest25[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest26 = { "9", "人体轴线至台边距离", "127", "123", "按HAF·J0055" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(21).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest26[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest27 = { "10", "人体中心轴至眼距离", "76", "82", "按HAF·J0055" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(22).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest27[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest28 = { "11", "坐姿眼高", "1037", "1295", "按以下公式计算：椅面以上坐姿眼高+小腿加足高" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(23).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest28[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest29 = { "12", "坐姿肩高", "860", "1089", "按以下公式计算：椅面以上坐姿肩高+小腿加足高" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(24).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest29[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest30 = { "13", "臀膝距", "495", "595", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(25).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest30[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest31 = { "14", "脚长", "213", "264", "" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(26).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest31[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest32 = { "15", "身体前端至脚尖距离", "510", "648", "按以下公式计算：臀膝距+脚长*2/3-人体轴线至台边距离" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(27).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest32[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest33 = { "16", "眼至盘台前沿", "51", "41", "按以下公式计算：人体轴线至台边距离-人体中心轴至眼距离" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(28).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest33[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }
        string[] contest34 = { "17", "肩至盘台前沿", "127", "123", "人体轴线至台边距离" };
        for (int i = 0; i < 5; i++)   //将数据表中的数据放进数组中
        {
            firstXwpfTable1.GetRow(29).GetCell(i).SetParagraph(SetTableParagraphInstanceSetting(m_Doc, firstXwpfTable1, contest34[i], NPOI.XWPF.UserModel.ParagraphAlignment.CENTER, 24, false, 13));
        }

        string cellValue = table.transform.GetChild(0).transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text;//第一行第二列内容
        XWPFParagraph p014 = m_Doc.CreateParagraph();
        XWPFRun r014 = p014.CreateRun();
        XWPFParagraph p015 = m_Doc.CreateParagraph();
        XWPFRun r015 = p015.CreateRun();
       

        if (cellValue.Contains("OWP"))
        {
            r014.FontFamily = "宋体";
            r014.FontSize = 15;
            r014.SetText("4 OWP人体工效学评估");

            r015.FontFamily = "宋体";
            r015.FontSize = 13;
            r015.SetText("4.1 OWP可视性视角视距分析 ");

            XWPFParagraph p28 = m_Doc.CreateParagraph();
            p28.IndentationFirstLine = (int)500;
            XWPFRun r28 = p28.CreateRun();
            r28.FontFamily = "宋体";
            r28.FontSize = 13;
            r28.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            //表格样式
            XWPFTable table001 = m_Doc.CreateTable(1, 2);
            table001.Width = 5000;
            table001.GetRow(0).GetCell(0).SetText("标准条例");
            table001.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    XWPFTableRow newRow = table001.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table001.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p29 = m_Doc.CreateParagraph();
            p29.IndentationFirstLine = (int)500;
            XWPFRun r29 = p29.CreateRun();
            r29.FontFamily = "宋体";
            r29.FontSize = 13;
            r29.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对OWP视角视距进行评估，如图4-1所示。");

            var imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            //var imagePath5 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left3.jpg";
            if (File.Exists(imagePath5))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath5, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p30 = m_Doc.CreateParagraph();
            p30.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r30 = p30.CreateRun();
            r30.FontFamily = "宋体";
            r30.FontSize = 12;
            r30.SetText("图4-1 OWP视角视距评估");

            XWPFParagraph p31 = m_Doc.CreateParagraph();
            p31.IndentationFirstLine = (int)500;
            XWPFRun r31 = p31.CreateRun();
            r31.FontFamily = "宋体";
            r31.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r31.AppendText(text);
                }
            }



            XWPFParagraph p22 = m_Doc.CreateParagraph();
            XWPFRun r22 = p22.CreateRun();
            r22.FontFamily = "宋体";
            r22.FontSize = 13;
            r22.SetText("4.2 OWP可达性分析");

            XWPFParagraph p23 = m_Doc.CreateParagraph();
            p23.IndentationFirstLine = (int)500;
            XWPFRun r23 = p23.CreateRun();
            r23.FontFamily = "宋体";
            r23.FontSize = 13;
            r23.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table002 = m_Doc.CreateTable(1, 2);
            table002.Width = 5000;
            table002.GetRow(0).GetCell(0).SetText("标准条例");
            table002.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    XWPFTableRow newRow = table002.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table002.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p24 = m_Doc.CreateParagraph();
            p24.IndentationFirstLine = (int)500;
            XWPFRun r24 = p24.CreateRun();
            r24.FontFamily = "宋体";
            r24.FontSize = 13;
            r24.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对OWP可操作范围进行评估，如图4-2所示。");

            var imagePath4 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            //var imagePath4 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left2.jpg";
            if (File.Exists(imagePath4))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath4, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p25 = m_Doc.CreateParagraph();
            p25.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r25 = p25.CreateRun();
            r25.FontFamily = "宋体";
            r25.FontSize = 12;
            r25.SetText("图4-2 OWP控制台可操作范围评估");

            XWPFParagraph p26 = m_Doc.CreateParagraph();
            p26.IndentationFirstLine = (int)500;
            XWPFRun r26 = p26.CreateRun();
            r26.FontFamily = "宋体";
            r26.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r26.AppendText(text);
                }
            }



            XWPFParagraph p17 = m_Doc.CreateParagraph();
            XWPFRun r17 = p17.CreateRun();
            r17.FontFamily = "宋体";
            r17.FontSize = 13;
            r17.SetText("4.3 OWP盘台尺寸及容膝容足空间分析");

            XWPFParagraph p18 = m_Doc.CreateParagraph();
            p18.IndentationFirstLine = (int)500;
            XWPFRun r18 = p18.CreateRun();
            r18.FontFamily = "宋体";
            r18.FontSize = 13;
            r18.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table003 = m_Doc.CreateTable(1, 2);
            table003.Width = 5000;
            table003.GetRow(0).GetCell(0).SetText("标准条例");
            table003.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台")|| category.Contains("容膝容足"))
                {
                    XWPFTableRow newRow = table003.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table003.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p19 = m_Doc.CreateParagraph();
            p19.IndentationFirstLine = (int)500;
            XWPFRun r19 = p19.CreateRun();
            r19.FontFamily = "宋体";
            r19.FontSize = 13;
            r19.SetText("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对OWP盘台尺寸和容膝容足空间进行分析，如图4-3所示");

            var imagePath3 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            //var imagePath3 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left1.jpg";
            if (File.Exists(imagePath3))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath3, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p21 = m_Doc.CreateParagraph();
            p21.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r21 = p21.CreateRun();
            r21.FontFamily = "宋体";
            r21.FontSize = 12;
            r21.SetText("图4-3 OWP盘台尺寸及容膝容足分析");

            XWPFParagraph p20 = m_Doc.CreateParagraph();
            p20.IndentationFirstLine = (int)500;
            XWPFRun r20 = p20.CreateRun();
            r20.FontFamily = "宋体";
            r20.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r20.AppendText(text);
                }
            }



            XWPFParagraph p35 = m_Doc.CreateParagraph();
            XWPFRun r35 = p35.CreateRun();
            r35.FontFamily = "宋体";
            r35.FontSize = 13;
            r35.SetText("4.4 OWP其他分析");

            XWPFParagraph p36 = m_Doc.CreateParagraph();
            p36.IndentationFirstLine = (int)500;
            XWPFRun r36 = p36.CreateRun();
            r36.FontFamily = "宋体";
            r36.FontSize = 13;
            r36.SetText("根据NUREG 0700等相关要求，仍需要对OWP坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。");

            XWPFParagraph p37 = m_Doc.CreateParagraph();
            p37.IndentationFirstLine = (int)500;
            XWPFRun r37 = p37.CreateRun();
            r37.FontFamily = "宋体";
            r37.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位")|| category.Contains("操作"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r37.AppendText(text);
                }
            }
        }

        else if (cellValue.Contains("BUP"))
        {
            r014.FontFamily = "宋体";
            r014.FontSize = 15;
            r014.SetText("4 BUP人体工效学评估");

            r015.FontFamily = "宋体";
            r015.FontSize = 13;
            r015.SetText("4.1 BUP可视性视角视距分析");

            XWPFParagraph p28 = m_Doc.CreateParagraph();
            p28.IndentationFirstLine = (int)500;
            XWPFRun r28 = p28.CreateRun();
            r28.FontFamily = "宋体";
            r28.FontSize = 13;
            r28.SetText("根据NUREG 0700等标准条例对站姿操作的控制台的要求：");

            //表格样式
            XWPFTable table001 = m_Doc.CreateTable(1, 2);
            table001.Width = 5000;
            table001.GetRow(0).GetCell(0).SetText("标准条例");
            table001.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    XWPFTableRow newRow = table001.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table001.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p29 = m_Doc.CreateParagraph();
            p29.IndentationFirstLine = (int)500;
            XWPFRun r29 = p29.CreateRun();
            r29.FontFamily = "宋体";
            r29.FontSize = 13;
            r29.SetText("根据GB 10000中的人体测量数据，取5%成年女性站姿眼高1371mm、眼至盘台前沿51mm，95%成年男性站姿眼高1664mm、眼至盘台前沿41mm。对BUP视角视距进行评估，如图4-1所示。");

            var imagePath5 = System.Environment.CurrentDirectory + @"\Output\BUP2.jpg";
            //var imagePath5 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\BUP2.jpg";
            if (File.Exists(imagePath5))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath5, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p30 = m_Doc.CreateParagraph();
            p30.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r30 = p30.CreateRun();
            r30.FontFamily = "宋体";
            r30.FontSize = 12;
            r30.SetText("图4-1 BUP视角视距评估");

            XWPFParagraph p31 = m_Doc.CreateParagraph();
            p31.IndentationFirstLine = (int)500;
            XWPFRun r31 = p31.CreateRun();
            r31.FontFamily = "宋体";
            r31.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r31.AppendText(text);
                }
            }



            XWPFParagraph p22 = m_Doc.CreateParagraph();
            XWPFRun r22 = p22.CreateRun();
            r22.FontFamily = "宋体";
            r22.FontSize = 13;
            r22.SetText("4.2 BUP可达性分析");

            XWPFParagraph p23 = m_Doc.CreateParagraph();
            p23.IndentationFirstLine = (int)500;
            XWPFRun r23 = p23.CreateRun();
            r23.FontFamily = "宋体";
            r23.FontSize = 13;
            r23.SetText("根据NUREG 0700等标准条例对站姿操作的控制台的要求：");

            XWPFTable table002 = m_Doc.CreateTable(1, 2);
            table002.Width = 5000;
            table002.GetRow(0).GetCell(0).SetText("标准条例");
            table002.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    XWPFTableRow newRow = table002.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table002.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p24 = m_Doc.CreateParagraph();
            p24.IndentationFirstLine = (int)500;
            XWPFRun r24 = p24.CreateRun();
            r24.FontFamily = "宋体";
            r24.FontSize = 13;
            r24.SetText("根据GB 10000中的人体测量数据，取5%成年女性站姿肩高1195mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性站姿肩高1455mm、功能伸展范围792mm、肩至盘台前沿123mm，对BUP可操作范围进行评估，如图4-2所示。");

            var imagePath4 = System.Environment.CurrentDirectory + @"\Output\BUP1.jpg";
            //var imagePath4 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\BUP1.jpg";
            if (File.Exists(imagePath4))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath4, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p25 = m_Doc.CreateParagraph();
            p25.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r25 = p25.CreateRun();
            r25.FontFamily = "宋体";
            r25.FontSize = 12;
            r25.SetText("图4-2 BUP控制台可操作范围评估");

            XWPFParagraph p26 = m_Doc.CreateParagraph();
            p26.IndentationFirstLine = (int)500;
            XWPFRun r26 = p26.CreateRun();
            r26.FontFamily = "宋体";
            r26.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r26.AppendText(text);
                }
            }



            XWPFParagraph p17 = m_Doc.CreateParagraph();
            XWPFRun r17 = p17.CreateRun();
            r17.FontFamily = "宋体";
            r17.FontSize = 13;
            r17.SetText("4.3 BUP盘台尺寸及容膝容足空间分析");

            XWPFParagraph p18 = m_Doc.CreateParagraph();
            p18.IndentationFirstLine = (int)500;
            XWPFRun r18 = p18.CreateRun();
            r18.FontFamily = "宋体";
            r18.FontSize = 13;
            r18.SetText("根据NUREG 0700等标准条例对站姿操作的控制台的要求：");

            XWPFTable table003 = m_Doc.CreateTable(1, 2);
            table003.Width = 5000;
            table003.GetRow(0).GetCell(0).SetText("标准条例");
            table003.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    XWPFTableRow newRow = table003.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table003.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p19 = m_Doc.CreateParagraph();
            p19.IndentationFirstLine = (int)500;
            XWPFRun r19 = p19.CreateRun();
            r19.FontFamily = "宋体";
            r19.FontSize = 13;
            r19.SetText("根据GB 10000中的人体测量数据，取5%女性站姿身高为1484mm、人体轴线至台边距离127mm，95%男性站姿身高为1775mm、人体轴线至台边距离123mm。对BUP盘台尺寸和容膝容足空间进行分析，如上图所示");

            //var imagePath3 = System.Environment.CurrentDirectory + @"\Assets\Output\left1.jpg";
            ////var imagePath3 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left1.jpg";
            //if (File.Exists(imagePath3))
            //{
            //    var picID = m_Doc.AddPictureData(new FileStream(imagePath3, FileMode.Open), 5);
            //    CreatePicture(m_Doc, picID, 550, 400);
            //}

            //XWPFParagraph p21 = m_Doc.CreateParagraph();
            //p21.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            //XWPFRun r21 = p21.CreateRun();
            //r21.FontFamily = "宋体";
            //r21.FontSize = 12;
            //r21.SetText("图4-3 OWP盘台尺寸");

            XWPFParagraph p20 = m_Doc.CreateParagraph();
            p20.IndentationFirstLine = (int)500;
            XWPFRun r20 = p20.CreateRun();
            r20.FontFamily = "宋体";
            r20.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r20.AppendText(text);
                }
            }



            XWPFParagraph p35 = m_Doc.CreateParagraph();
            XWPFRun r35 = p35.CreateRun();
            r35.FontFamily = "宋体";
            r35.FontSize = 13;
            r35.SetText("4.4 BUP其他分析");

            XWPFParagraph p36 = m_Doc.CreateParagraph();
            p36.IndentationFirstLine = (int)500;
            XWPFRun r36 = p36.CreateRun();
            r36.FontFamily = "宋体";
            r36.FontSize = 13;
            r36.SetText("根据NUREG 0700等相关要求，仍需要对站姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性站姿人体轴线至台边距离127mm、指尖至地面高581mm、人体中心轴至眼距离76mm，95%男性坐姿人体轴线至台边距离为123mm、指尖至地面高663mm、人体中心轴至眼距离82mm。");

            XWPFParagraph p37 = m_Doc.CreateParagraph();
            p37.IndentationFirstLine = (int)500;
            XWPFRun r37 = p37.CreateRun();
            r37.FontFamily = "宋体";
            r37.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r37.AppendText(text);
                }
            }
        }

        else if (cellValue.Contains("ECP"))
        {
            r014.FontFamily = "宋体";
            r014.FontSize = 15;
            r014.SetText("4 ECP人体工效学评估");

            r015.FontFamily = "宋体";
            r015.FontSize = 13;
            r015.SetText("4.1 ECP可视性视角视距分析");

            XWPFParagraph p28 = m_Doc.CreateParagraph();
            p28.IndentationFirstLine = (int)500;
            XWPFRun r28 = p28.CreateRun();
            r28.FontFamily = "宋体";
            r28.FontSize = 13;
            r28.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            //表格样式
            XWPFTable table001 = m_Doc.CreateTable(1, 2);
            table001.Width = 5000;
            table001.GetRow(0).GetCell(0).SetText("标准条例");
            table001.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    XWPFTableRow newRow = table001.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table001.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p29 = m_Doc.CreateParagraph();
            p29.IndentationFirstLine = (int)500;
            XWPFRun r29 = p29.CreateRun();
            r29.FontFamily = "宋体";
            r29.FontSize = 13;
            r29.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对ECP视角视距进行评估，如图4-1所示。");

            var imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            //var imagePath5 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left3.jpg";
            if (File.Exists(imagePath5))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath5, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p30 = m_Doc.CreateParagraph();
            p30.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r30 = p30.CreateRun();
            r30.FontFamily = "宋体";
            r30.FontSize = 12;
            r30.SetText("图4-1 ECP视角视距评估");

            XWPFParagraph p31 = m_Doc.CreateParagraph();
            p31.IndentationFirstLine = (int)500;
            XWPFRun r31 = p31.CreateRun();
            r31.FontFamily = "宋体";
            r31.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r31.AppendText(text);
                }
            }



            XWPFParagraph p22 = m_Doc.CreateParagraph();
            XWPFRun r22 = p22.CreateRun();
            r22.FontFamily = "宋体";
            r22.FontSize = 13;
            r22.SetText("4.2 ECP可达性分析");

            XWPFParagraph p23 = m_Doc.CreateParagraph();
            p23.IndentationFirstLine = (int)500;
            XWPFRun r23 = p23.CreateRun();
            r23.FontFamily = "宋体";
            r23.FontSize = 13;
            r23.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table002 = m_Doc.CreateTable(1, 2);
            table002.Width = 5000;
            table002.GetRow(0).GetCell(0).SetText("标准条例");
            table002.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    XWPFTableRow newRow = table002.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table002.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p24 = m_Doc.CreateParagraph();
            p24.IndentationFirstLine = (int)500;
            XWPFRun r24 = p24.CreateRun();
            r24.FontFamily = "宋体";
            r24.FontSize = 13;
            r24.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对ECP可操作范围进行评估，如图4-2所示。");

            var imagePath4 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            //var imagePath4 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left2.jpg";
            if (File.Exists(imagePath4))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath4, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p25 = m_Doc.CreateParagraph();
            p25.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r25 = p25.CreateRun();
            r25.FontFamily = "宋体";
            r25.FontSize = 12;
            r25.SetText("图4-2 ECP控制台可操作范围评估");

            XWPFParagraph p26 = m_Doc.CreateParagraph();
            p26.IndentationFirstLine = (int)500;
            XWPFRun r26 = p26.CreateRun();
            r26.FontFamily = "宋体";
            r26.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r26.AppendText(text);
                }
            }



            XWPFParagraph p17 = m_Doc.CreateParagraph();
            XWPFRun r17 = p17.CreateRun();
            r17.FontFamily = "宋体";
            r17.FontSize = 13;
            r17.SetText("4.3 ECP盘台尺寸及容膝容足空间分析");

            XWPFParagraph p18 = m_Doc.CreateParagraph();
            p18.IndentationFirstLine = (int)500;
            XWPFRun r18 = p18.CreateRun();
            r18.FontFamily = "宋体";
            r18.FontSize = 13;
            r18.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table003 = m_Doc.CreateTable(1, 2);
            table003.Width = 5000;
            table003.GetRow(0).GetCell(0).SetText("标准条例");
            table003.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    XWPFTableRow newRow = table003.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table003.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p19 = m_Doc.CreateParagraph();
            p19.IndentationFirstLine = (int)500;
            XWPFRun r19 = p19.CreateRun();
            r19.FontFamily = "宋体";
            r19.FontSize = 13;
            r19.SetText("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对ECP盘台尺寸和容膝容足空间进行分析，如图4-3所示");

            var imagePath3 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            //var imagePath3 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left1.jpg";
            if (File.Exists(imagePath3))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath3, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p21 = m_Doc.CreateParagraph();
            p21.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r21 = p21.CreateRun();
            r21.FontFamily = "宋体";
            r21.FontSize = 12;
            r21.SetText("图4-3 ECP盘台尺寸及容膝容足分析");

            XWPFParagraph p20 = m_Doc.CreateParagraph();
            p20.IndentationFirstLine = (int)500;
            XWPFRun r20 = p20.CreateRun();
            r20.FontFamily = "宋体";
            r20.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r20.AppendText(text);
                }
            }



            XWPFParagraph p35 = m_Doc.CreateParagraph();
            XWPFRun r35 = p35.CreateRun();
            r35.FontFamily = "宋体";
            r35.FontSize = 13;
            r35.SetText("4.4 ECP其他分析");

            XWPFParagraph p36 = m_Doc.CreateParagraph();
            p36.IndentationFirstLine = (int)500;
            XWPFRun r36 = p36.CreateRun();
            r36.FontFamily = "宋体";
            r36.FontSize = 13;
            r36.SetText("根据NUREG 0700等相关要求，仍需要对ECP坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。");

            XWPFParagraph p37 = m_Doc.CreateParagraph();
            p37.IndentationFirstLine = (int)500;
            XWPFRun r37 = p37.CreateRun();
            r37.FontFamily = "宋体";
            r37.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r37.AppendText(text);
                }
            }

        }

        else if (cellValue.Contains("VDU"))
        {
            r014.FontFamily = "宋体";
            r014.FontSize = 15;
            r014.SetText("4 VDU人体工效学评估");

            r015.FontFamily = "宋体";
            r015.FontSize = 13;
            r015.SetText("4.1 VDU可视性视角视距分析");

            XWPFParagraph p28 = m_Doc.CreateParagraph();
            p28.IndentationFirstLine = (int)500;
            XWPFRun r28 = p28.CreateRun();
            r28.FontFamily = "宋体";
            r28.FontSize = 13;
            r28.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            //表格样式
            XWPFTable table001 = m_Doc.CreateTable(1, 2);
            table001.Width = 5000;
            table001.GetRow(0).GetCell(0).SetText("标准条例");
            table001.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    XWPFTableRow newRow = table001.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table001.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p29 = m_Doc.CreateParagraph();
            p29.IndentationFirstLine = (int)500;
            XWPFRun r29 = p29.CreateRun();
            r29.FontFamily = "宋体";
            r29.FontSize = 13;
            r29.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对VDU视角视距进行评估，如图4-1所示。");

            var imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            //var imagePath5 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left3.jpg";
            if (File.Exists(imagePath5))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath5, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p30 = m_Doc.CreateParagraph();
            p30.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r30 = p30.CreateRun();
            r30.FontFamily = "宋体";
            r30.FontSize = 12;
            r30.SetText("图4-1 VDU视角视距评估");

            XWPFParagraph p31 = m_Doc.CreateParagraph();
            p31.IndentationFirstLine = (int)500;
            XWPFRun r31 = p31.CreateRun();
            r31.FontFamily = "宋体";
            r31.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r31.AppendText(text);
                }
            }



            XWPFParagraph p22 = m_Doc.CreateParagraph();
            XWPFRun r22 = p22.CreateRun();
            r22.FontFamily = "宋体";
            r22.FontSize = 13;
            r22.SetText("4.2 VDU可达性分析");

            XWPFParagraph p23 = m_Doc.CreateParagraph();
            p23.IndentationFirstLine = (int)500;
            XWPFRun r23 = p23.CreateRun();
            r23.FontFamily = "宋体";
            r23.FontSize = 13;
            r23.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table002 = m_Doc.CreateTable(1, 2);
            table002.Width = 5000;
            table002.GetRow(0).GetCell(0).SetText("标准条例");
            table002.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    XWPFTableRow newRow = table002.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table002.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p24 = m_Doc.CreateParagraph();
            p24.IndentationFirstLine = (int)500;
            XWPFRun r24 = p24.CreateRun();
            r24.FontFamily = "宋体";
            r24.FontSize = 13;
            r24.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对VDU可操作范围进行评估，如图4-2所示。");

            var imagePath4 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            //var imagePath4 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left2.jpg";
            if (File.Exists(imagePath4))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath4, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p25 = m_Doc.CreateParagraph();
            p25.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r25 = p25.CreateRun();
            r25.FontFamily = "宋体";
            r25.FontSize = 12;
            r25.SetText("图4-2 VDU控制台可操作范围评估");

            XWPFParagraph p26 = m_Doc.CreateParagraph();
            p26.IndentationFirstLine = (int)500;
            XWPFRun r26 = p26.CreateRun();
            r26.FontFamily = "宋体";
            r26.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r26.AppendText(text);
                }
            }



            XWPFParagraph p17 = m_Doc.CreateParagraph();
            XWPFRun r17 = p17.CreateRun();
            r17.FontFamily = "宋体";
            r17.FontSize = 13;
            r17.SetText("4.3 VDU盘台尺寸及容膝容足空间分析");

            XWPFParagraph p18 = m_Doc.CreateParagraph();
            p18.IndentationFirstLine = (int)500;
            XWPFRun r18 = p18.CreateRun();
            r18.FontFamily = "宋体";
            r18.FontSize = 13;
            r18.SetText("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：");

            XWPFTable table003 = m_Doc.CreateTable(1, 2);
            table003.Width = 5000;
            table003.GetRow(0).GetCell(0).SetText("标准条例");
            table003.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    XWPFTableRow newRow = table003.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table003.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p19 = m_Doc.CreateParagraph();
            p19.IndentationFirstLine = (int)500;
            XWPFRun r19 = p19.CreateRun();
            r19.FontFamily = "宋体";
            r19.FontSize = 13;
            r19.SetText("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对VDU盘台尺寸和容膝容足空间进行分析，如图4-3所示");

            var imagePath3 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            //var imagePath3 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\left1.jpg";
            if (File.Exists(imagePath3))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath3, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p21 = m_Doc.CreateParagraph();
            p21.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r21 = p21.CreateRun();
            r21.FontFamily = "宋体";
            r21.FontSize = 12;
            r21.SetText("图4-3 VDU盘台尺寸及容膝容足分析");

            XWPFParagraph p20 = m_Doc.CreateParagraph();
            p20.IndentationFirstLine = (int)500;
            XWPFRun r20 = p20.CreateRun();
            r20.FontFamily = "宋体";
            r20.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r20.AppendText(text);
                }
            }



            XWPFParagraph p35 = m_Doc.CreateParagraph();
            XWPFRun r35 = p35.CreateRun();
            r35.FontFamily = "宋体";
            r35.FontSize = 13;
            r35.SetText("4.4 VDU其他分析");

            XWPFParagraph p36 = m_Doc.CreateParagraph();
            p36.IndentationFirstLine = (int)500;
            XWPFRun r36 = p36.CreateRun();
            r36.FontFamily = "宋体";
            r36.FontSize = 13;
            r36.SetText("根据NUREG 0700等相关要求，仍需要对VDU坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。");

            XWPFParagraph p37 = m_Doc.CreateParagraph();
            p37.IndentationFirstLine = (int)500;
            XWPFRun r37 = p37.CreateRun();
            r37.FontFamily = "宋体";
            r37.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r37.AppendText(text);
                }
            }
        }

        else
        {
            r014.FontFamily = "宋体";
            r014.FontSize = 15;
            r014.SetText("4 大屏幕（LDP）人体工效学评估");

            r015.FontFamily = "宋体";
            r015.FontSize = 13;
            r015.SetText("4.1 LDP可视性评估 ");

            XWPFParagraph p28 = m_Doc.CreateParagraph();
            p28.IndentationFirstLine = (int)500;
            XWPFRun r28 = p28.CreateRun();
            r28.FontFamily = "宋体";
            r28.FontSize = 13;
            r28.SetText("按照运行需求，主控制室的布局需要保证值长、副值长在运行期间能够清晰地读取大屏幕上的相关信息。因此，使用NUREG 0700《人机接口设计审查导则》中的要求来对大屏幕可视性进行评估");

            //表格样式
            XWPFTable table001 = m_Doc.CreateTable(1, 2);
            table001.Width = 5000;
            table001.GetRow(0).GetCell(0).SetText("标准条例");
            table001.GetRow(0).GetCell(1).SetText("标准描述");
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    XWPFTableRow newRow = table001.CreateRow();  // 创建一个新的行
                    newRow.GetCell(0).SetText(standard_arr[i]);  // 在新行的第一列添加标准条例
                    newRow.GetCell(1).SetText(description_arr[i]);   // 在新行的第二列添加标准描述
                }
            }
            foreach (XWPFTableRow row in table001.Rows)
            {
                foreach (XWPFTableCell cell in row.GetTableCells())
                {
                    cell.Paragraphs[0].Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER; // 水平对齐方式为居中
                    cell.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
                    foreach (XWPFRun run in cell.Paragraphs[0].Runs)  // 设置单元格文本的字体样式为13号宋体
                    {
                        run.FontFamily = "宋体";
                        run.FontSize = 13;
                    }
                }
            }

            XWPFParagraph p29 = m_Doc.CreateParagraph();
            p29.IndentationFirstLine = (int)500;
            XWPFRun r29 = p29.CreateRun();
            r29.FontFamily = "宋体";
            r29.FontSize = 13;
            r29.SetText("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm，95%成年男性坐姿眼高1295mm。结合当前控制室布局和OWP显示器安装方式，对LDP可视性进行评估，如图4-1、4-2所示。");

            var imagePath5 = System.Environment.CurrentDirectory + @"\Output\screen2.jpg";
            //var imagePath5 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\screen2.jpg";
            if (File.Exists(imagePath5))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath5, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p30 = m_Doc.CreateParagraph();
            p30.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r30 = p30.CreateRun();
            r30.FontFamily = "宋体";
            r30.FontSize = 12;
            r30.SetText("图4-1 LDP可视性评估1");

            var imagePath4 = System.Environment.CurrentDirectory + @"\Output\screen.jpg";
            //var imagePath4 = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\Assets\Output\screen.jpg";
            if (File.Exists(imagePath4))
            {
                var picID = m_Doc.AddPictureData(new FileStream(imagePath4, FileMode.Open), 5);
                CreatePicture(m_Doc, picID, 550, 400);
            }

            XWPFParagraph p25 = m_Doc.CreateParagraph();
            p25.Alignment = NPOI.XWPF.UserModel.ParagraphAlignment.CENTER;
            XWPFRun r25 = p25.CreateRun();
            r25.FontFamily = "宋体";
            r25.FontSize = 12;
            r25.SetText("图4-2 LDP可视性评估2");

            XWPFParagraph p31 = m_Doc.CreateParagraph();
            p31.IndentationFirstLine = (int)500;
            XWPFRun r31 = p31.CreateRun();
            r31.FontFamily = "宋体";
            r31.FontSize = 13;
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    string text = $"根据标准条例{standard_arr[i]}：{description_arr[i]}。实际值为{value_arr[i]}，{result_arr[i]}人体工效学需求。";
                    r31.AppendText(text);
                }
            }               
        }

        //向文档流中写入内容，生成word
        FileStream file = new FileStream(uploadPath + "/控制室人体工效学设计评估.docx", FileMode.Create);
        m_Doc.Write(file);
        Debug.Log("finish word55555555555");
        file.Close();
        Debug.Log("finish word!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

    }




    public void PDFEXPORT()//有水印未使用
    {
        WORDExport();
        //Aspose.Words.Document doc = new Aspose.Words.Document(@"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\控制室人体工效学设计评估.docx");
        Aspose.Words.Document doc = new Aspose.Words.Document(System.Environment.CurrentDirectory + @"\控制室人体工效学设计评估.docx");
        PdfSaveOptions options = new PdfSaveOptions();
        options.Compliance = PdfCompliance.Pdf17;
        //doc.Save(@"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\控制室人体工效学设计评估.pdf", options);
        doc.Save(System.Environment.CurrentDirectory + @"\控制室人体工效学设计评估.pdf", options);
    }
    public void ConvertWordToPdf()//有水印未使用
    {
        WORDExport();
        // 读取Word文档
        string inputFilePath = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\控制室人体工效学设计评估.docx";
        Aspose.Words.Document doc = new Aspose.Words.Document(inputFilePath);

        // 创建PDF文档
        string outputFilePath = @"F:\ZhongheKongzhishi\nuclear-conctrol-room-simulation2\控制室人体工效学设计评估.pdf";

        // 将Word文档内容写入PDF
        doc.Save(outputFilePath, SaveFormat.Pdf);       
    }
   
    

    public void PDFWrite()
    {
        // 创建PDF文件
        FileStream pdfFs = new FileStream("控制室人体工效学设计评估.pdf", FileMode.Create);
        // 获取实例
        iTextSharp.text.Document doc = new iTextSharp.text.Document();
        PdfWriter.GetInstance(doc, pdfFs);
        // 打开pdf
        doc.Open();
        int column = 8;
        GameObject table = InfoTable;
        //获取行内容
        int j = table.transform.childCount;       
        System.Data.DataSet dc = new System.Data.DataSet();
        string[] id_arr = new string[j];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] target_arr = new string[j];
        string[] human_arr = new string[j];
        string[] standard_arr = new string[j];
        string[] description_arr = new string[j];
        string[] value_arr = new string[j];
        string[] result_arr = new string[j];
        string[] category_arr = new string[j];

        for (int i = 0; i < j; i++)   //将数据表中的数据放进数组中
        {
        string id = table.transform.GetChild(i).transform.Find("id").GetComponent<UnityEngine.UI.Text>().text;
        if (id != null)
            id_arr[i] = id;

        string target = table.transform.GetChild(i).transform.Find("target").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动素类别ActType数据放进数组中
        if (target != null)
            target_arr[i] = target;

        string human = table.transform.GetChild(i).transform.Find("human").GetComponent<UnityEngine.UI.Text>().text;//将数据表中的动作类别ActGenType数据放进数组中
        if (human != null)
            human_arr[i] = human;

        string standard = table.transform.GetChild(i).transform.Find("standard").GetComponent<UnityEngine.UI.Text>().text;
        if (standard != null)
            standard_arr[i] = standard;

        string description = table.transform.GetChild(i).transform.Find("description").GetComponent<UnityEngine.UI.Text>().text;
        if (description != null)
            description_arr[i] = description;

        string value = table.transform.GetChild(i).transform.Find("value").GetComponent<UnityEngine.UI.Text>().text;
        if (value != null)
            value_arr[i] = value;

        string result = table.transform.GetChild(i).transform.Find("result").GetComponent<UnityEngine.UI.Text>().text;
        if (result != null)
            result_arr[i] = result;

        string category = table.transform.GetChild(i).transform.Find("category").GetComponent<UnityEngine.UI.Text>().text;
        if (category != null)
            category_arr[i] = category;
        }

        /*
        List<string> tabledata = new List<string> { "编号", "盘台", "数字人", "标准条例", "标准描述", "实际值", "是否符合", "分类" };

        for (int k = 0; k < j; k++)   //将数据表中的数据放进数组中
        {
        tabledata.Add(id_arr[k]);
        tabledata.Add(target_arr[k]);
        tabledata.Add(human_arr[k]);
        tabledata.Add(standard_arr[k]);
        tabledata.Add(description_arr[k]);
        tabledata.Add(value_arr[k]);
        tabledata.Add(result_arr[k]);
        tabledata.Add(category_arr[k]);
        }
    
        PdfPTable tablepdf = CreateMultRowMultColumnTable(column, tabledata);

        doc.Add(tablepdf); // 添加表格

        */

        BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\simfang.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        iTextSharp.text.Paragraph p0 = new iTextSharp.text.Paragraph("控制室人体工效学设计评估", new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
        p0.Alignment = Element.ALIGN_CENTER;
        doc.Add(p0);

        iTextSharp.text.Paragraph p1 = new iTextSharp.text.Paragraph("1 缩略语", new iTextSharp.text.Font(baseFont, 15));
        doc.Add(p1);
        
        doc.Add(new iTextSharp.text.Paragraph("\n"));

        PdfPTable t1 = new PdfPTable(2);
        t1.WidthPercentage = 100; // 表格宽度占页面宽度的100%
        t1.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
        t1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
        // 添加第一行
        t1.AddCell(CreateCell("缩略语", baseFont));
        t1.AddCell(CreateCell("说明", baseFont));
        // 添加第二行
        t1.AddCell(CreateCell("BUP", baseFont));
        t1.AddCell(CreateCell("后备盘", baseFont));
        // 添加第三行
        t1.AddCell(CreateCell("DCS", baseFont));
        t1.AddCell(CreateCell("集散控制系统（包括1层和2层设备）", baseFont));
        // 添加第四行
        t1.AddCell(CreateCell("ECP", baseFont));
        t1.AddCell(CreateCell("紧急操作台", baseFont));
        // 添加第五行
        t1.AddCell(CreateCell("LDP", baseFont));
        t1.AddCell(CreateCell("大屏幕", baseFont));
        // 添加第六行
        t1.AddCell(CreateCell("OWP", baseFont));
        t1.AddCell(CreateCell("操纵员工作站", baseFont));
        // 添加第七行
        t1.AddCell(CreateCell("RSS", baseFont));
        t1.AddCell(CreateCell("远程停堆站", baseFont));
        // 添加第八行
        t1.AddCell(CreateCell("SVDU", baseFont));
        t1.AddCell(CreateCell("安全级显示器", baseFont));
        doc.Add(t1);

        iTextSharp.text.Paragraph p3 = new iTextSharp.text.Paragraph("2 控制室设计概述", new iTextSharp.text.Font(baseFont, 15));
        p3.Leading = 15 * 1.5f;
        doc.Add(p3);

        iTextSharp.text.Paragraph p4 = new iTextSharp.text.Paragraph("2.1 主控制室", new iTextSharp.text.Font(baseFont, 13));
        p4.Leading = 13 * 1.5f;
        doc.Add(p4);

        iTextSharp.text.Paragraph p5 = new iTextSharp.text.Paragraph("主控制室的主要目标是实现核电厂在所有运行和事故工况下的安全有效运行。主控制室为主控制室工作人员提供实现电厂运行目标所必需的人机接口和有关的信息和设备，此外，主控制室为主控制室工作人员提供适宜的工作环境，以利于执行任务，而无不适之感和人身危险。", new iTextSharp.text.Font(baseFont, 13));
        p5.FirstLineIndent = 20f;
        p5.Leading = 13 * 1.5f;
        doc.Add(p5);

        iTextSharp.text.Paragraph p6 = new iTextSharp.text.Paragraph("主控制室设计的基本目标是及时、准确和完整地向操纵员提供关于电厂设备和系统的功能状态的信息。主控制室设计必须考虑到所有运行状态（包括换料和事故工况）下使任务最佳化，并将检测与控制电厂所要求的工作量减到最小。主控制室还必须向主控制室外的其他设施提供必要的信息。主控制室设计必须提供各项功能最佳分配，以便操纵员和系统能最大限度地发挥其能力。主控制室设计的另一个目标是使电厂能有效地进行试运行、并允许修改和维护。", new iTextSharp.text.Font(baseFont, 13));
        p6.FirstLineIndent = 20f;
        p6.Leading = 13 * 1.5f;
        doc.Add(p6);

        iTextSharp.text.Paragraph p7 = new iTextSharp.text.Paragraph("主控制室主要包括以下盘台：", new iTextSharp.text.Font(baseFont, 13));
        p7.FirstLineIndent = 20f;
        p7.Leading = 13 * 1.5f;
        doc.Add(p7);

        iTextSharp.text.Paragraph p8 = new iTextSharp.text.Paragraph("操纵员控制台（OWP）", new iTextSharp.text.Font(baseFont, 13));
        p8.FirstLineIndent = 20f;
        p8.Leading = 13 * 1.5f;
        doc.Add(p8);

        iTextSharp.text.Paragraph p9 = new iTextSharp.text.Paragraph("紧急操作台（ECP），包括安全级部分（ECP-S）和非安全级部分（ECP-N）", new iTextSharp.text.Font(baseFont, 13));
        p9.FirstLineIndent = 20f;
        p9.Leading = 13 * 1.5f;
        doc.Add(p9);

        iTextSharp.text.Paragraph p10 = new iTextSharp.text.Paragraph("大屏幕（LDP）", new iTextSharp.text.Font(baseFont, 13));
        p10.FirstLineIndent = 20f;
        p10.Leading = 13 * 1.5f;
        doc.Add(p10);

        iTextSharp.text.Paragraph p11 = new iTextSharp.text.Paragraph("后备盘（BUP）", new iTextSharp.text.Font(baseFont, 13));
        p11.FirstLineIndent = 20f;
        p11.Leading = 13 * 1.5f;
        doc.Add(p11);

        iTextSharp.text.Paragraph p12 = new iTextSharp.text.Paragraph("2.2 远程停堆站", new iTextSharp.text.Font(baseFont, 13));
        p12.Leading = 13 * 1.5f;
        doc.Add(p12);

        iTextSharp.text.Paragraph p13 = new iTextSharp.text.Paragraph("当主控制室可用时，远程停堆站控制功能被闭锁。当主控制室由于某种原因（例如发生火灾）变得不可利用时，操纵员可以不需要进入主控制室，就能从远程停堆站停闭反应堆，将核电厂带入并保持在安全停堆状态。", new iTextSharp.text.Font(baseFont, 13));
        p13.FirstLineIndent = 20f;
        p13.Leading = 13 * 1.5f;
        doc.Add(p13);

        iTextSharp.text.Paragraph p14 = new iTextSharp.text.Paragraph("远程停堆站设有三套简化的操纵员工作站和RSS-SVDU控制台（安装有序列1、2的SVDU共2台，紧急停堆控制器，以及序列1MCR/RSS切换装置）。", new iTextSharp.text.Font(baseFont, 13));
        p14.FirstLineIndent = 20f;
        p14.Leading = 13 * 1.5f;
        doc.Add(p14);

        iTextSharp.text.Paragraph p15 = new iTextSharp.text.Paragraph("3 人体尺寸基础数据", new iTextSharp.text.Font(baseFont, 15));
        p15.Leading = 15 * 1.5f;
        doc.Add(p15);

        iTextSharp.text.Paragraph p16 = new iTextSharp.text.Paragraph("本报告根据GB10000（1988）《中国成年人人体尺寸》中给出的5%成年女性到95%成年男性的人体测量数据，对控制室布局及盘台盘面设计进行人体工效学评价。对于GB10000中缺少的数据采用HAF·J0055（1995）《核电厂控制室设计的人因工程原则》中提供的数据。", new iTextSharp.text.Font(baseFont, 13));
        p16.FirstLineIndent = 20f;
        p16.Leading = 13 * 1.5f;
        doc.Add(p16);

        iTextSharp.text.Paragraph p17 = new iTextSharp.text.Paragraph("具体采用数据如下表所示：", new iTextSharp.text.Font(baseFont, 13));
        p17.FirstLineIndent = 20f;
        p17.Leading = 13 * 1.5f;
        doc.Add(p17);
        
        doc.Add(new iTextSharp.text.Paragraph("\n"));

        PdfPTable t2 = new PdfPTable(5);
        t2.WidthPercentage = 100; // 表格宽度占页面宽度的100%
        t2.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
        t2.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
        // 添加第一行
        t2.AddCell(CreateCell(" ", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        t2.AddCell(CreateCell("5%女（mm）", baseFont));
        t2.AddCell(CreateCell("95%男（mm）", baseFont));
        t2.AddCell(CreateCell("备注", baseFont));
        // 添加第二行
        PdfPCell L2 = CreateCell("站姿", baseFont);
        L2.Colspan = 5; // 合并两个单元格
        t2.AddCell(L2);
        // 添加第三行
        t2.AddCell(CreateCell("序号", baseFont));
        t2.AddCell(CreateCell("项目", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第四行
        t2.AddCell(CreateCell("1", baseFont));
        t2.AddCell(CreateCell("身高", baseFont));
        t2.AddCell(CreateCell("1484", baseFont));
        t2.AddCell(CreateCell("1775", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第5行
        t2.AddCell(CreateCell("2", baseFont));
        t2.AddCell(CreateCell("眼至地面高", baseFont));
        t2.AddCell(CreateCell("1371", baseFont));
        t2.AddCell(CreateCell("1664", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第6行
        t2.AddCell(CreateCell("3", baseFont));
        t2.AddCell(CreateCell("肩高", baseFont));
        t2.AddCell(CreateCell("1195", baseFont));
        t2.AddCell(CreateCell("1455", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第7行
        t2.AddCell(CreateCell("4", baseFont));
        t2.AddCell(CreateCell("指尖至地面高", baseFont));
        t2.AddCell(CreateCell("581", baseFont));
        t2.AddCell(CreateCell("663", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：肩高-上臂-前臂-手长", baseFont));
        // 添加第8行
        t2.AddCell(CreateCell("5", baseFont));
        t2.AddCell(CreateCell("功能伸展范围", baseFont));
        t2.AddCell(CreateCell("614", baseFont));
        t2.AddCell(CreateCell("792", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：上臂+前臂+手长", baseFont));
        // 添加第9行
        t2.AddCell(CreateCell("6", baseFont));
        t2.AddCell(CreateCell("人体轴线至台边距离", baseFont));
        t2.AddCell(CreateCell("127", baseFont));
        t2.AddCell(CreateCell("123", baseFont));
        t2.AddCell(CreateCell("按HAF·J0055", baseFont));
        // 添加第10行
        t2.AddCell(CreateCell("7", baseFont));
        t2.AddCell(CreateCell("人体中心轴至眼距离", baseFont));
        t2.AddCell(CreateCell("76", baseFont));
        t2.AddCell(CreateCell("82", baseFont));
        t2.AddCell(CreateCell("按HAF·J0055", baseFont));
        // 添加第11行
        t2.AddCell(CreateCell("8", baseFont));
        t2.AddCell(CreateCell("眼至盘台前沿", baseFont));
        t2.AddCell(CreateCell("51", baseFont));
        t2.AddCell(CreateCell("41", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：人体轴线至台边距离-人体中心轴至眼距离", baseFont));
        // 添加第12行
        t2.AddCell(CreateCell("9", baseFont));
        t2.AddCell(CreateCell("肩至盘台前沿", baseFont));
        t2.AddCell(CreateCell("127", baseFont));
        t2.AddCell(CreateCell("123", baseFont));
        t2.AddCell(CreateCell("人体轴线至台边距离", baseFont));
        // 添加第13行
        PdfPCell L13 = CreateCell("坐姿", baseFont);
        L13.Colspan = 5; // 合并两个单元格
        t2.AddCell(L13);
        // 添加第14行
        t2.AddCell(CreateCell("1", baseFont));
        t2.AddCell(CreateCell("腿部弯曲部位的高度（膝下方）", baseFont));
        t2.AddCell(CreateCell("342", baseFont));
        t2.AddCell(CreateCell("448", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第15行
        t2.AddCell(CreateCell("2", baseFont));
        t2.AddCell(CreateCell("椅面以上的身高（伸直）", baseFont));
        t2.AddCell(CreateCell("809", baseFont));
        t2.AddCell(CreateCell("958", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第16行
        t2.AddCell(CreateCell("3", baseFont));
        t2.AddCell(CreateCell("椅面以上的眼高（伸直）", baseFont));
        t2.AddCell(CreateCell("695", baseFont));
        t2.AddCell(CreateCell("847", baseFont));
        t2.AddCell(CreateCell("", baseFont));
        // 添加第17行
        t2.AddCell(CreateCell("4", baseFont));
        t2.AddCell(CreateCell("椅面以上的肩高", baseFont));
        t2.AddCell(CreateCell("518", baseFont));
        t2.AddCell(CreateCell("641", baseFont));
        t2.AddCell(CreateCell(" ", baseFont));
        // 添加第18行
        t2.AddCell(CreateCell("5", baseFont));
        t2.AddCell(CreateCell("功能伸展范围", baseFont));
        t2.AddCell(CreateCell("614", baseFont));
        t2.AddCell(CreateCell("792", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：上臂+前臂+手长", baseFont));
        // 添加第19行
        t2.AddCell(CreateCell("6", baseFont));
        t2.AddCell(CreateCell("大腿的净高度（膝盖处）", baseFont));
        t2.AddCell(CreateCell("113", baseFont));
        t2.AddCell(CreateCell("151", baseFont));
        t2.AddCell(CreateCell("GB 10000为大腿中部尺寸", baseFont));
        // 添加第20行
        t2.AddCell(CreateCell("7", baseFont));
        t2.AddCell(CreateCell("臀部至膝弯内侧的距离", baseFont));
        t2.AddCell(CreateCell("401", baseFont));
        t2.AddCell(CreateCell("494", baseFont));
        t2.AddCell(CreateCell("", baseFont));
        // 添加第21行
        t2.AddCell(CreateCell("8", baseFont));
        t2.AddCell(CreateCell("膝的高度", baseFont));
        t2.AddCell(CreateCell("424", baseFont));
        t2.AddCell(CreateCell("532", baseFont));
        t2.AddCell(CreateCell("", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("9", baseFont));
        t2.AddCell(CreateCell("人体轴线至台边距离", baseFont));
        t2.AddCell(CreateCell("127", baseFont));
        t2.AddCell(CreateCell("123", baseFont));
        t2.AddCell(CreateCell("按HAF·J0055", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("10", baseFont));
        t2.AddCell(CreateCell("人体中心轴至眼距离", baseFont));
        t2.AddCell(CreateCell("76", baseFont));
        t2.AddCell(CreateCell("82", baseFont));
        t2.AddCell(CreateCell("按HAF·J0055", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("11", baseFont));
        t2.AddCell(CreateCell("坐姿眼高", baseFont));
        t2.AddCell(CreateCell("1037", baseFont));
        t2.AddCell(CreateCell("1295", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：椅面以上坐姿眼高+小腿加足高", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("12", baseFont));
        t2.AddCell(CreateCell("坐姿肩高", baseFont));
        t2.AddCell(CreateCell("860", baseFont));
        t2.AddCell(CreateCell("1089", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：椅面以上坐姿肩高+小腿加足高", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("13", baseFont));
        t2.AddCell(CreateCell("臀膝距", baseFont));
        t2.AddCell(CreateCell("495", baseFont));
        t2.AddCell(CreateCell("595", baseFont));
        t2.AddCell(CreateCell("", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("14", baseFont));
        t2.AddCell(CreateCell("脚长", baseFont));
        t2.AddCell(CreateCell("213", baseFont));
        t2.AddCell(CreateCell("264", baseFont));
        t2.AddCell(CreateCell("", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("15", baseFont));
        t2.AddCell(CreateCell("身体前端至脚尖距离", baseFont));
        t2.AddCell(CreateCell("510", baseFont));
        t2.AddCell(CreateCell("648", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：臀膝距+脚长*2/3-人体轴线至台边距离", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("16", baseFont));
        t2.AddCell(CreateCell("眼至盘台前沿", baseFont));
        t2.AddCell(CreateCell("51", baseFont));
        t2.AddCell(CreateCell("41", baseFont));
        t2.AddCell(CreateCell("按以下公式计算：人体轴线至台边距离-人体中心轴至眼距离", baseFont));
        // 添加第行
        t2.AddCell(CreateCell("17", baseFont));
        t2.AddCell(CreateCell("肩至盘台前沿", baseFont));
        t2.AddCell(CreateCell("127", baseFont));
        t2.AddCell(CreateCell("123", baseFont));
        t2.AddCell(CreateCell("人体轴线至台边距离", baseFont));
        doc.Add(t2);

        string cellValue = table.transform.GetChild(0).transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text;//第一行第二列内容

        if (cellValue.Contains("OWP"))
        {
            iTextSharp.text.Paragraph p41 = new iTextSharp.text.Paragraph("4 OWP人体工效学评估", new iTextSharp.text.Font(baseFont, 15));
            p41.Leading = 15 * 1.5f;
            doc.Add(p41);

            iTextSharp.text.Paragraph p42 = new iTextSharp.text.Paragraph("4.1 OWP可视性视角视距分析", new iTextSharp.text.Font(baseFont, 13));
            p42.Leading = 13 * 1.5f;
            doc.Add(p42);

            iTextSharp.text.Paragraph p43 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p43.FirstLineIndent = 20f;
            p43.Leading = 13 * 1.5f;
            doc.Add(p43);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t3 = new PdfPTable(2);
            t3.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t3.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t3.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t3.AddCell(CreateCell("标准条例", baseFont));
            t3.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    t3.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t3.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t3);

            iTextSharp.text.Paragraph p44 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对OWP视角视距进行评估，如图4-1所示。", new iTextSharp.text.Font(baseFont, 13));
            p44.FirstLineIndent = 20f;
            p44.Leading = 13 * 1.5f;
            doc.Add(p44);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            iTextSharp.text.Image image5 = iTextSharp.text.Image.GetInstance(imagePath5);
            image5.Alignment = Element.ALIGN_CENTER;
            image5.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t05 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell5 = new PdfPCell(image5);
            imageCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(imageCell5);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图4-1 OWP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            PdfPCell paragraphCell5 = new PdfPCell(p46);
            paragraphCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(paragraphCell5);
            // 将表格添加到文档
            doc.Add(t05);


            // 创建一个段落对象
            Paragraph p45 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p45.FirstLineIndent = 20f;
            p45.Leading = 13 * 1.5f;
            // 创建一个字体对象
            Font font = new Font(baseFont, 13);
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p45.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p45);

            /*
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图6-1 OWP控制台高度评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            doc.Add(p46);*/

            iTextSharp.text.Paragraph p47 = new iTextSharp.text.Paragraph("4.2 OWP可达性分析", new iTextSharp.text.Font(baseFont, 13));
            p47.Leading = 13 * 1.5f;
            doc.Add(p47);

            iTextSharp.text.Paragraph p48 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p48.FirstLineIndent = 20f;
            p48.Leading = 13 * 1.5f;
            doc.Add(p48);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t4 = new PdfPTable(2);
            t4.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t4.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t4.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t4.AddCell(CreateCell("标准条例", baseFont));
            t4.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    t4.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t4.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t4);

            iTextSharp.text.Paragraph p49 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对OWP可操作范围进行评估，如图4-2所示。", new iTextSharp.text.Font(baseFont, 13));
            p49.FirstLineIndent = 20f;
            p49.Leading = 13 * 1.5f;
            doc.Add(p49);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath6 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            iTextSharp.text.Image image6 = iTextSharp.text.Image.GetInstance(imagePath6);
            image6.Alignment = Element.ALIGN_CENTER;
            image6.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t06 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell6 = new PdfPCell(image6);
            imageCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(imageCell6);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图4-2 OWP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            PdfPCell paragraphCell6 = new PdfPCell(p50);
            paragraphCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(paragraphCell6);
            // 将表格添加到文档
            doc.Add(t06);

            /*
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图6-2 OWP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            doc.Add(p50);*/

            // 创建一个段落对象
            Paragraph p51 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p51.FirstLineIndent = 20f;
            p51.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p51.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p51);

            iTextSharp.text.Paragraph p52 = new iTextSharp.text.Paragraph("4.3 OWP盘台尺寸及容膝容足空间分析", new iTextSharp.text.Font(baseFont, 13));
            p52.Leading = 13 * 1.5f;
            doc.Add(p52);

            iTextSharp.text.Paragraph p53 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p53.FirstLineIndent = 20f;
            p53.Leading = 13 * 1.5f;
            doc.Add(p53);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t6 = new PdfPTable(2);
            t6.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t6.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t6.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t6.AddCell(CreateCell("标准条例", baseFont));
            t6.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    t6.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t6.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t6);

            iTextSharp.text.Paragraph p54 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对OWP盘台尺寸和容膝容足空间进行分析，如图4-3所示", new iTextSharp.text.Font(baseFont, 13));
            p54.FirstLineIndent = 20f;
            p54.Leading = 13 * 1.5f;
            doc.Add(p54);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath7 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            iTextSharp.text.Image image7 = iTextSharp.text.Image.GetInstance(imagePath7);
            image7.Alignment = Element.ALIGN_CENTER;
            image7.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t7 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell7 = new PdfPCell(image7);
            imageCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(imageCell7);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图4-3 OWP盘台尺寸及容膝容足分析", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            PdfPCell paragraphCell7 = new PdfPCell(p55);
            paragraphCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(paragraphCell7);
            // 将表格添加到文档
            doc.Add(t7);

            /*
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图6-3 OWP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            doc.Add(p55);*/

            // 创建一个段落对象
            Paragraph p56 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p56.FirstLineIndent = 20f;
            p56.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p56.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p56);

            iTextSharp.text.Paragraph p60 = new iTextSharp.text.Paragraph("4.4 OWP其他分析", new iTextSharp.text.Font(baseFont, 13));
            p60.Leading = 13 * 1.5f;
            doc.Add(p60);


            iTextSharp.text.Paragraph p57 = new iTextSharp.text.Paragraph("根据NUREG 0700等相关要求，仍需要对OWP坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。", new iTextSharp.text.Font(baseFont, 13));
            p57.FirstLineIndent = 20f;
            p57.Leading = 13 * 1.5f;
            doc.Add(p57);

            // 创建一个段落对象
            Paragraph p58 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p58.FirstLineIndent = 20f;
            p58.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p58.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p58);

        }
        else if (cellValue.Contains("BUP"))
        {
            iTextSharp.text.Paragraph p27 = new iTextSharp.text.Paragraph("4 BUP人体工效学评估", new iTextSharp.text.Font(baseFont, 15));
            p27.Leading = 15 * 1.5f;
            doc.Add(p27);

            iTextSharp.text.Paragraph p28 = new iTextSharp.text.Paragraph("4.1 BUP可视性视角视距分析", new iTextSharp.text.Font(baseFont, 13));
            p28.Leading = 13 * 1.5f;
            doc.Add(p28);

            iTextSharp.text.Paragraph p29 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对站姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p29.FirstLineIndent = 20f;
            p29.Leading = 13 * 1.5f;
            doc.Add(p29);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t5 = new PdfPTable(2);
            t5.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t5.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t5.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t5.AddCell(CreateCell("标准条例", baseFont));
            t5.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    t5.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t5.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t5);


            iTextSharp.text.Paragraph p30 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性站姿眼高1371mm、眼至盘台前沿51mm，95%成年男性站姿眼高1664mm、眼至盘台前沿41mm。对BUP视角视距进行评估，如图4-1所示。", new iTextSharp.text.Font(baseFont, 13));
            p30.FirstLineIndent = 20f;
            p30.Leading = 13 * 1.5f;
            doc.Add(p30);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath3 = System.Environment.CurrentDirectory + @"\Output\BUP2.jpg";
            iTextSharp.text.Image image3 = iTextSharp.text.Image.GetInstance(imagePath3);
            image3.Alignment = Element.ALIGN_CENTER;
            image3.ScalePercent(15f);
            // 创建一个表格，并指定列数为1
            PdfPTable t03 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell3 = new PdfPCell(image3);
            imageCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell3.Border = PdfPCell.NO_BORDER;
            t03.AddCell(imageCell3);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p31 = new iTextSharp.text.Paragraph("图4-1 BUP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p31.Alignment = Element.ALIGN_CENTER;
            p31.Leading = 12 * 1.5f;
            PdfPCell paragraphCell3 = new PdfPCell(p31);
            paragraphCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell3.Border = PdfPCell.NO_BORDER;
            t03.AddCell(paragraphCell3);
            // 将表格添加到文档
            doc.Add(t03);

            /*
            iTextSharp.text.Paragraph p31 = new iTextSharp.text.Paragraph("图5-1 BUP可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p31.Alignment = Element.ALIGN_CENTER;
            p31.Leading = 12 * 1.5f;
            doc.Add(p31);*/



            // 创建一个段落对象
            Paragraph p33 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p33.FirstLineIndent = 20f;
            p33.Leading = 13 * 1.5f;
            // 创建一个字体对象
            Font font = new Font(baseFont, 13);
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p33.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p33);

            iTextSharp.text.Paragraph p32 = new iTextSharp.text.Paragraph("4.2 BUP可达性分析", new iTextSharp.text.Font(baseFont, 13));
            p32.Leading = 13 * 1.5f;
            doc.Add(p32);

            iTextSharp.text.Paragraph p34 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对站姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p34.FirstLineIndent = 20f;
            p34.Leading = 13 * 1.5f;
            doc.Add(p34);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t6 = new PdfPTable(2);
            t6.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t6.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t6.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t6.AddCell(CreateCell("标准条例", baseFont));
            t6.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    t6.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t6.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t6);

            iTextSharp.text.Paragraph p36 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性站姿肩高1195mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性站姿肩高1455mm、功能伸展范围792mm、肩至盘台前沿123mm，对BUP可操作范围进行评估，如图4-2所示。", new iTextSharp.text.Font(baseFont, 13));
            p36.FirstLineIndent = 20f;
            p36.Leading = 13 * 1.5f;
            doc.Add(p36);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath4 = System.Environment.CurrentDirectory + @"\Output\BUP1.jpg";
            iTextSharp.text.Image image4 = iTextSharp.text.Image.GetInstance(imagePath4);
            image4.Alignment = Element.ALIGN_CENTER;
            image4.ScalePercent(15f);
            // 创建一个表格，并指定列数为1
            PdfPTable t04 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell4 = new PdfPCell(image4);
            imageCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell4.Border = PdfPCell.NO_BORDER;
            t04.AddCell(imageCell4);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p35 = new iTextSharp.text.Paragraph("图4-2 BUP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p35.Alignment = Element.ALIGN_CENTER;
            p35.Leading = 12 * 1.5f;
            PdfPCell paragraphCell4 = new PdfPCell(p35);
            paragraphCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell4.Border = PdfPCell.NO_BORDER;
            t04.AddCell(paragraphCell4);
            // 将表格添加到文档
            doc.Add(t04);

            /*
            iTextSharp.text.Paragraph p35 = new iTextSharp.text.Paragraph("图5-2 BUP视角评估", new iTextSharp.text.Font(baseFont, 12));
            p35.Alignment = Element.ALIGN_CENTER;
            p35.Leading = 12 * 1.5f;
            doc.Add(p35);*/


            // 创建一个段落对象
            Paragraph p37 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p37.FirstLineIndent = 20f;
            p37.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p37.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p37);


            iTextSharp.text.Paragraph p39 = new iTextSharp.text.Paragraph("4.3 BUP盘台尺寸及容膝容足空间分析", new iTextSharp.text.Font(baseFont, 13));
            p39.Leading = 13 * 1.5f;
            doc.Add(p39);


            iTextSharp.text.Paragraph p38 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对站姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p38.FirstLineIndent = 20f;
            p38.Leading = 13 * 1.5f;
            doc.Add(p38);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t7 = new PdfPTable(2);
            t7.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t7.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t7.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t7.AddCell(CreateCell("标准条例", baseFont));
            t7.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    t7.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t7.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t7);

            iTextSharp.text.Paragraph p40 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%女性站姿身高为1484mm、人体轴线至台边距离127mm，95%男性站姿身高为1775mm、人体轴线至台边距离123mm。对BUP盘台尺寸和容膝容足空间进行分析，如上图所示", new iTextSharp.text.Font(baseFont, 13));
            p40.FirstLineIndent = 20f;
            p40.Leading = 13 * 1.5f;
            doc.Add(p40);


            // 创建一个段落对象
            Paragraph p41 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p41.FirstLineIndent = 20f;
            p41.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p41.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p41);



            iTextSharp.text.Paragraph p42 = new iTextSharp.text.Paragraph("4.4 BUP其他分析", new iTextSharp.text.Font(baseFont, 13));
            p42.Leading = 13 * 1.5f;
            doc.Add(p42);


            iTextSharp.text.Paragraph p43 = new iTextSharp.text.Paragraph("根据NUREG 0700等相关要求，仍需要对站姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性站姿人体轴线至台边距离127mm、指尖至地面高581mm、人体中心轴至眼距离76mm，95%男性坐姿人体轴线至台边距离为123mm、指尖至地面高663mm、人体中心轴至眼距离82mm。", new iTextSharp.text.Font(baseFont, 13));
            p43.FirstLineIndent = 20f;
            p43.Leading = 13 * 1.5f;
            doc.Add(p43);



            // 创建一个段落对象
            Paragraph p44 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p44.FirstLineIndent = 20f;
            p44.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p44.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p44);

        }
        else if (cellValue.Contains("ECP"))
        {
            iTextSharp.text.Paragraph p41 = new iTextSharp.text.Paragraph("4 ECP人体工效学评估", new iTextSharp.text.Font(baseFont, 15));
            p41.Leading = 15 * 1.5f;
            doc.Add(p41);

            iTextSharp.text.Paragraph p42 = new iTextSharp.text.Paragraph("4.1 ECP可视性视角视距分析", new iTextSharp.text.Font(baseFont, 13));
            p42.Leading = 13 * 1.5f;
            doc.Add(p42);

            iTextSharp.text.Paragraph p43 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p43.FirstLineIndent = 20f;
            p43.Leading = 13 * 1.5f;
            doc.Add(p43);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t3 = new PdfPTable(2);
            t3.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t3.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t3.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t3.AddCell(CreateCell("标准条例", baseFont));
            t3.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    t3.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t3.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t3);

            iTextSharp.text.Paragraph p44 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对ECP视角视距进行评估，如图4-1所示。", new iTextSharp.text.Font(baseFont, 13));
            p44.FirstLineIndent = 20f;
            p44.Leading = 13 * 1.5f;
            doc.Add(p44);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            iTextSharp.text.Image image5 = iTextSharp.text.Image.GetInstance(imagePath5);
            image5.Alignment = Element.ALIGN_CENTER;
            image5.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t05 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell5 = new PdfPCell(image5);
            imageCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(imageCell5);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图4-1 ECP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            PdfPCell paragraphCell5 = new PdfPCell(p46);
            paragraphCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(paragraphCell5);
            // 将表格添加到文档
            doc.Add(t05);


            // 创建一个段落对象
            Paragraph p45 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p45.FirstLineIndent = 20f;
            p45.Leading = 13 * 1.5f;
            // 创建一个字体对象
            Font font = new Font(baseFont, 13);
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p45.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p45);

            /*
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图6-1 OWP控制台高度评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            doc.Add(p46);*/

            iTextSharp.text.Paragraph p47 = new iTextSharp.text.Paragraph("4.2 ECP可达性分析", new iTextSharp.text.Font(baseFont, 13));
            p47.Leading = 13 * 1.5f;
            doc.Add(p47);

            iTextSharp.text.Paragraph p48 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p48.FirstLineIndent = 20f;
            p48.Leading = 13 * 1.5f;
            doc.Add(p48);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t4 = new PdfPTable(2);
            t4.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t4.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t4.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t4.AddCell(CreateCell("标准条例", baseFont));
            t4.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    t4.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t4.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t4);

            iTextSharp.text.Paragraph p49 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对ECP可操作范围进行评估，如图4-2所示。", new iTextSharp.text.Font(baseFont, 13));
            p49.FirstLineIndent = 20f;
            p49.Leading = 13 * 1.5f;
            doc.Add(p49);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath6 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            iTextSharp.text.Image image6 = iTextSharp.text.Image.GetInstance(imagePath6);
            image6.Alignment = Element.ALIGN_CENTER;
            image6.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t06 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell6 = new PdfPCell(image6);
            imageCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(imageCell6);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图4-2 ECP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            PdfPCell paragraphCell6 = new PdfPCell(p50);
            paragraphCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(paragraphCell6);
            // 将表格添加到文档
            doc.Add(t06);

            /*
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图6-2 OWP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            doc.Add(p50);*/

            // 创建一个段落对象
            Paragraph p51 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p51.FirstLineIndent = 20f;
            p51.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p51.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p51);

            iTextSharp.text.Paragraph p52 = new iTextSharp.text.Paragraph("4.3 ECP盘台尺寸及容膝容足空间分析", new iTextSharp.text.Font(baseFont, 13));
            p52.Leading = 13 * 1.5f;
            doc.Add(p52);

            iTextSharp.text.Paragraph p53 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p53.FirstLineIndent = 20f;
            p53.Leading = 13 * 1.5f;
            doc.Add(p53);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t6 = new PdfPTable(2);
            t6.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t6.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t6.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t6.AddCell(CreateCell("标准条例", baseFont));
            t6.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    t6.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t6.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t6);

            iTextSharp.text.Paragraph p54 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对ECP盘台尺寸和容膝容足空间进行分析，如图4-3所示", new iTextSharp.text.Font(baseFont, 13));
            p54.FirstLineIndent = 20f;
            p54.Leading = 13 * 1.5f;
            doc.Add(p54);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath7 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            iTextSharp.text.Image image7 = iTextSharp.text.Image.GetInstance(imagePath7);
            image7.Alignment = Element.ALIGN_CENTER;
            image7.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t7 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell7 = new PdfPCell(image7);
            imageCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(imageCell7);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图4-3 ECP盘台尺寸及容膝容足分析", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            PdfPCell paragraphCell7 = new PdfPCell(p55);
            paragraphCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(paragraphCell7);
            // 将表格添加到文档
            doc.Add(t7);

            /*
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图6-3 OWP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            doc.Add(p55);*/

            // 创建一个段落对象
            Paragraph p56 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p56.FirstLineIndent = 20f;
            p56.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p56.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p56);

            iTextSharp.text.Paragraph p60 = new iTextSharp.text.Paragraph("4.4 ECP其他分析", new iTextSharp.text.Font(baseFont, 13));
            p60.Leading = 13 * 1.5f;
            doc.Add(p60);


            iTextSharp.text.Paragraph p57 = new iTextSharp.text.Paragraph("根据NUREG 0700等相关要求，仍需要对ECP坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。", new iTextSharp.text.Font(baseFont, 13));
            p57.FirstLineIndent = 20f;
            p57.Leading = 13 * 1.5f;
            doc.Add(p57);

            // 创建一个段落对象
            Paragraph p58 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p58.FirstLineIndent = 20f;
            p58.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p58.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p58);

        }
        else if (cellValue.Contains("VDU"))
        {
            iTextSharp.text.Paragraph p41 = new iTextSharp.text.Paragraph("4 VDU人体工效学评估", new iTextSharp.text.Font(baseFont, 15));
            p41.Leading = 15 * 1.5f;
            doc.Add(p41);

            iTextSharp.text.Paragraph p42 = new iTextSharp.text.Paragraph("4.1 VDU可视性视角视距分析", new iTextSharp.text.Font(baseFont, 13));
            p42.Leading = 13 * 1.5f;
            doc.Add(p42);

            iTextSharp.text.Paragraph p43 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p43.FirstLineIndent = 20f;
            p43.Leading = 13 * 1.5f;
            doc.Add(p43);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t3 = new PdfPTable(2);
            t3.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t3.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t3.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t3.AddCell(CreateCell("标准条例", baseFont));
            t3.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    t3.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t3.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t3);

            iTextSharp.text.Paragraph p44 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm、眼至盘台前沿51mm，95%成年男性坐姿眼高1295mm、眼至盘台前沿41mm。对VDU视角视距进行评估，如图4-1所示。", new iTextSharp.text.Font(baseFont, 13));
            p44.FirstLineIndent = 20f;
            p44.Leading = 13 * 1.5f;
            doc.Add(p44);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath5 = System.Environment.CurrentDirectory + @"\Output\left3.jpg";
            iTextSharp.text.Image image5 = iTextSharp.text.Image.GetInstance(imagePath5);
            image5.Alignment = Element.ALIGN_CENTER;
            image5.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t05 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell5 = new PdfPCell(image5);
            imageCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(imageCell5);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图4-1 VDU视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            PdfPCell paragraphCell5 = new PdfPCell(p46);
            paragraphCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell5.Border = PdfPCell.NO_BORDER;
            t05.AddCell(paragraphCell5);
            // 将表格添加到文档
            doc.Add(t05);


            // 创建一个段落对象
            Paragraph p45 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p45.FirstLineIndent = 20f;
            p45.Leading = 13 * 1.5f;
            // 创建一个字体对象
            Font font = new Font(baseFont, 13);
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p45.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p45);

            /*
            iTextSharp.text.Paragraph p46 = new iTextSharp.text.Paragraph("图6-1 OWP控制台高度评估", new iTextSharp.text.Font(baseFont, 12));
            p46.Alignment = Element.ALIGN_CENTER;
            p46.Leading = 12 * 1.5f;
            doc.Add(p46);*/

            iTextSharp.text.Paragraph p47 = new iTextSharp.text.Paragraph("4.2 VDU可达性分析", new iTextSharp.text.Font(baseFont, 13));
            p47.Leading = 13 * 1.5f;
            doc.Add(p47);

            iTextSharp.text.Paragraph p48 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p48.FirstLineIndent = 20f;
            p48.Leading = 13 * 1.5f;
            doc.Add(p48);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t4 = new PdfPTable(2);
            t4.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t4.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t4.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t4.AddCell(CreateCell("标准条例", baseFont));
            t4.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    t4.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t4.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t4);

            iTextSharp.text.Paragraph p49 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿肩高860mm、功能伸展范围614mm、肩至盘台前沿127mm，取95%成年男性坐姿肩高1089mm、功能伸展范围792mm、肩至盘台前沿123mm。对VDU可操作范围进行评估，如图4-2所示。", new iTextSharp.text.Font(baseFont, 13));
            p49.FirstLineIndent = 20f;
            p49.Leading = 13 * 1.5f;
            doc.Add(p49);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath6 = System.Environment.CurrentDirectory + @"\Output\left2.jpg";
            iTextSharp.text.Image image6 = iTextSharp.text.Image.GetInstance(imagePath6);
            image6.Alignment = Element.ALIGN_CENTER;
            image6.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t06 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell6 = new PdfPCell(image6);
            imageCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(imageCell6);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图4-2 VDU控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            PdfPCell paragraphCell6 = new PdfPCell(p50);
            paragraphCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell6.Border = PdfPCell.NO_BORDER;
            t06.AddCell(paragraphCell6);
            // 将表格添加到文档
            doc.Add(t06);

            /*
            iTextSharp.text.Paragraph p50 = new iTextSharp.text.Paragraph("图6-2 OWP控制台可操作范围评估", new iTextSharp.text.Font(baseFont, 12));
            p50.Alignment = Element.ALIGN_CENTER;
            p50.Leading = 12 * 1.5f;
            doc.Add(p50);*/

            // 创建一个段落对象
            Paragraph p51 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p51.FirstLineIndent = 20f;
            p51.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可达"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p51.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p51);

            iTextSharp.text.Paragraph p52 = new iTextSharp.text.Paragraph("4.3 VDU盘台尺寸及容膝容足空间分析", new iTextSharp.text.Font(baseFont, 13));
            p52.Leading = 13 * 1.5f;
            doc.Add(p52);

            iTextSharp.text.Paragraph p53 = new iTextSharp.text.Paragraph("根据NUREG 0700等标准条例对坐姿操作的控制台的要求：", new iTextSharp.text.Font(baseFont, 13));
            p53.FirstLineIndent = 20f;
            p53.Leading = 13 * 1.5f;
            doc.Add(p53);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t6 = new PdfPTable(2);
            t6.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t6.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t6.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t6.AddCell(CreateCell("标准条例", baseFont));
            t6.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    t6.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t6.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t6);

            iTextSharp.text.Paragraph p54 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%女性坐姿膝盖高度为424mm、臀膝距495mm、脚长213mm，95%男性坐姿膝盖高度为532mm、臀膝距595mm、脚长264mm。对VDU盘台尺寸和容膝容足空间进行分析，如图4-3所示", new iTextSharp.text.Font(baseFont, 13));
            p54.FirstLineIndent = 20f;
            p54.Leading = 13 * 1.5f;
            doc.Add(p54);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath7 = System.Environment.CurrentDirectory + @"\Output\left1.jpg";
            iTextSharp.text.Image image7 = iTextSharp.text.Image.GetInstance(imagePath7);
            image7.Alignment = Element.ALIGN_CENTER;
            image7.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t7 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell7 = new PdfPCell(image7);
            imageCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(imageCell7);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图4-3 VDU盘台尺寸及容膝容足分析", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            PdfPCell paragraphCell7 = new PdfPCell(p55);
            paragraphCell7.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell7.Border = PdfPCell.NO_BORDER;
            t7.AddCell(paragraphCell7);
            // 将表格添加到文档
            doc.Add(t7);

            /*
            iTextSharp.text.Paragraph p55 = new iTextSharp.text.Paragraph("图6-3 OWP视角视距评估", new iTextSharp.text.Font(baseFont, 12));
            p55.Alignment = Element.ALIGN_CENTER;
            p55.Leading = 12 * 1.5f;
            doc.Add(p55);*/

            // 创建一个段落对象
            Paragraph p56 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p56.FirstLineIndent = 20f;
            p56.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("盘台") || category.Contains("容膝容足"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p56.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p56);

            iTextSharp.text.Paragraph p60 = new iTextSharp.text.Paragraph("4.4 VDU其他分析", new iTextSharp.text.Font(baseFont, 13));
            p60.Leading = 13 * 1.5f;
            doc.Add(p60);


            iTextSharp.text.Paragraph p57 = new iTextSharp.text.Paragraph("根据NUREG 0700等相关要求，仍需要对VDU坐姿控制台与座位距离、人体关节角度、操作空间进行分析。取5%女性坐姿人体轴线至台边距离127mm、椅面以上的身高（伸直）809mm、椅面以上的眼高（伸直）695mm，95%男性坐姿人体轴线至台边距离为123mm、椅面以上的身高（伸直）958mm、椅面以上的眼高（伸直）847mm。", new iTextSharp.text.Font(baseFont, 13));
            p57.FirstLineIndent = 20f;
            p57.Leading = 13 * 1.5f;
            doc.Add(p57);

            // 创建一个段落对象
            Paragraph p58 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p58.FirstLineIndent = 20f;
            p58.Leading = 13 * 1.5f;
            // 创建一个字体对象
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("关节") || category.Contains("座位") || category.Contains("操作"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p58.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p58);

        }
        else
        {
            iTextSharp.text.Paragraph p18 = new iTextSharp.text.Paragraph("4 大屏幕（LDP）人体工效学评估", new iTextSharp.text.Font(baseFont, 15));
            p18.Leading = 15 * 1.5f;
            doc.Add(p18);

            iTextSharp.text.Paragraph p19 = new iTextSharp.text.Paragraph("4.1 LDP可视性评估", new iTextSharp.text.Font(baseFont, 13));
            p19.Leading = 13 * 1.5f;
            doc.Add(p19);

            iTextSharp.text.Paragraph p20 = new iTextSharp.text.Paragraph("按照运行需求，主控制室的布局需要保证值长、副值长在运行期间能够清晰地读取大屏幕上的相关信息。因此，使用NUREG 0700《人机接口设计审查导则》中的要求来对大屏幕可视性进行评估", new iTextSharp.text.Font(baseFont, 13));
            p20.FirstLineIndent = 20f;
            p20.Leading = 13 * 1.5f;
            doc.Add(p20);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            PdfPTable t3 = new PdfPTable(2);
            t3.WidthPercentage = 100; // 表格宽度占页面宽度的100%
            t3.HorizontalAlignment = Element.ALIGN_CENTER; // 表格水平居中
            t3.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE; // 表格内单元格垂直居中
            t3.AddCell(CreateCell("标准条例", baseFont));
            t3.AddCell(CreateCell("标准描述", baseFont));
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    t3.AddCell(CreateCell(standard_arr[i], baseFont));  // 在新行的第一列添加标准条例
                    t3.AddCell(CreateCell(description_arr[i], baseFont));      // 在新行的第二列添加标准描述
                }
            }
            doc.Add(t3);

            iTextSharp.text.Paragraph p21 = new iTextSharp.text.Paragraph("根据GB 10000中的人体测量数据，取5%成年女性坐姿眼高1037mm，95%成年男性坐姿眼高1295mm。结合当前控制室布局和OWP显示器安装方式，对LDP可视性进行评估，如图4-1、4-2所示。", new iTextSharp.text.Font(baseFont, 13));
            p21.FirstLineIndent = 20f;
            p21.Leading = 13 * 1.5f;
            doc.Add(p21);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath1 = System.Environment.CurrentDirectory + @"\Output\screen2.jpg";
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(imagePath1);
            image1.Alignment = Element.ALIGN_CENTER;
            image1.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t01 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell1 = new PdfPCell(image1);
            imageCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell1.Border = PdfPCell.NO_BORDER;
            t01.AddCell(imageCell1);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p22 = new iTextSharp.text.Paragraph("图4-1 LDP可视性评估1", new iTextSharp.text.Font(baseFont, 12));
            p22.Alignment = Element.ALIGN_CENTER;
            p22.Leading = 12 * 1.5f;
            PdfPCell paragraphCell1 = new PdfPCell(p22);
            paragraphCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell1.Border = PdfPCell.NO_BORDER;
            t01.AddCell(paragraphCell1);
            // 将表格添加到文档
            doc.Add(t01);

            /*
            iTextSharp.text.Paragraph p22 = new iTextSharp.text.Paragraph("图4-1 大屏幕可视性评价", new iTextSharp.text.Font(baseFont, 12));
            p22.Alignment = Element.ALIGN_CENTER;
            p22.Leading = 12 * 1.5f;
            doc.Add(p22);*/

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            string imagePath2 = System.Environment.CurrentDirectory + @"\Output\screen.jpg";
            iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(imagePath2);
            image2.Alignment = Element.ALIGN_CENTER;
            image2.ScalePercent(30f);
            // 创建一个表格，并指定列数为1
            PdfPTable t02 = new PdfPTable(1);
            // 第一行单元格：添加图像
            PdfPCell imageCell2 = new PdfPCell(image2);
            imageCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            imageCell2.Border = PdfPCell.NO_BORDER;
            t02.AddCell(imageCell2);
            // 第二行单元格：添加段落
            iTextSharp.text.Paragraph p26 = new iTextSharp.text.Paragraph("图4-2 LDP可视性评估2", new iTextSharp.text.Font(baseFont, 12));
            p26.Alignment = Element.ALIGN_CENTER;
            p26.Leading = 12 * 1.5f;
            PdfPCell paragraphCell2 = new PdfPCell(p26);
            paragraphCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            paragraphCell2.Border = PdfPCell.NO_BORDER;
            t02.AddCell(paragraphCell2);
            // 将表格添加到文档
            doc.Add(t02);


            // 创建一个段落对象
            Paragraph p24 = new Paragraph();
            // 设置段落的首行缩进和行间距
            p24.FirstLineIndent = 20f;
            p24.Leading = 13 * 1.5f;
            // 创建一个字体对象
            Font font = new Font(baseFont, 13);
            for (int i = 0; i < j; i++)
            {
                string category = category_arr[i];
                if (category.Contains("可视"))
                {
                    // 创建文本内容
                    string text = $"根据标准条例{standard_arr[i]}要求：{description_arr[i]}。实际值为{value_arr[i]}，因此{result_arr[i]}人体工效学需求。";
                    // 将文本添加到段落中
                    Chunk chunk = new Chunk(text, font);
                    p24.Add(chunk);
                }
            }
            // 将段落添加到文档中
            doc.Add(p24);

            /*
            iTextSharp.text.Paragraph p26 = new iTextSharp.text.Paragraph("图4-2 坐姿空间评估", new iTextSharp.text.Font(baseFont, 12));
            p26.Alignment = Element.ALIGN_CENTER;
            p26.Leading = 12 * 1.5f;
            doc.Add(p26);*/
        }

        // 关闭
        doc.Close();
        pdfFs.Close();

    }


    private PdfPCell CreateCell(string text, BaseFont font)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(font, 13, iTextSharp.text.Font.NORMAL)));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        return cell;
    }
    private static PdfPTable CreateMultRowMultColumnTable(int column, List<string> columnDataNameList)
    {
        // 创建多列表格
        PdfPTable table = new PdfPTable(column);
        // 设置字号与字体颜色
        iTextSharp.text.Font font = SetFontSizeColor(12);
        // 设置自动换页
        table.SplitLate = true;
        table.SplitRows = true;
        // 定义单元格
        PdfPCell cell;
        // 这里的意思是：
        // 将链表中的数据一个一个的设置到单元格中，然后单元格添加到表格中
        // 这里需要注意，因为我们定义表格对象是设置了列数，所以设置单元格达到表的列数后，再次往表中添加单元格会自动换行添加！
        int rowColumn = columnDataNameList.Count;
        for (int i = 0; i < rowColumn; i++)
        {
            // 实例化单元格对象，数据与字体设置到单元格中
            cell = new PdfPCell(new Phrase(columnDataNameList[i], font));
            cell.HorizontalAlignment = 1;

            // 将单元格添加到表格中
            table.AddCell(cell);
        }
        return table;
    }
    private static iTextSharp.text.Font SetFontSizeColor(int size)
    {
        BaseFont bfchinese = BaseFont.CreateFont(@"c:\windows\fonts\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//simkai.ttf
        iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfchinese, size, 0);
        return ChFont;
    }
    public XWPFParagraph SetTableParagraphInstanceSetting(XWPFDocument document, XWPFTable table, string fillContent, NPOI.XWPF.UserModel.ParagraphAlignment paragraphAlign, int textPosition = 24, bool isBold = false, int fontSize = 10, string fontColor = "000000", bool isItalic = false)
    {
        var para = new CT_P();
        //设置单元格文本对齐
        para.AddNewPPr().AddNewTextAlignment();
        XWPFParagraph paragraph = new XWPFParagraph(para, table.Body);//创建表格中的段落对象
        paragraph.Alignment = paragraphAlign;//文字显示位置,段落排列（左对齐，居中，右对齐）
                                             //paragraph.FontAlignment =Convert.ToInt32(ParagraphAlignment.CENTER);//字体在单元格内显示位置与 paragraph.Alignment效果相似

        XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
        xwpfRun.SetText(fillContent);
        xwpfRun.FontSize = fontSize;//字体大小
        xwpfRun.SetColor(fontColor);//设置字体颜色--十六进制
        xwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
        xwpfRun.IsBold = isBold;//是否加粗
        xwpfRun.SetFontFamily("宋体", FontCharRange.None);//设置字体（如：微软雅黑,华文楷体,宋体）
        xwpfRun.SetTextPosition(textPosition);//设置文本位置（设置两行之间的行间），从而实现table的高度设置效果 
        return paragraph;
    }
    public static void CreatePicture(XWPFDocument doc, string id, int width, int height)
    {
        int EMU = 9525;
        width *= EMU;
        height *= EMU;

        string picXml = ""
                //+ "<a:graphic xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\">"
                //+ "   <a:graphicData uri=\"http://schemas.openxmlformats.org/drawingml/2006/picture\">"
                + "      <pic:pic xmlns:pic=\"http://schemas.openxmlformats.org/drawingml/2006/picture\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\">"
                + "         <pic:nvPicPr>" + "            <pic:cNvPr id=\""
                + "0"
                + "\" name=\"Generated\"/>"
                + "            <pic:cNvPicPr/>"
                + "         </pic:nvPicPr>"
                + "         <pic:blipFill>"
                + "            <a:blip r:embed=\""
                + id
                + "\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"/>"
                + "            <a:stretch>"
                + "               <a:fillRect/>"
                + "            </a:stretch>"
                + "         </pic:blipFill>"
                + "         <pic:spPr>"
                + "            <a:xfrm>"
                + "               <a:off x=\"0\" y=\"0\"/>"
                + "               <a:ext cx=\""
                + width
                + "\" cy=\""
                + height
                + "\"/>"
                + "            </a:xfrm>"
                + "            <a:prstGeom prst=\"rect\">"
                + "               <a:avLst/>"
                + "            </a:prstGeom>"
                + "         </pic:spPr>"
                + "      </pic:pic>";
        //+ "   </a:graphicData>" + "</a:graphic>";
        var run = doc.CreateParagraph().CreateRun();
        CT_Inline inline = run.GetCTR().AddNewDrawing().AddNewInline();

        inline.graphic = new CT_GraphicalObject();
        inline.graphic.graphicData = new CT_GraphicalObjectData();
        inline.graphic.graphicData.uri = "http://schemas.openxmlformats.org/drawingml/2006/picture";

        // CT_GraphicalObjectData graphicData = inline.graphic.AddNewGraphicData();
        // graphicData.uri = "http://schemas.openxmlformats.org/drawingml/2006/picture";

        //XmlDocument xmlDoc = new XmlDocument();
        try
        {
            //xmlDoc.LoadXml(picXml);
            //var element = xmlDoc.DocumentElement;
            inline.graphic.graphicData.AddPicElement(picXml);

        }
        catch (XmlException xe)
        {

        }

        NPOI.OpenXmlFormats.Dml.WordProcessing.CT_PositiveSize2D extent = inline.AddNewExtent();
        extent.cx = width;
        extent.cy = height;

        NPOI.OpenXmlFormats.Dml.WordProcessing.CT_NonVisualDrawingProps docPr = inline.AddNewDocPr();
        docPr.id = 1;
        docPr.name = "Image" + id;
    }
}


