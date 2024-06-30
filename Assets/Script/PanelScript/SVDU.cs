using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CNCC.Panels
{
    public class SVDU : Panel, IOWP
    {
        [SerializeField] Transform LeftPoint;
        [SerializeField] Transform RightPoint;
        [SerializeField] GameObject part3;
        [SerializeField] GameObject part2;
        [SerializeField] GameObject part1;

        public static List<SVDU> AllSVDU = new List<SVDU>();
        // public string ID { get; set; }
        GameObject generatedSVDU;
        [SerializeField] float objectHeight = 1.140f;
        public SVDU(GameObject SVDUPrefab, Transform transform, Transform position, string id)
        {
            generatedSVDU = Instantiate(SVDUPrefab, transform);
            generatedSVDU.GetComponent<SVDU>().ID = SetID(id);
            generatedSVDU.gameObject.transform.position = new Vector3(position.position.x, position.position.y + objectHeight / 2, position.position.z);
            AllSVDU.Add(generatedSVDU.GetComponent<SVDU>());
            Panel.AllPanels.Add(generatedSVDU.GetComponent<SVDU>());
        }

        string SetID(string id)
        {
            if (id.Trim() == "")
            {
                id = "SVDU" + AllSVDU.Count;
            }
            return id;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void DeletePanel(Panel panel)
        {
            base.DeletePanel(panel);
            AllSVDU.Remove((SVDU)panel);
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

            Vector3 relativePosition = GetRelativePosition(targetPoint, referPoint);
            Vector3 relativeEulerAngles = GetRelativeEulerAngles(targetPoint, referPoint);
            gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x - relativeEulerAngles.x, transform.eulerAngles.y - relativeEulerAngles.y, transform.eulerAngles.z - relativeEulerAngles.z);
            gameObject.transform.position = new Vector3(transform.position.x - relativePosition.x, transform.position.y - relativePosition.y, transform.position.z - relativePosition.z);
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
        /// 盘台台面
        /// </summary>
        public Vector3 GetPart3Scale()
        {
            return part3.transform.localScale;
        }
        /// <summary>
        /// 盘台台面
        /// </summary>
        public void SetPart3Scale(Vector3 vector3)
        {
            part3.transform.localScale = vector3;
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
        /// 返回盘台台面中心的世界坐标
        /// </summary>
        public Vector3 GetPart3Position()
        {
            return part3.transform.position;
        }

        public override float Hcompensation(float height)
        {
            float heightcompensation = objectHeight * transform.localScale.y / 2;
            return heightcompensation;
        }
    }
}