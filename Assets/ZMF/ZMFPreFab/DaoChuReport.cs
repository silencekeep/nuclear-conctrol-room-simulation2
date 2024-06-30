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

public class DaoChuReport : MonoBehaviour
{
    public GameObject openReport;
    public Dropdown format;
    int formatSelected;
    bool ifOpen;
    private int hour;
    private int minute;
    private int second;
    private int year;
    private int month;
    private int day;
    private string filePath;
    private FileStream fs;
    public InputField renyuan, chanpin;
    private string na, reny, chanp;
    private string neirong;
    private FileStream fsfs;
    public string[] shushixing;
    public string[] caozuokongjian;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        
        //if (format != null)
        //{
        //    //获取当前时间
        //    hour = DateTime.Now.Hour;
        //    minute = DateTime.Now.Minute;
        //    second = DateTime.Now.Second;
        //    year = DateTime.Now.Year;
        //    month = DateTime.Now.Month;
        //    day = DateTime.Now.Day;

        //    //na = name.text;
        //    reny = renyuan.text;
        //    chanp = chanpin.text;

        //    //value = 1是doc，= 2是txt
        //    formatSelected = format.value;
        //    //if (formatSelected == 0)
        //    //{
        //    //    format_excel();
        //    //}
        //    //else
        //    //{
        //    //    format_txt();
        //    //}
        //}
        openReport.SetActive(true);
        //openReport.GetComponent<OpenReport>().path = filePath;
        //if (ifOpen)
        //{
        
