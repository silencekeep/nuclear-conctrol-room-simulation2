using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearOncli : MonoBehaviour
{
    public Transform tran;
    int childcount;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(ClearModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClearModel()
    {
        childcount = tran.childCount;
        for (int i = 0;i < childcount; i++)
        {
            Transform child = tran.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }
}
