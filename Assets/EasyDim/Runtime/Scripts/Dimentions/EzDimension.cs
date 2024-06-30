using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EzDimension
{
    public enum DimDirection { X, Y, Z }
    public enum OffsetDirection { X, Y, Z }

    public class Ez_GetDistance : MonoBehaviour
    {
        static GameObject mainlineGO = new GameObject("MainLine");
        static LineRenderer mainLine = mainlineGO.gameObject.AddComponent<LineRenderer>();

        static GameObject numberGO = new GameObject("Number");
        static GameObject numberParent = new GameObject("NumberParent");
        static BoxCollider numberCol;

        static GameObject arrows = new GameObject("Arrows");
        static GameObject arrowAGO;
        static GameObject arrowBGO;

        static TextMeshPro number;
        static RawImage backgroundImg;

        static Vector2 numberBoundsSize;
        static Vector3 lineCenter;
        static Vector3 lineDirection;
        static Vector3 otherDirection;
        static Vector3 rotationForward;
        static Quaternion numberRotation;
        static Vector3 parentRotation;
        static float textRotation;

        public static void Create(Vector3 _pointA, Vector3 _pointB, float _lineTickness, float _textHeight, Color _dimColor, Transform _cameraCenter,
            GameObject _mainParent, GameObject _arrowPrefab, float _arrowHeigh)
        {
            // main line :
            mainlineGO.transform.parent = _mainParent.transform;
            // set values:
            mainLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            mainLine.startColor = _dimColor;
            mainLine.endColor = _dimColor;
            _lineTickness = Mathf.Abs(_lineTickness);
            mainLine.startWidth = _lineTickness;

            // number :
            numberGO.transform.parent = numberParent.transform;
            numberParent.transform.parent = _mainParent.transform;
            number = numberGO.AddComponent<TextMeshPro>();
            numberCol = numberGO.gameObject.AddComponent<BoxCollider>();


            // arrows :
            arrows.transform.parent = _mainParent.transform;
            arrowAGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowAGO.name = "Arrow A";
            arrowBGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowBGO.name = "Arrow B";
        }

        public static void Update(GameObject GO, Vector3 _pointA, Vector3 _pointB, float _lineTickness, float _textHeight, Color _dimColor, Transform _cameraCenter,
            GameObject _mainParent, float _arrowHeigh)
        {

            // main line > set values:
            mainLine.SetPosition(0, _pointA - ((_pointA - _pointB).normalized) * (_arrowHeigh / 50));
            mainLine.SetPosition(1, _pointB - ((_pointB - _pointA).normalized) * (_arrowHeigh / 50));
            mainLine.startColor = _dimColor;
            mainLine.endColor = _dimColor;
            _lineTickness = Mathf.Abs(_lineTickness);
            mainLine.startWidth = _lineTickness;

            // number :
            number.text = (_pointB - _pointA).magnitude.ToString("0.00");
            number.fontSize = _textHeight;
            number.enableWordWrapping = false;
            number.color = _dimColor;
            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;

            // rotation :
            lineDirection = (_pointB - _pointA).normalized;
            lineCenter = Vector3.Lerp(_pointA, _pointB, 0.5f);
            otherDirection = Vector3.Cross(lineDirection, (lineCenter - _cameraCenter.position));
            numberParent.transform.position = lineCenter;
            // **************1.5 should replace with a variable**************
            number.ForceMeshUpdate();
            number.rectTransform.localPosition = new Vector3(0, (number.bounds.extents.y * 1.5f) + (_lineTickness / 2), 0);
            rotationForward = Vector3.Cross(otherDirection, lineDirection);
            numberRotation = Quaternion.LookRotation(rotationForward, lineDirection);
            parentRotation = numberParent.transform.eulerAngles = numberRotation.eulerAngles - new Vector3(0, 0, 90f);
            textRotation = numberParent.transform.localEulerAngles.z;

            if (textRotation > 90 && textRotation <= 270)
                numberParent.transform.eulerAngles = parentRotation + new Vector3(0, 0, 180f);

            // arrows :
            arrowAGO.GetComponent<MeshRenderer>().sharedMaterial.color = _dimColor;
            
            arrowAGO.transform.localScale = new Vector3(_arrowHeigh, _arrowHeigh * 2, _arrowHeigh);
            arrowBGO.transform.localScale = new Vector3(_arrowHeigh, _arrowHeigh * 2, _arrowHeigh);
            arrowAGO.transform.position = _pointA;
            arrowAGO.transform.LookAt(lineCenter, Vector3.up);
            arrowAGO.transform.eulerAngles += new Vector3(-90, 0, 0);
            arrowBGO.transform.position = _pointB;
            arrowBGO.transform.LookAt(lineCenter, Vector3.up);
            arrowBGO.transform.eulerAngles += new Vector3(-90, 0, 0);

            // selection :
            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);
        }
    }
  
}

