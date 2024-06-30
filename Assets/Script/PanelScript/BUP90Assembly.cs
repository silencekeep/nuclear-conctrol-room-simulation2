using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Panels
{
    public class BUP90Assembly : Panel, IBUP
    {
        [SerializeField] Transform LeftPoint;
        [SerializeField] Transform RightPoint;

        public static List<BUP90Assembly> AllBUP90s = new List<BUP90Assembly>();
        GameObject generatedBUP90Assembly;
        [SerializeField] float objectHeight = 2.2f;
        public BUP90Assembly(GameObject BUPPrefab, Transform spawnPoint, Transform position, string ID)
        {
            generatedBUP90Assembly = Instantiate(BUPPrefab, spawnPoint);
            generatedBUP90Assembly.gameObject.GetComponent<BUP90Assembly>().ID = SetID(ID);
            generatedBUP90Assembly.gameObject.transform.position = new Vector3(position.position.x, position.position.y + objectHeight/2, position.position.z);
            AllBUP90s.Add(generatedBUP90Assembly.GetComponent<BUP90Assembly>());
            Panel.AllPanels.Add(generatedBUP90Assembly.GetComponent<BUP90Assembly>());
        }

        string SetID(string id)
        {
            if (id.Trim() == "")
            {
                id = "BUP90Assembly" + AllBUP90s.Count ;
            }
            return id;
        }

        public BUP90Assembly()
        {
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
            AllBUP90s.Remove((BUP90Assembly)panel);
        }

        public bool IsSplice(GameObject gameObject)
        {
            return true;
        }

        public bool IsSplice(GameObject gameObject, bool IsLeft)
        {
            if (gameObject.GetComponent<IBUP>() != null)
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

        public override float Hcompensation(float height)
        {
            float heightcompensation = objectHeight * transform.localScale.y / 2;
            return heightcompensation;
        }

        void LeftSplice(GameObject obj)
        {
            int n = 0;
            Transform rightPoint = RightPoint;
            Transform leftPoint = obj.GetComponent<IBUP>().GetLeftPoint();
            while (SetNewTransform(rightPoint, leftPoint) && n < 5)
            {
                rightPoint = RightPoint;
                n++;
            }
        }

        void RightSplice(GameObject obj)
        {
            int n = 0;
            Transform rightPoint = obj.GetComponent<IBUP>().GetRightPoint();
            Transform leftPoint = LeftPoint;

            while (SetNewTransform(leftPoint, rightPoint) && n < 5)
            {
                leftPoint = LeftPoint;
                n++;
            }
        }

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
    }
}