using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using EzDimension;
using EzDimension.dims;

namespace EzDimension.dims
{
    public class LinearDimension : MonoBehaviour
    {
        [Header("Always Individual :")]
        [Space]
        public EzDimStarter valuesReference;
        [Space]
        [Space]
        public Funcs.MeasurementDirection measurementDirection;
        public Funcs.OffsetDirection offsetDirection;
        public float offsetDistance;
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
        public float textTowardsCameraOffset;
        public float textOffset = 1.5f;
        public float secondaryLinesThickness = 1f;
        public float secondaryLinesExtend;
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
        public Transform cameraTransform;
        [HideInInspector]
        public GameObject mainParent;
        [HideInInspector]
        public Vector3 pointA = Vector3.zero;
        [HideInInspector]
        public Vector3 pointB = Vector3.one;
        [HideInInspector]
        public bool isDone = false;
        [HideInInspector]
        public bool secondDrawStep = false;
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

        float unitMultiplier;
        Vector3 pointAProxy;
        Vector3 pointBProxy;
        Vector3 alignedPointB;
        Vector3 planeNormal;
        Plane plane;

        // create and update:
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
        int secondaryLineBExtendDirCorrection;
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

            // dynamic transforms :
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

            // value reference
            valuesReference = GetComponentInParent<EzDimStarter>();
            unitMultiplier = valuesReference.customUnitMultiplier;
            isDynamic = valuesReference.isDynamic;
            arrowHeight = valuesReference.arrowHeight;
            arrowPrefab = valuesReference.arrowPrefab;
            mainLineThickness = valuesReference.linesThickness;
            secondaryLinesThickness = valuesReference.secondaryLinesThickness;
            secondaryLinesExtend = valuesReference.secondaryLinesExtend;
            textSize = valuesReference.textSize;
            textOffset = valuesReference.textOffset;
            NormalOffset = valuesReference.hitNormalOffset;
            textTowardsCameraOffset = valuesReference.textTowardsCameraOffset;
            autoTextPosition = valuesReference.autoTextPosition;
            flipTextPosition = valuesReference.flipTextPosition;
            cameraTransform = valuesReference.cameraTransform;
            // materials :
            mainLineMat = new Material(valuesReference.mainLineMaterial);
            secondaryLinesMat = new Material(valuesReference.secondaryLinesMaterial);
            arrowMat = new Material(valuesReference.arrowMaterial);
            // colors :
            numberColor = valuesReference.numberColor;
            mainColor = valuesReference.mainColor;
            secondaryColor = valuesReference.secondaryColor;
            arrowColor = valuesReference.arrowColor;
            hoveredTint = valuesReference.hoveredTint;
            selectedTint = valuesReference.selectedTint;
            hoveredOnSelectedTint = valuesReference.hoveredOnSelectedTint;
            // linear dimension additional settings:
            measurementDirection = valuesReference.measurementDirection;
            offsetDirection = valuesReference.offsetDirection;
            offsetDistance = valuesReference.offsetDistance;

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

            // arrows :
            arrows.transform.parent = _mainParent.transform;
            arrowAGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowAGO.name = "Arrow A";
            arrowBGO = Instantiate(_arrowPrefab, arrows.transform);
            arrowBGO.name = "Arrow B";

            // materials :
            mainLine.material = mainLineMat;
            mainlineSmallA.material = mainLineMat;
            mainlineSmallB.material = mainlineSmallA.material;
            secondaryLineA.material = secondaryLinesMat;
            secondaryLineB.material = secondaryLineA.material;
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

