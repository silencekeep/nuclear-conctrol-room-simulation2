using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using EzDimension;
using EzDimension.dims;

namespace EzDimension.dims
{
    public class PointToPointDimension : MonoBehaviour
    {

        [Header("Always Individual :")]
        [Space]
        public EzDimStarter valuesReference;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global & Individual :")]
        [Space]
        [Header("—— Parameters :")]
        [Space]
        public bool isIndividual = false;
        public bool isDynamic = true;
        public float lineThickness = 0.02f;
        public float arrowHeight = 4f;
        public float textSize = 0.01f;
        public float NormalOffset;
        public float textTowardsCameraOffset = 0.001f;
        public float textOffset = 1.5f;
        [Space]
        public Color numberColor = new Color(0, 0, 0, 1);
        public Color mainColor = new Color(0, 0, 0, 1);
        public Color arrowColor = new Color(0, 0, 0, 1);
        [Space]
        public Color hoveredTint = new Color(0, 0.3f, 0.5f, 1);
        public Color selectedTint = new Color(0, 0.7f, 1, 1);
        public Color hoveredOnSelectedTint = new Color(0.5f, 0.8f, 1, 1);
        [Space]
        public Material mainLineMat;
        public Material arrowMat;

        [HideInInspector]
        public GameObject arrowPrefab;
        [HideInInspector]
        public Transform cameraTransform;
        [HideInInspector]
        public Camera rendererCamera;

        [HideInInspector]
        public GameObject mainParent;
        [HideInInspector]
        public Vector3 pointA = Vector3.zero;
        [HideInInspector]
        public Vector3 pointB = Vector3.one;
        [HideInInspector]
        public bool secondDrawStep = false;
        [HideInInspector]
        public bool isDone = false;
        [HideInInspector]
        public Vector3 firstPointHitNormal;

        // dynamic length :
        GameObject dynamicTransforms;
        [HideInInspector]
        public GameObject objectATransformGO;
        [HideInInspector]
        public GameObject objectBTransformGO;
        [HideInInspector]
        public GameObject pointATransformGO;
        [HideInInspector]
        public GameObject pointBTransformGO;
        [HideInInspector]
        public GameObject objectA;
        [HideInInspector]
        public GameObject objectB;

        GameObject mainLineGO;
        LineRenderer mainLine;
        GameObject numberGO;
        GameObject numberParent;
        BoxCollider numberCol;
        GameObject arrows;
        GameObject arrowAGO;
        GameObject arrowBGO;
        TextMeshPro number;
        float textRotation;
        Vector2 numberBoundsSize;
        Vector2 oldMousePos;
        Vector3 lineCenter;
        Vector3 lineDirection;
        Vector3 otherDirection;
        Vector3 rotationForward;
        Vector3 parentRotation;
        Quaternion numberRotation;
        string unitTextShortForm;
        // end create and update
        private void Awake()
        {
            // value reference :
            valuesReference = GetComponentInParent<EzDimStarter>();
            isDynamic = valuesReference.isDynamic;
            lineThickness = valuesReference.linesThickness;
            arrowHeight = valuesReference.arrowHeight;
            textSize = valuesReference.textSize;
            NormalOffset = valuesReference.hitNormalOffset;
            textOffset = valuesReference.textOffset;
            textTowardsCameraOffset = valuesReference.textTowardsCameraOffset;
            arrowPrefab = valuesReference.arrowPrefab;
            cameraTransform = valuesReference.cameraTransform;
            rendererCamera = valuesReference.rendererCamera;
            // materials :
            mainLineMat = new Material(valuesReference.mainLineMaterial);
            arrowMat = new Material(valuesReference.arrowMaterial);
            // colors :
            numberColor = valuesReference.numberColor;
            mainColor = valuesReference.mainColor;
            arrowColor = valuesReference.arrowColor;
            hoveredTint = valuesReference.hoveredTint;
            selectedTint = valuesReference.selectedTint;
            hoveredOnSelectedTint = valuesReference.hoveredOnSelectedTint;

            mainParent = this.gameObject;
        }

        void Start()
        {
            CreateDimension(mainParent, arrowPrefab);
            Funcs.SetActiveAllChilds(this.transform, false);
        }

        public void CreateDimension(GameObject _mainParent, GameObject _arrowPrefab)
        {
            mainLineGO = new GameObject("MainLine");
            mainLine = mainLineGO.gameObject.AddComponent<LineRenderer>();
            numberGO = new GameObject("EzDimensionDistanceNumber");
            numberParent = new GameObject("NumberParent");
            arrows = new GameObject("Arrows");

            // dynamic objects :
            dynamicTransforms = new GameObject("dynamicTransforms");
            objectATransformGO = new GameObject("objectATransformGO");
            objectBTransformGO = new GameObject("objectBTransformGO");
            pointATransformGO = new GameObject("pointATransformGO");
            pointBTransformGO = new GameObject("pointBTransformGO");
            dynamicTransforms.transform.parent = this.transform;
            objectATransformGO.transform.parent = dynamicTransforms.transform;
            objectBTransformGO.transform.parent = dynamicTransforms.transform;
            pointATransformGO.transform.parent = objectATransformGO.transform;
            pointBTransformGO.transform.parent = objectBTransformGO.transform;

            // main line :
            mainLineGO.transform.parent = _mainParent.transform;

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

            // turn off shadow
            // main line
            mainLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mainLine.receiveShadows = false;
            // arrows
            arrowAGO.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            arrowAGO.GetComponent<MeshRenderer>().receiveShadows = false;
            arrowBGO.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            arrowBGO.GetComponent<MeshRenderer>().receiveShadows = false;


        }

