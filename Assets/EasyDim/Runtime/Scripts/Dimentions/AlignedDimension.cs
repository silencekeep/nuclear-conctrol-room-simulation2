using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using EzDimension;

namespace EzDimension.dims
{
    public class AlignedDimension : MonoBehaviour
    {
        [Header("Always Individual :")]
        [Space]
        public EzDimStarter valuesReference;
        [Space]
        [Space]
        public Funcs.MeasurementPlane measurementPlane;
        public float offsetDistance;
        public bool reverseDirection;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global & Individual :")]
        [Space]
        [Header("—— Parameters :")]
        [Space]
        public bool isIndividual = false;
        public bool isDynamic = true;
        [Space]
        public float mainLineThickness = 1f;
        public float arrowHeight = 1f;
        public float textSize = 1f;
        public float NormalOffset;
        public float textTowardsCameraOffset = 0.00f;
        public float textOffset = 1.5f;
        public float secondaryLinesThickness = 1f;
        public float secondaryLinesExtend;
        public Vector3 DirectionPointInFreeMode = Vector3.zero;
        [Space]
        public bool autoTextPosition = true;
        public bool flipTextPosition = false;
        [Space]
        [Header("—— Colors :")]
        [Space]
        public Color numberColor = new Color(0, 0, 0, 1);
        public Color mainColor = new Color(0, 0, 0, 1);
        public Color secondaryColor = new Color(0, 0, 0, 1);
        public Color arrowColor = new Color(0, 0, 0, 1);
        [Space]
        public Color hoveredTint = new Color(0, 0.3f, 0.5f, 1);
        public Color selectedTint = new Color(0, 0.7f, 1, 1);
        public Color hoveredOnSelectedTint = new Color(0.5f, 0.8f, 1, 1);
        [Space]
        [Header("—— Materials :")]
        [Space]
        public Material mainLineMat;
        public Material secondaryLinesMat;
        public Material arrowMat;

        [HideInInspector]
        public GameObject arrowPrefab;
        [HideInInspector]
        public GameObject mainParent;
        [HideInInspector]
        public Transform cameraTransform;
        [HideInInspector]
        public Vector3 firstPointHitNormal;
        [HideInInspector]
        public Vector3 pointA = Vector3.zero;
        [HideInInspector]
        public Vector3 pointB = Vector3.one;

        [HideInInspector]
        public bool secondDrawStep = false;
        [HideInInspector]
        public bool isDone = false;
        [HideInInspector]

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

        Vector3 pointAProxy;
        Vector3 pointBProxy;
        Vector3 alignedPointB;
        // create and update:
        float unitMultiplier;
        GameObject mainLineGO;
        GameObject mainLineSmallAGO;
        GameObject mainLineSmallBGO;
        GameObject secondaryLineAGO;
        GameObject secondaryLineBGO;
        LineRenderer mainLine;
        LineRenderer secondaryLineA;
        LineRenderer secondaryLineB;
        LineRenderer mainlineSmallA;
        LineRenderer mainlineSmallB;
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
            mainLineGO = new GameObject("MainLine");
            mainLineSmallAGO = new GameObject("Small Line A");
            mainLineSmallBGO = new GameObject("Small Line B");
            secondaryLineAGO = new GameObject("Proxy Line A");
            secondaryLineBGO = new GameObject("Proxy Line B");
            mainLine = mainLineGO.gameObject.AddComponent<LineRenderer>();
            mainlineSmallA = mainLineSmallAGO.AddComponent<LineRenderer>();
            mainlineSmallB = mainLineSmallBGO.AddComponent<LineRenderer>();
            secondaryLineA = secondaryLineAGO.gameObject.AddComponent<LineRenderer>();
            secondaryLineB = secondaryLineBGO.gameObject.AddComponent<LineRenderer>();
            numberGO = new GameObject("EzDimensionDistanceNumber");
            numberParent = new GameObject("NumberParent");
            arrows = new GameObject("Arrows");
            mainParent = this.gameObject;

