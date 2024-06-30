using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relationship : MonoBehaviour
{
    public GameObject singleWorkstation_Button;
    public InputField height_StandingInputField, stretch_StandingInputField;
    private Vector3 tableHeight_Value, heightValue;
    public GameObject human;
    public static bool QType1DropDownValueChanged;
    private bool target;
    private GameObject myTarget;
    public InputField targetName;

    public void Single_WorktableChange()
    {
        float tableHeight = float.Parse(height_StandingInputField.text) / 160;
        float tableWidth = float.Parse(stretch_StandingInputField.text) / 70;
        //float a = 0.1f;
        //ControlTable_1 = GameObject.Find("OWP三平台 (4)");
        tableHeight_Value = new Vector3(tableWidth, tableHeight, 1);

        myTarget.transform.localScale = tableHeight_Value;
        human.transform.localPosition = new Vector3(12.454f, -0.42f, 4.1f);

    }

    // Start is called before the first frame update
    void Start()
    {
       // QType1DropDownValueChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //relationonclick();
                targetName.text = hit.transform.name;
                myTarget = hit.transform.gameObject;
                
            }
            target = false;

        }
    }
    public void targetChoose()
    {
        target = true;
    }

}
