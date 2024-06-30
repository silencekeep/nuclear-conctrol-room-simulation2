using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CNCC.Saving;

namespace CNCC.WorkArea
{
    public class Wallz0 : MonoBehaviour, IWallAdjust,ISaveable
    {
        float wallLength;
        float wallWidth;
        float wallHeight;
        // Start is called before the first frame update
        void Start()
        {
            wallHeight = 10;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetWallPosition(double length, double width)
        {
            gameObject.transform.position = new Vector3(wallLength / 2, wallHeight / 2, 0);
        }

        public void SetWallScale(double length, double width)
        {
            gameObject.transform.localScale = new Vector3(wallLength, wallHeight, (float)0.01);
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            data["scale"] = new SerializableVector3(transform.localScale);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            transform.localScale = ((SerializableVector3)data["scale"]).ToVector();
        }
    }
}
