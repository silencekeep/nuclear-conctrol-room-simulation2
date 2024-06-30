using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CNCC.Saving;
using System;

namespace CNCC.Models
{
    public class Model : MonoBehaviour, ISaveable
    {

        // [SerializeField] Transform spawnHumanPosition;
        public static List<Model> createdModel = new List<Model>();
        //public static Model[] createdModel = new Model[100];
        public string Name { get; set; }
        public string gender { get; set; }
        public string text;
        public Transform LeftFootHorizontalPlane;
        public Transform RightFootHorizontalPlane;
        public Transform HumanSitPoint;
        float heightMan = 0f;
        float heightWoman = 0f;
        GameObject generatedCharacters;
        public Model(GameObject humanPrefab, Transform transform, string modelName, string modelGender)
        {
            Name = modelName;
            gender = modelGender;
            generatedCharacters = Instantiate(humanPrefab, transform);
            generatedCharacters.GetComponent<Model>().Name = modelName;
            generatedCharacters.GetComponent<Model>().gender = modelGender;
            createdModel.Add(generatedCharacters.GetComponent<Model>());
            PostureInit();
            generatedCharacters.GetComponent<Model>().text = generatedCharacters.GetComponent<Model>().Name;
        }

        public Model(GameObject humanPrefab, Transform transform)
        {
            generatedCharacters = Instantiate(humanPrefab, transform);
            generatedCharacters.GetComponent<Model>().Name = "新增人物" + (createdModel.Count + 1).ToString();
            generatedCharacters.GetComponent<Model>().gender = "男";
            createdModel.Add(generatedCharacters.GetComponent<Model>());
            PostureInit();
            generatedCharacters.GetComponent<Model>().text = generatedCharacters.GetComponent<Model>().Name;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        //人物生成
        public void MOdelInstantiate()
        {

        }

        //人物删除
        public virtual void ModelDelete(Model model)
        {
            createdModel.Remove(model);
            Destroy(model.gameObject);
            print("删除后人物数" + createdModel.Count);

        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetPosition()
        {
            Vector3 humanposition;
            humanposition.x = transform.position.x;
            //humanposition.y = Math.Min(GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).gameObject.transform.position.y, GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightFoot).gameObject.transform.position.y);
            humanposition.y = Math.Min(LeftFootHorizontalPlane.position.y, RightFootHorizontalPlane.position.y);
            humanposition.z = transform.position.z;
            return humanposition;
        }

        public static void ModelDelete(string name)
        {
            for (int i = 0; i < createdModel.Count; i++)
            {
                if (createdModel[i].Name == name)
                {
                    //createdModel.Remove(createdModel[i]);
                    Destroy(createdModel[i]);
                }
                else
                {
                    print("该模型不存在");
                }
            }
        }
        //关节调整
        public void JointAdjust(IJoint joint, Vector3 vector3)
        {
            joint.SetCurrentAngle(this.gameObject, vector3);
        }
        private void PostureInit()
        {
            //generatedCharacters.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject.transform.localEulerAngles = new Vector3(0, 0, 85);
            //generatedCharacters.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject.transform.localEulerAngles = new Vector3(0, 0, -85);
            generatedCharacters.GetComponent<Shoulder_L>().SetCurrentAngle(generatedCharacters, new Vector3(0, 0, 85));
            generatedCharacters.GetComponent<Shoulder_R>().SetCurrentAngle(generatedCharacters, new Vector3(0, 0, -85));
        }

        public static List<Model> GetCreatedHuman()
        {
            return createdModel;
        }

        //设置人体姿势
        public void SetPosutre(Model model, Dictionary<HumanBodyBones, Vector3> postureDic)
        {
            foreach (KeyValuePair<HumanBodyBones, Vector3> kvp in postureDic)
            {
                float x = kvp.Value.x;
                float y = kvp.Value.y;
                float z = kvp.Value.z;
                model.GetComponent<Animator>().GetBoneTransform(kvp.Key).gameObject.transform.localEulerAngles = new Vector3(x, y, z);
            }

            model.GetComponent<Head>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.Head]);
            model.GetComponent<Neck>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.Neck]);
            model.GetComponent<Chest_M>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.Chest]);
            model.GetComponent<Shoulder_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftUpperArm]);
            model.GetComponent<Shoulder_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightUpperArm]);
            model.GetComponent<Elbow_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftLowerArm]);
            model.GetComponent<Elbow_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightLowerArm]);
            model.GetComponent<Wrist_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftHand]);
            model.GetComponent<Wrist_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightHand]);
            model.GetComponent<Spine1_M>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.Spine]);
            model.GetComponent<Hip_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftUpperLeg]);
            model.GetComponent<Hip_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightUpperLeg]);
            model.GetComponent<Knee_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftLowerLeg]);
            model.GetComponent<Knee_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightLowerLeg]);
            model.GetComponent<Ankle_L>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.LeftFoot]);
            model.GetComponent<Ankle_R>().SetCurrentAngle(model.gameObject, postureDic[HumanBodyBones.RightFoot]);
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            data["scale"] = new SerializableVector3(transform.localScale);
            return data;

        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            transform.localScale = ((SerializableVector3)data["scale"]).ToVector();
        }

        //获取人体所有关节角度
        public List<Vector3> GetJointInfo()
        {
            List<Vector3> AngleInfo = new List<Vector3>();

            //
            AngleInfo.Add(gameObject.GetComponent<Ankle_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Ankle_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Ankle_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Ankle_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Ankle_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Ankle_R>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Chest_M>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Chest_M>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Chest_M>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Elbow_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Elbow_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Elbow_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Elbow_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Elbow_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Elbow_R>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Head>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Head>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Head>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Hip_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Hip_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Hip_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Hip_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Hip_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Hip_R>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Knee_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Knee_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Knee_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Knee_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Knee_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Knee_R>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Neck>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Neck>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Neck>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Shoulder_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Shoulder_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Shoulder_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Shoulder_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Shoulder_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Shoulder_R>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Spine1_M>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Spine1_M>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Spine1_M>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Wrist_L>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Wrist_L>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Wrist_L>().GetMinAngle());
            //
            AngleInfo.Add(gameObject.GetComponent<Wrist_R>().GetCurrentAngle());
            AngleInfo.Add(gameObject.GetComponent<Wrist_R>().GetMaxAngle());
            AngleInfo.Add(gameObject.GetComponent<Wrist_R>().GetMinAngle());

            return AngleInfo;
        }


        /// <summary>
        /// 设置人体所有关节角度
        /// </summary>
        /// <param name="dBJointInfo">为人体所有关节的角度</param>
        public void SetJointInfo(List<Vector3> dBJointInfo)
        {
            int n = 0;
            gameObject.GetComponent<Ankle_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Ankle_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Ankle_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Ankle_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Ankle_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Ankle_R>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Chest_M>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Chest_M>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Chest_M>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Elbow_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Elbow_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Elbow_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Elbow_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Elbow_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Elbow_R>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Head>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Head>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Head>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Hip_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Hip_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Hip_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Hip_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Hip_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Hip_R>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Knee_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Knee_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Knee_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Knee_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Knee_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Knee_R>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Neck>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Neck>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Neck>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Shoulder_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Shoulder_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Shoulder_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Shoulder_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Shoulder_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Shoulder_R>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Spine1_M>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Spine1_M>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Spine1_M>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Wrist_L>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Wrist_L>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Wrist_L>().SetMinAngle(gameObject, dBJointInfo[n++]);

            gameObject.GetComponent<Wrist_R>().SetCurrentAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Wrist_R>().SetMaxAngle(gameObject, dBJointInfo[n++]);
            gameObject.GetComponent<Wrist_R>().SetMinAngle(gameObject, dBJointInfo[n++]);
        }

        //重名判断
        public static bool IsRepeat(string name)
        {
            foreach (var existPanel in createdModel)
            {
                if (existPanel.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;

        }

        ///人物高度补偿
        public float Hcompensation()
        {
            if (gender == "男")
            {
                float heightcompensation = heightMan * transform.localScale.y / 2;
                return heightcompensation;
            }
            else
            {
                float heightcompensation = heightWoman * transform.localScale.y / 2;
                return heightcompensation;
            }

        }

        /// <summary>
        /// 判断两个人物是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return (this.Name == ((Model)other).Name && this.gender == ((Model)other).gender && TansfromJudge(this, ((Model)other)));
        }
        //两个向量数组直接用等于号判断可以吗？不可以。C#中数组为引用类型
        //在C#中，你不能直接使用==运算符来判断两个数组是否相等。==运算符在C#中用于比较引用类型对象的引用是否相等，而不是它们的内容是否相等。因此，即使两个数组具有相同的内容，==运算符也会返回false。

        //public Vector3[] TansfromJudge(Model model)
        //{
        //    Vector3[] vector3s = new Vector3[3];
        //    vector3s[0] = model.gameObject.transform.position;
        //    vector3s[1] = model.gameObject.transform.eulerAngles;
        //    vector3s[2] = model.gameObject.transform.localScale;
        //    return vector3s;
        //}
        public bool TansfromJudge(Model model1, Model model2)
        {
            if (IsSameVector3(model1.gameObject.transform.position, model2.gameObject.transform.position) && IsSameVector3(model1.gameObject.transform.localEulerAngles, model2.gameObject.transform.localEulerAngles) && IsSameVector3(model1.gameObject.transform.localScale, model2.gameObject.transform.localScale))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断两个向量是否相同
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        private bool IsSameVector3(Vector3 vector1, Vector3 vector2)
        {
            if (Math.Abs(vector1.x - vector2.x) < 0.00001 && Math.Abs(vector1.y - vector2.y) < 0.00001 && Math.Abs(vector1.z - vector2.z) < 0.00001)
            {
                return true;
            }
            return false;
        }
    }
}