using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HullDelaunayVoronoi
{
    public class CollisionDetection1 : MonoBehaviour
    {
        public  string collisionname;
        GameObject object1;
       public bool iscollision = false;
        dropdown drop;
        public string collisionname1;
        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {
           
        }
        private void OnTriggerEnter(Collider other)
        {


              collisionname1 = other.tag;
            if(collisionname1.Length!=0)
            {
                collisionname = GameObject.Find("Cube").name;
            }

            foreach (Material Mat in GameObject.Find("Cube").GetComponent<Renderer>().materials)
            {
                Mat.EnableKeyword("_EMISSION");
                // Mat.SetColor("_EmissionColor", new Color(0f,0.4568f, 0.7279f));
                Mat.SetColor("_EmissionColor", Color.red);

            }
            iscollision = true;
            
        }
    }
}