            // value reference
            valuesReference = GetComponentInParent<EzDimStarter>();
            unitMultiplier = valuesReference.customUnitMultiplier;
            arrowHeight = valuesReference.arrowHeight;
            arrowPrefab = valuesReference.arrowPrefab;
            mainLineThickness = valuesReference.linesThickness;
            secondaryLinesThickness = valuesReference.secondaryLinesThickness;
            secondaryLinesExtend = valuesReference.secondaryLinesExtend;
            textSize = valuesReference.textSize;
            NormalOffset = valuesReference.hitNormalOffset;
            textTowardsCameraOffset = valuesReference.textTowardsCameraOffset;
            textOffset = valuesReference.textOffset;
            autoTextPosition = valuesReference.autoTextPosition;
            flipTextPosition = valuesReference.flipTextPosition;
            cameraTransform = valuesReference.cameraTransform;
            // materials :
            mainLineMat = new Material(valuesReference.mainLineMaterial);
            secondaryLinesMat = new Material(valuesReference.secondaryLinesMaterial);
            arrowMat = new Material(valuesReference.arrowMaterial);
            //colors :
            numberColor = valuesReference.numberColor;
            mainColor = valuesReference.mainColor;
            secondaryColor = valuesReference.secondaryColor;
            arrowColor = valuesReference.arrowColor;
            hoveredTint = valuesReference.hoveredTint;
            selectedTint = valuesReference.selectedTint;
            hoveredOnSelectedTint = valuesReference.hoveredOnSelectedTint;
            // aligned dimension additional settings:
            measurementPlane = valuesReference.measurementPlane;
            offsetDistance = valuesReference.offsetDistance;
            reverseDirection = valuesReference.reverseDirection;
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
        }

        void Start()
        {
            CreateDimension(mainParent, arrowPrefab);
            Funcs.SetActiveAllChilds(this.transform, false);
        }


        public void CreateDimension(GameObject _mainParent, GameObject _arrowPrefab)
        {
            // lines :
            mainLineGO.transform.parent = _mainParent.transform;
            secondaryLineAGO.transform.parent = _mainParent.transform;
            secondaryLineBGO.transform.parent = _mainParent.transform;
            mainLineSmallAGO.transform.parent = _mainParent.transform;
            mainLineSmallBGO.transform.parent = _mainParent.transform;
            mainLineSmallAGO.SetActive(false);
            mainLineSmallBGO.SetActive(false);

            // number :
            numberGO.transform.parent = numberParent.transform;
            numberParent.transform.parent = _mainParent.transform;
            number = numberGO.AddComponent<TextMeshPro>();
            numberCol = numberGO.gameObject.AddComponent<BoxCollider>();
            numberCol.enabled = false; // true when dimension created.

            // arrows :
            arrows.transform.parent = _mainParent.transform;
            arrowAGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowAGO.name = "Arrow A";
            arrowBGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowBGO.name = "Arrow B";

            // materials :
            mainLine.material = mainLineMat;
            mainlineSmallA.material = mainLine.material;
            mainlineSmallB.material = mainLine.material;
            secondaryLineA.material = secondaryLinesMat;
            secondaryLineB.material = secondaryLineA.material;
            arrowAGO.GetComponent<MeshRenderer>().material = mainLineMat;
            arrowBGO.GetComponent<MeshRenderer>().material = arrowAGO.GetComponent<MeshRenderer>().material;

            secondaryLineA.textureMode = LineTextureMode.Tile;
            secondaryLineB.textureMode = LineTextureMode.Tile;

            // turn off shadow
            // main line
            mainLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mainLine.receiveShadows = false;
            // secondary lines
            secondaryLineA.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            secondaryLineA.receiveShadows = false;
            secondaryLineB.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            secondaryLineB.receiveShadows = false;
            // small lines
            mainlineSmallA.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mainlineSmallA.receiveShadows = false;
            mainlineSmallB.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mainlineSmallB.receiveShadows = false;
            // arrows
            arrowAGO.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            arrowAGO.GetComponent<MeshRenderer>().receiveShadows = false;
            arrowBGO.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            arrowBGO.GetComponent<MeshRenderer>().receiveShadows = false;
        }

