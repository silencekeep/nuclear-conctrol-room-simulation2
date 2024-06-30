using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CubeSectionExample : MonoBehaviour {
    public GameObject hatchedCube;

	void Start () {

        Shader.DisableKeyword("CLIP_PLANE");
        Shader.DisableKeyword("CLIP_CUBE");
        Renderer[] allrenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allrenderers)
        {
            Material[] mats = r.sharedMaterials;
            foreach (Material m in mats) if (m.shader.name.Substring(0, 13) == "CrossSection/") m.DisableKeyword("CLIP_PLANE");
        }
        Shader.SetGlobalColor("_SectionColor", Color.black);
        Matrix4x4 mx = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Shader.SetGlobalMatrix("_WorldToBoxMatrix", mx.inverse);
        //Shader.SetGlobalVector("_SectionDirX", transform.right);
        //Shader.SetGlobalVector("_SectionDirY", transform.up);
        //Shader.SetGlobalVector("_SectionDirZ", transform.forward);
    }
	
	void Update () {
        //Shader.SetGlobalFloat("_Radius", 0.2f);
        //return;
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                if (hit.transform.IsChildOf(transform))
                {
                    Debug.Log("hit");
                    Shader.EnableKeyword("CLIP_CUBE");
                    Shader.SetGlobalVector("_SectionPoint", hit.point);
                    Shader.SetGlobalVector("_SectionPlane", hit.normal);
                    Vector3 upVector = Vector3.Cross(hit.transform.up, hit.normal).normalized;
                    Shader.SetGlobalVector("_SectionPlane2",upVector);
                    Shader.SetGlobalFloat("_Radius", 0.05f);
                    if (hatchedCube)
                    {
                        Quaternion rot = Quaternion.LookRotation(hit.normal, upVector);
                        hatchedCube.transform.position = hit.point;
                        hatchedCube.transform.rotation = rot;
                        StartCoroutine(drag(hatchedCube.transform));
                    }
                    else
                    {
                        StartCoroutine(drag());
                    }
                }
            }
        }
	}

    void OnEnable()
    {
        Shader.EnableKeyword("CLIP_CUBE");
        //Shader.EnableKeyword("CLIP_PLANE");
    }

    void OnDisable()
    {
        Shader.DisableKeyword("CLIP_CUBE");
        //Shader.DisableKeyword("CLIP_PLANE");
    }

    void OnApplicationQuit()
    {
        //disable clipping so we could see the materials and objects in editor properly
        Shader.DisableKeyword("CLIP_CUBE");

    }


    IEnumerator drag()
    {
        float cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 translation = Vector3.zero;
        Camera.main.GetComponent<MaxCamera>().enabled = false;
        while (Input.GetMouseButton(0))
        {
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            float m = translation.magnitude;
            if(m>0.05f) Shader.SetGlobalFloat("_Radius", m);
            yield return null;
        }
        Camera.main.GetComponent<MaxCamera>().enabled = true;
        
    }

    IEnumerator drag(Transform hatchCube)
    {
        float cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        hatchCube.localScale = 0.1f * Vector3.one;
        hatchCube.gameObject.SetActive(true);
        Vector3 translation = Vector3.zero;
        Camera.main.GetComponent<MaxCamera>().enabled = false;
        while (Input.GetMouseButton(0))
        {
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            float m = translation.magnitude;
            if (m > 0.05f)
            {
                Shader.SetGlobalFloat("_Radius", m);
                hatchCube.localScale = 2*m * Vector3.one;
            }
            yield return null;
        }

        Camera.main.GetComponent<MaxCamera>().enabled = true;
        //hatchCube.gameObject.SetActive(false);
    }
}
