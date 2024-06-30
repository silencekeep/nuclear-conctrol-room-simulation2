using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace HullDelaunayVoronoi
{
    public class input2 : MonoBehaviour
    {
        public string endValue2;
        public float endValue3;

        private void Start()
        {


            GameObject.Find("InputField1").GetComponent<InputField>().onEndEdit.AddListener(EndValue1);//文本输入结束时会调用


        }
        //用户输入时的变化

        private void EndValue1(string value)
        {

            endValue2 = value;
            endValue3 = float.Parse(endValue2);




        }
    }
}
