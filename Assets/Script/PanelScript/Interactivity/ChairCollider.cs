using CNCC.Models;
using CNCC.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Interactive
{
    public class ChairCollider : MonoBehaviour
    {
        bool isOccupied = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 忽略物理因素，只作为碰撞触发检测
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "human" && this.isOccupied == false)
            {
                if (other.gameObject.GetComponent<Model>() == null) return;
                //GameManager.Instance.Save();
                other.gameObject.GetComponent<Model>().SetPosutre(other.gameObject.GetComponent<Model>(), PostureDB.PostureJointAngle(PostureDB.SitDown()));
                isOccupied = true;
                SetNewTransform(other.gameObject, other.gameObject.GetComponent<Model>().HumanSitPoint, GetComponent<Chair>().Sitransform);
                //print("发生碰撞");
            }
        }

        /// <summary>
        /// 物理碰撞
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionStay(Collision collision)
        {


        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "human" && this.isOccupied == true)
            {
                if (other.gameObject.GetComponent<Model>() == null) return;
                //GameManager.Instance.Save();
                other.gameObject.GetComponent<Model>().SetPosutre(other.gameObject.GetComponent<Model>(), PostureDB.PostureJointAngle(PostureDB.ZhanLi()));
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 0, other.gameObject.transform.position.z);
                isOccupied = false;
                print("发生碰撞");
            }
        }

        private void OnCollisionExit(Collision collision)
        {

        }

        bool SetNewTransform(GameObject gameObject, Transform targetPoint, Transform referPoint)
        {
            Vector3 relativePosition = GetRelativePosition(targetPoint, referPoint);
            //Vector3 relativeEulerAngles = GetRelativeEulerAngles(targetPoint, referPoint);
            //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x - relativeEulerAngles.x, gameObject.transform.eulerAngles.y - relativeEulerAngles.y, gameObject.transform.eulerAngles.z - relativeEulerAngles.z);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - relativePosition.x, gameObject.transform.position.y - relativePosition.y, gameObject.transform.position.z - relativePosition.z);
            //if (relativePosition != Vector3.zero || relativeEulerAngles != Vector3.zero)
            //{
            //    return true;
            //}

            if (IsSameDistance(relativePosition, Vector3.zero))
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

        private bool IsSameDistance(Vector3 vector1, Vector3 vector2)
        {
            if (Math.Abs(vector1.x - vector2.x) < 0.00001 && Math.Abs(vector1.y - vector2.y) < 0.00001 && Math.Abs(vector1.z - vector2.z) < 0.00001)
            {
                return true;
            }
            return false;
        }

    }
}

