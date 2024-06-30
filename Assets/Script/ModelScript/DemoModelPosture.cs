using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CNCC.Models;

public class DemoModelPosture : MonoBehaviour
{
    [SerializeField] GameObject _currentHuman;
    GameObject Head_M;


    GameObject Neck_M;

    GameObject Scapula_L;
    GameObject Scapula_R1;


    GameObject Shoulder_L;
    GameObject Shoulder_R;



    GameObject Elbow_L;
    GameObject Elbow_R;



    GameObject Wrist_L;
    GameObject Wrist_R;



    GameObject Spine1_M;

    GameObject Chest_M;


    GameObject Hip_L;
    GameObject Hip_R;



    GameObject Knee_L;
    GameObject Knee_R;


    GameObject Ankle_L;
    GameObject Ankle_R;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDown()
    {
        if (Model.createdModel.Count !=0)
        {
            return;
        }
        SelectActionHUman();
        PostureAngleDesign_SitDown();
        _currentHuman.transform.position = new Vector3(12.441f, -0.392f, 4.033f);
    }
    public void TPose()
    {
        if (Model.createdModel.Count != 0)
        {
            return;
        }
        SelectActionHUman();
        PostureAngleDesign_TPose();
        _currentHuman.transform.position = new Vector3(12.389f, 0, 2.721f);
    }
    public void Stand()
    {
        if (Model.createdModel.Count != 0)
        {
            return;
        }
        SelectActionHUman();
        PostureAngleDesign_Stand();
        _currentHuman.transform.position = new Vector3(12.389f, 0, 2.721f);
    }

