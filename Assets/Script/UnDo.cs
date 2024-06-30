using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Rigist(gameObject);    //注册该物体
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        GameManager.Instance.Remove(gameObject);
    }
}