        //}
    }

    public void format_txt()
    {        
        filePath = "";
        string path = System.Environment.CurrentDirectory + "/ZMF/Report/评价报告：" + reny + /*"_"  + chanp +*/ "_" + year + month + day + hour + minute + ".txt";
        //string path = EditorUtility.SaveFilePanel("Output Log", Application.dataPath, "Report" + year + month + day + hour + minute, "txt");
        string info1 = /*"测试名称：" + na + "\n" + */"测试人员: " + reny + " \n" /*+ "测试产品: " + chanp + " \n"*/ + "日期：" + year + month + day + hour + minute + "\n";
        string info2 = "        基于虚拟数字样机的集成式测评系统主要承担KJZ任务中开展方案阶段针对虚拟数字样机维修的工效学评价方法研究与评价实施的任务。为了支撑方案阶段维修界面的工效学评价，需要构建基于虚拟数字样机的集成式自主化测评系统：针对维修数字样机，搭建维修虚拟环境，开展基于虚拟数字样机的集成式工效学测评，尽早地发现维修设计中存在的工效学问题，最大限度的降低由产品更改带来的周期与经费的损失。" + 

        "任务内容包括基于导入的维修场景、数字样机数据创建虚拟维修环境，基于维修典型动作库驱动虚拟人进行维修操作，对虚拟维修过程进行建模，并分析仿真维修过程；基于工效学评价项目与内容，从维修典型动作的可视可达性分析、操作空间分析、辅助装置分布合理性分析、舒适性分析、多人间协同分析等方面进行评价，输出工效学分析参数，生成分析与测评报告，为工程样机提供改进建议，并进一步对工效学评价方法进行研究，达到有效提高工效学评价和产品研制工作效率的目的。" +

        "根据工效学评价承担任务以及学科长期建设的必要性，研制一套基于虚拟数字样机的集成式测评系统。该系统紧跟虚拟现实技术发展前沿，适用于KJZ任务中HT产品虚拟数字样机仿真维修测评。同时，测评系统具备评价数据采集、整理、分析、记录能力，能对数字样机维修等人机交互操作进行系统、准确、深入的工效学测评，并通过任务迭代推进工效学评价方法的发展。"+

        "操作复杂、体积较大的设备进行维修时，需要多人相互协作完成。人与人间的任务分配和程序设计的设计性影响HTY的工作效率，因此需要针对多人共同维修时的绩效、负荷等开展工效学评价。为了支撑维修程序的工效学评价，需要研制基于任务分析的维修程序综合评价系统。受制于地面重力条件和待修产品质量、体积等的限制，很难基于物理模型开展受试者参与的多人维修程序评价试验。因此，需将维修程序数字化，并构建任务分析数学模型，对维修程序分工合理性、时空干涉性等进行量化工效学评价。"+

        "根据工效学评价承担任务以及学科长期建设的必要性，研制一套基于任务分析的多人维修程序综合评价系统。该系统紧跟软硬件技术发展前沿，将维修程序数字化，适用于受制于地面重力条件和待修产品质量、体积等的限制，难以基于物理模型开展受试者参与的多人维修程序评价试验。多人维修程序综合评价系统应具备构建任务分析数学模型的能力，以对维修程序分工合理性、时空干涉性等进行量化工效学评价。基于任务分析的维修程序综合评价系统主要承担KJZ任务中典型维修作业程序的时间压力评估和负荷评估的任务，分析维修程序的协同设计合理性，通过记录作业过程的时间，分析多人协作过程中的时间压力值，进而分析程序设计的合理性。" + "\n";

        string info3 = "可视性分析: " + "  可视性就是维修人员对目标件以及自身的维修动作的观察的难易程度，直接关系到维修人员是否能舒适地看得到维修部件。基于GJB2873-97，最佳视锥的原则有：①最佳视野范围：最佳视野的区域是人体头部直立眼球转动，以视线中心线为轴线斜角为 15 度的圆锥形区域。②最大视野范围:视线中心线上15°~ 40°，下15°~ 20°，左右15°~ 35°的椭圆区域。 " +
        "通过对虚拟人的可视锥分析，可以确定进行每个维修操作时的可视范围，进而可以对维修操作的可视性进行评价，判断虚拟人在进行维修时，在当前位置是否可以看到维修对象的零部件，或是否可以清晰的看到零件上的维修部位。 在程序的可视化图形界面，虚拟人在执行维修操作时，点击按钮可运行视锥窗口，在视锥窗口内可实时观察虚拟人的可视域，观察到人体视野范围内的场景，可以直观地观察到当前维修部件是否在虚拟人的最佳视野范围内，来判断维修可视性好坏；或是是否在虚拟人的最大视野范围内，来判断维修是否可以开展。对维修人员来说，长时间保持视角不在最佳视野范围内会造成维修人员的疲惫尤其是眼睛部位，增加维修人员负荷。如果目标件不在最佳视野区域，再根据它所处区域来判定，依次按等级评估，对于可视性不好的姿势需要修正。当位于最大视野区域范围以外时，则表明该产品维修性可视性差。在可视性不佳的情况下，可以保存当前虚拟维修步骤节点，在此基础上进行虚拟维修的调整和修正。系统可以列出虚拟人可视域内与目标维修部件之间的遮挡物，提示测试实验人员对维修过程进行改良，如增加去除遮挡视野部件操作、变换维修者位置等。同时，在视锥窗口内提供截图工具，可以留存当前视野范围状况，可供实验人员进行对比分析。 " + "\n" +
        "盲操作次数：" + "0" + "次";
        string infokeda = "        实体可达性，指维修人员接触目标件的难易程度。实体可达性一般遵循的原则有：虚拟人可以很好的接触到目标件，不需要拆卸周围零件；需要接触大的目标件时，在设计布局是应当考虑可达性，安放在易于接触的位置；易于故障的零部件以及更换周期短的零部件需要放在易于接触的位置。" +
"虚拟维修系统的实体可达性分析主要包括两方面，一是检查虚拟人在进行虚拟维修动作时，在当前位置是否可以触及待维修零部件。如果在虚拟人触及不到零部件，表示此位置可达性较差，无法继续进行维修。发生无法触及的情况时，系统会弹出窗口提示维修无法继续，并自动计算触及距离差，将距离差标红并闪烁显示，直至实验操作人员进行了维修操作的修正，此时可以保存当前虚拟维修步骤节点，在此基础上进行重复实验，无需担心数据发生丢失。二是检查虚拟人在进行虚拟维修动作时，在当前位置触及操作部位后，手臂等身体部位是否会与其他物体发生穿越现象。如果发生模型的穿越，表明实际操作中航天员是无法真正接触到维修零部件的，这样一来虚拟维修就无法还原真实场景。" + "\n";
        string info4 = "操作空间分析:" + "        维修人员应能够有足够的操作空间，根据人体尺寸设计，作业空间应大于人体最小作业空间。系统通过研究工具与虚拟人在维修过程中的运动，分析虚拟人在维修操作过程中是否与环境发生碰撞，是否有足够的活动空间，以及多人协同操作时是否相互干涉，实现对操作空间的评价。"+

        "维修部位是否实体可达采用可达包膜判断，采用可达包膜描述人体上肢能触及到的范围，它可以精确地判断维修部位是否处于可达包膜之内可达包膜用球状面描述了人体上肢能触及到的范围，其中包含了碰撞检测算法，可以精确判断维修部位是否处于可达包膜之内。系统会定义虚拟人的上肢触及范围，在界面中设置显示包膜按钮，点按后虚拟人的可达包膜会显示在图形界面中。" + "\n";
        string info5 = "舒适性分析: " + "        通过对虚拟维修全流程中的操作力、维修操作姿态、人体受力进行分析，对整个维修操作的舒适性进行评价。在虚拟维修过程中，虚拟人的姿势是否合理直接关系到维修是否可以安全进行。为更好满足舒适度的要求，对虚拟维修过程中的虚拟人维修姿势进行下背部压力分析，通过各个部位的受力分析实时反映虚拟人的舒适度情况，并针对受力较大部位进行合理的调整，使其受力满足舒适度要求。" +

        "静强度预测工具有助于评估能够满足任务强度要求的人群百分比，该工具针对姿势、劳动强度和人体测量学进行评估。" +

        "静强度评估的是为了分析指定工作的强度是否能满足所有工人的要求；预测男性和女性中能够满足指定工作静强度要求的人数百分比；通过实时监控，找出超过工效学要求数据库中规定或用户指定的强度极限的姿势。" +

        "在系统中设定任务中最艰难的姿势，也可以运行实时仿真执行全部或部分任务；确定工人的性别和人体测量学参数；确定每只手受力的数值和方向。检测静强度后会生成静强度评估报，包括能够完成任务强度要求的工人的百分比、上肢角度（肘、肩、肱骨旋转、髋、膝、踝）和躯干角度（弯曲、旋转、侧弯）、四肢和躯干的扭矩以及肌肉反应（弯曲、伸展、外展或内收）、群体强度意义和强度标准偏差等。在图形化的界面上，实时检测工作中虚拟人各个关节的静强度，当有静强度到达警戒值时，标红显示该部位，生成错误行，引导操作者修改工作环节。" + 
        "关节平均得分： 3.4  " + "\n";
        string info6 = "辅助装置布局分析：" + " 脚限最优距离：40-100 " + "\n";
        string information = info1 + info2 + info3 + infokeda + info4 + info5 + info6;

        if (!File.Exists(path))
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(information);
            sw.Close();
        }
        else
        {
            if (path.Length != 0)
            {
                FileStream aFile = new FileStream(@"" + path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                sw.Write(information);
                sw.Close();
                sw.Dispose();
            }
        }        
        filePath = path;
        Debug.Log("导出文本文档成功" + path);
        
    }

    public void format_excel()
    {
        filePath = "";
        //string path = Application.dataPath + "/ZMF/Report/评价报告" + year + month + day + hour + minute + ".xls";
        string path = System.Environment.CurrentDirectory + "/ZMF/Report/评价报告：" + reny + /*"_" + chanp +*/ "_" + year + month + day + hour + minute + ".xls";

        //read thetemplate via FileStream, it is suggested to use FileAccess.Read to prevent filelok.
        //book1.xlsis an Excel-2007-generated file, so some new unknown BIFF records are added.
        fs = new FileStream(Application.dataPath + "/ZMF/Report/muban1.xls", FileMode.Open, FileAccess.Read);

        HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);

        HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.GetSheet("评价报告");

        try
        {
            //sheet1.GetRow(42).GetCell(4).SetCellValue(chanp);

            sheet1.GetRow(45).GetCell(4).SetCellValue(reny);

            sheet1.GetRow(48).GetCell(4).SetCellValue(year+"."+month + "." + day);

            sheet1.GetRow(53).GetCell(1).SetCellValue(month);

            sheet1.GetRow(53).GetCell(2).SetCellValue(day);


        }
        catch (Exception e3)
        {
            Debug.Log(e3);
        }
        
        
        //Force excel to recalculate all the formulawhile open
        sheet1.ForceFormulaRecalculation = true;

        filePath = Application.dataPath + "/ZMF/Report/评价报告" + reny + /*"_" + chanp +*/ "_" + year + month + day + hour + minute + ".xls";

        FileStream file = new FileStream(filePath, FileMode.Create);
        filePath = Application.dataPath + "/ZMF/Report/评价报告" + reny + /*"_" + chanp +*/ "_" + year + month + day + hour + minute + ".xls";

        hssfworkbook.Write(file);

        file.Close();
        
        //NPOI.HSSF.UserModel.HSSFWorkbook wk = new NPOI.HSSF.UserModel.HSSFWorkbook();
        //ISheet sheet = wk.CreateSheet("评价报告");//excel表格的sheet名称，可自行更改
        //int columns = 1;
        //int rows = 1;;
        //int columns = ds.Tables[0].Columns.Count;
        //int rows = ds.Tables[0].Rows.Count;
        //IRow rowHead = sheet.CreateRow(0);
        //for (int j = 0; j < columns; j++)
        //{
        //    rowHead.CreateCell(j, CellType.String).SetCellValue("test" + "\n" + "可视性分析: ");
        //    //rowHead.CreateCell(j, CellType.String).SetCellValue(ds.Tables[0].Columns[j].ToString());
        //}

        //for (int i = 0; i < rows; i++)
        //{
        //    IRow row = sheet.CreateRow(i + 1);
        //    for (int j = 0; j < columns; j++)
        //    {
        //        row.CreateCell(j).SetCellValue("1");
        //    }
        //}
        //fs = File.Create(path);//文件保存路径和名称，可自行更改
        //wk.Write(fs);
        //fs.Close();
        //fs.Dispose();

        //filePath = path;
        Debug.Log("导出Excel成功");

    }
}
