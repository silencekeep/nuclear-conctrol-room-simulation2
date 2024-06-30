using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CNCC.Models
{

    public class PostureDB : MonoBehaviour
    {
        public static bool humanStand = true;//վ��
        #region ��������
        //����������ת
        public static float x_Rotation;
        public static float y_Rotation;
        public static float z_Rotation;
        //ͷ
        public static float x_HeadAngle;
        public static float y_HeadAngle;
        public static float z_HeadAngle;
        //����
        public static float x_NeckAngle;
        public static float y_NeckAngle;
        public static float z_NeckAngle;
        //���
        public static float x_Shoulder_L_Angle;
        public static float y_Shoulder_L_Angle;
        public static float z_Shoulder_L_Angle;
        //�Ҽ�
        public static float x_Shoulder_R_Angle;
        public static float y_Shoulder_R_Angle;
        public static float z_Shoulder_R_Angle;
        //����ι�
        public static float x_Scapula_L_Angle;
        public static float y_Scapula_L_Angle;
        public static float z_Scapula_L_Angle;
        //�Ҽ��ι�
        public static float x_Scapula_R_Angle;
        public static float y_Scapula_R_Angle;
        public static float z_Scapula_R_Angle;
        //����
        public static float x_Elbow_L_Angle;
        public static float y_Elbow_L_Angle;
        public static float z_Elbow_L_Angle;
        //����
        public static float x_Elbow_R_Angle;
        public static float y_Elbow_R_Angle;
        public static float z_Elbow_R_Angle;
        //����
        public static float x_Wrist_L_Angle;
        public static float y_Wrist_L_Angle;
        public static float z_Wrist_L_Angle;
        //����
        public static float x_Wrist_R_Angle;
        public static float y_Wrist_R_Angle;
        public static float z_Wrist_R_Angle;
        //��׵
        public static float x_Chest_M_Angle;
        public static float y_Chest_M_Angle;
        public static float z_Chest_M_Angle;
        //��׵
        public static float x_Spine1_M_Angle;
        public static float y_Spine1_M_Angle;
        public static float z_Spine1_M_Angle;
        //�����
        public static float x_Hip_L_Angle;
        public static float y_Hip_L_Angle;
        public static float z_Hip_L_Angle;
        //�Ҵ���
        public static float x_Hip_R_Angle;
        public static float y_Hip_R_Angle;
        public static float z_Hip_R_Angle;
        //��ϥ��
        public static float x_Knee_L_Angle;
        public static float y_Knee_L_Angle;
        public static float z_Knee_L_Angle;
        //��ϥ��
        public static float x_Knee_R_Angle;
        public static float y_Knee_R_Angle;
        public static float z_Knee_R_Angle;
        //�����
        public static float x_Ankle_L_Angle;
        public static float y_Ankle_L_Angle;
        public static float z_Ankle_L_Angle;
        //�ҽ���
        public static float x_Ankle_R_Angle;
        public static float y_Ankle_R_Angle;
        public static float z_Ankle_R_Angle;
        //��ÿһ���ű������ж�

        //ģ��1   
        public static bool IsHead_Model1 = false;
        public static bool IsNeck_Model1 = false;
        public static bool IsClavicle_L_Model1 = false;
        public static bool IsClavicle_R_Model1 = false;
        public static bool IsShoulder_L_Model1 = false;
        public static bool IsShoulder_R_Model1 = false;
        public static bool IsElbow_L_Model1 = false;
        public static bool IsElbow_R_Model1 = false;
        public static bool IsWrist_L_Model1 = false;
        public static bool IsWrist_R_Model1 = false;
        public static bool IsSpine1_M_Model1 = false;
        public static bool IsChest_M_Model1 = false;
        public static bool IsHip_L_Model1 = false;
        public static bool IsHip_R_Model1 = false;
        public static bool IsKnee_L_Model1 = false;
        public static bool IsKnee_R_Model1 = false;
        public static bool IsAnkle_L_Model1 = false;
        public static bool IsAnkle_R_Model1 = false;

        //ģ��2
        public static bool IsHead_Model2 = false;
        public static bool IsNeck_Model2 = false;
        public static bool IsClavicle_L_Model2 = false;
        public static bool IsClavicle_R_Model2 = false;
        public static bool IsShoulder_L_Model2 = false;
        public static bool IsShoulder_R_Model2 = false;
        public static bool IsElbow_L_Model2 = false;
        public static bool IsElbow_R_Model2 = false;
        public static bool IsWrist_L_Model2 = false;
        public static bool IsWrist_R_Model2 = false;
        public static bool IsSpine1_M_Model2 = false;
        public static bool IsChest_M_Model2 = false;
        public static bool IsHip_L_Model2 = false;
        public static bool IsHip_R_Model2 = false;
        public static bool IsKnee_L_Model2 = false;
        public static bool IsKnee_R_Model2 = false;
        public static bool IsAnkle_L_Model2 = false;
        public static bool IsAnkle_R_Model2 = false;

        //ģ��3
        public static bool IsHead_Model3 = false;
        public static bool IsNeck_Model3 = false;
        public static bool IsClavicle_L_Model3 = false;
        public static bool IsClavicle_R_Model3 = false;
        public static bool IsShoulder_L_Model3 = false;
        public static bool IsShoulder_R_Model3 = false;
        public static bool IsElbow_L_Model3 = false;
        public static bool IsElbow_R_Model3 = false;
        public static bool IsWrist_L_Model3 = false;
        public static bool IsWrist_R_Model3 = false;
        public static bool IsSpine1_M_Model3 = false;
        public static bool IsChest_M_Model3 = false;
        public static bool IsHip_L_Model3 = false;
        public static bool IsHip_R_Model3 = false;
        public static bool IsKnee_L_Model3 = false;
        public static bool IsKnee_R_Model3 = false;
        public static bool IsAnkle_L_Model3 = false;
        public static bool IsAnkle_R_Model3 = false;

        static Vector3[] vector3s = new Vector3[16];

        #endregion
        // Start is called before the first frame update
        void Start()
        {
            //vector3s = new Vector3[16];
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static Dictionary<HumanBodyBones, Vector3> PostureJointAngle(Vector3[] vector3s)
        {
            Dictionary<HumanBodyBones, Vector3> postureJointDic = new Dictionary<HumanBodyBones, Vector3>();
            postureJointDic.Add(HumanBodyBones.Head, vector3s[0]);
            postureJointDic.Add(HumanBodyBones.Neck, vector3s[1]);
            postureJointDic.Add(HumanBodyBones.Chest, vector3s[2]);
            postureJointDic.Add(HumanBodyBones.LeftUpperArm, vector3s[3]);
            postureJointDic.Add(HumanBodyBones.RightUpperArm, vector3s[4]);
            postureJointDic.Add(HumanBodyBones.LeftLowerArm, vector3s[5]);
            postureJointDic.Add(HumanBodyBones.RightLowerArm, vector3s[6]);
            postureJointDic.Add(HumanBodyBones.LeftHand, vector3s[7]);
            postureJointDic.Add(HumanBodyBones.RightHand, vector3s[8]);
            postureJointDic.Add(HumanBodyBones.Spine, vector3s[9]);
            postureJointDic.Add(HumanBodyBones.LeftUpperLeg, vector3s[10]);
            postureJointDic.Add(HumanBodyBones.RightUpperLeg, vector3s[11]);
            postureJointDic.Add(HumanBodyBones.LeftLowerLeg, vector3s[12]);
            postureJointDic.Add(HumanBodyBones.RightLowerLeg, vector3s[13]);
            postureJointDic.Add(HumanBodyBones.LeftFoot, vector3s[14]);
            postureJointDic.Add(HumanBodyBones.RightFoot, vector3s[15]);
            return postureJointDic;
        }
        public static Vector3[] TPose()
        {
            //ͷ (x ,y ,z)
            x_HeadAngle = 0;
            y_HeadAngle = 0;
            z_HeadAngle = 0;
            vector3s[0] = new Vector3(x_HeadAngle, y_HeadAngle, z_HeadAngle);

            //�� (x , y, z)
            x_NeckAngle = 0;
            y_NeckAngle = 0;
            z_NeckAngle = 0;
            vector3s[1] = new Vector3(x_NeckAngle, y_NeckAngle, z_NeckAngle);

            //��׵ (x, y, z)
            x_Chest_M_Angle = 0;
            y_Chest_M_Angle = 0;
            z_Chest_M_Angle = 0;
            vector3s[2] = new Vector3(x_Chest_M_Angle, y_Chest_M_Angle, z_Chest_M_Angle);

            //��� (x, y, z)
            x_Shoulder_L_Angle = 0;
            y_Shoulder_L_Angle = 0;
            z_Shoulder_L_Angle = 0;
            vector3s[3] = new Vector3(x_Shoulder_L_Angle, y_Shoulder_L_Angle, z_Shoulder_L_Angle);

            //�Ҽ� (x, y, z)
            x_Shoulder_R_Angle = 0;
            y_Shoulder_R_Angle = 0;
            z_Shoulder_R_Angle = 0;
            vector3s[4] = new Vector3(x_Shoulder_R_Angle, y_Shoulder_R_Angle, z_Shoulder_R_Angle);

            //����
            x_Elbow_L_Angle = 0;
            y_Elbow_L_Angle = 0;
            z_Elbow_L_Angle = 0;
            vector3s[5] = new Vector3(x_Elbow_L_Angle, y_Elbow_L_Angle, z_Elbow_L_Angle);

            //����
            x_Elbow_R_Angle = 0;
            y_Elbow_R_Angle = 0;
            z_Elbow_R_Angle = 0;
            vector3s[6] = new Vector3(x_Elbow_R_Angle, y_Elbow_R_Angle, z_Elbow_R_Angle);

            //����
            x_Wrist_L_Angle = 0;
            y_Wrist_L_Angle = 0;
            z_Wrist_L_Angle = 0;
            vector3s[7] = new Vector3(x_Wrist_L_Angle, y_Wrist_L_Angle, z_Wrist_L_Angle);

            //����
            x_Wrist_R_Angle = 0;
            y_Wrist_R_Angle = 0;
            z_Wrist_R_Angle = 0;
            vector3s[8] = new Vector3(x_Wrist_R_Angle, y_Wrist_R_Angle, z_Wrist_R_Angle);

            //��׵
            x_Spine1_M_Angle = 0;
            y_Spine1_M_Angle = 0;
            z_Spine1_M_Angle = 0;
            vector3s[9] = new Vector3(x_Spine1_M_Angle, y_Spine1_M_Angle, z_Spine1_M_Angle);

            //�����
            x_Hip_L_Angle = 0;
            y_Hip_L_Angle = 0;
            z_Hip_L_Angle = 0;
            vector3s[10] = new Vector3(x_Hip_L_Angle, y_Hip_L_Angle, z_Hip_L_Angle);

            //�Ҵ���
            x_Hip_R_Angle = 0;
            y_Hip_R_Angle = 0;
            z_Hip_R_Angle = 0;
            vector3s[11] = new Vector3(x_Hip_R_Angle, y_Hip_R_Angle, z_Hip_R_Angle);

            //��ϥ��
            x_Knee_L_Angle = 0;
            y_Knee_L_Angle = 0;
            z_Knee_L_Angle = 0;
            vector3s[12] = new Vector3(x_Knee_L_Angle, y_Knee_L_Angle, z_Knee_L_Angle);

            //��ϥ��
            x_Knee_R_Angle = 0;
            y_Knee_R_Angle = 0;
            z_Knee_R_Angle = 0;
            vector3s[13] = new Vector3(x_Knee_R_Angle, y_Knee_R_Angle, z_Knee_R_Angle);

            //�����
            x_Ankle_L_Angle = 0;
            y_Ankle_L_Angle = 0;
            z_Ankle_L_Angle = 0;
            vector3s[14] = new Vector3(x_Ankle_L_Angle, y_Ankle_L_Angle, z_Ankle_L_Angle);

            //�ҽ���
            x_Ankle_R_Angle = 0;
            y_Ankle_R_Angle = 0;
            z_Ankle_R_Angle = 0;
            vector3s[15] = new Vector3(x_Ankle_R_Angle, y_Ankle_R_Angle, z_Ankle_R_Angle);

            return vector3s;
        }

        /// <summary>
        /// վ��
        /// </summary>
        public static Vector3[] ZhanLi()
        {
            //
            humanStand = true;

            //ͷ
            x_HeadAngle = 0;
            y_HeadAngle = 0;
            z_HeadAngle = 0;
            vector3s[0] = new Vector3(x_HeadAngle, y_HeadAngle, z_HeadAngle);

            //����
            x_NeckAngle = 0;
            y_NeckAngle = 0;
            z_NeckAngle = 0;
            vector3s[1] = new Vector3(x_NeckAngle, y_NeckAngle, z_NeckAngle);

            //�ع�
            x_Chest_M_Angle = 0;
            y_Chest_M_Angle = 0;
            z_Chest_M_Angle = 0;
            vector3s[2] = new Vector3(x_Chest_M_Angle, y_Chest_M_Angle, z_Chest_M_Angle);

            //���
            x_Shoulder_L_Angle = 0;
            y_Shoulder_L_Angle = 0;
            z_Shoulder_L_Angle = 85;
            vector3s[3] = new Vector3(x_Shoulder_L_Angle, y_Shoulder_L_Angle, z_Shoulder_L_Angle);

            //�Ҽ�
            x_Shoulder_R_Angle = 0;
            y_Shoulder_R_Angle = 0;
            z_Shoulder_R_Angle = -85;
            vector3s[4] = new Vector3(x_Shoulder_R_Angle, y_Shoulder_R_Angle, z_Shoulder_R_Angle);
            
            //����
            x_Elbow_L_Angle = 0;
            y_Elbow_L_Angle = 0;
            z_Elbow_L_Angle = 0;
            vector3s[5] = new Vector3(x_Elbow_L_Angle, y_Elbow_L_Angle, z_Elbow_L_Angle);

            //����
            x_Elbow_R_Angle = 0;
            y_Elbow_R_Angle = 0;
            z_Elbow_R_Angle = 0;
            vector3s[6] = new Vector3(x_Elbow_R_Angle, y_Elbow_R_Angle, z_Elbow_R_Angle);

            //����
            x_Wrist_L_Angle = 0;
            y_Wrist_L_Angle = 0;
            z_Wrist_L_Angle = 0;
            vector3s[7] = new Vector3(x_Wrist_L_Angle, y_Wrist_L_Angle, z_Wrist_L_Angle);

            //����
            x_Wrist_R_Angle = 0;
            y_Wrist_R_Angle = 0;
            z_Wrist_R_Angle = 0;
            vector3s[8] = new Vector3(x_Wrist_R_Angle, y_Wrist_R_Angle, z_Wrist_R_Angle);

            //��׵
            x_Spine1_M_Angle = 0;
            y_Spine1_M_Angle = 0;
            z_Spine1_M_Angle = 0;
            vector3s[9] = new Vector3(x_Spine1_M_Angle, y_Spine1_M_Angle, z_Spine1_M_Angle);

            //�����
            x_Hip_L_Angle = 0;
            y_Hip_L_Angle = 0;
            z_Hip_L_Angle = 2;
            vector3s[10] = new Vector3(x_Hip_L_Angle, y_Hip_L_Angle, z_Hip_L_Angle);

            //�Ҵ���
            x_Hip_R_Angle = 0;
            y_Hip_R_Angle = 0;
            z_Hip_R_Angle = -2;
            vector3s[11] = new Vector3(x_Hip_R_Angle, y_Hip_R_Angle, z_Hip_R_Angle);

            //��ϥ��
            x_Knee_L_Angle = 0;
            y_Knee_L_Angle = 0;
            z_Knee_L_Angle = 0;
            vector3s[12] = new Vector3(x_Knee_L_Angle, y_Knee_L_Angle, z_Knee_L_Angle);

            //��ϥ��
            x_Knee_R_Angle = 0;
            y_Knee_R_Angle = 0;
            z_Knee_R_Angle = 0;
            vector3s[13] = new Vector3(x_Knee_R_Angle, y_Knee_R_Angle, z_Knee_R_Angle);

            //�����
            x_Ankle_L_Angle = 0;
            y_Ankle_L_Angle = 0;
            z_Ankle_L_Angle = 0;
            vector3s[14] = new Vector3(x_Ankle_L_Angle, y_Ankle_L_Angle, z_Ankle_L_Angle);

            //�ҽ���
            x_Ankle_R_Angle = 0;
            y_Ankle_R_Angle = 0;
            z_Ankle_R_Angle = 0;
            vector3s[15] = new Vector3(x_Ankle_R_Angle, y_Ankle_R_Angle, z_Ankle_R_Angle);

            return vector3s;
        }

        public static Vector3[] SitDown()
        {
            humanStand = false;

            //ͷ
            x_HeadAngle = 0;
            y_HeadAngle = 0;
            z_HeadAngle = 0;
            vector3s[0] = new Vector3(x_HeadAngle, y_HeadAngle, z_HeadAngle);

            //����
            x_NeckAngle = 15;
            y_NeckAngle = 0;
            z_NeckAngle = 0;
            vector3s[1] = new Vector3(x_NeckAngle, y_NeckAngle, z_NeckAngle);

            //�ع�
            x_Chest_M_Angle = 0;
            y_Chest_M_Angle = 0;
            z_Chest_M_Angle = 0;
            vector3s[2] = new Vector3(x_Chest_M_Angle, y_Chest_M_Angle, z_Chest_M_Angle);

            //���
            x_Shoulder_L_Angle = -65;
            y_Shoulder_L_Angle = 10;
            z_Shoulder_L_Angle = 75;
            vector3s[3] = new Vector3(x_Shoulder_L_Angle, y_Shoulder_L_Angle, z_Shoulder_L_Angle);

            //�Ҽ�
            x_Shoulder_R_Angle = -65;
            y_Shoulder_R_Angle = -10;
            z_Shoulder_R_Angle = -75;
            vector3s[4] = new Vector3(x_Shoulder_R_Angle, y_Shoulder_R_Angle, z_Shoulder_R_Angle);

            
            //����
            x_Elbow_L_Angle = 75;
            y_Elbow_L_Angle = 30;
            z_Elbow_L_Angle = 0;
            vector3s[5] = new Vector3(x_Elbow_L_Angle, y_Elbow_L_Angle, z_Elbow_L_Angle);

            //����
            x_Elbow_R_Angle = 75;
            y_Elbow_R_Angle = -30;
            z_Elbow_R_Angle = 0;
            vector3s[6] = new Vector3(x_Elbow_R_Angle, y_Elbow_R_Angle, z_Elbow_R_Angle);

            //����
            x_Wrist_L_Angle = 0;
            y_Wrist_L_Angle = 0;
            z_Wrist_L_Angle = 0;
            vector3s[7] = new Vector3(x_Wrist_L_Angle, y_Wrist_L_Angle, z_Wrist_L_Angle);

            //����
            x_Wrist_R_Angle = 0;
            y_Wrist_R_Angle = 0;
            z_Wrist_R_Angle = 0;
            vector3s[8] = new Vector3(x_Wrist_R_Angle, y_Wrist_R_Angle, z_Wrist_R_Angle);

            //��׵
            x_Spine1_M_Angle = 5;
            y_Spine1_M_Angle = 0;
            z_Spine1_M_Angle = 0;
            vector3s[9] = new Vector3(x_Spine1_M_Angle, y_Spine1_M_Angle, z_Spine1_M_Angle);

            //�����
            x_Hip_L_Angle = -80;
            y_Hip_L_Angle = 0;
            z_Hip_L_Angle = 0;
            vector3s[10] = new Vector3(x_Hip_L_Angle, y_Hip_L_Angle, z_Hip_L_Angle);

            //�Ҵ���
            x_Hip_R_Angle = -80;
            y_Hip_R_Angle = 0;
            z_Hip_R_Angle = 0;
            vector3s[11] = new Vector3(x_Hip_R_Angle, y_Hip_R_Angle, z_Hip_R_Angle);

            //��ϥ��
            x_Knee_L_Angle = 85;
            y_Knee_L_Angle = 0;
            z_Knee_L_Angle = 0;
            vector3s[12] = new Vector3(x_Knee_L_Angle, y_Knee_L_Angle, z_Knee_L_Angle);

            //��ϥ��
            x_Knee_R_Angle = 85;
            y_Knee_R_Angle = 0;
            z_Knee_R_Angle = 0;
            vector3s[13] = new Vector3(x_Knee_R_Angle, y_Knee_R_Angle, z_Knee_R_Angle);

            //�����
            x_Ankle_L_Angle = 0;
            y_Ankle_L_Angle = 0;
            z_Ankle_L_Angle = 0;
            vector3s[14] = new Vector3(x_Ankle_L_Angle, y_Ankle_L_Angle, z_Ankle_L_Angle);

            //�ҽ���
            x_Ankle_R_Angle = 0;
            y_Ankle_R_Angle = 0;
            z_Ankle_R_Angle = 0;
            vector3s[15] = new Vector3(x_Ankle_R_Angle, y_Ankle_R_Angle, z_Ankle_R_Angle);
            return vector3s;
        }

        #region ���ֵ���ʱ�����µ�rubbish
        //��һ����ͻȻҪ��ĳ�����ƣ�����������
        /// <summary>
        /// ����
        /// </summary>
        //public void ChaYao()
        //{
        //    //ͷ
        //    x_HeadAngle = -20;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = -22;
        //    y_NeckAngle = -180;
        //    z_NeckAngle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 4;
        //    y_Shoulder_L_Angle = -40;
        //    z_Shoulder_L_Angle = 4;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = -4;
        //    y_Shoulder_R_Angle = -40;
        //    z_Shoulder_R_Angle = -4;
        //    //����ι�
        //    x_Scapula_L_Angle= -79;
        //    y_Scapula_L_Angle = -36;
        //    z_Scapula_L_Angle = 31;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = -79;
        //    y_Scapula_R_Angle = 36;
        //    z_Scapula_R_Angle = 149;
        //    //����
        //    x_Elbow_L_Angle = 12;
        //    y_Elbow_L_Angle = 3;
        //    z_Elbow_L_Angle = 12;
        //    //����
        //    x_Elbow_R_Angle = -12;
        //    y_Elbow_R_Angle = 3;
        //    z_Elbow_R_Angle = -12;
        //    //����
        //    x_Wrist_L_Angle = 83;
        //    y_Wrist_L_Angle = -45;
        //    z_Wrist_L_Angle = -43;
        //    //����
        //    x_Wrist_R_Angle = 96;
        //    y_Wrist_R_Angle = -45;
        //    z_Wrist_R_Angle = -48;
        //    //��׵
        //    x_Spine1_M_Angle = 3;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 5;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 80;
        //    //�Ҵ���
        //    x_Hip_R_Angle = -5;
        //    y_Hip_R_Angle = 180;
        //    z_Hip_R_Angle = 81;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = -17;
        //    y_Ankle_L_Angle = 61;
        //    z_Ankle_L_Angle = -11;
        //    //�ҽ���
        //    x_Ankle_R_Angle = -163;
        //    y_Ankle_R_Angle = -61;
        //    z_Ankle_R_Angle = 11;
        //}
        /// <summary>
        /// ��ʻ��
        /// </summary>
        //public void Driving()
        //{
        //    //����
        //    x_Rotation = -5;
        //    y_Rotation = 220;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = -10;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 10;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = -80;
        //    y_Shoulder_L_Angle = 80;
        //    z_Shoulder_L_Angle = 0;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = -80;
        //    y_Shoulder_R_Angle = -80;
        //    z_Shoulder_R_Angle = 0;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 20;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = -20;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 20;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = -100;
        //    y_Hip_L_Angle = -10;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = -100;
        //    y_Hip_R_Angle = 10;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 75;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 75;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 10;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 10;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;
        //}
        /// <summary>
        /// T����
        /// </summary>
        //public void TPose()
        //{       
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 0;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = 0;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;
        //}
        /// <summary>
        /// ��ǹվ��1
        /// </summary>
        // public void ChiQiangZhanZi1()
        // {
        //     //��������
        //     x_Rotation = 0;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = 0;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 0;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = 0;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = 0;
        //     y_Shoulder_L_Angle = 0;
        //     z_Shoulder_L_Angle = 90;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = 0;
        //     y_Shoulder_R_Angle = 0;
        //     z_Shoulder_R_Angle = -82;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = 0;
        //     y_Elbow_L_Angle = 0;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = 0;
        //     y_Elbow_R_Angle = 0;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 0;
        //     z_Wrist_L_Angle = 0;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = 6;
        //     z_Wrist_R_Angle = 0;
        //     //��׵
        //     x_Spine1_M_Angle = 0;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = 0;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = 0;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 0;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 0;
        //     //��ϥ��
        //     x_Knee_L_Angle = 0;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 0;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 0;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 0;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }
        ///// <summary>
        ///// ��ǹվ��2
        ///// </summary>
        // public void ChiQiangZhanZi2()
        // {
        //     //��������
        //     x_Rotation = 0;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = 0;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 0;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = 0;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = 0;
        //     y_Shoulder_L_Angle = 0;
        //     z_Shoulder_L_Angle = 90;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = -54;
        //     y_Shoulder_R_Angle = 4;
        //     z_Shoulder_R_Angle = -53;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = 0;
        //     y_Elbow_L_Angle = 0;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = -18;
        //     y_Elbow_R_Angle = -111;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 0;
        //     z_Wrist_L_Angle = 0;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = -12;
        //     z_Wrist_R_Angle = 13;
        //     //��׵
        //     x_Spine1_M_Angle = 0;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = 0;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = 0;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 0;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 0;
        //     //��ϥ��
        //     x_Knee_L_Angle = 0;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 0;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 0;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 0;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }
        // /// <summary>
        // /// ˫�ֳ�ǹվ��
        // /// </summary>
        // public void ChiQiangZhanzi_ShuangShou()
        // {
        //     //��������
        //     x_Rotation = 0;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = 0;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 0;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = 0;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = 10;
        //     y_Shoulder_L_Angle = -19;
        //     z_Shoulder_L_Angle = 64;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = -3;
        //     y_Shoulder_R_Angle = -57;
        //     z_Shoulder_R_Angle = -53;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = -98;
        //     y_Elbow_L_Angle = 141;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = -41;
        //     y_Elbow_R_Angle = -120;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 14;
        //     z_Wrist_L_Angle = 17;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = 19;
        //     z_Wrist_R_Angle = -16;
        //     //��׵
        //     x_Spine1_M_Angle = 0;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = 0;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = 0;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 0;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 0;
        //     //��ϥ��
        //     x_Knee_L_Angle = 0;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 0;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 0;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 0;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }
        // /// <summary>
        // /// վ�����
        // /// </summary>
        // public void ZhanZiSheJi()
        // {
        //     //��������
        //     x_Rotation = 0;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = -18;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 0;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = 0;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = -73;
        //     y_Shoulder_L_Angle = 64;
        //     z_Shoulder_L_Angle = 36;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = -43;
        //     y_Shoulder_R_Angle = -55;
        //     z_Shoulder_R_Angle = -45;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = 0;
        //     y_Elbow_L_Angle = 28;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = -19;
        //     y_Elbow_R_Angle = -62;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 0;
        //     z_Wrist_L_Angle = 5;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = -2;
        //     z_Wrist_R_Angle = 33;
        //     //��׵
        //     x_Spine1_M_Angle = 28;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = -21;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = -5;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 10;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 5;
        //     //��ϥ��
        //     x_Knee_L_Angle = 10;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 0;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 24;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 0;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }
        // /// <summary>
        // /// �������
        // /// </summary>
        // public void GuiZiSheJi()
        // {
        //     //��������
        //     x_Rotation = 0;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = -18;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 0;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = 0;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = -73;
        //     y_Shoulder_L_Angle = 64;
        //     z_Shoulder_L_Angle = 36;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = -43;
        //     y_Shoulder_R_Angle = -55;
        //     z_Shoulder_R_Angle = -45;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = 0;
        //     y_Elbow_L_Angle = 28;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = -19;
        //     y_Elbow_R_Angle = -62;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 0;
        //     z_Wrist_L_Angle = 5;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = -2;
        //     z_Wrist_R_Angle = 33;
        //     //��׵
        //     x_Spine1_M_Angle = 28;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = -98;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = -5;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 10;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 5;
        //     //��ϥ��
        //     x_Knee_L_Angle = 94;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 105;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 0;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 0;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }
        // ///�Ե����
        // public void WoDaoSheJi()
        // {//85, 180, 0
        //     //��������
        //     x_Rotation = 85;
        //     y_Rotation = 180;
        //     z_Rotation = 0;
        //     //ͷ
        //     x_HeadAngle = -57;
        //     y_HeadAngle = 0;
        //     z_HeadAngle = 0;
        //     //����
        //     x_NeckAngle = 0;
        //     y_NeckAngle = 28;
        //     z_NeckAngle = 0;
        //     //�ع�
        //     x_Chest_M_Angle = -10;
        //     y_Chest_M_Angle = 0;
        //     z_Chest_M_Angle = 0;
        //     //���
        //     x_Shoulder_L_Angle = -153;
        //     y_Shoulder_L_Angle = 5;
        //     z_Shoulder_L_Angle = 114;
        //     //�Ҽ�
        //     x_Shoulder_R_Angle = -67;
        //     y_Shoulder_R_Angle = -111;
        //     z_Shoulder_R_Angle = 40;
        //     //����ι�
        //     x_Scapula_L_Angle = 0;
        //     y_Scapula_L_Angle = 0;
        //     z_Scapula_L_Angle = 0;
        //     //�Ҽ��ι�
        //     x_Scapula_R_Angle = 0;
        //     y_Scapula_R_Angle = 0;
        //     z_Scapula_R_Angle = 0;
        //     //����
        //     x_Elbow_L_Angle = 0;
        //     y_Elbow_L_Angle = 5;
        //     z_Elbow_L_Angle = 0;
        //     //����
        //     x_Elbow_R_Angle = -25;
        //     y_Elbow_R_Angle = -70;
        //     z_Elbow_R_Angle = 0;
        //     //����
        //     x_Wrist_L_Angle = 0;
        //     y_Wrist_L_Angle = 0;
        //     z_Wrist_L_Angle = -11;
        //     //����
        //     x_Wrist_R_Angle = 0;
        //     y_Wrist_R_Angle = 7;
        //     z_Wrist_R_Angle = 17;
        //     //��׵
        //     x_Spine1_M_Angle = 0;
        //     y_Spine1_M_Angle = 0;
        //     z_Spine1_M_Angle = 0;
        //     //�����
        //     x_Hip_L_Angle = 10;
        //     y_Hip_L_Angle = 0;
        //     z_Hip_L_Angle = -30;
        //     //�Ҵ���
        //     x_Hip_R_Angle = 10;
        //     y_Hip_R_Angle = 0;
        //     z_Hip_R_Angle = 30;
        //     //��ϥ��
        //     x_Knee_L_Angle = 0;
        //     y_Knee_L_Angle = 0;
        //     z_Knee_L_Angle = 0;
        //     //��ϥ��
        //     x_Knee_R_Angle = 0;
        //     y_Knee_R_Angle = 0;
        //     z_Knee_R_Angle = 0;
        //     //�����
        //     x_Ankle_L_Angle = 20;
        //     y_Ankle_L_Angle = 0;
        //     z_Ankle_L_Angle = 0;
        //     //�ҽ���
        //     x_Ankle_R_Angle = 20;
        //     y_Ankle_R_Angle = 0;
        //     z_Ankle_R_Angle = 0;
        // }

        /// <summary>
        /// ��ͷ
        /// </summary>
        //public void YangTou()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = -57;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ��ͷ
        /// </summary>
        //public void DiTou()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 48;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ��ϥ���
        /// </summary>
        //public void DanXiGuiDi()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = -100;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = -20;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 120;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 130;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = -20;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ����
        /// </summary>
        //public void ChaYao()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 59;
        //    y_Shoulder_L_Angle = 46;
        //    z_Shoulder_L_Angle = 41;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 59;
        //    y_Shoulder_R_Angle = -46;
        //    z_Shoulder_R_Angle = -41;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 50;
        //    y_Elbow_L_Angle = 110;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 50;
        //    y_Elbow_R_Angle = -110;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 20;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = -25;
        //    //����
        //    x_Wrist_R_Angle = 20;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 25;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ˫�۴�
        /// </summary>
        //public void ShuangBiDaKai()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 0;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = 0;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ������
        /// </summary>
        //public void YouTiTUi()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = -80;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ������
        /// </summary>
        //public void ZuoTiTUi()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = -80;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ��תͷ
        /// </summary>
        //public void ZuoZhuanTou()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = -63;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;
        //}

        /// <summary>
        /// ��תͷ
        /// </summary>
        //public void YouZhuanTou()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 63;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ����
        /// </summary>
        //public void WanYao()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 0;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = 0;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 60;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ��̧��
        /// </summary>
        //public void ZhuoTaiTui()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = -100;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 115;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        /// <summary>
        /// ��̧��
        /// </summary>
        //public void YouTaiTUi()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = 0;
        //    y_Shoulder_R_Angle = 0;
        //    z_Shoulder_R_Angle = -88;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = 0;
        //    y_Elbow_R_Angle = 0;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 0;
        //    //�Ҵ���
        //    x_Hip_R_Angle = -100;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = 0;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 115;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}



        /// <summary>
        /// ����
        /// </summary>
        //public void JingLi()
        //{
        //    //����
        //    x_Rotation = 0;
        //    y_Rotation = 180;
        //    z_Rotation = 0;
        //    //ͷ
        //    x_HeadAngle = 0;
        //    y_HeadAngle = 0;
        //    z_HeadAngle = 0;
        //    //����
        //    x_NeckAngle = 0;
        //    y_NeckAngle = 0;
        //    z_NeckAngle = 0;
        //    //�ع�
        //    x_Chest_M_Angle = 0;
        //    y_Chest_M_Angle = 0;
        //    z_Chest_M_Angle = 0;
        //    //���
        //    x_Shoulder_L_Angle = 0;
        //    y_Shoulder_L_Angle = 0;
        //    z_Shoulder_L_Angle = 88;
        //    //�Ҽ�
        //    x_Shoulder_R_Angle = -70;
        //    y_Shoulder_R_Angle = 12;
        //    z_Shoulder_R_Angle = -24;
        //    //����ι�
        //    x_Scapula_L_Angle = 0;
        //    y_Scapula_L_Angle = 0;
        //    z_Scapula_L_Angle = 0;
        //    //�Ҽ��ι�
        //    x_Scapula_R_Angle = 0;
        //    y_Scapula_R_Angle = 0;
        //    z_Scapula_R_Angle = 0;
        //    //����
        //    x_Elbow_L_Angle = 0;
        //    y_Elbow_L_Angle = 0;
        //    z_Elbow_L_Angle = 0;
        //    //����
        //    x_Elbow_R_Angle = -68;
        //    y_Elbow_R_Angle = -140;
        //    z_Elbow_R_Angle = 0;
        //    //����
        //    x_Wrist_L_Angle = 0;
        //    y_Wrist_L_Angle = 0;
        //    z_Wrist_L_Angle = 0;
        //    //����
        //    x_Wrist_R_Angle = 0;
        //    y_Wrist_R_Angle = 0;
        //    z_Wrist_R_Angle = 0;
        //    //��׵
        //    x_Spine1_M_Angle = 0;
        //    y_Spine1_M_Angle = 0;
        //    z_Spine1_M_Angle = 0;
        //    //�����
        //    x_Hip_L_Angle = 0;
        //    y_Hip_L_Angle = 0;
        //    z_Hip_L_Angle = 2;
        //    //�Ҵ���
        //    x_Hip_R_Angle = 0;
        //    y_Hip_R_Angle = 0;
        //    z_Hip_R_Angle = -2;
        //    //��ϥ��
        //    x_Knee_L_Angle = 0;
        //    y_Knee_L_Angle = 0;
        //    z_Knee_L_Angle = 0;
        //    //��ϥ��
        //    x_Knee_R_Angle = 0;
        //    y_Knee_R_Angle = 0;
        //    z_Knee_R_Angle = 0;
        //    //�����
        //    x_Ankle_L_Angle = 0;
        //    y_Ankle_L_Angle = 0;
        //    z_Ankle_L_Angle = 0;
        //    //�ҽ���
        //    x_Ankle_R_Angle = 0;
        //    y_Ankle_R_Angle = 0;
        //    z_Ankle_R_Angle = 0;

        //}

        ////ͷ
        //float x_HeadAngle;
        //float y_HeadAngle;
        //float z_HeadAngle;
        ////����
        //float x_NeckAngle;
        //float y_NeckAngle;
        //float z_NeckAngle;
        ////���
        //float x_Shoulder_L_Angle;
        //float y_Shoulder_L_Angle;
        //float z_Shoulder_L_Angle;
        ////�Ҽ�
        //float x_Shoulder_R_Angle;
        //float y_Shoulder_R_Angle;
        //float z_Shoulder_R_Angle;
        ////����ι�
        //float x_Scapula_L_Angle;
        //float y_Scapula_L_Angle;
        //float z_Scapula_L_Angle;
        ////�Ҽ��ι�
        //float x_Scapula_R_Angle;
        //float y_Scapula_R_Angle;
        //float z_Scapula_R_Angle;
        ////����
        //float x_Elbow_L_Angle;
        //float y_Elbow_L_Angle;
        //float z_Elbow_L_Angle;
        ////����
        //float x_Elbow_R_Angle;
        //float y_Elbow_R_Angle;
        //float z_Elbow_R_Angle;
        ////����
        //float x_Wrist_L_Angle;
        //float y_Wrist_L_Angle;
        //float z_Wrist_L_Angle;
        ////����
        //float x_Wrist_R_Angle;
        //float y_Wrist_R_Angle;
        //float z_Wrist_R_Angle;
        ////��׵
        //float x_Spine1_M_Angle;
        //float y_Spine1_M_Angle;
        //float z_Spine1_M_Angle;
        ////�����
        //float x_Hip_L_Angle;
        //float y_Hip_L_Angle;
        //float z_Hip_L_Angle;
        ////�Ҵ���
        //float x_Hip_R_Angle;
        //float y_Hip_R_Angle;
        //float z_Hip_R_Angle;
        ////��ϥ��
        //float x_Knee_L_Angle;
        //float y_Knee_L_Angle;
        //float z_Knee_L_Angle;
        ////��ϥ��
        //float x_Knee_R_Angle;
        //float y_Knee_R_Angle;
        //float z_Knee_R_Angle;
        ////�����
        //float x_Ankle_L_Angle;
        //float y_Ankle_L_Angle;
        //float z_Ankle_L_Angle;
        ////�ҽ���
        //float x_Ankle_R_Angle;
        //float y_Ankle_R_Angle;
        //float z_Ankle_R_Angle;

        #endregion

        #region 5��6�յĻ���֮��
        public void Human3Posture()
        {
            //����
            x_Rotation = 0;
            y_Rotation = 180;
            z_Rotation = 0;
            //ͷ
            x_HeadAngle = 0;
            y_HeadAngle = 0;
            z_HeadAngle = 0;
            //����
            x_NeckAngle = 0;
            y_NeckAngle = 0;
            z_NeckAngle = 0;
            //�ع�
            x_Chest_M_Angle = 10;
            y_Chest_M_Angle = 15;
            z_Chest_M_Angle = 0;
            //���
            x_Shoulder_L_Angle = -95;
            y_Shoulder_L_Angle = 0;
            z_Shoulder_L_Angle = 85;
            //�Ҽ�
            x_Shoulder_R_Angle = -50;
            y_Shoulder_R_Angle = 0;
            z_Shoulder_R_Angle = -42;
            //����ι�
            x_Scapula_L_Angle = 0;
            y_Scapula_L_Angle = 0;
            z_Scapula_L_Angle = 0;
            //�Ҽ��ι�
            x_Scapula_R_Angle = 0;
            y_Scapula_R_Angle = 0;
            z_Scapula_R_Angle = 0;
            //����
            x_Elbow_L_Angle = 0;
            y_Elbow_L_Angle = 0;
            z_Elbow_L_Angle = 0;
            //����
            x_Elbow_R_Angle = 0;
            y_Elbow_R_Angle = 0;
            z_Elbow_R_Angle = 0;
            //����
            x_Wrist_L_Angle = 0;
            y_Wrist_L_Angle = 0;
            z_Wrist_L_Angle = 0;
            //����
            x_Wrist_R_Angle = 0;
            y_Wrist_R_Angle = 0;
            z_Wrist_R_Angle = 0;
            //��׵
            x_Spine1_M_Angle = 20;
            y_Spine1_M_Angle = 30;
            z_Spine1_M_Angle = 0;
            //�����
            x_Hip_L_Angle = 0;
            y_Hip_L_Angle = 0;
            z_Hip_L_Angle = -5;
            //�Ҵ���
            x_Hip_R_Angle = 0;
            y_Hip_R_Angle = 0;
            z_Hip_R_Angle = 5;
            //��ϥ��
            x_Knee_L_Angle = 0;
            y_Knee_L_Angle = 0;
            z_Knee_L_Angle = 0;
            //��ϥ��
            x_Knee_R_Angle = 0;
            y_Knee_R_Angle = 0;
            z_Knee_R_Angle = 0;
            //�����
            x_Ankle_L_Angle = 0;
            y_Ankle_L_Angle = 0;
            z_Ankle_L_Angle = 0;
            //�ҽ���
            x_Ankle_R_Angle = 0;
            y_Ankle_R_Angle = 0;
            z_Ankle_R_Angle = 0;
        }
        #endregion
    }
}