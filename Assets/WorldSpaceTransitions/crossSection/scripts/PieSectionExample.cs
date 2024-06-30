using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PieSectionExample : MonoBehaviour {

    //private Material sMat;
    private Vector3 normal1;
    private Vector3 normal2;
    public float angle = 60f;
    [Range(0.5f, 12)]
    public float angleIncrement = 1f;
    public Transform quadPlane1;
    public Transform quadPlane2;
    // Use this for initialization
    void Start () {
        Shader.DisableKeyword("CLIP_PLANE");
        Shader.EnableKeyword("CLIP_TWO_PLANES");
        //we have declared: "material.EnableKeyword("CLIP_PLANE");" on all the crossSectionStandard derived materials - in the CrossSectionStdShaderGUI editor script - so we have to switch it off
        Renderer[] allrenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allrenderers)
        {
            Material[] mats = r.sharedMaterials;
            foreach (Material m in mats) m.DisableKeyword("CLIP_PLANE");
        }
        normal1 = transform.right;
        normal2 = Quaternion.AngleAxis(angle, transform.up) * normal1;
        Shader.SetGlobalVector("_SectionPoint", transform.position);
        Shader.SetGlobalVector("_SectionPlane", transform.right);
        Shader.SetGlobalVector("_SectionPlane2", normal2);
        if (quadPlane2) quadPlane2.rotation = Quaternion.LookRotation(normal2, transform.up);
        if (quadPlane1) quadPlane1.rotation = Quaternion.LookRotation(normal1, -transform.up);
    }
	
	// Update is called once per frame
	void Update () {
        angle = (angle + angleIncrement) %360f;
        normal2 = Quaternion.AngleAxis(angle, transform.up) * transform.right;
        Shader.SetGlobalVector("_SectionPlane2", normal2);
        if (quadPlane2) quadPlane2.rotation = Quaternion.LookRotation(normal2, transform.up);

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Collider coll = gameObject.GetComponent<Collider>();
            if (coll.Raycast(ray, out hit, 10000f)) StartCoroutine(drag());
        }
	}

    void OnEnable()
    {
        Shader.EnableKeyword("CLIP_TWO_PLANES");
        //Shader.EnableKeyword("CLIP_PLANE");
    }

    void OnDisable()
    {
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        //Shader.DisableKeyword("CLIP_PLANE");
    }

    void OnApplicationQuit()
    {
        //disable clipping so we could see the materials and objects in editor properly
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        Shader.DisableKeyword("CLIP_PLANE");
    }


    IEnumerator drag()
    {
        float cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 startNormal = normal1;
        Vector3 translation = Vector3.zero;
        Camera.main.GetComponent<MaxCamera>().enabled = false;
        while (Input.GetMouseButton(0))
        {
            translation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            normal1 = Quaternion.AngleAxis(50f * Vector3.Dot(translation, transform.up), transform.up) * startNormal;
            Shader.SetGlobalVector("_SectionPlane", normal1);
            if (quadPlane1) quadPlane1.rotation = Quaternion.LookRotation(normal1, -transform.up);
            yield return null;
        }
        Camera.main.GetComponent<MaxCamera>().enabled = true;
        
    }













}
