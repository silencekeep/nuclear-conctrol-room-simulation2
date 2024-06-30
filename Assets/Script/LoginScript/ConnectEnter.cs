// using System.Collections;
// using System.Collections.Generic;
// using System.Data.SqlClient;
// using System.Data;
// using UnityEngine;
// using TMPro;
// using System.Threading;

// public class ConnectEnter : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
//     public void enter() {
//         Gamedata.Instance.datastate = 2;
//         GameObject ip = GameObject.Find("登录注册界面/UI/用户名/Text Area/Text");
//         Gamedata.Instance.Ip = ip.GetComponent<TMP_Text>().text;
//         Gamedata.Instance.Ip = Gamedata.Instance.Ip.Substring(0, Gamedata.Instance.Ip.Length - 1);
//         StartCoroutine(Demo(Gamedata.Instance.Ip));
//     }
//     IEnumerator Demo(string service)
//     {
//         yield return null;
//         string sqlAddress = "server=" + service + ";database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
//         SqlConnection sqlCon = new SqlConnection(sqlAddress);
//         try
//         {
//             sqlCon.Open();
//             if (sqlCon.State == ConnectionState.Open)
//             {
//                 Gamedata.Instance.datastate = 3;
//             }
//             else
//             {
//                 Gamedata.Instance.datastate = 1;
//             }
//         }
//         catch
//         {
//             Gamedata.Instance.datastate = 1;
//         }
//         finally
//         {
//             sqlCon.Close();
//         }
//         yield break;
//     }
//     public void exit()
//     {
// #if UNITY_EDITOR
//         UnityEditor.EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif
//     }
// }