        public void UpdateDimension(Vector3 _pointA, Vector3 _pointB, float _mainLineThickness, float _secondaryLinesThickness, float _secondaryLinesExtend,
            float _textSize, float _textOffset,
            Material _mainLineMaterial, Material _secoundaryLineMaterial, Material _arrowMaterial, Color _numberColor, Color _mainColor,
            Color _secondaryColor, Color _arrowColor, Transform _cameraTransform,
            GameObject _mainParent, float _arrowHeight, Funcs.MeasurementDirection _measurementDirection, Funcs.OffsetDirection _offsetDirection,
            float _offsetDistance, bool _autoTextPosition, bool _flipTextPosition, float _unitMultiplier, float _textLocalYOffset,
            float _normalOffset)
        {
            if (_offsetDistance == 0)
                _offsetDistance = 0.00001f;

            // materials :
            mainLine.material = mainlineSmallA.material = mainlineSmallB.material = _mainLineMaterial;
            secondaryLineA.material = secondaryLineB.material = _secoundaryLineMaterial;
            arrowAGO.GetComponent<MeshRenderer>().material = _arrowMaterial;
            arrowBGO.GetComponent<MeshRenderer>().material = arrowAGO.GetComponent<MeshRenderer>().material;
            // colors :
            number.color = _numberColor.linear;
            mainLine.material.color = mainlineSmallA.material.color = mainlineSmallB.material.color = _mainColor;
            secondaryLineA.material.color = secondaryLineB.material.color = _secondaryColor;
            arrowAGO.GetComponent<MeshRenderer>().sharedMaterial.color = _arrowColor;

            if (_measurementDirection == Funcs.MeasurementDirection.X)
            {
                if (_offsetDirection == Funcs.OffsetDirection.Y)
                {
                    pointAProxy = _pointA + new Vector3(0, _offsetDistance, 0);
                    alignedPointB = new Vector3(_pointB.x, _pointB.y, _pointA.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else if (_offsetDirection == Funcs.OffsetDirection.Z)
                {
                    pointAProxy = _pointA + new Vector3(0, 0, _offsetDistance);
                    alignedPointB = new Vector3(_pointB.x, _pointA.y, _pointB.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else // if measurment direction == offset direction, offset direction will set in z axis to avoid any error.
                {
                    pointAProxy = _pointA + new Vector3(0, 0, _offsetDistance);
                    alignedPointB = new Vector3(_pointB.x, _pointA.y, _pointB.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
            }
            else if (_measurementDirection == Funcs.MeasurementDirection.Y)
            {
                if (_offsetDirection == Funcs.OffsetDirection.X)
                {
                    pointAProxy = _pointA + new Vector3(_offsetDistance, 0, 0);
                    alignedPointB = new Vector3(_pointB.x, _pointB.y, _pointA.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.x > pointAProxy.x)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.x > pointAProxy.x)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else if (_offsetDirection == Funcs.OffsetDirection.Z)
                {
                    pointAProxy = _pointA + new Vector3(0, 0, _offsetDistance);
                    alignedPointB = new Vector3(_pointA.x, _pointB.y, _pointB.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;

                    }
                    else
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else // if measurment direction == offset direction, offset direction will apply in z axis to avoid any error.
                {
                    pointAProxy = _pointA + new Vector3(0, 0, _offsetDistance);
                    alignedPointB = new Vector3(_pointA.x, _pointB.y, _pointB.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.z > pointAProxy.z)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }

                }
            }
            else // Z  
            {
                if (_offsetDirection == Funcs.OffsetDirection.X)
                {
                    pointAProxy = _pointA + new Vector3(_offsetDistance, 0, 0);
                    alignedPointB = new Vector3(_pointB.x, _pointA.y, _pointB.z);


                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.x > pointAProxy.x)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.x > pointAProxy.x)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else if (_offsetDirection == Funcs.OffsetDirection.Y)
                {
                    pointAProxy = _pointA + new Vector3(0, _offsetDistance, 0);
                    alignedPointB = new Vector3(_pointA.x, _pointB.y, _pointB.z);

                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
                else // if measurment direction == offset direction, offset direction will apply in y axis to avoid any error.
                {
                    pointAProxy = _pointA + new Vector3(0, _offsetDistance, 0);
                    alignedPointB = new Vector3(_pointA.x, _pointB.y, _pointB.z);


                    if (offsetDistance >= 0)
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = -1;
                        else
                            secondaryLineBExtendDirCorrection = 1;
                    }
                    else
                    {
                        if (alignedPointB.y > pointAProxy.y)
                            secondaryLineBExtendDirCorrection = 1;
                        else
                            secondaryLineBExtendDirCorrection = -1;
                    }
                }
            }

            planeNormal = (pointAProxy - _pointA).normalized;
            plane = new Plane(planeNormal, pointAProxy);
            pointBProxy = plane.ClosestPointOnPlane(alignedPointB);

            // lines :
            mainLine.SetPosition(0, pointAProxy - (((pointAProxy - pointBProxy).normalized) * (_arrowHeight / 50)) + firstPointHitNormal * _normalOffset);
            mainLine.SetPosition(1, pointBProxy - (((pointBProxy - pointAProxy).normalized) * (_arrowHeight / 50)) + firstPointHitNormal * _normalOffset);
            secondaryLineA.SetPosition(0, _pointA + firstPointHitNormal * _normalOffset);
            secondaryLineA.SetPosition(1, pointAProxy + (planeNormal * _secondaryLinesExtend) + firstPointHitNormal * _normalOffset);
            secondaryLineB.SetPosition(0, alignedPointB + firstPointHitNormal * _normalOffset);
            secondaryLineB.SetPosition(1, pointBProxy + (planeNormal * secondaryLineBExtendDirCorrection * _secondaryLinesExtend) + firstPointHitNormal * _normalOffset);

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

            //number.color = _numberColor.linear;
            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;

            // rotation :
            UpdateTextTransform(_cameraTransform, _textOffset, _mainLineThickness, _arrowHeight, _autoTextPosition, _flipTextPosition,
                _textLocalYOffset, _normalOffset);

            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);
        }

        public void UpdateTextTransform(Transform _cameraTransform, float _textOffset, float _mainLineThickness, float _arrowHeight, bool _autoTextPosition,
            bool _flipTextPosition, float _textLocalYOffset, float _normalOffset)
        {
            lineDirection = (pointBProxy - pointAProxy).normalized;
            lineCenter = Vector3.Lerp(pointAProxy, pointBProxy, 0.5f);
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