        public void UpdateDimension(Vector3 _pointA, Vector3 _pointB, float _lineThickness, float _textSize, float _textOffset, Color _numberColor, Color _mainColor, Color _arrowColor,
            Material _mainMaterial, Material _arrowMaterial, Transform _cameraTransform,
            GameObject _mainParent, float _arrowHeigh, float _unitMultiplier, float _textLocalYOffset, float _normalOffset)
        {
            // materials :
            mainLine.material = _mainMaterial;
            arrowAGO.GetComponent<MeshRenderer>().material = _arrowMaterial;
            arrowBGO.GetComponent<MeshRenderer>().material = arrowAGO.GetComponent<MeshRenderer>().material;
            // colors :
            mainLine.material.color = _mainColor;
            number.color = _numberColor.linear;
            arrowAGO.GetComponent<MeshRenderer>().sharedMaterial.color = _arrowColor;
            // main line :
            mainLine.SetPosition(0, _pointA - ((_pointA - _pointB).normalized) * (_arrowHeigh / 50) + firstPointHitNormal * _normalOffset);
            mainLine.SetPosition(1, _pointB - ((_pointB - _pointA).normalized) * (_arrowHeigh / 50) + firstPointHitNormal * _normalOffset);
            _lineThickness = Mathf.Abs(_lineThickness);
            mainLine.startWidth = _lineThickness;

            // number :
            if (valuesReference.showUnitAfterNumber)
                unitTextShortForm = internalFuncs.GetUnitTextShortForm(valuesReference);
            else
                unitTextShortForm = "";

            number.text = ((_pointB - _pointA).magnitude * _unitMultiplier).ToString(internalFuncs.DecimalCalculator(valuesReference)) + " " + unitTextShortForm;
            number.fontSize = _textSize;
            number.enableWordWrapping = false;
            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;

            // rotation :
            UpdateTextTransform(_pointA, _pointB, _cameraTransform, _textOffset, _lineThickness, _textLocalYOffset, _normalOffset);

            // arrows :
            arrowAGO.transform.localScale = new Vector3(_arrowHeigh, _arrowHeigh * 2, _arrowHeigh);
            arrowBGO.transform.localScale = new Vector3(_arrowHeigh, _arrowHeigh * 2, _arrowHeigh);
            arrowAGO.transform.position = _pointA;
            arrowAGO.transform.LookAt(lineCenter, Vector3.up);
            arrowAGO.transform.eulerAngles += new Vector3(-90, 0, 0);
            arrowBGO.transform.position = _pointB;
            arrowBGO.transform.LookAt(lineCenter, Vector3.up);
            arrowBGO.transform.eulerAngles += new Vector3(-90, 0, 0);

            // hit normal offset
            arrowAGO.transform.localPosition += firstPointHitNormal * _normalOffset;
            arrowBGO.transform.localPosition += firstPointHitNormal * _normalOffset;

            // update number collider (to select dimension we should click on a child that has a colider, the script will get its root and will select it. the only selectable part is number now)
            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);
        }

        public void UpdateTextTransform(Vector3 _pointA, Vector3 _pointB, Transform _cameraTransform, float _textOffset,
            float _lineThickness, float _textLocalYOffset, float _normalOffset)
        {
            Vector2 lineCenterOnScreen;
            Vector2 numberCenterOnScreen;

            lineDirection = (_pointB - _pointA).normalized;
            lineCenter = Vector3.Lerp(_pointA, _pointB, 0.5f);
            otherDirection = Vector3.Cross(lineDirection, (lineCenter - _cameraTransform.position));
            numberParent.transform.position = lineCenter;
            number.ForceMeshUpdate();
            number.rectTransform.localPosition = new Vector3(0, (number.bounds.extents.y * _textOffset) + (_lineThickness / 2), 0);

            rotationForward = Vector3.Cross(otherDirection, lineDirection);
            numberRotation = Quaternion.LookRotation(rotationForward, lineDirection);
            parentRotation = numberParent.transform.eulerAngles = numberRotation.eulerAngles - new Vector3(0, 0, 90f);
            textRotation = numberParent.transform.localEulerAngles.z;

            if (textRotation > 90 && textRotation <= 270)
                numberParent.transform.eulerAngles = parentRotation + new Vector3(0, 0, 180f);

            lineCenterOnScreen = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(lineCenter);
            numberCenterOnScreen = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(numberGO.transform.position);

            if (lineCenterOnScreen.y > numberCenterOnScreen.y)
            {
                numberParent.transform.eulerAngles += new Vector3(0, 0, 180f);
            }

            // hit normal offset
            number.rectTransform.position += firstPointHitNormal * _normalOffset;
            // offset text towards camera
            number.rectTransform.position += (_cameraTransform.position - numberGO.transform.position).normalized * _textLocalYOffset;
        }
    }
}