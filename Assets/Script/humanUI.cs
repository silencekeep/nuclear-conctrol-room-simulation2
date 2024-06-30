using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanUI : MonoBehaviour
{
    public GameObject humanChange1, humanChange2, humanChange3, humanSelect, humanPanel, exitImage, humanInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HumanScene()
    {
        humanChange1.SetActive(true);
        humanSelect.SetActive(true);
        humanPanel.SetActive(true);
        exitImage.SetActive(true);
        humanInput.SetActive(true);
    }

    public void HideHumanScene()
    {
        humanChange1.SetActive(false);
        humanChange2.SetActive(false);
        humanChange3.SetActive(false);
        humanSelect.SetActive(false);
        humanPanel.SetActive(false);
        exitImage.SetActive(false);
        humanInput.SetActive(true);
    }

}
