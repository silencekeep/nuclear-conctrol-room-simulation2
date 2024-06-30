using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Panels
{
    public class IMonitor : MonoBehaviour
    {

        // Start is called before the first frame update
        [SerializeField] Transform LockjPoint;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public Vector3 GetLockjPointPosition()
        {
            return LockjPoint.position;
        }

        public void SetLockjPointPosition(Vector3 vector3)
        {
            LockjPoint.position = vector3;
        }
    }
}

