using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CNCC.Panels
{
    public class LargeScreen45Assembly : Panel, IScreen
    {
        [SerializeField] Transform LeftPoint;
        [SerializeField] Transform RightPoint;
        public static List<LargeScreen45Assembly> AllLargeScreen45Assembly = new List<LargeScreen45Assembly>();
        //public string ID { get; set; }
        GameObject generatedLargeScreen45Assembly;
        [SerializeField] float objectHeight = 2.609f;
        public LargeScreen45Assembly(GameObject LargeScreen45AssemblyPrefab, Transform transform, Transform position, string id)
        {
            generatedLargeScreen45Assembly = Instantiate(LargeScreen45AssemblyPrefab, transform);
            generatedLargeScreen45Assembly.GetComponent<LargeScreen45Assembly>().ID = SetID(id);
            generatedLargeScreen45Assembly.gameObject.transform.position = new Vector3(position.position.x, position.position.y + objectHeight / 2, position.position.z);
            AllLargeScreen45Assembly.Add(generatedLargeScreen45Assembly.GetComponent<LargeScreen45Assembly>());
            Panel.AllPanels.Add(generatedLargeScreen45Assembly.GetComponent<LargeScreen45Assembly>());
        }

        string SetID(string id)
        {
            if (id.Trim() == "")
            {
                id = "LargeScreen45度拼角台" + AllLargeScreen45Assembly.Count;
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
            AllLargeScreen45Assembly.Remove((LargeScreen45Assembly)panel);
        }

        public bool IsSplice(GameObject gameObject)
        {
            return true;
        }

        public bool IsSplice(GameObject gameObject, bool IsLeft)
        {
            if (gameObject.GetComponent<IScreen>() != null)
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
            Transform leftPoint = obj.GetComponent<IScreen>().GetLeftPoint();
            while (SetNewTransform(rightPoint, leftPoint) && n < 5)
            {
                rightPoint = RightPoint;
                n++;
            }
        }

        void RightSplice(GameObject obj)
        {
            int n = 0;
            Transform rightPoint = obj.GetComponent<IScreen>().GetRightPoint();
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

        public override float Hcompensation(float height)
        {
            float heightcompensation = objectHeight * transform.localScale.y / 2;
            return heightcompensation;
        }
    }
}