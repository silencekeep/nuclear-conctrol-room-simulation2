using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class Gamedata : MonoBehaviour
{
    public static Gamedata Instance { get; private set; }
    public string location = "高级管理员";
    public string people = "yingyuan";
    //public string Ip = "127.0.0.1";
    public int datastate = 0;

    // 场景状态持有者
    SceneStateController m_SceneStateController = new SceneStateController();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // 设定起始场景
        m_SceneStateController.SetState(new LoginScene(m_SceneStateController), "");
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        m_SceneStateController.StateUpdate();
    }



}
