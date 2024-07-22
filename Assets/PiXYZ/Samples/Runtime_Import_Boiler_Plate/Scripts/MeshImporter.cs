using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Collections;
using PiXYZ;

public class MeshImporter : MonoBehaviour
{
    public GameObject importPanel;
    public GameObject importOkPanel;
    public GameObject sceneRoot;
    public InputField ImportDescriptionInputField;
    private CADLoader cadLoader;

    void Start()
    {
        cadLoader = new CADLoader();
        cadLoader.sceneRoot = sceneRoot;
    }
    public void confirmClick()
    {

        importPanel.SetActive(false);
    }
    public void cancelClick()
    {
        ImportDescriptionInputField.text = "还未选择文件";
        importPanel.SetActive(false);
    }
    public void closeClick()
    {
        ImportDescriptionInputField.text = "还未选择文件";//string.Empty;
        importPanel.SetActive(false);
    }

    public void CADFileBrowse()
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "所有模型文件类型\0*.ifc;*.sldprt;*.sldasm;*.step\0IFC文件\0*.ifc\0SLDPRT文件\0*.sldprt\0SLDASM文件\0*.sldasm\0STEP文件\0*.step\0\0"; ;
        //openFileName.filter = "所有文件\0*.*\0\0";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.dataPath.Replace('/', '\\');//默认路径
        openFileName.title = "模型文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        if (LocalDialog.GetOpenFileName(openFileName))
        {
            cadLoader.tubFileName = openFileName.file;
            cadLoader.cadFileName = openFileName.file;
            ImportDescriptionInputField.text = openFileName.file;
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
}




