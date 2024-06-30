using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityOnclic : MonoBehaviour
{
    public GameObject View_Popup;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        //button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClick()
    {
        if (!View_Popup.activeInHierarchy)
        {
            View_Popup.SetActive(true);
            //transform.Rotate(0, 0, 360 / 60 * Time.deltaTime);
        }
        else
        {
            View_Popup.SetActive(false);
        }
    }
}
