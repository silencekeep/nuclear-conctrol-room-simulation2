using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HunmanIntanstiation : MonoBehaviour
{
    public Dropdown GenderSelect;
    public Dropdown CreatedHUmanDropDown;
    public GameObject[] HumanPrefab;
    public static List<GameObject> createdHuman = new List<GameObject>();
    public Button Yes;
    GameObject HumanInstantiation;
    static int n = 1;
    void Start()
    {
        GenderSelect.GetComponent<Dropdown>();
        Yes.GetComponent<Button>().onClick.AddListener(CreatHuamnModel);

        CreatedHUmanDropDown.GetComponent<Dropdown>();
        CreatedHUmanDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(CreatedHUmanDropDown); });
    }

    /// <summary>
    /// 创建人物并且添加到列表当中
    /// </summary>
    void CreatHuamnModel()
    {
        //Creat By Gendeer
        if (GenderSelect.value == 0)
        {
            HumanInstantiate(HumanPrefab[0]);

        }
        else
        {
            HumanInstantiate(HumanPrefab[1]);
        }
    }
    void HumanInstantiate(GameObject humanPrefab)
    {
        if (SceneManager.GetActiveScene().name == "Demoscene")
        {
            HumanInstantiation = Instantiate(humanPrefab, new Vector3(12.5f, 0f, 1.5f), new Quaternion(0, 0, 0, 0));
            HumanInstantiation.name = "新增人物" + n++.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Demo2scene")
        {
            HumanInstantiation = Instantiate(humanPrefab, new Vector3(5f, 0f, 5f), new Quaternion(0, 0, 0, 0));
            HumanInstantiation.name = "新增人物" + n++.ToString(); ;
        }

        //GameObject leftHand =  HumanInstantiate.GetComponentInChildren<Transform>().name = "Shoulder_L";
        foreach (Transform item in HumanInstantiation.GetComponentsInChildren<Transform>())
        {
            if (item.name == "Shoulder_L")
            {
                item.transform.localEulerAngles = new Vector3(0, 0, 80);
            }
            else if (item.name == "Shoulder_R")
            {
                item.transform.localEulerAngles = new Vector3(0, 0, -80);
            }
        }
        createdHuman.Add(HumanInstantiation);
        CreatedHUmanDropDownRefresh();

    }

    // Update is called once per frame
    void Update()
    {

    }
    ///刷新列表
    void CreatedHUmanDropDownRefresh()
    {
        CreatedHUmanDropDown.options.Clear();
        foreach (GameObject human in createdHuman)
        {
            CreatedHUmanDropDown.options.Add(new Dropdown.OptionData() { text = human.name });
        }
    }

    /// <summary>
    /// 选择列表中的某一项
    /// </summary>
    /// <param name="dropdown"></param>
    /// <returns></returns>
    int DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        return index;
    }

    /// <summary>
    /// 删除实例化人物
    /// </summary>
    public void DeleteIntanstiationHuman()
    {
        int DeleteHumanNumber = DropdownItemSelected(CreatedHUmanDropDown);
        CreatedHUmanDropDown.options.RemoveAt(DeleteHumanNumber);
        Destroy(createdHuman[DeleteHumanNumber]);
        createdHuman.RemoveAt(DeleteHumanNumber);//????
        CreatedHUmanDropDownRefresh();
    }
}
