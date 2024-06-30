using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class click1 : MonoBehaviour
{
    public int lister = 0;
    void Start()
    {
        GameObject.Find("Button2").GetComponent<Button>().onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        lister = 1;
    }
}
