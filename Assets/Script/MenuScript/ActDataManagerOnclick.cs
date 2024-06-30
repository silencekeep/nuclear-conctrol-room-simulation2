using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActDataManagerOnclick : MonoBehaviour
{
    public GameObject ActionBtn;
    public GameObject GameObjectHum;
    // Start is called before the first frame update
    void Start()
    {
        ActionBtn = GameObject.Find("Action");
        ActionBtn.GetComponent<Button>().onClick.AddListener(ActDataManagerOnClick);
    }
    private void ActDataManagerOnClick()
    {
        SceneManager.LoadScene(2);

       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
