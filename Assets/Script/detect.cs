using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect : MonoBehaviour
{
    float speed = 100;
    public Material yellow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float y = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            y = 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            y = -1;
        }
        this.transform.Translate(new Vector3(h*2, y*2, v*2) * speed * Time.deltaTime);
        //float TranslateSpeed = 10f;

        ////transform.Translate(transform.forward * speed * Time.deltaTime);
        //transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.CompareTag("zone"))
    //    {
    //        speed = 0;
    //        GetComponent<MeshRenderer>().material = yellow;
    //        //Destroy(coll.gameObject);
    //        //coll.gameObject.

    //    }
    //    //else
    //    //{
    //    //    x = score;
    //    //    SceneManager.LoadScene(0);
    //    //}

    //}
}
