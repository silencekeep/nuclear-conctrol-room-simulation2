using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RuntimeGizmos
{
    public class TransformController : MonoBehaviour
    {
        public Camera cam;
        public GameObject ui;
        private bool flag = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void trans()
        {
            if (flag)
            {
                flag = false;
                cam.GetComponent<TransformGizmo>().enabled = false;
                ui.GetComponent<move>().close();
                //ui.SetActive(false);
            }
            else
            {
                flag = true;
                cam.GetComponent<TransformGizmo>().enabled = true;
                //ui.SetActive(true);
            }
            
        }
    }
}
