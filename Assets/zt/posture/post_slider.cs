using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class post_slider : MonoBehaviour
{
    
    public float elbow_x_value, elbow_y_value, shoulder_x_value, shoulder_y_value, shoulder_z_value,wrist_z_value,wrist_y_value, figure1_value, figure2_value, figure3_value;
    void Awake()
    {
        GameObject.Find("post_slider/elbow/Slider_x").GetComponent<Slider>().onValueChanged.AddListener(elbow_x);
        GameObject.Find("post_slider/elbow/Slider_y").GetComponent<Slider>().onValueChanged.AddListener(elbow_y);
        GameObject.Find("post_slider/shoulder/Slider_x").GetComponent<Slider>().onValueChanged.AddListener(shoulder_x);
        GameObject.Find("post_slider/shoulder/Slider_y").GetComponent<Slider>().onValueChanged.AddListener(shoulder_y);
        GameObject.Find("post_slider/shoulder/Slider_z").GetComponent<Slider>().onValueChanged.AddListener(shoulder_z);
        GameObject.Find("post_slider/wrist/Slider_z").GetComponent<Slider>().onValueChanged.AddListener(wrist_z);
        GameObject.Find("post_slider/wrist/Slider_y").GetComponent<Slider>().onValueChanged.AddListener(wrist_y);


        GameObject.Find("post_slider/fingerprint/Slider1").GetComponent<Slider>().onValueChanged.AddListener(figure1);
        GameObject.Find("post_slider/fingerprint/Slider2").GetComponent<Slider>().onValueChanged.AddListener(figure2);
        GameObject.Find("post_slider/fingerprint/Slider3").GetComponent<Slider>().onValueChanged.AddListener(figure3);

    }
    private void elbow_x(float value)
    {
        elbow_x_value = value;
    }
    private void elbow_y(float value)
    {
        elbow_y_value = value;
    }
    private void shoulder_x(float value)
    {
        shoulder_x_value = value;
    }
    private void shoulder_y(float value)
    {
        shoulder_y_value = value;
    }
    private void shoulder_z(float value)
    {
        shoulder_z_value = value;
    }
    private void wrist_z(float value)
    {
        wrist_z_value = value;
    }
    private void wrist_y(float value)
    {
        wrist_y_value = value;
    }


    private void figure1(float value)
    {
        figure1_value = value;
    }
    private void figure2(float value)
    {
        figure2_value = value;
    }
    private void figure3(float value)
    {
        figure3_value = value;
    }


}
