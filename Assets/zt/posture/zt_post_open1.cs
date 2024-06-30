using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_post_open1 : MonoBehaviour
{
    public int lister_post = 0;
    public int lister_post1 = 0;
    public GameObject post_button;
    zt_comfort_click2 click;
    void Start()
    {
        GameObject.Find("Menu/Simulation/GameObjectSim/Posture").GetComponent<Button>().onClick.AddListener(onclick1);
        post_button.GetComponent<Button>().onClick.AddListener(onclick2);
        click = FindObjectOfType<zt_comfort_click2>();
    }

    private void onclick1()
    {
        lister_post = 1;
    }
    private void onclick2()
    {
        lister_post1 = 1;
    }
}