        public void UpdateDimension(Vector3 _pointA, Vector3 _pointB, float _mainLineThickness, float _secondaryLinesThickness, float _secondaryLineExtend, float _textSize, float _textOffset,
            Color _numberColor, Color _mainColor, Color _secondaryColor, Color _arrowColor, Material _mainMaterial, Material _secondaryMaterial, Material _arrowMaterial, Transform _cameraTransform,
            GameObject _mainParent, float _arrowHeight, Funcs.MeasurementPlane _measurmentPlane, float _offsetDistance, float _unitMultiplier, bool _autoTextPosition,
            bool _flipTextPosition, float _textLocalYOffset, float _normalOffset)
        {
            if (offsetDistance < 0)
                _secondaryLineExtend = -_secondaryLineExtend;

            lineCenter = Vector3.Lerp(_pointA, _pointB, 0.5f);
            Vector3 LineDirection = (_pointB - _pointA).normalized;
            Plane plane = new Plane(LineDirection, lineCenter);

            // materials :
            mainLine.material = _mainMaterial;
            mainlineSmallA.material = mainLine.material;
            mainlineSmallB.material = mainLine.material;
            secondaryLineA.material = secondaryLineB.material = _secondaryMaterial;
            arrowAGO.GetComponent<MeshRenderer>().material = _arrowMaterial;
            arrowBGO.GetComponent<MeshRenderer>().material = arrowAGO.GetComponent<MeshRenderer>().material;

            // colors :
            number.color = _numberColor.linear;
            mainLine.material.color = mainlineSmallA.material.color = mainlineSmallB.material.color = _mainColor;
            secondaryLineA.material.color = secondaryLineB.material.color = _secondaryColor;
            arrowAGO.GetComponent<MeshRenderer>().sharedMaterial.color = arrowBGO.GetComponent<MeshRenderer>().sharedMaterial.color = _arrowColor;

            Vector3 offestDirection;
            if (_measurmentPlane == Funcs.MeasurementPlane.XZ)
            {
                alignedPointB = new Vector3(_pointB.x, _pointA.y, _pointB.z);
                offestDirection = Vector3.Cross((alignedPointB - _pointA).normalized, Vector3.up);

                if (DirectionPointInFreeMode == Vector3.zero)
                    DirectionPointInFreeMode = plane.ClosestPointOnPlane(lineCenter + Vector3.up);
            }
            else if (_measurmentPlane == Funcs.MeasurementPlane.ZY)
            {
                alignedPointB = new Vector3(_pointA.x, _pointB.y, _pointB.z);
                offestDirection = Vector3.Cross((alignedPointB - _pointA).normalized, Vector3.right);

                if (DirectionPointInFreeMode == Vector3.zero)
                    DirectionPointInFreeMode = plane.ClosestPointOnPlane(lineCenter + Vector3.right);
            }
            else if (_measurmentPlane == Funcs.MeasurementPlane.XY)
            {
                alignedPointB = new Vector3(_pointB.x, _pointB.y, _pointA.z);
                offestDirection = Vector3.Cross((alignedPointB - _pointA).normalized, Vector3.forward);

                if (DirectionPointInFreeMode == Vector3.zero)
                    DirectionPointInFreeMode = plane.ClosestPointOnPlane(lineCenter + Vector3.forward);
            }
            else
            {

                alignedPointB = _pointB;
                offestDirection = (DirectionPointInFreeMode - lineCenter).normalized;
            }

            if (reverseDirection)
                offestDirection *= -1;

            pointAProxy = _pointA + (offestDirection * _offsetDistance);
            pointBProxy = alignedPointB + (offestDirection * _offsetDistance);

            // lines :
            mainLine.SetPosition(0, pointAProxy - ((pointAProxy - pointBProxy).normalized) * (_arrowHeight / 50) + firstPointHitNormal * _normalOffset);
            mainLine.SetPosition(1, pointBProxy - ((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 50) + firstPointHitNormal * _normalOffset);
            secondaryLineA.SetPosition(0, _pointA + firstPointHitNormal * _normalOffset);
            secondaryLineA.SetPosition(1, pointAProxy + (offestDirection * _secondaryLineExtend) + firstPointHitNormal * _normalOffset);
            secondaryLineB.SetPosition(0, alignedPointB + firstPointHitNormal * _normalOffset);
            secondaryLineB.SetPosition(1, pointBProxy + (offestDirection * _secondaryLineExtend) + firstPointHitNormal * _normalOffset);
            _mainLineThickness = Mathf.Abs(_mainLineThickness);
            _secondaryLinesThickness = Mathf.Abs(_secondaryLinesThickness);
            mainLine.startWidth = _mainLineThickness;
            mainlineSmallA.startWidth = _mainLineThickness;
            mainlineSmallB.startWidth = _mainLineThickness;
            secondaryLineA.startWidth = _secondaryLinesThickness;
            secondaryLineB.startWidth = _secondaryLinesThickness;

            // number :
            if (valuesReference.showUnitAfterNumber)
                unitTextShortForm = internalFuncs.GetUnitTextShortForm(valuesReference);
            else
                unitTextShortForm = "";

            number.text = ((pointBProxy - pointAProxy).magnitude * _unitMultiplier).ToString(internalFuncs.DecimalCalculator(valuesReference)) + " " + unitTextShortForm;
            number.fontSize = _textSize;
            number.enableWordWrapping = false;
            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;

            // rotation :
            lineCenter = Vector3.Lerp(pointAProxy, pointBProxy, 0.5f);
            UpdateTextTransform(_cameraTransform, _textOffset, _mainLineThickness, _arrowHeight, _autoTextPosition, _flipTextPosition,
                _textLocalYOffset, _normalOffset);

            // update number collider (to select dimension we should click on a child that has a colider, the script will get its root and will select it. the only selectable part is number now)
            if (isDone)
                if (numberCol.enabled == false)
                    numberCol.enabled = true;

            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);
        }

