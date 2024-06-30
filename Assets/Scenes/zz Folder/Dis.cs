using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dis : MonoBehaviour
{
    public float dis;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cube.transform.position, cube.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

        }
        float distance = 0f;
        if (hit.collider == null)
        {
            distance = 0f;
        }
        else
        {
            distance = Vector3.Distance(transform.position, hit.collider.gameObject.GetComponent<Collider>().bounds.center) - hit.collider.gameObject.GetComponent<Collider>().bounds.size.z / 2 - 1.36f;

        }
        dis = distance;


    }
}
