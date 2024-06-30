using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using SFB;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine.EventSystems;

public class Importer : MonoBehaviour {

    // Poppup && Canvas Obj
    public RectTransform Import_Popup;
    public RectTransform Texure_Popup;
    public RectTransform ToolsDB_Popup;
    public RectTransform Model_Popup;
    public RectTransform Update_Popup;
    public RectTransform Menu_Bar;
    public RectTransform Loading_Popup;
    public RectTransform Error_Popup;
    public RectTransform m_ProgressBar;
    public RectTransform m_ProgressBarFull;
    public RectTransform finish_button;
    public GameObject TreeView_Popup;

    //Text field
    public Text Import_Info;
    public Text Import_Value;
    public Text tessel_text;
    public Text scale_text;
    public Toggle Z_UP;
    public Toggle Right_Handed;
    public Text Loading_text;
    public Dropdown MeshQualityDropdown;

    // Required
    public CADLoader cadLoader;
    public GameObject RootLoader;

    // Use this for initialization
    void Start()
     {
        //cadLoader.sceneRoot = RootLoader;
        cadLoader.ImportEnded += CADImportEnded;
    }

    // Update is called once per frame
    void Update()
    {
        if (cadLoader != null && cadLoader.IsImporting)
        {
            setProgressBar(cadLoader.Progress);
        }
    }

    public void HideShowPopup()
    {
        bool state = false;
        if (Import_Popup != null && Import_Popup != null)
        {
            state = !Import_Popup.gameObject.activeSelf;
            Import_Popup.gameObject.SetActive(state);
            Menu_Bar.gameObject.SetActive(state);
        }

        //bool state2 = false;
        //if (Texure_Popup != null && Texure_Popup != null)
        //{
        //    state2 = !Texure_Popup.gameObject.activeSelf;
        //    Texure_Popup.gameObject.SetActive(state2);
        //    Menu_Bar.gameObject.SetActive(state2);
        //}
    }

    public void HideShowPopupTexure()
    {
        //bool state = false;
        //if (Import_Popup != null && Import_Popup != null)
        //{
        //    state = !Import_Popup.gameObject.activeSelf;
        //    Import_Popup.gameObject.SetActive(state);
        //    Menu_Bar.gameObject.SetActive(state);
        //}

        bool state2 = false;
        if (Texure_Popup != null && Texure_Popup != null)
        {
            state2 = !Texure_Popup.gameObject.activeSelf;
            Texure_Popup.gameObject.SetActive(state2);
            Menu_Bar.gameObject.SetActive(state2);
        }
    }
    public void HideShowPopupToolsDB()
    {
        

        bool state2 = false;
        if (ToolsDB_Popup != null && ToolsDB_Popup != null)
        {
            state2 = !ToolsDB_Popup.gameObject.activeSelf;
            ToolsDB_Popup.gameObject.SetActive(state2);
            Menu_Bar.gameObject.SetActive(state2);
        }
    }

    public void HideErrorPopup()
    {
        if (Error_Popup != null)
            Error_Popup.gameObject.SetActive(false);
    }

    public void HideModelPopup()
    {
        if (Model_Popup != null)
            Model_Popup.gameObject.SetActive(false);
    }
    public void HideUpdatePopup()
    {
        if (Update_Popup != null)
            Update_Popup.gameObject.SetActive(false);
    }
    public void HideLoadingPopup()
    {
        if (Error_Popup != null)
            Loading_Popup.gameObject.SetActive(false);
        Menu_Bar.gameObject.SetActive(false);
    }

    public void TreeViewPopup()
    {
        if (!TreeView_Popup.activeInHierarchy)
        {
            TreeView_Popup.SetActive(true);
        }
        else
        {
            TreeView_Popup.SetActive(false);
        }
    }
    public void CADFileBrowse()
    {

        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);        
        openFileName.filter = "所有CAD文件\0*.3dxml;*.igs;*.jt;*.wrl;*.iges;*.CatProduct;*.CatPart;*.stp;*.step;*.sldasm;*.sldprt;*.fbx;*.ifc\0.wrl文件\0*.wrl\0.jt文件\0*.jt\0.igs文件\0*.igs\0.stp文件\0*.stp\0\0";
        //openFileName.filter = "所有文件\0*.*\0\0";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.dataPath.Replace('/', '\\');//默认路径
        openFileName.title = "文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetOpenFileName(openFileName))
        {
            cadLoader.tubFileName = openFileName.file;
            cadLoader.cadFileName = openFileName.file;
            Import_Value.text = Path.GetFileNameWithoutExtension(cadLoader.cadFileName);
            Import_Info.enabled = false;
        }