        public void UpdateTextTransform(Transform _cameraTransform, float _textOffset, float _mainLineThickness, float _arrowHeight
            , bool _autoTextPosition, bool _flipTextPosition, float _textLocalYOffset, float _normalOffset)
        {
            lineDirection = (pointBProxy - pointAProxy).normalized;
            otherDirection = Vector3.Cross(lineDirection, (lineCenter - _cameraTransform.position));
            numberParent.transform.position = lineCenter;
            number.ForceMeshUpdate();
            number.rectTransform.localPosition = new Vector3(0, (number.bounds.extents.y * _textOffset) + (_mainLineThickness / 2), 0);

            rotationForward = Vector3.Cross(otherDirection, lineDirection);
            numberRotation = Quaternion.LookRotation(rotationForward, lineDirection);
            parentRotation = numberParent.transform.eulerAngles = numberRotation.eulerAngles - new Vector3(0, 0, 90f);
            textRotation = numberParent.transform.localEulerAngles.z;

            if (textRotation > 90 && textRotation <= 270)
                numberParent.transform.eulerAngles = parentRotation + new Vector3(0, 0, 180f);

            // arrows :
            arrowAGO.transform.localScale = new Vector3(_arrowHeight, _arrowHeight * 2, _arrowHeight);
            arrowBGO.transform.localScale = new Vector3(_arrowHeight, _arrowHeight * 2, _arrowHeight);
            arrowAGO.transform.position = pointAProxy;
            arrowAGO.transform.LookAt(lineCenter, Vector3.up);
            arrowAGO.transform.eulerAngles += new Vector3(-90, 0, 0);
            arrowBGO.transform.position = pointBProxy;
            arrowBGO.transform.LookAt(lineCenter, Vector3.up);
            arrowBGO.transform.eulerAngles += new Vector3(-90, 0, 0);

            if (_autoTextPosition && ((pointAProxy - pointBProxy).magnitude < number.bounds.extents.x * 2 || (pointAProxy - pointBProxy).magnitude < _arrowHeight / 25))
            {
                mainLineSmallAGO.SetActive(true);
                mainLineSmallBGO.SetActive(true);

                mainLine.SetPosition(0, pointAProxy + firstPointHitNormal * _normalOffset);
                mainLine.SetPosition(1, pointBProxy + firstPointHitNormal * _normalOffset);

                if (!_flipTextPosition)
                {
                    mainlineSmallA.SetPosition(0, pointAProxy - (pointBProxy - pointAProxy).normalized * _arrowHeight / 50 + firstPointHitNormal * _normalOffset);
                    mainlineSmallA.SetPosition(1, pointAProxy - ((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 25 + (number.bounds.extents.x * 2)) + firstPointHitNormal * _normalOffset);

                    mainlineSmallB.SetPosition(0, pointBProxy + (pointBProxy - pointAProxy).normalized * _arrowHeight / 50 + firstPointHitNormal * _normalOffset);
                    mainlineSmallB.SetPosition(1, pointBProxy + (pointBProxy - pointAProxy).normalized * (_arrowHeight / 25) + firstPointHitNormal * _normalOffset);
                }
                else
                {
                    mainlineSmallA.SetPosition(0, pointAProxy - (pointBProxy - pointAProxy).normalized * _arrowHeight / 50 + firstPointHitNormal * _normalOffset);
                    mainlineSmallA.SetPosition(1, pointAProxy - ((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 25) + firstPointHitNormal * _normalOffset);

                    mainlineSmallB.SetPosition(0, pointBProxy + (pointBProxy - pointAProxy).normalized * _arrowHeight / 50 + firstPointHitNormal * _normalOffset);
                    mainlineSmallB.SetPosition(1, pointBProxy + (pointBProxy - pointAProxy).normalized * (_arrowHeight / 25 + (number.bounds.extents.x * 2)) + firstPointHitNormal * _normalOffset);
                }

                arrowAGO.transform.LookAt(lineCenter, Vector3.up);
                arrowAGO.transform.eulerAngles += new Vector3(90, 0, 0);

                arrowBGO.transform.LookAt(lineCenter, Vector3.up);
                arrowBGO.transform.eulerAngles += new Vector3(90, 0, 0);

                if (!_flipTextPosition)
                    lineCenter = Vector3.Lerp(pointAProxy - ((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 25 + (number.bounds.extents.x * 2)), pointAProxy - (pointBProxy - pointAProxy).normalized * (arrowHeight / 50), 0.5f);
                else
                    lineCenter = Vector3.Lerp(pointBProxy + ((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 25 + (number.bounds.extents.x * 2)), pointBProxy + (pointBProxy - pointAProxy).normalized * (arrowHeight / 50), 0.5f);

                otherDirection = Vector3.Cross(lineDirection, (lineCenter - _cameraTransform.position));
                numberParent.transform.position = lineCenter;
                number.ForceMeshUpdate();
                number.rectTransform.localPosition = new Vector3(0, (number.bounds.extents.y * _textOffset) + (_mainLineThickness / 2), 0);

                rotationForward = Vector3.Cross(otherDirection, lineDirection);
                numberRotation = Quaternion.LookRotation(rotationForward, lineDirection);
                parentRotation = numberParent.transform.eulerAngles = numberRotation.eulerAngles - new Vector3(0, 0, 90f);
                textRotation = numberParent.transform.localEulerAngles.z;

                if (textRotation > 90 && textRotation <= 270)
                    numberParent.transform.eulerAngles = parentRotation + new Vector3(0, 0, 180f);
            }
            else
            {
                mainLineSmallAGO.SetActive(false);
                mainLineSmallBGO.SetActive(false);
            }

            // hit normal offset
            arrowAGO.transform.localPosition += firstPointHitNormal * _normalOffset;
            arrowBGO.transform.localPosition += firstPointHitNormal * _normalOffset;

            Vector2 lineCenterOnScreen;
            Vector2 numberCenterOnScreen;

            lineCenterOnScreen = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(lineCenter);
            numberCenterOnScreen = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(numberGO.transform.position);

            if (lineCenterOnScreen.y > numberCenterOnScreen.y)
            {
                numberParent.transform.eulerAngles += new Vector3(0, 0, 180f);
            }

            // hit normal offset
            number.rectTransform.position += firstPointHitNormal * _normalOffset;
        }
    }
}