//using NPOI.SS.Formula.Functions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConeDetect : MonoBehaviour
{
    public Text text;
    public Material yellow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("CollObj"))
        {
            //speed = 0;
            //coll.gameObject.GetComponent<MeshRenderer>().material = yellow;
            //Destroy(coll.gameObject);
            //coll.gameObject.
            text.text += coll.name + "\n";
        }
        //else
        //{
        //    x = score;
        //    SceneManager.LoadScene(0);
        //}

    }
}
