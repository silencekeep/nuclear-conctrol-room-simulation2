using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Models
{
    public class Knee_L : MonoBehaviour, IJoint
    {
        ///个性化内容
        //初始边界范围
        static float x_max = 130;
        static float x_min = 0;
        static float y_max = 0;
        static float y_min = -1;
        static float z_max = 0;
        static float z_min = -1;
        //边界最大值
        float x_LimitMax_Max = 155;
        float x_LimitMax_Min = 105;
        float x_LimitMin_Max = 5;
        float x_LimitMin_Min = -5;
        float y_LimitMax_Max = 0;
        float y_LimitMax_Min = 0;
        float y_LimitMin_Max = -1;
        float y_LimitMin_Min = -1;
        float z_LimitMax_Max = 0;
        float z_LimitMax_Min = 0;
        float z_LimitMin_Max = -1;
        float z_LimitMin_Min = -1;

        float x_current;
        float y_current;
        float z_current;
        //该脚本对应的关节
        public Vector3 GetCurrentAngle(GameObject model)
        {
            return model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerLeg).gameObject.transform.localEulerAngles;
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
            model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerLeg).gameObject.transform.localEulerAngles = angle;
            SetCurrentAngle(angle);
        }


        public void SetMaxAngle(GameObject model, Vector3 MaxAngle)
        {
            //x
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
        }

        public void SetMinAngle(GameObject model, Vector3 MinAngle)
        {
            //x
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

        public void SetCurrentAngle(float x, float y, float z)
        {
            x_current = x;
            y_current = y;
            z_current = z;
        }

        public void SetCurrentAngle(Vector3 angle)
        {
            x_current = angle.x;
            y_current = angle.y;
            z_current = angle.z;
        }

        public Vector3 GetCurrentAngle()
        {
            return new Vector3(x_current, y_current, z_current);
        }

        public void SetCurrentAngle(GameObject model, float x, float y, float z)
        {
(x, y, z) = CheckAngleValue(x, y, z);
            model.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerLeg).localEulerAngles = new Vector3(x, y, z);
            SetCurrentAngle(x, y, z);
        }
private (float x, float y, float z) CheckAngleValue(float a, float b, float c)
        {
            float x = a % 360;
            if (x > x_max)
            {
                x = x_max;
            }
            else if (x < x_min)
            {
                x = x_min;
            }

            float y = b % 360;
            if (y > y_max)
            {
                y = y_max;
            }
            else if (y < y_min)
            {
                y = y_min;
            }

            float z = c % 360;
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
