using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
 
public class OpenFile : MonoBehaviour
{
    public static string ActPathString;
    public InputField AddPathInputField;
    public InputField EditPathInputField;
    //public Button OpenFileBtn;
    private void Start()
    {
        //OpenFileBtn.onClick.AddListener(delegate () { OnOpenFile(); });
    }

    public void OnOpenFile()//选择excel文件
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "Excel文件(*.xlsx)\0*.xlsx\0Excel文件(*.xls)\0*.xls\0\0";//|Excel文件(*.xls)|*.xls
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            ActPathString = openFileName.file;
            //if(ActManager.AddActPathSign == true)
            //{
            //    AddPathInputField.text = ActPathString;                
            //}
            //if(ActManager.EditActPathSign == true)
            //{               
            //    EditPathInputField.text = ActPathString;
            //}
            //Debug.Log("打开的文件路径：" + openFileName.file);
            //Debug.Log("文件名：" + openFileName.fileTitle);
        }
    }

    public void OnOpenFileAni()//选择动捕文件
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "动捕文件(*.fbx)|*.fbx";//|Excel文件(*.xls)|*.xls
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = UnityEngine.Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "文件选择";//窗口标题
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            ActPathString = openFileName.file;
            //if (ActManager.AddActPathSign == true)
            //{
            //    AddPathInputField.text = ActPathString;
            //}
            //if (ActManager.EditActPathSign == true)
            //{
            //    EditPathInputField.text = ActPathString;
            //}
            //Debug.Log("打开的文件路径：" + openFileName.file);
            //Debug.Log("文件名：" + openFileName.fileTitle);
        }
    }
}