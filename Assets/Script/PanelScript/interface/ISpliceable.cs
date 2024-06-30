using UnityEngine;

namespace CNCC.Panels
{
    public interface ISpliceable
    {       
        bool IsSplice(GameObject gameObject);
        bool IsSplice(GameObject gameObject, bool IsLeft);
        Transform GetLeftPoint();
        Transform GetRightPoint();
        //Vector3 GetPosition(GameObject gameObject);
        //Vector3 GetEulerAngles(GameObject gameObject);
    }
}

