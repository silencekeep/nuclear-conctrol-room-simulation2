using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CNCC.Panels
{
    public class OWP_two : Panel, IOWP
    {
        [SerializeField] Transform LeftPoint;
        [SerializeField] Transform RightPoint;

        [SerializeField] GameObject part2;
        [SerializeField] GameObject part1;
        public GameObject monitorLeft;
        public GameObject monitorRight;
        public static List<OWP_two> AllOWP_two = new List<OWP_two>();
        // public string ID { get; set; }
        GameObject generatedOWP_two;
        [SerializeField] float objectHeight = 1.140f;
        public OWP_two(GameObject OWP_twoPrefab, Transform transform, Transform position, string id)
        {
            generatedOWP_two = Instantiate(OWP_twoPrefab, transform);
            generatedOWP_two.GetComponent<OWP_two>().ID = SetID(id);
            generatedOWP_two.gameObject.transform.position = new Vector3(position.position.x, position.position.y + objectHeight / 2, position.position.z);
            AllOWP_two.Add(generatedOWP_two.GetComponent<OWP_two>());
            Panel.AllPanels.Add(generatedOWP_two.GetComponent<OWP_two>());
        }

        string SetID(string id)
        {
            if (id.Trim() == "")
            {
                id = "OWP两平台" + AllOWP_two.Count;
            }
            return id;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //  print("正常输出"+transform.position);
            //  print("F3输出" + transform.position.ToString("F4"));
        }

        public override void DeletePanel(Panel panel)
        {
            base.DeletePanel(panel);
            AllOWP_two.Remove((OWP_two)panel);
        }

        public bool IsSplice(GameObject gameObject)
        {
            return true;
        }

        public bool IsSplice(GameObject gameObject, bool IsLeft)
        {
            if (gameObject.GetComponent<IOWP>() != null)
            {
                if (IsLeft)
                {
                    LeftSplice(gameObject);

                }
                else
                {
                    RightSplice(gameObject);
                }
                return true;
            }
            return false;
        }

        public Transform GetLeftPoint()
        {
            return LeftPoint;
        }

        public Transform GetRightPoint()
        {
            return RightPoint;
        }

        Vector3 GetRelativePosition(Transform target, Transform refer)
        {
            Vector3 relative = Vector3.zero;
            relative.x = target.position.x - refer.position.x;
            relative.y = target.position.y - refer.position.y;
            relative.z = target.position.z - refer.position.z;
            return relative;
        }

        Vector3 GetRelativeEulerAngles(Transform target, Transform refer)
        {
            Vector3 relative = Vector3.zero;
            relative.x = target.eulerAngles.x - refer.eulerAngles.x;
            relative.y = target.eulerAngles.y - refer.eulerAngles.y;
            relative.z = target.eulerAngles.z - refer.eulerAngles.z;
            return relative;
        }

        void LeftSplice(GameObject obj)
        {
            int n = 0;
            Transform rightPoint = RightPoint;
            Transform leftPoint = obj.GetComponent<IOWP>().GetLeftPoint();
            while (SetNewTransform(rightPoint, leftPoint) && n < 5)
            {
                rightPoint = RightPoint;
                n++;
            }
        }

        void RightSplice(GameObject obj)
        {
            int n = 0;
            Transform rightPoint = obj.GetComponent<IOWP>().GetRightPoint();
            Transform leftPoint = LeftPoint;

            while (SetNewTransform(leftPoint, rightPoint) && n < 5)
            {
                leftPoint = LeftPoint;
                n++;
            }
        }

        /// <summary>
        /// 拼接方法
        /// </summary>
        /// <param name="targetPoint">被拼接盘台</param>
        /// <param name="referPoint">拼接参照盘台</param>
        bool SetNewTransform(Transform targetPoint, Transform referPoint)
        {
            print("拼接前坐标1：" + transform.position);
            print("拼接前坐标2：" + gameObject.transform.position);
            Vector3 relativePosition = GetRelativePosition(targetPoint, referPoint);
            Vector3 relativeEulerAngles = GetRelativeEulerAngles(targetPoint, referPoint);
            print("相对坐标：" + relativePosition);
            //gameObject.transform.position.Set(new Vector3(transform.position.x - relativePosition.x, transform.position.y - relativePosition.y, transform.position.z - relativePosition.z));
            //gameObject.transform.eulerAngles.Set(transform.eulerAngles.x - relativeEulerAngles.x, transform.eulerAngles.y - relativeEulerAngles.y, transform.eulerAngles.z - relativeEulerAngles.z);
            //while (relativePosition!=Vector3.zero|| relativeEulerAngles!=Vector3.zero)
            //{
            //    gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x - relativeEulerAngles.x, transform.eulerAngles.y - relativeEulerAngles.y, transform.eulerAngles.z - relativeEulerAngles.z);
            //    gameObject.transform.position = new Vector3(transform.position.x - relativePosition.x, transform.position.y - relativePosition.y, transform.position.z - relativePosition.z);

            //}
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x - relativeEulerAngles.x, transform.eulerAngles.y - relativeEulerAngles.y, transform.eulerAngles.z - relativeEulerAngles.z);
            gameObject.transform.position = new Vector3(transform.position.x - relativePosition.x, transform.position.y - relativePosition.y, transform.position.z - relativePosition.z);

            print("拼接后坐标1：" + transform.position);
            print("拼接后坐标2：" + gameObject.transform.position);
            if (relativePosition != Vector3.zero || relativeEulerAngles != Vector3.zero)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 盘台底座
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPart1Scale()
        {
            return part1.transform.localScale;
        }
        /// <summary>
        /// 盘台底座
        /// </summary>
        public void SetPart1Scale(Vector3 vector3)
        {
            part1.transform.localScale = vector3;
        }

        /// <summary>
        /// 盘台前沿
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPart2Scale()
        {
            return part2.transform.localScale;
        }
        /// <summary>
        /// 盘台前沿
        /// </summary>
        public void SetPart2Scale(Vector3 vector3)
        {
            part2.transform.localScale = vector3;
        }

        /// <summary>
        /// 左显示器
        /// </summary>
        public Vector3 GetmonitorLeftScale()
        {
            return monitorLeft.transform.localScale;
        }
        /// <summary>
        /// 左显示器
        /// </summary>
        public void SetmonitorLeftScale(Vector3 vector3)
        {
            monitorLeft.transform.localScale = vector3;
        }

        /// <summary>
        /// 右显示器
        /// </summary>
        public Vector3 GetmonitorRightScale()
        {
            return monitorRight.transform.localScale;
        }
        /// <summary>
        /// 右显示器
        /// </summary>
        public void SetmonitorRightScale(Vector3 vector3)
        {
            monitorRight.transform.localScale = vector3;
        }

        /// <summary>
        /// 返回盘台底座中心的世界坐标
        /// </summary>
        public Vector3 GetPart1Position()
        {
            return part1.transform.position;
        }

        /// <summary>
        /// 返回盘台前沿中心的世界坐标
        /// </summary>
        public Vector3 GetPart2Position()
        {
            return part2.transform.position;
        }

        /// <summary>
        /// 返回左边显示器的世界坐标
        /// </summary>
        public Vector3 GetmonitorLeftPosition()
        {
            return monitorLeft.transform.position;
        }

        /// <summary>
        /// 设置左边显示器坐标，坐标y向量的值应与原来的向量的值相同
        /// </summary>
        /// <param name="vector3">(可变值, GetmonitorMiddlePosition().y, 可变值)</param>
        public void SetmonitorLeftPositio(Vector3 vector3)
        {
            float y = monitorLeft.GetComponent<IMonitor>().GetLockjPointPosition().y;
            if (Math.Abs(y - vector3.y) > 0.001)
            {
                print("显示器y方向上不可修改");
                return;
            }
            part1.GetComponent<OWP_twoPoint>().SetLeftPointPosition(vector3);
        }

        /// <summary>
        /// 返回右边显示器的世界坐标
        /// </summary>
        public Vector3 GetmonitorRightPosition()
        {
            return monitorRight.transform.position;
        }

        /// <summary>
        /// 设置右边显示器坐标，坐标y向量的值应与原来的向量的值相同
        /// </summary>
        /// <param name="vector3">(可变值, GetmonitorMiddlePosition().y, 可变值)</param>
        public void SetmonitorRightPositio(Vector3 vector3)
        {
            float y = monitorRight.GetComponent<IMonitor>().GetLockjPointPosition().y;
            if (Math.Abs(y - vector3.y) > 0.001)
            {
                print("显示器y方向上不可修改");
                return;
            }
            part1.GetComponent<OWP_twoPoint>().SetRightPointPosition(vector3);
        }

        public override float Hcompensation(float height)
        {
            float heightcompensation = objectHeight * transform.localScale.y / 2;
            return heightcompensation;
        }
    }
}