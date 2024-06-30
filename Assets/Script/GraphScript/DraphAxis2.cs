﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraphAxis2 : MaskableGraphic
{/// <summary>
/// 原始代码参考(CSDN)
/// </summary>
        private RectTransform _myRect;
        private Vector2 _xPoint;
        private Vector2 _yPoint;

        //绘制坐标轴上的文字
        private void OnGUI()
        {
            
                Vector3 result = transform.localPosition;
                Vector3 realPosition = getScreenPosition(transform, ref result);
                GUIStyle guiStyleX = new GUIStyle();
                guiStyleX.normal.textColor = Color.black;
                guiStyleX.fontSize = 16;
                guiStyleX.fontStyle = FontStyle.Bold;
                guiStyleX.alignment = TextAnchor.MiddleLeft;
                GUI.Label(new Rect(local2Screen(realPosition, _xPoint) + new Vector2(20, 0), new Vector2(0, 0)), "XUnit", guiStyleX);

                GUIStyle guiStyleY = new GUIStyle();
                guiStyleY.normal.textColor = Color.black;
                guiStyleY.fontSize = 16;
                guiStyleY.fontStyle = FontStyle.Bold;
                guiStyleY.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(local2Screen(realPosition, _yPoint) - new Vector2(0, 20), new Vector2(0, 0)), "YUnit", guiStyleY);
            
        }

        /// <summary>
        /// 初始化函数信息
        /// </summary>
        private void Init()
        {
            _myRect = this.rectTransform;
            
        }

        /// <summary>
        /// 重写这个类以绘制UI
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Init();
            vh.Clear();

            #region 基础框架的绘制

            // 绘制X轴
            float lenght = _myRect.sizeDelta.x;
            Vector2 leftPoint = new Vector2(-lenght / 2.0f, 0);
            Vector2 rightPoint = new Vector2(lenght / 2.0f, 0);
            vh.AddUIVertexQuad(GetQuad(leftPoint, rightPoint, Color.black, 2.0f));
            // 绘制X轴的箭头
            float arrowUnit = 2.0f * 3;
            Vector2 firstPointX = rightPoint + new Vector2(0, arrowUnit);
            Vector2 secondPointX = rightPoint;
            Vector2 thirdPointX = rightPoint + new Vector2(0, -arrowUnit);
            Vector2 fourPointX = rightPoint + new Vector2(Mathf.Sqrt(3) * arrowUnit, 0);
            vh.AddUIVertexQuad(GetQuad(firstPointX, secondPointX, thirdPointX, fourPointX, Color.black));
           

              _xPoint = rightPoint;



        #region 刻度的绘制
     
        
                // X 轴的正方向
                for (int i = 1; i * 50f < _myRect.sizeDelta.x / 2.0f; i++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(50f * i, 0);
                    Vector2 secongPoint = firstPoint + new Vector2(0,5.0f);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, Color.black));
                }
                // X 轴的负方向
                for (int i = 1; i * -50f > -_myRect.sizeDelta.x / 2.0f; i++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(-50f * i, 0);
                    Vector2 secongPoint = firstPoint + new Vector2(0,5.0f);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, Color.black));
                }


        #endregion
        #endregion
    }

    //通过两个端点绘制矩形
    private UIVertex[] GetQuad(Vector2 startPos, Vector2 endPos, Color color0, float lineWidth = 2.0f)
        {
            float dis = Vector2.Distance(startPos, endPos);
            float y = lineWidth * 0.5f * (endPos.x - startPos.x) / dis;
            float x = lineWidth * 0.5f * (endPos.y - startPos.y) / dis;
            if (y <= 0) y = -y;
            else x = -x;
            UIVertex[] vertex = new UIVertex[4];
            vertex[0].position = new Vector3(startPos.x + x, startPos.y + y);
            vertex[1].position = new Vector3(endPos.x + x, endPos.y + y);
            vertex[2].position = new Vector3(endPos.x - x, endPos.y - y);
            vertex[3].position = new Vector3(startPos.x - x, startPos.y - y);
            for (int i = 0; i < vertex.Length; i++) vertex[i].color = color0;
            return vertex;
        }

        //通过四个顶点绘制矩形
        private UIVertex[] GetQuad(Vector2 first, Vector2 second, Vector2 third, Vector2 four, Color color0)
        {
            UIVertex[] vertexs = new UIVertex[4];
            vertexs[0] = GetUIVertex(first, color0);
            vertexs[1] = GetUIVertex(second, color0);
            vertexs[2] = GetUIVertex(third, color0);
            vertexs[3] = GetUIVertex(four, color0);
            return vertexs;
        }

        //构造UIVertex
        private UIVertex GetUIVertex(Vector2 point, Color color0)
        {
            UIVertex vertex = new UIVertex
            {
                position = point,
                color = color0,
                uv0 = new Vector2(0, 0)
            };
            return vertex;
        }

        //绘制虚线
        private void GetImaglinaryLine(ref VertexHelper vh, Vector2 first, Vector2 second, Color color0, float imaginaryLenght, float spaceingWidth, float lineWidth = 2.0f)
        {
            if (first.y.Equals(second.y)) //  X轴
            {
                Vector2 indexSecond = first + new Vector2(imaginaryLenght, 0);
                while (indexSecond.x < second.x)
                {
                    vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    first = indexSecond + new Vector2(spaceingWidth, 0);
                    indexSecond = first + new Vector2(imaginaryLenght, 0);
                    if (indexSecond.x > second.x)
                    {
                        indexSecond = new Vector2(second.x, indexSecond.y);
                        vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    }
                }
            }
            if (first.x.Equals(second.x)) //  Y轴
            {
                Vector2 indexSecond = first + new Vector2(0, imaginaryLenght);
                while (indexSecond.y < second.y)
                {
                    vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    first = indexSecond + new Vector2(0, spaceingWidth);
                    indexSecond = first + new Vector2(0, imaginaryLenght);
                    if (indexSecond.y > second.y)
                    {
                        indexSecond = new Vector2(indexSecond.x, second.y);
                        vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    }
                }
            }
        }

        //本地坐标转化屏幕坐标绘制GUI文字
        private Vector2 local2Screen(Vector2 parentPos, Vector2 localPosition)
        {
            Vector2 pos = localPosition + parentPos;
            float xValue, yValue = 0;
            if (pos.x > 0)
                xValue = pos.x + Screen.width / 2.0f;
            else
                xValue = Screen.width / 2.0f - Mathf.Abs(pos.x);
            if (pos.y > 0)
                yValue = Screen.height / 2.0f - pos.y;
            else
                yValue = Screen.height / 2.0f + Mathf.Abs(pos.y);
            return new Vector2(xValue, yValue);
        }

        //递归计算位置
        private Vector2 getScreenPosition(Transform trans, ref Vector3 result)
        {
            if (null != trans.parent && null != trans.parent.parent)
            {
                result += trans.parent.localPosition;
                getScreenPosition(trans.parent, ref result);
            }
            if (null != trans.parent && null == trans.parent.parent)
                return result;
            return result;
        }


    }