    void SelectActionHUman()
    {
        //Model = FindChildGameObjectByName(_currentHuman, Model);
        Head_M = FindChildGameObjectByName(_currentHuman, "Head_M");
        Neck_M = FindChildGameObjectByName(_currentHuman, "Neck_M");
        Scapula_L = FindChildGameObjectByName(_currentHuman, "Scapula_L");
        Scapula_R1 = FindChildGameObjectByName(_currentHuman, "Scapula_R1");
        Shoulder_L = FindChildGameObjectByName(_currentHuman, "Shoulder_L");
        Shoulder_R = FindChildGameObjectByName(_currentHuman, "Shoulder_R");
        Elbow_L = FindChildGameObjectByName(_currentHuman, "Elbow_L");
        Elbow_R = FindChildGameObjectByName(_currentHuman, "Elbow_R");

        Wrist_L = FindChildGameObjectByName(_currentHuman, "Wrist_L");
        Wrist_R = FindChildGameObjectByName(_currentHuman, "Wrist_R");
        Spine1_M = FindChildGameObjectByName(_currentHuman, "Spine1_M");
        Chest_M = FindChildGameObjectByName(_currentHuman, "Chest_M");

        Hip_L = FindChildGameObjectByName(_currentHuman, "Hip_L");
        Hip_R = FindChildGameObjectByName(_currentHuman, "Hip_R");

        Knee_L = FindChildGameObjectByName(_currentHuman, "Knee_L");
        Knee_R = FindChildGameObjectByName(_currentHuman, "Knee_R");

        Ankle_L = FindChildGameObjectByName(_currentHuman, "Ankle_L");
        Ankle_R = FindChildGameObjectByName(_currentHuman, "Ankle_R");
    }
    void PostureAngleDesign_SitDown()
    {
        //mdeol
        //Model.transform.localRotation = new Quaternion(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation,0);
        //Model.transform.rotation = Quaternion.Euler(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);//new Vector3(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);
        //Model.transform.localEulerAngles = new Vector3(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);
        //头
        Head_M.transform.localEulerAngles = new Vector3(0,0,0);

        //颈
        Neck_M.transform.localEulerAngles = new Vector3(15,0,0);
        //胸椎
        Chest_M.transform.localEulerAngles = new Vector3(0,0,0);

        //左肩
        Shoulder_L.transform.localEulerAngles = new Vector3(-35,10,75);

        //右肩
        Shoulder_R.transform.localEulerAngles = new Vector3(-35,-10,-75);

        //左肘
        Elbow_L.transform.localEulerAngles = new Vector3(75,55,0);

        //右肘
        Elbow_R.transform.localEulerAngles = new Vector3(75,-55,0);

        //左腕
        Wrist_L.transform.localEulerAngles = new Vector3(0,0,0);

        //右腕
        Wrist_R.transform.localEulerAngles = new Vector3(0,0,0);

        //脊椎
        Spine1_M.transform.localEulerAngles = new Vector3(5,0,0);

        //左大腿
        Hip_L.transform.localEulerAngles = new Vector3(-90,0,0);

        //右大腿
        Hip_R.transform.localEulerAngles = new Vector3(-90,0,0);

        //左膝盖
        Knee_L.transform.localEulerAngles = new Vector3(85,0,0);

        //右膝盖
        Knee_R.transform.localEulerAngles = new Vector3(85,0,0);

        //左脚踝
        Ankle_L.transform.localEulerAngles = new Vector3(0,0,0);

        //右脚踝
        Ankle_R.transform.localEulerAngles = new Vector3(0,0,0);
    }
    GameObject FindChildGameObjectByName(GameObject currentGamobject, string gameObjectName)
    {

        for (int i = 0; i < currentGamobject.transform.childCount; i++)
        {
            if (currentGamobject.transform.GetChild(i).name == gameObjectName)
            {
                return currentGamobject.transform.GetChild(i).gameObject;
            }

            GameObject tmp = FindChildGameObjectByName(currentGamobject.transform.GetChild(i).gameObject, gameObjectName);
            if (tmp != null)
            {
                return tmp;
            }
        }
        return null;
    }
    GameObject FindChildGameObjectByName(GameObject currentGamobject, GameObject gameObject)
    {
        for (int i = 0; i < currentGamobject.transform.childCount; i++)
        {
            if (currentGamobject.transform.GetChild(i).name == gameObject.name)
            {
                return currentGamobject.transform.GetChild(i).gameObject;
            }

            GameObject tmp = FindChildGameObjectByName(currentGamobject.transform.GetChild(i).gameObject, gameObject);
            if (tmp != null)
            {
                return tmp;
            }
        }
        return null;
    }
    void PostureAngleDesign_TPose()
    {
        //头
        Head_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //颈
        Neck_M.transform.localEulerAngles = new Vector3(0, 0, 0);
        //胸椎
        Chest_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左肩
        Shoulder_L.transform.localEulerAngles = new Vector3(0,0,0);

        //右肩
        Shoulder_R.transform.localEulerAngles = new Vector3(0,0,0);

        //左肘
        Elbow_L.transform.localEulerAngles = new Vector3(0,0,0);

        //右肘
        Elbow_R.transform.localEulerAngles = new Vector3(0,0,0);

        //左腕
        Wrist_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右腕
        Wrist_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //脊椎
        Spine1_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左大腿
        Hip_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右大腿
        Hip_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左膝盖
        Knee_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右膝盖
        Knee_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左脚踝
        Ankle_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右脚踝
        Ankle_R.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void PostureAngleDesign_Stand()
    {
        //头
        Head_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //颈
        Neck_M.transform.localEulerAngles = new Vector3(0, 0, 0);
        //胸椎
        Chest_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左肩
        Shoulder_L.transform.localEulerAngles = new Vector3(0, 0, 85);

        //右肩
        Shoulder_R.transform.localEulerAngles = new Vector3(0, 0, -85);

        //左肘
        Elbow_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右肘
        Elbow_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左腕
        Wrist_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右腕
        Wrist_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //脊椎
        Spine1_M.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左大腿
        Hip_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右大腿
        Hip_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左膝盖
        Knee_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右膝盖
        Knee_R.transform.localEulerAngles = new Vector3(0, 0, 0);

        //左脚踝
        Ankle_L.transform.localEulerAngles = new Vector3(0, 0, 0);

        //右脚踝
        Ankle_R.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
