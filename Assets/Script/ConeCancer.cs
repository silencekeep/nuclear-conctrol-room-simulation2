using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConeCancer : MonoBehaviour
{
    public GameObject left1;
    public GameObject right1;
    public GameObject left2;
    public GameObject right2;
    public GameObject left3;
    public GameObject right3;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnClick()
    {
        left1.SetActive(false);
        right1.SetActive(false);
        left2.SetActive(false);
        right2.SetActive(false);
        left3.SetActive(false);
        right3.SetActive(false);
    }
}
