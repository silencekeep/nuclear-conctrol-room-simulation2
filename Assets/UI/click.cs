using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class click : MonoBehaviour
{
    public int lister = 0;
    void Start()
    {
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        lister = 1;
    }
}
