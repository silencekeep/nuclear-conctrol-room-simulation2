using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using SFB;

public class excel : MonoBehaviour
{
    private string filePath;
    private FileStream fs;          //文件流
    public GameObject exportokpanel;
    public static int ExpNum;

    private GameObject buttonObj;
    
    private void Start()
    {
        ExpNum = 0;
        buttonObj = GameObject.Find("Export");
        buttonObj.GetComponent<Button>().onClick.AddListener(M);

        void M()
        {
            ExpNum = ExpNum + 1;
            SqlConnection conn = new SqlConnection(@"server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456");
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM actions", conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
            //导出 
            NPOI.HSSF.UserModel.HSSFWorkbook wk = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet = wk.CreateSheet("mySheet");
            //ExcelFile excelFile = new ExcelFile();
            //ExcelWorksheet sheet = excelFile.Worksheets.Add("Test");

            int columns = ds.Tables[0].Columns.Count;
            int rows = ds.Tables[0].Rows.Count;

            //row = sheet.CreateRow(rows);
            //cell = row.CreateCell(0);
            //cell.SetCellValue(i);

            IRow rowHead = sheet.CreateRow(0);

            for (int j = 0; j < columns; j++)
            {
                rowHead.CreateCell(j, CellType.String).SetCellValue(ds.Tables[0].Columns[j].ToString());
            }

            for (int i = 0; i < rows; i++)
            {
                IRow row = sheet.CreateRow(i + 1);

                for (int j = 0; j < columns; j++)
                {

                    row.CreateCell(j).SetCellValue(ds.Tables[0].Rows[i][j].ToString());
                }

            }
            //var path = StandaloneFileBrowser.SaveFilePanel("保存", "", "动素库文件"+ ExpNum , "xls");//, "xls"
            var path = System.Environment.CurrentDirectory + "/动素库.xls";
            //fs = File.Create("D:/Develep/unity4/TryProject5_3/Assets/exportfiles/TEST"+ ExpNum + ".xls");
            if (path.Length > 0)
            {
                fs = File.Create(path);//+ ExpNum+ ".xls"
                wk.Write(fs);
                fs.Close();
                fs.Dispose();

                exportokpanel.SetActive(true);
            }
            
        }

       
    }
}

//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace ExportImportExecl
//{
//    public partial class index : System.Web.UI.Page
//    {
//        EntitiesModel db = new EntitiesModel();
//        protected void Page_Load(object sender, EventArgs e)
//        {
//        }

//        //导出学生信息
//        protected void btn_Export_Click(object sender, EventArgs e)
//        {
//            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"; // 文件名称
//            string urlPath = "upload/execl/" + fileName; // 文件下载的URL地址，供给前台下载
//            string filePath = HttpContext.Current.Server.MapPath("\\" + urlPath); // 文件路径

//            // 1.检测是否存在文件夹，若不存在就建立个文件夹
//            string DirectoryName = Path.GetDirectoryName(filePath);
//            if (!Directory.Exists(DirectoryName))
//            {
//                Directory.CreateDirectory(DirectoryName);
//            }
//            try
//            {
//                DataTable dt = new DataTable();
//                DataColumn dc = new DataColumn();
//                dc = dt.Columns.Add("id", typeof(string));         //标识id
//                dc = dt.Columns.Add("name", typeof(string));       //姓名
//                dc = dt.Columns.Add("age", typeof(string));        //年龄
//                dc = dt.Columns.Add("sex", typeof(string));        //性别
//                dc = dt.Columns.Add("sort", typeof(string));       //排序
//                var stu_info = db.s_studentinfo.Where(x => x.id > 0);
//                foreach (var item in stu_info)
//                {
//                    DataRow dr = dt.NewRow();
//                    dr["id"] = item.id.ToString();       //标识id
//                    dr["name"] = item.name.ToString();   //姓名
//                    dr["age"] = item.age.ToString();     //年龄
//                    dr["sex"] = item.sex.ToString();     //性别
//                    dr["sort"] = item.sort.ToString();   //排序
//                    dt.Rows.Add(dr);
//                }
//                string[] s = new string[] { "标识id", "姓名", "年龄", "性别", "排序" };

//                HSSFWorkbook workbook = DownExcel(s, dt);

//                FileStream file = new FileStream(filePath, FileMode.Create);
//                workbook.Write(file);
//                file.Close();

//                string A = urlPath;
//            }
//            catch (Exception)
//            {
//                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导出失败');</script>");
//            }
//        }


//        #region 生成Excel ================================================================
//        /// <summary>
//        /// 生成Excel
//        /// </summary>
//        /// <param name="s">excel第一行的数据（列名）</param>
//        /// <param name="dt">table</param>
//        /// <returns></returns>
//        public HSSFWorkbook DownExcel(string[] s, DataTable dt)
//        {
//            HSSFWorkbook workbook = new HSSFWorkbook();
//            ISheet sheet = workbook.CreateSheet("sheet1");
//            for (int i = 0; i < s.Length; i++)
//            {
//                if (i == 0)
//                {
//                    sheet.CreateRow(0).CreateCell(i).SetCellType(CellType.STRING);
//                    sheet.CreateRow(0).CreateCell(i).SetCellValue(s[i]);
//                }
//                else
//                {
//                    sheet.GetRow(0).CreateCell(i).SetCellType(CellType.STRING);
//                    sheet.GetRow(0).CreateCell(i).SetCellValue(s[i]);
//                }
//            }
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                for (int j = 0; j < s.Length; j++)
//                {
//                    if (j == 0)
//                    {
//                        sheet.CreateRow(i + 1).CreateCell(j).SetCellType(CellType.STRING);
//                        sheet.CreateRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
//                    }
//                    else
//                    {
//                        sheet.GetRow(i + 1).CreateCell(j).SetCellType(CellType.STRING);
//                        sheet.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
//                    }
//                }
//            }
//            return workbook;
//        }
//        #endregion
//    }
//}

   


    

