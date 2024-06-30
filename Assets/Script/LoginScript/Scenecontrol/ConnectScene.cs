// 连接服务器状态
using iTextSharp.xmp.impl.xpath;
using Pada1.BBCore.Framework;
using System.Data.SqlClient;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Collections;

public class ConnectScene : SceneState
{
    public ConnectScene(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "ConnectScene";
    }

    public override void StateUpdate()
    {
        GameObject error = GameObject.Find("登录注册界面/UI/error");
        
        switch (Gamedata.Instance.datastate)
        {
            case 3:
                m_Controller.SetState(new LoginScene(m_Controller), "LoginScene");
                error.SetActive(false);
                break;
            case 2:
                error.SetActive(true);
                error.GetComponent<TMP_Text>().color = new Color(25 / 255f, 25 / 255f, 112 / 255f, 255 / 255f);
                error.GetComponent<TMP_Text>().text = "正在尝试连接数据库，请稍候";
                break;
            case 1:
                error.SetActive(true);
                error.GetComponent<TMP_Text>().color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
                error.GetComponent<TMP_Text>().text = "连接失败";
                break;
            default:
                error.SetActive(false);
                break;
        }       
    }





}