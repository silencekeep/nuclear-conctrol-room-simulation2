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

public class importWord : MonoBehaviour
{
    public InputField height_StandingInputField, seegle_StandingInputField, shoulder_StandingInputField, fingertips_StandingInputField,
                      stretch_StandingInputField, axisDistance_StandingInputField, centralAxisToEye_StandingInputField, eyeToSide_StandingInputField, shoulderToSide_StandingInputField,//站姿
                      belowTheKnee_SittingInputField, heightAboveChair_SittingInputField, seegleAboveChair_SittingInputField,
                      shoulderAboveChair_SittingInputField, stretch_SittingInputField, thigh_SittingInputField, hipToKneeMesial_SittingInputField, kneeHeight_SittingInputField,
                      axisDistance_SittingInputField, centralAxisToEye_SittingInputField, seegleHeight_SittingInputField, shoulderHeight_SittingInputField,
                      hipToKnee_SittingInputField, foot_SittingInputField, bodyToToes_SittingInputField, eyeToSide_SittingInputField, shoulderToSide_SittingInputField;//坐姿 
    public GameObject ImportPanel;

    public void ReadWord()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "simple.docx");
        FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        XWPFDocument doc = new XWPFDocument(stream);

        //string[] paragraphs = doc.Paragraphs.Select(p => p.ParagraphText).ToArray();
        
        List<string> arraylist = new List<string>();
        foreach (var paragraph in doc.Paragraphs)
        {
            string text = paragraph.ParagraphText;
            Debug.Log(text);
            arraylist.Add(text);
        }

        height_StandingInputField.text = Convert.ToString(arraylist[0]);
        seegle_StandingInputField.text = Convert.ToString(arraylist[1]);
        shoulder_StandingInputField.text = Convert.ToString(arraylist[2]);
        fingertips_StandingInputField.text = Convert.ToString(arraylist[3]);
        stretch_StandingInputField.text = Convert.ToString(arraylist[4]);
        axisDistance_StandingInputField.text = Convert.ToString(arraylist[5]);
        centralAxisToEye_StandingInputField.text = Convert.ToString(arraylist[6]);
        eyeToSide_StandingInputField.text = Convert.ToString(arraylist[7]);
        shoulderToSide_StandingInputField.text = Convert.ToString(arraylist[8]);

        belowTheKnee_SittingInputField.text = Convert.ToString(arraylist[9]);
        heightAboveChair_SittingInputField.text = Convert.ToString(arraylist[10]);
        seegleAboveChair_SittingInputField.text = Convert.ToString(arraylist[11]);
        shoulderAboveChair_SittingInputField.text = Convert.ToString(arraylist[12]);
        stretch_SittingInputField.text = Convert.ToString(arraylist[13]);
        thigh_SittingInputField.text = Convert.ToString(arraylist[14]);
        hipToKneeMesial_SittingInputField.text = Convert.ToString(arraylist[15]);
        kneeHeight_SittingInputField.text = Convert.ToString(arraylist[16]);
        axisDistance_SittingInputField.text = Convert.ToString(arraylist[17]);
        centralAxisToEye_SittingInputField.text = Convert.ToString(arraylist[18]);
        seegleHeight_SittingInputField.text = Convert.ToString(arraylist[19]);
        shoulderHeight_SittingInputField.text = Convert.ToString(arraylist[20]);
        hipToKnee_SittingInputField.text = Convert.ToString(arraylist[21]);
        foot_SittingInputField.text = Convert.ToString(arraylist[22]);
        bodyToToes_SittingInputField.text = Convert.ToString(arraylist[23]);
        eyeToSide_SittingInputField.text = Convert.ToString(arraylist[24]);
        shoulderToSide_SittingInputField.text = Convert.ToString(arraylist[25]);
        doc.Close();
        stream.Close();
    }

    public void wordImportExit()
    {
        ImportPanel.SetActive(false);

    }
}
