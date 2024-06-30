using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBarIntanstiation : MonoBehaviour
{
    //public List<GameObject> createdPanel = new List<GameObject>();

    //public Button[] PanelPrefabButton;
    //public GameObject[] PanelObjects;

    public GameObject PanelPrefab;
    //this.GetComponent<Button>().onClick.AddListener(OnClick);
    public static List<GameObject> CreatedPanel = new List<GameObject>();
    private float minX, maxX, minY, maxY;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CreateObject);
        //for (int i = 0; i < PanelPrefabButton.Length; i++)
        //{
        //    PanelPrefabButton[i].GetComponent<Button>().onClick.AddListener(delegate { CreateObject(PanelPrefabButton[i].name); });
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateObject()
    {
        // a prefab is need to perform the instantiation
        GameObject PanelGenerationPosition = GameObject.Find("CaseRoom");
        if (PanelPrefab != null)
        {
            // get a random postion to instantiate the prefab - you can change this to be created at a fied point if desired
            //Vector3 position = new Vector3(Random.Range(minX + 0.5f, maxX - 0.5f), Random.Range(minY + 0.5f, maxY - 0.5f), 0);

            // instantiate the object
            //GameObject go = (GameObject)Instantiate(PanelPrefab, position, Quaternion.identity);

            //NuclearControlRoom.transform.position = new Vector3(NuclearControlRoom.transform.position.x + 0.77f, NuclearControlRoom.transform.position.y + 0.9f, NuclearControlRoom.transform.position.z - 0.443f);
            //BuildPosition = Vector3(0, 0, 0);
            //
            GameObject PanelIntanstiation = Instantiate(PanelPrefab, new Vector3(6.85f, 0.6f, 3.89f), PanelGenerationPosition.transform.rotation);
            PanelIntanstiation.transform.SetParent(PanelGenerationPosition.transform);
            //PanelIntanstiation.transform.position = new Vector3(PanelIntanstiation.transform.position.x + 0.75f, PanelIntanstiation.transform.position.y + 0.86f, PanelIntanstiation.transform.position.z - 0.63f);
            //PanelIntanstiation.transform.localScale = Vector3.one;
            CreatedPanel.Add(PanelIntanstiation);
            
        }
    }
}
