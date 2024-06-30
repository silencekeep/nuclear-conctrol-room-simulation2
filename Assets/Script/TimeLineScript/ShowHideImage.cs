using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHideImage : MonoBehaviour
{
    private float alpha = 0.0f;
    private float alphaSpeed = 2.0f;

    private CanvasGroup cg;

    void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        //if (alpha != cg.alpha)
        //{
        //    cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpeed * Time.deltaTime);
        //    if (Mathf.Abs(alpha - cg.alpha) <= 0.01)
        //    {
        //        cg.alpha = alpha;
        //    }
        //}
    }

    public void Show()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
        cg.alpha = 1;

        cg.blocksRaycasts = true;//可以和该UI对象交互
    }

    public void Hide()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
        cg.alpha = 0.4f;

        cg.blocksRaycasts = false;//不可以和该UI对象交互
    }
}
    
 

