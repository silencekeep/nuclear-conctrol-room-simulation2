using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callback : MonoBehaviour
{
    public bool IsSuccessCallBack = true;
    public GameObject menu11,menu21, menu31,menu41, menu51;
    private delegate void VoidDelegate();
    [ContextMenu("Build")]
    public void Build()
    {
        StartCoroutine(Builder(SuccessCallBack, FailureCallBack));
    }

    private IEnumerator Builder(VoidDelegate success, VoidDelegate failure)
    {
        if (!IsSuccessCallBack)
        {
            failure();
            yield break;
        }
        //前面是种种导致执行失败的判断
        success();
    }

    private void SuccessCallBack()
    {
        menu11.SetActive(false);
        menu21.SetActive(false);
        menu31.SetActive(false);
        menu41.SetActive(false);
        menu51.SetActive(false);
        Debug.Log("该函数执行成功");
    }

    private void FailureCallBack()
    {
        Debug.Log("该函数执行失败");
    }
}