using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.WorkArea
{
    public class Wall : MonoBehaviour
    {

        [SerializeField] GameObject x0;
        [SerializeField] GameObject z0;
        [SerializeField] GameObject x25;
        [SerializeField] GameObject z15;

        public static float WallLength;
        public static float WallWidth;
        public static float Wallheight;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            WallWidth = x0.transform.localScale.z;
            WallLength = z0.transform.localScale.x;
        }
    }
}

