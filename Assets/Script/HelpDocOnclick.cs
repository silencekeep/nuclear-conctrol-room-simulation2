using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpDocOnclick : MonoBehaviour
{
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
        Debug.Log(Application.dataPath + "/Help.docx");
        System.Diagnostics.Process.Start(Application.dataPath + "/Help.docx");
    }
}