        //OpenFileDialog form = new OpenFileDialog();
        ///// INITIAL LOCALISATION
        //if (cadLoader.cadFileName.Length == 0)
        //    form.InitialDirectory = UnityEngine.Application.dataPath;
        //else
        //    form.InitialDirectory = Path.GetDirectoryName(cadLoader.cadFileName);

        //// FILTER
        //form.Title = "选择文件";
        //form.Filter = "CAD文件(*.3dxml;*.igs;*.iges;*.CatProduct;*.CatPart;*.stp;*.step;*.sldasm;*.sldprt;*.fbx;*.ifc) | *.3dxml;*.igs;*.iges;*.CATProduct;*.CATPart;*.stp;*.step;*.sldasm;*.sldprt;*.fbx;*.ifc";

        //// RESULT
        //if (form.ShowDialog() == DialogResult.OK)
        //{
        //    cadLoader.tubFileName = form.FileName;
        //    cadLoader.cadFileName = form.FileName;
        //    Import_Value.text = Path.GetFileNameWithoutExtension(cadLoader.cadFileName);
        //    Import_Info.enabled = false;
        //}
    }

    public void CADFileBrowseKETISAN()
    {

        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "所有CAD文件\0*.3dxml;*.igs;*.jt;*.wrl;*.iges;*.CatProduct;*.CatPart;*.stp;*.step;*.sldasm;*.sldprt;*.fbx;*.ifc\0.stp文件\0*.stp\0\0";
        //openFileName.filter = "所有文件\0*.*\0\0";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.dataPath.Replace('/', '\\');//默认路径
        openFileName.title = "文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetOpenFileName(openFileName))
        {
            cadLoader.tubFileName = openFileName.file;
            cadLoader.cadFileName = openFileName.file;
            Import_Value.text = Path.GetFileNameWithoutExtension(cadLoader.cadFileName);
            Import_Info.enabled = false;
        }
    }


        public void CADFileImport_start()
    {
        string tesselation = "";
        string scale = "";
        bool zUp = true;
        bool rightHanded = true;
        int meshQuality = 1;

        if (tessel_text != null && tessel_text.text != "")
            tesselation = tessel_text.text;

        if (scale_text != null && scale_text.text != "")
            scale = scale_text.text;

        if (Z_UP != null)
        {
            zUp = Z_UP.isOn;
        }

        if (Right_Handed != null)
        {
                rightHanded = Right_Handed.isOn;
        }

        if (MeshQualityDropdown != null)
        {
            meshQuality = MeshQualityDropdown.value;
        }

        CADFileImport(meshQuality, scale, zUp, rightHanded);
    }

    public void CADFileImport(int meshQuality, string scale, bool zUp, bool rightHanded)
    {

        float my_scale = 0.001f;
        if (scale.Length > 0)
            my_scale = (float)Math.Abs(Convert.ToDouble(scale));


        if (cadLoader == null)
            return;

        Import_Popup.gameObject.SetActive(false);

        if (!cadLoader.CheckLicense())
        {
            Loading_Popup.gameObject.SetActive(true);
            Loading_text.text = "Invalid License";
            return;
        }


        if (cadLoader.tubFileName.Length == 0) // cadFileName
        {
            Debug.Log("请选择正确的三维模型文件");
            Error_Popup.gameObject.SetActive(true);
            return;
        }

        Loading_Popup.gameObject.SetActive(true);
        Loading_text.text = "正在导入 : " + cadLoader.tubFileName;
        m_ProgressBar.gameObject.SetActive(true);
        m_ProgressBarFull.gameObject.SetActive(true);
        finish_button.gameObject.SetActive(false);


        Debug.Log("正在导入 ...");

        cadLoader.cadFileName = cadLoader.tubFileName;
        cadLoader.LoadCAD(true, -1, meshQuality, my_scale, zUp, rightHanded);
    }

    private void CADImportEnded()
    {
        Loading_text.text = "导入完成 ! (用时 " + (int)(cadLoader.CadImportTiming) + " 秒)\n" + Path.GetFileName(cadLoader.cadFileName);
        m_ProgressBar.gameObject.SetActive(false);
        m_ProgressBarFull.gameObject.SetActive(false);
        finish_button.gameObject.SetActive(true);
    }
	
    void setProgressBar(float progress)
    {
        Vector3 scale = m_ProgressBar.localScale;
        scale.x = progress;
        m_ProgressBar.localScale = scale;
    }

}
