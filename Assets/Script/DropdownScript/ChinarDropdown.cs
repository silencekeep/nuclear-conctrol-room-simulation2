using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 将下拉菜单上的 Dropdown组件移除，替换为该脚本
/// </summary>
public class ChinarDropdown : MonoBehaviour
{    //设置ScrollRect变量

    ScrollRect rect;
       
    void Start()

    {

        //获取 ScrollRect变量

        rect = this.GetComponent<ScrollRect>();

    }

    void Update()

    {

        //在Update函数中调用ScrollValue函数

        ScrollValue();

    }

    private void ScrollValue()

    {

        //当对应值超过1，重新开始从 0 开始

        if (rect.horizontalNormalizedPosition > 1.0f)

        {

            rect.horizontalNormalizedPosition = 0;

        }

        //逐渐递增 ScrollRect 水平方向上的值

        rect.horizontalNormalizedPosition = rect.horizontalNormalizedPosition + 0.05f * Time.deltaTime;

    }

}