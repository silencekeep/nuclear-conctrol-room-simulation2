using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //0：玉霖/陆洋；1：徐翔；2：梦雯；3：孟峰；4：张腾；5：玉霖
    public void showsceneKD()
    {
        SceneManager.LoadScene(1);
    }

    public void showsceneCSH()
    {
        SceneManager.LoadScene(1);
    }

    public void showscenePathFind()
    {
        SceneManager.LoadScene(3);
    }
    public void showsceneTools()
    {
        SceneManager.LoadScene(1);
    }
    public void showsceneActDB()
    {
        SceneManager.LoadScene(2);
    }
    public void showMainScene()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
