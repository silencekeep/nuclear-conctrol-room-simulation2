using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteModel : MonoBehaviour
{
    public GameObject deleteGo;
    public GameObject DeletePopUp;
    public GameObject importer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSceneModel()
    {
        deleteGo.SetActive(false);
        //Destroy(deleteGo, 0);
        //importer.GetComponent<AddComponent>().i -= 1;
        //Debug.Log(importer.GetComponent<AddComponent>().i);
        DeletePopUp.SetActive(false);
    }
}
