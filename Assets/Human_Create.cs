using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Human_Create : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HumanCreateOnclick()
    {
        SceneManager.LoadScene("HumanModelScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
