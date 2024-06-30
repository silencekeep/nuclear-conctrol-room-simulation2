using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.Models
{
    public interface IJoint 
    {
        Vector3 GetCurrentAngle(GameObject model);
        void SetCurrentAngle(GameObject model, Vector3 angle);
        void SetCurrentAngle(GameObject model, float x,float y,float z);

        Vector3 GetMaxAngle();
        void SetMaxAngle(GameObject model, Vector3 angle);

        Vector3 GetMinAngle();
        void SetMinAngle(GameObject model, Vector3 angle);
        void SetCurrentAngle(Vector3 angle);
        Vector3 GetCurrentAngle();
    }
}

