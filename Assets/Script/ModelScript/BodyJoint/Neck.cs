using CNCC.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Models
{
    public class Neck : MonoBehaviour, IJoint
    {
        ///个性化内容    Neck x 为 0 ；z 为 0
        //边界最大值
        float x_LimitMax_Max = 0;
        float x_LimitMax_Min = 0;
        float x_LimitMin_Max = -1;
        float x_LimitMin_Min = -1;
        float y_LimitMax_Max = 78;
        float y_LimitMax_Min = 45;
        float y_LimitMin_Max = -45;
        float y_LimitMin_Min = -78;
        float z_LimitMax_Max = 0;
        float z_LimitMax_Min = 0;
        float z_LimitMin_Max = -1;
        float z_LimitMin_Min = -1;
        //初始边界范围    
        float x_max = 0;
        float x_min = -1;
        float y_max = 63;
        float y_min = -63;
        float z_max = 0;
        float z_min = -1;

        float x_current;
        float y_current;
        float z_current;

        void Update()
        {
            // print("颈的y最大值: " + y_max);
        }

        public Vector3 GetCurrentAngle(GameObject model)
        {

            return model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).gameObject.transform.localEulerAngles;
        }

        public Vector3 GetMaxAngle()
        {
            return new Vector3(x_max, y_max, z_max);
        }

        public Vector3 GetMinAngle()
        {
            return new Vector3(x_min, y_min, z_min);
        }

        public void SetCurrentAngle(GameObject model, Vector3 angle)
        {
            //model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angle.z));  //new Vector3(angle.x,angle.y,angle.z)  ;
            //print(model.GetComponent<Model>().Name + "Neck :Vector3为 " + angle);
            model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angle.z));  //new Vector3(angle.x,angle.y,angle.z)  ;
            SetCurrentAngle(angle);
        }

        public void SetCurrentAngle(GameObject model, float x, float y, float z)
        {
            (x, y, z) = CheckAngleValue(x, y, z);
            model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).localEulerAngles = new Vector3(x, y, z);
            SetCurrentAngle(x, y, z);
        }

        public void SetMaxAngle(GameObject model, Vector3 MaxAngle)
        {
            if (MaxAngle.x > x_LimitMax_Max)
            {
                x_max = x_LimitMax_Max;
            }
            else if (MaxAngle.x < x_LimitMax_Min)
            {
                x_max = x_LimitMax_Min;
            }
            else
            {
                x_max = MaxAngle.x;
            }
            //y
            if (MaxAngle.y > y_LimitMax_Max)
            {
                y_max = y_LimitMax_Max;
            }
            else if (MaxAngle.y < y_LimitMax_Min)
            {
                y_max = y_LimitMax_Min;
            }
            else
            {
                y_max = MaxAngle.y;
            }
            //z
            if (MaxAngle.z > z_LimitMax_Max)
            {
                z_max = z_LimitMax_Max;
            }
            else if (MaxAngle.z < z_LimitMax_Min)
            {
                z_max = z_LimitMax_Min;
            }
            else
            {
                z_max = MaxAngle.z;
            }
            // model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).gameObject.transform.localEulerAngles = angle;// new Vector3(angle.x, angle.y, angle.z);
        }

        public void SetMinAngle(GameObject model, Vector3 MinAngle)
        {
            // model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Neck).gameObject.transform.localEulerAngles = angle;//new Vector3(angle.x, angle.y, angle.z);
            if (MinAngle.x > x_LimitMin_Max)
            {
                x_min = x_LimitMin_Max;
            }
            else if (MinAngle.x < x_LimitMin_Min)
            {
                x_min = x_LimitMin_Min;
            }
            else
            {
                x_min = MinAngle.x;
            }
            //y
            if (MinAngle.y > y_LimitMin_Max)
            {
                y_min = y_LimitMin_Max;
            }
            else if (MinAngle.y < y_LimitMin_Min)
            {
                y_min = y_LimitMin_Min;
            }
            else
            {
                y_min = MinAngle.y;
            }
            //z
            if (MinAngle.z > z_LimitMin_Max)
            {
                z_min = z_LimitMin_Max;
            }
            else if (MinAngle.z < z_LimitMin_Min)
            {
                z_min = z_LimitMin_Min;
            }
            else
            {
                z_min = MinAngle.z;
            }
        }

        public void SetCurrentAngle(Vector3 angle)
        {
            x_current = angle.x;
            y_current = angle.y;
            z_current = angle.z;
        }

        public void SetCurrentAngle(float x, float y, float z)
        {
            x_current = x;
            y_current = y;
            z_current = z;
        }
        public Vector3 GetCurrentAngle()
        {
            return new Vector3(x_current, y_current, z_current);
        }

        private (float x, float y, float z) CheckAngleValue(float a, float b, float c)
        {
            float x = a % 180;
            if (x > x_max)
            {
                x = x_max;
            }
            else if (x < x_min)
            {
                x = x_min;
            }

            float y = b % 180;
            if (y > y_max)
            {
                y = y_max;
            }
            else if (y < y_min)
            {
                y = y_min;
            }

            float z = c % 180;
            if (z > z_max)
            {
                z = z_max;
            }
            else if (z < z_min)
            {
                z = z_min;
            }
            return (x, y, z);
        }
    }
}
