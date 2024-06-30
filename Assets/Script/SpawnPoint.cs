using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject x0;
    [SerializeField] GameObject z0;

    // Update is called once per frame

    public void SetPointPosition()
    {
        gameObject.transform.position = new Vector3(z0.transform.localScale.x / 2, 0, x0.transform.localScale.z / 2);
    }
}
