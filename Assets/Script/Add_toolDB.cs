using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using Button = UnityEngine.UI.Button;
using System.Runtime.InteropServices;

public class Add_toolDB : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    public InputField ifd1,ifd2,ifd3;
    // Use this for initialization

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        //OpenFileDialog form = new OpenFileDialog();
        //form.InitialDirectory = UnityEngine.Application.dataPath;
        //if (form.ShowDialog() == DialogResult.OK)
        //{
        //    string[] after = form.FileName.Split(new char[] { '\\' });
        //    //ifd3.text = form.FileName;
        //    ifd3.text = after[after.Length - 1];
        //    //File.Copy(form.FileName, UnityEngine.Application.dataPath + "/Tools/" + ifd3.text, true);
        //}
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "所有CAD文件\0*.3dxml;*.igs;*.wrl;*.iges;*.CatProduct;*.CatPart;*.stp;*.step;*.sldasm;*.sldprt;*.fbx;*.ifc\0.wrl文件\0*.wrl\0.jt文件\0*.jt\0.igs文件\0*.igs\0.stp文件\0*.stp\0\0";
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
            string[] after = openFileName.file.Split(new char[]    { '\\' });
            //ifd3.text = form.FileName;
            ifd3.text = after[after.Length - 1];
        }

    }
    public static void FileCoppy(string OrignFile, string NewFile)
    {
        
    }
}