using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class post_angle : MonoBehaviour
{
    public Transform transform1,transform2,transform3;

    public Transform figure1, figure2, figure3, figure4, figure5, figure6, figure7, figure8, figure9,figure10,figure11,figure12;
    post_dropdown dropdown1;
    post_slider slider1;
    post_slider2 slider3;
    post_dropdown2 dropdown2;
    void Start()
    {

        dropdown1 = FindObjectOfType<post_dropdown>();

        
        slider1 = FindObjectOfType<post_slider>();
        slider3 = FindObjectOfType<post_slider2>();
        dropdown2 = FindObjectOfType<post_dropdown2>();






    }
    
    // Update is called once per frame
    void Update()
    {

        



        if (dropdown1.a == 1)
        {
            transform1 = GameObject.Find(dropdown2.post+dropdown1.f1).GetComponent<Transform>();
            transform2 = GameObject.Find(dropdown2.post+dropdown1.f2).GetComponent<Transform>();
            transform3 = GameObject.Find(dropdown2.post+dropdown1.f3).GetComponent<Transform>();
            figure1 = GameObject.Find(dropdown2.post+dropdown1.f4).GetComponent<Transform>();
            figure2 = GameObject.Find(dropdown2.post+dropdown1.f5).GetComponent<Transform>();
            figure3 = GameObject.Find(dropdown2.post+dropdown1.f6).GetComponent<Transform>();
            figure4 = GameObject.Find(dropdown2.post+dropdown1.f7).GetComponent<Transform>();

            figure5 = GameObject.Find(dropdown2.post+dropdown1.f8).GetComponent<Transform>();
            figure6 = GameObject.Find(dropdown2.post+dropdown1.f9).GetComponent<Transform>();
            figure7 = GameObject.Find(dropdown2.post+dropdown1.f10).GetComponent<Transform>();
            figure8 = GameObject.Find(dropdown2.post+dropdown1.f11).GetComponent<Transform>();

            figure9 = GameObject.Find(dropdown2.post+dropdown1.f12).GetComponent<Transform>();
            figure10 = GameObject.Find(dropdown2.post+dropdown1.f13).GetComponent<Transform>();
            figure11 = GameObject.Find(dropdown2.post+dropdown1.f14).GetComponent<Transform>();
            figure12 = GameObject.Find(dropdown2.post+dropdown1.f15).GetComponent<Transform>();

            transform1.localEulerAngles = new Vector3(slider1.elbow_x_value, slider1.elbow_y_value, 0);
            transform2.localEulerAngles = new Vector3(slider1.shoulder_x_value, slider1.shoulder_y_value, slider1.shoulder_z_value);
            transform3.localEulerAngles = new Vector3(0, slider1.wrist_y_value, slider1.wrist_z_value);


            figure1.localEulerAngles = new Vector3(0, 0, slider1.figure1_value);
            figure2.localEulerAngles = new Vector3(0, 0, slider1.figure1_value);
            figure3.localEulerAngles = new Vector3(0, 0, slider1.figure1_value);
            figure4.localEulerAngles = new Vector3(0, 0, slider1.figure1_value);

            figure5.localEulerAngles = new Vector3(0, 0, slider1.figure2_value);
            figure6.localEulerAngles = new Vector3(0, 0, slider1.figure2_value);
            figure7.localEulerAngles = new Vector3(0, 0, slider1.figure2_value);
            figure8.localEulerAngles = new Vector3(0, 0, slider1.figure2_value);

            figure9.localEulerAngles = new Vector3(0, 0, slider1.figure3_value);
            figure10.localEulerAngles = new Vector3(0, 0, slider1.figure3_value);
            figure11.localEulerAngles = new Vector3(0, 0, slider1.figure3_value);
            figure12.localEulerAngles = new Vector3(0, 0, slider1.figure3_value);
        }
        if (dropdown1.a == 2)
        {
            transform1 = GameObject.Find(dropdown2.post+dropdown1.f1).GetComponent<Transform>();
            transform2 = GameObject.Find(dropdown2.post+dropdown1.f2).GetComponent<Transform>();
            transform3 = GameObject.Find(dropdown2.post+dropdown1.f3).GetComponent<Transform>();
            figure1 = GameObject.Find(dropdown2.post+dropdown1.f4).GetComponent<Transform>();
            figure2 = GameObject.Find(dropdown2.post+dropdown1.f5).GetComponent<Transform>();
            figure3 = GameObject.Find(dropdown2.post+dropdown1.f6).GetComponent<Transform>();
            figure4 = GameObject.Find(dropdown2.post+dropdown1.f7).GetComponent<Transform>();

            figure5 = GameObject.Find(dropdown2.post+dropdown1.f8).GetComponent<Transform>();
            figure6 = GameObject.Find(dropdown2.post+dropdown1.f9).GetComponent<Transform>();
            figure7 = GameObject.Find(dropdown2.post+dropdown1.f10).GetComponent<Transform>();
            figure8 = GameObject.Find(dropdown2.post+dropdown1.f11).GetComponent<Transform>();

            figure9 = GameObject.Find(dropdown2.post+dropdown1.f12).GetComponent<Transform>();
            figure10 = GameObject.Find(dropdown2.post+dropdown1.f13).GetComponent<Transform>();
            figure11 = GameObject.Find(dropdown2.post+dropdown1.f14).GetComponent<Transform>();
            figure12 = GameObject.Find(dropdown2.post+dropdown1.f15).GetComponent<Transform>();

            transform1.localEulerAngles = new Vector3(slider3.elbow_x_value, slider3.elbow_y_value, 0);
            transform2.localEulerAngles = new Vector3(slider3.shoulder_x_value, slider3.shoulder_y_value, slider3.shoulder_z_value);
            transform3.localEulerAngles = new Vector3(0, slider3.wrist_y_value, slider3.wrist_z_value);


            figure1.localEulerAngles = new Vector3(0, 0, slider3.figure1_value);
            figure2.localEulerAngles = new Vector3(0, 0, slider3.figure1_value);
            figure3.localEulerAngles = new Vector3(0, 0, slider3.figure1_value);
            figure4.localEulerAngles = new Vector3(0, 0, slider3.figure1_value);

            figure5.localEulerAngles = new Vector3(0, 0, slider3.figure2_value);
            figure6.localEulerAngles = new Vector3(0, 0, slider3.figure2_value);
            figure7.localEulerAngles = new Vector3(0, 0, slider3.figure2_value);
            figure8.localEulerAngles = new Vector3(0, 0, slider3.figure2_value);

            figure9.localEulerAngles = new Vector3(0, 0, slider3.figure3_value);
            figure10.localEulerAngles = new Vector3(0, 0, slider3.figure3_value);
            figure11.localEulerAngles = new Vector3(0, 0, slider3.figure3_value);
            figure12.localEulerAngles = new Vector3(0, 0, slider3.figure3_value);
        }
    }
    
}
