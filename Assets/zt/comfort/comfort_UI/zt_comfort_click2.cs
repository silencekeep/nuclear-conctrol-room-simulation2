using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_click2 : MonoBehaviour
{
    public int lister1 = 0;
    public int lister2 = 0;
    public GameObject button;
    public GameObject jieguo;
    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(OnClick1);
        jieguo.GetComponent<Button>().onClick.AddListener(OnClick2);
    }

    // Update is called once per frame
    private void OnClick1()
    {
        lister1 = 1;
    }
    private void OnClick2()
    {
        lister2 = 1;
    }
}
