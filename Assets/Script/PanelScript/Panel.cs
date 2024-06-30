using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Panels
{

    public class Panel : MonoBehaviour
    {
        public static List<Panel> AllPanels = new List<Panel>();
        public string ID { get; set; }
        float y = 0;

        void Start()
        {
            // y = transform.localScale.y;
        }

        // Update is called once per frame
        void Update()
        {

            //if (y!= transform.localScale.y)
            //{
            //    HeightAdjust();
            //}
        }
        public Transform GetTransform(Panel panel)
        {
            return panel.transform;
        }

        //public virtual Panel GetPanelType()
        //{
        //    return
        //}

        public virtual void DeletePanel(Panel panel)
        {
            Destroy(panel.gameObject);
            AllPanels.Remove(panel);
        }

        //public void HeightAdjust(float newScaleY)
        //{
        //    float height = transform.position.y;
        //    float scaleY = transform.localScale.y;
        //    transform.position = new Vector3(transform.position.x, height * newScaleY / scaleY, transform.position.z);
        //}

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void ChangeMesh(Material mat)
        {
            Transform[] allChild;
            allChild = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChild)
            {
                if (child.gameObject.GetComponent<MeshRenderer>())
                {
                    child.GetComponent<MeshRenderer>().material = mat;
                    child.gameObject.layer = 8;
                }
            }
        }

        public static bool IsRepeat(string name)
        {
            foreach (var existPanel in AllPanels)
            {
                if (existPanel.ID.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        //
        public virtual float Hcompensation(float height)
        {
            float heightcompensation = height * transform.localScale.y / 2;
            return heightcompensation;
        }
    }
}
