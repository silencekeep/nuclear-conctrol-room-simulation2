using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace EzDimension.dims
{
    public class EzDimStarter : MonoBehaviour
    {
        [Header("VR :")]
        public Component drawInVrComp;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Units :")]
        [Space]
        public internalFuncs.Units unit = internalFuncs.Units.meter;
        public float customUnitMultiplier = 1f;
        public string customUnitName = "cu";
        public bool showUnitAfterNumber;
        public internalFuncs.numberOfDecimal numberOfDecimal = internalFuncs.numberOfDecimal.two;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global Inputs :")]
        [Space]
        [Header("—— Camera :")]
        [Space]
        public Transform cameraTransform;
        public Camera rendererCamera;
        [Space]
        [Header("—— Materials :")]
        [Space]
        public Material mainLineMaterial;
        public Material secondaryLinesMaterial;
        public Material arrowMaterial;
        public Material arcMaterial;
        public Material areaSurfaceMaterial;
        public Material areaBorderMaterial;
        [Space]
        [Header("—— Prefabs :")]
        [Space]
        public GameObject arrowPrefab;
        public GameObject areaHandlesPrefab;
        public GameObject alignedDimFreeModeDirPointer;
        public GameObject alignedDimFreeModePlanePrefab;
        public float planePrefabScaleMultiplier = 0f;
        public float areaHandlesScale = 1f;
        public float pointerScale = 1f;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global Parameters :")]
        [Space]
        [Header("—— Geberal :")]
        [Space]
        public bool isDynamic = true;
        public float linesThickness = 0.02f;
        public float textSize = 1f;
        public float textOffset = 1.5f;
        public float arrowHeight = 6f;
        [Space]
        [Header("—— PointToPoint, Linear & Aligned Dimension :")]
        [Space]
        public float hitNormalOffset = 0.01f;
        public float textTowardsCameraOffset = 0.001f;
        [Space]
        [Header("—— Linear Dimension :")]
        [Space]
        public Funcs.MeasurementDirection measurementDirection = Funcs.MeasurementDirection.X;
        public Funcs.OffsetDirection offsetDirection = Funcs.OffsetDirection.Y;
        [Space]
        [Header("—— Aligned Dimension :")]
        [Space]
        public Funcs.MeasurementPlane measurementPlane = Funcs.MeasurementPlane.XZ;
        public bool reverseDirection;
        [Space]
        [Header("—— Linear & Aligned Dimension :")]
        [Space]
        public float offsetDistance = 0.5f;
        public float secondaryLinesThickness = 0.05f;
        public float secondaryLinesExtend = 0.15f;
        public bool autoTextPosition = true;
        public bool flipTextPosition = false;
        [Space]
        [Header("—— Angle Dimension :")]
        [Space]
        public Funcs.AngleMeasurmentPlane angleMeasurmentPlane = Funcs.AngleMeasurmentPlane.XZ;
        public float arcScale = 1f;
        public float angleDimTextOffsetFromCenter = 1f;
        public float angleDimHitNormalOffset = 0.02f;
        public Vector2 textOffsetWhenNotFit = new Vector2(0.03f, 0.05f);
        [Space]
        [Header("—— Area Measure :")]
        [Space]
        public Funcs.MeasurementPlane areaMeasurementPlane = Funcs.MeasurementPlane.XZ;
        public bool enableAreaBorderLine = true;
        public float areaLocalYOffset = 0.01f;
        public float areaTextLocalYOffset = 0.02f;
        public float areaBorderLocalYOffset = 0.02f;
        public float areaBorderLineThickness = 0.01f;
        public Vector2 areaNumberPositionOffset = Vector2.zero;
        [Space]
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Colors :")]
        [Space]
        [Header("—— Dimensions Colors :")]
        [Space]
        public Color numberColor = new Color(0, 0, 0, 1);
        public Color mainColor = new Color(0, 0, 0, 1);
        public Color secondaryColor = new Color(0, 0, 0, 1);
        public Color arrowColor = new Color(0, 0, 0, 1);
        public Color arcColor = new Color(0, 0, 0, 1);
        public Color areaBorderColor = new Color(0, 0, 0, 1);
        public Color areaSurfaceColor = new Color(0.6f, 0.3f, 0, 1);
        [Space]
        [Header("—— Overlay Colors :")]
        [Space]
        public Color hoveredTint = new Color(0, 0.3f, 0.5f, 1);
        public Color selectedTint = new Color(0, 0.7f, 1, 1);
        public Color hoveredOnSelectedTint = new Color(0.5f, 0.8f, 1, 1);
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Events :")]
        [Space]
        [Space]

        public UnityEvent<List<GameObject>> onSelectionChanged;
        public UnityEvent<GameObject> onHovered;

        [HideInInspector]
        public Vector3 pointA;
        [HideInInspector]
        public Vector3 pointB;
        [HideInInspector]
        public bool isCreating;
        [HideInInspector]
        public List<GameObject> DimensionsList;
        [HideInInspector]
        public List<GameObject> SelectionList;
        [Space]
        [HideInInspector]
        public GameObject oldHoveredGo;
        [HideInInspector]
        public RaycastHit hit;
        [HideInInspector]
        public bool isOldSelectionListEmpty;
        [HideInInspector]
        public bool stepA;
        [HideInInspector]
        public GameObject planePrefab;
        [HideInInspector]
        public GameObject directionPointer;

        public Coroutine createDimensionCort;

        Vector2 oldMousePos;

        private void Awake()
        {
            DimensionsList = new List<GameObject>();

            SelectionList = new List<GameObject>();

            areaSurfaceColor = new Color(0.6f, 0.3f, 0, areaSurfaceMaterial.color.a);
        }

        private void Update()
        {
            if (drawInVrComp == null)
            {
                GameObject hoveredGO;

                if (oldMousePos != Mouse.current.position.ReadValue() && !isCreating && !EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        hoveredGO = Funcs.IsHoveredOnDimension(this, SelectionList, hit);
                    else hoveredGO = null;

                    if (hoveredGO != null && oldHoveredGo != hoveredGO)
                    {
                        onHovered.Invoke(hoveredGO);
                    }

                    oldHoveredGo = hoveredGO;
                }
                oldMousePos = Mouse.current.position.ReadValue();
            }
            else
            {
                this.gameObject.SendMessage("VR_HighlightHoveredDimension", this, SendMessageOptions.DontRequireReceiver);
            }

            if (cameraTransform.hasChanged && !isCreating)
            {
                Funcs.UpdateNumbersRotation(this, DimensionsList, cameraTransform);
            }
            cameraTransform.hasChanged = false;

            if (drawInVrComp == null)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
                {
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        isOldSelectionListEmpty = SelectionList.Count == 0;

                        Funcs.SelectDimension(this, DimensionsList, SelectionList, hit);

                        if (SelectionList.Count != 0 || !isOldSelectionListEmpty)
                        {
                            onSelectionChanged.Invoke(SelectionList);
                        }
                    }
                }
            }
            else
            {
                this.gameObject.SendMessage("VR_SelectDimension", this, SendMessageOptions.DontRequireReceiver);
            }
        }

        IEnumerator CreatePointToPointDimension(PointToPointDimension _p2PDim)
        {
            bool secondDrawStep = false;
            Vector2 p0MousePosOnScreen = Vector2.zero;

            while (_p2PDim.isDone != true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !secondDrawStep && !_p2PDim.isDone && isCreating == true)
                {
                    p0MousePosOnScreen = Mouse.current.position.ReadValue();
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _p2PDim.firstPointHitNormal = hit.normal;
                        _p2PDim.pointA = hit.point;
                        _p2PDim.objectA = hit.transform.gameObject;
                        _p2PDim.objectATransformGO.transform.position = hit.transform.position;
                        _p2PDim.objectATransformGO.transform.rotation = hit.transform.rotation;
                        _p2PDim.objectATransformGO.transform.localScale = hit.transform.localScale;
                        _p2PDim.pointATransformGO.transform.position = _p2PDim.pointA;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().p2PDimComponentsList.Add(_p2PDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                        }
                        else
                        {
                            var p2PDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            p2PDimComp.p2PDimComponentsList.Add(_p2PDim);
                        }

                        secondDrawStep = true;
                    }
                }
                else if (secondDrawStep && p0MousePosOnScreen != Mouse.current.position.ReadValue())
                {
                    Funcs.SetActiveAllChilds(_p2PDim.gameObject.transform, true);
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _p2PDim.pointB = hit.point;
                        _p2PDim.objectB = hit.transform.gameObject;
                        _p2PDim.objectBTransformGO.transform.position = hit.transform.position;
                        _p2PDim.objectBTransformGO.transform.rotation = hit.transform.rotation;
                        _p2PDim.objectBTransformGO.transform.localScale = hit.transform.localScale;
                        _p2PDim.pointBTransformGO.transform.position = _p2PDim.pointB;

                        _p2PDim.UpdateDimension(_p2PDim.pointA, _p2PDim.pointB, _p2PDim.lineThickness, _p2PDim.textSize, _p2PDim.textOffset,
                            _p2PDim.numberColor, _p2PDim.mainColor, _p2PDim.arrowColor,
                            _p2PDim.mainLineMat, _p2PDim.arrowMat, _p2PDim.cameraTransform, _p2PDim.mainParent, _p2PDim.arrowHeight,
                            internalFuncs.LengthUnitCalculator(this), textTowardsCameraOffset, hitNormalOffset);

                        if (Mouse.current.leftButton.wasPressedThisFrame && secondDrawStep)
                        {
                            if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                            {
                                hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().p2PDimComponentsList.Add(_p2PDim);
                                hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                            }
                            else
                            {
                                var p2PDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                                p2PDimComp.p2PDimComponentsList.Add(_p2PDim);
                            }

                            secondDrawStep = false;
                            _p2PDim.isDone = true;
                            isCreating = false; // end of drawing of the dimension.
                        }
                    }
                }

                yield return null;
            }

        }
        IEnumerator CreateLinearDimension(LinearDimension _linDim)
        {
            Vector2 p0MousePosOnScreen = Vector2.zero;

            while (_linDim.isDone != true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !_linDim.isDone && !_linDim.secondDrawStep && isCreating == true)
                {
                    p0MousePosOnScreen = Mouse.current.position.ReadValue();
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _linDim.firstPointHitNormal = hit.normal;
                        _linDim.pointA = hit.point;
                        _linDim.objectA = hit.transform.gameObject;
                        _linDim.objectATransformGO.transform.position = hit.transform.position;
                        _linDim.objectATransformGO.transform.rotation = hit.transform.rotation;
                        _linDim.objectATransformGO.transform.localScale = hit.transform.localScale;
                        _linDim.pointATransformGO.transform.position = _linDim.pointA;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().linDimComponentsList.Add(_linDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                        }
                        else
                        {
                            var LinDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            LinDimComp.linDimComponentsList.Add(_linDim);
                        }

                        _linDim.secondDrawStep = true;
                    }
                }
                else if (_linDim.secondDrawStep && !_linDim.isDone && p0MousePosOnScreen != Mouse.current.position.ReadValue())
                {
                    Funcs.SetActiveAllChilds(_linDim.gameObject.transform, true);
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _linDim.pointB = hit.point;
                        _linDim.objectB = hit.transform.gameObject;
                        _linDim.objectBTransformGO.transform.position = hit.transform.position;
                        _linDim.objectBTransformGO.transform.rotation = hit.transform.rotation;
                        _linDim.objectBTransformGO.transform.localScale = hit.transform.localScale;
                        _linDim.pointBTransformGO.transform.position = _linDim.pointB;

                        _linDim.UpdateDimension(_linDim.pointA, _linDim.pointB, _linDim.mainLineThickness, _linDim.secondaryLinesThickness,
                            _linDim.secondaryLinesExtend, _linDim.textSize, _linDim.textOffset, _linDim.mainLineMat, _linDim.secondaryLinesMat,
                            _linDim.arrowMat, _linDim.numberColor, _linDim.mainColor, _linDim.secondaryColor, _linDim.arrowColor,
                            _linDim.cameraTransform, _linDim.mainParent, _linDim.arrowHeight, _linDim.measurementDirection,
                            _linDim.offsetDirection, _linDim.offsetDistance, _linDim.autoTextPosition, _linDim.flipTextPosition,
                            internalFuncs.LengthUnitCalculator(this), _linDim.textTowardsCameraOffset, _linDim.NormalOffset);

                        if (Mouse.current.leftButton.wasPressedThisFrame && _linDim.secondDrawStep)
                        {
                            if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                            {
                                hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().linDimComponentsList.Add(_linDim);
                                hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                            }
                            else
                            {
                                var linDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                                linDimComp.linDimComponentsList.Add(_linDim);
                            }

                            _linDim.secondDrawStep = false;
                            _linDim.isDone = true;
                            isCreating = false; // end of drawing of the dimension.
                        }
                    }
                }

                yield return null;
            }
        }
        IEnumerator CreateAlignedDimension(AlignedDimension _alignDim)
        {
            Vector2 p0MousePosOnScreen = Vector2.zero;

            while (_alignDim.isDone != true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !_alignDim.secondDrawStep && !_alignDim.isDone && isCreating == true)
                {
                    stepA = false;
                    p0MousePosOnScreen = Mouse.current.position.ReadValue();
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _alignDim.firstPointHitNormal = hit.normal;
                        _alignDim.pointA = hit.point;
                        _alignDim.objectA = hit.transform.gameObject;
                        _alignDim.objectATransformGO.transform.position = hit.transform.position;
                        _alignDim.objectATransformGO.transform.rotation = hit.transform.rotation;
                        _alignDim.objectATransformGO.transform.localScale = hit.transform.localScale;
                        _alignDim.pointATransformGO.transform.position = _alignDim.pointA;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().alignDimComponentsList.Add(_alignDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                        }
                        else
                        {
                            var alignDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            alignDimComp.alignDimComponentsList.Add(_alignDim);
                        }

                        _alignDim.secondDrawStep = true;
                    }
                }
                else if (_alignDim.secondDrawStep && !_alignDim.isDone && p0MousePosOnScreen != Mouse.current.position.ReadValue())
                {
                    Funcs.SetActiveAllChilds(_alignDim.gameObject.transform, true);
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) & _alignDim.measurementPlane != Funcs.MeasurementPlane.Free)
                    {
                        _alignDim.pointB = hit.point;
                        _alignDim.objectB = hit.transform.gameObject;
                        _alignDim.objectBTransformGO.transform.position = hit.transform.position;
                        _alignDim.objectBTransformGO.transform.rotation = hit.transform.rotation;
                        _alignDim.objectBTransformGO.transform.localScale = hit.transform.localScale;
                        _alignDim.pointBTransformGO.transform.position = _alignDim.pointB;

                        _alignDim.UpdateDimension(_alignDim.pointA, _alignDim.pointB, _alignDim.mainLineThickness,
                            _alignDim.secondaryLinesThickness, _alignDim.secondaryLinesExtend, _alignDim.textSize, _alignDim.textOffset,
                            _alignDim.numberColor, _alignDim.mainColor, _alignDim.secondaryColor, _alignDim.arrowColor,
                           _alignDim.mainLineMat, _alignDim.secondaryLinesMat, _alignDim.arrowMat, _alignDim.cameraTransform,
                           _alignDim.mainParent, _alignDim.arrowHeight, _alignDim.measurementPlane, _alignDim.offsetDistance,
                           internalFuncs.LengthUnitCalculator(this), autoTextPosition, flipTextPosition, _alignDim.textTowardsCameraOffset
                           , _alignDim.NormalOffset);

                        if (Mouse.current.leftButton.wasPressedThisFrame && _alignDim.secondDrawStep)
                        {
                            if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                            {
                                hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().alignDimComponentsList.Add(_alignDim);
                                hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                            }
                            else
                            {
                                var alignDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                                alignDimComp.alignDimComponentsList.Add(_alignDim);
                            }

                            _alignDim.secondDrawStep = false;
                            _alignDim.isDone = true;
                            isCreating = false; // end of drawing of the dimension.
                        }
                    }
                    else if ((Physics.Raycast(ray, out hit, Mathf.Infinity) & _alignDim.measurementPlane == Funcs.MeasurementPlane.Free))
                    {
                        if (!stepA)
                        {
                            _alignDim.pointB = hit.point;
                            _alignDim.objectB = hit.transform.gameObject;
                            _alignDim.objectBTransformGO.transform.position = hit.transform.position;
                            _alignDim.objectBTransformGO.transform.rotation = hit.transform.rotation;
                            _alignDim.objectBTransformGO.transform.localScale = hit.transform.localScale;
                            _alignDim.pointBTransformGO.transform.position = _alignDim.pointB;
                        }

                        _alignDim.UpdateDimension(_alignDim.pointA, _alignDim.pointB, _alignDim.mainLineThickness,
                            _alignDim.secondaryLinesThickness, _alignDim.secondaryLinesExtend, _alignDim.textSize, _alignDim.textOffset,
                            _alignDim.numberColor, _alignDim.mainColor, _alignDim.secondaryColor, _alignDim.arrowColor,
                           _alignDim.mainLineMat, _alignDim.secondaryLinesMat, _alignDim.arrowMat, _alignDim.cameraTransform,
                           _alignDim.mainParent, _alignDim.arrowHeight, _alignDim.measurementPlane, _alignDim.offsetDistance,
                           internalFuncs.LengthUnitCalculator(this), _alignDim.autoTextPosition, _alignDim.flipTextPosition,
                           _alignDim.textTowardsCameraOffset, _alignDim.NormalOffset);

                        if (Mouse.current.leftButton.wasPressedThisFrame)
                        {
                            if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                            {
                                hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().alignDimComponentsList.Add(_alignDim);
                                hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                            }
                            else
                            {
                                var alignDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                                alignDimComp.alignDimComponentsList.Add(_alignDim);
                            }
                        }

                        if (stepA)
                        {
                            Vector3 lineCenter = Vector3.Lerp(_alignDim.pointA, _alignDim.pointB, 0.5f);
                            Vector3 LineDirection = (_alignDim.pointB - _alignDim.pointA).normalized;
                            Plane plane = new Plane(LineDirection, lineCenter);

                            float enter = 0.0f;
                            if (plane.Raycast(ray, out enter))
                            {
                                Vector3 hitPoint = ray.GetPoint(enter);


                                if (alignedDimFreeModePlanePrefab != null)
                                    if (planePrefab == null)
                                        planePrefab = GameObject.Instantiate(alignedDimFreeModePlanePrefab, _alignDim.transform);

                                if (alignedDimFreeModeDirPointer != null)
                                    if (directionPointer == null)
                                    {
                                        directionPointer = GameObject.Instantiate(alignedDimFreeModeDirPointer, _alignDim.transform);
                                        directionPointer.transform.localScale *= pointerScale;
                                    }


                                _alignDim.DirectionPointInFreeMode = hitPoint;

                                if (directionPointer != null)
                                {
                                    directionPointer.transform.position = hitPoint;
                                }

                                if (planePrefab != null)
                                {
                                    planePrefab.transform.position = lineCenter;
                                    planePrefab.transform.rotation = Quaternion.LookRotation(LineDirection);

                                    if (planePrefabScaleMultiplier > 0)
                                    {
                                        float scaleNumber = (hitPoint - lineCenter).magnitude;
                                        planePrefab.transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber) * planePrefabScaleMultiplier;
                                    }
                                    else
                                    {
                                        planePrefab.transform.localScale = Vector3.one;
                                    }
                                }
                            }

                            _alignDim.UpdateDimension(_alignDim.pointA, _alignDim.pointB, _alignDim.mainLineThickness,
                              _alignDim.secondaryLinesThickness, _alignDim.secondaryLinesExtend, _alignDim.textSize, _alignDim.textOffset,
                              _alignDim.numberColor, _alignDim.mainColor, _alignDim.secondaryColor, _alignDim.arrowColor,
                              _alignDim.mainLineMat, _alignDim.secondaryLinesMat, _alignDim.arrowMat, _alignDim.cameraTransform,
                              _alignDim.mainParent, _alignDim.arrowHeight, _alignDim.measurementPlane, _alignDim.offsetDistance,
                              internalFuncs.LengthUnitCalculator(this), autoTextPosition, flipTextPosition, _alignDim.textTowardsCameraOffset
                              , _alignDim.NormalOffset);

                            if (Mouse.current.leftButton.wasPressedThisFrame)
                            {
                                if (directionPointer != null)
                                    GameObject.Destroy(directionPointer);

                                if (planePrefab != null)
                                    GameObject.Destroy(planePrefab);

                                stepA = false;
                                _alignDim.secondDrawStep = false;
                                _alignDim.isDone = true;
                                isCreating = false; // end of drawing of the dimension.
                            }
                        }

                        if (Mouse.current.leftButton.wasPressedThisFrame)
                            stepA = true;

                    }
                }
                yield return null;
            }
        }
        IEnumerator CreateAngleDimension(AngleDimension _angleDim)
        {
            Vector2 p0MousePosOnScreen = Vector2.zero;

            while (_angleDim.isDone != true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !_angleDim.trueOnFirstStep && !_angleDim.trueOnSecoundStep && !_angleDim.isDone && isCreating == true)
                {
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        _angleDim.firstPointHitNormal = hit.normal;
                        _angleDim.lineAGO.SetActive(true);
                        _angleDim.pointA = hit.point;
                        _angleDim.objectA = hit.transform.gameObject;
                        _angleDim.objectATransformGO.transform.position = hit.transform.position;
                        _angleDim.objectATransformGO.transform.rotation = hit.transform.rotation;
                        _angleDim.objectATransformGO.transform.localScale = hit.transform.localScale;
                        _angleDim.pointATransformGO.transform.position = _angleDim.pointA;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().angleDimComponentsList.Add(_angleDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this.GetComponentInParent<EzDimStarter>();
                        }
                        else
                        {
                            var angleDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            angleDimComp.angleDimComponentsList.Add(_angleDim);
                        }

                        p0MousePosOnScreen = Mouse.current.position.ReadValue();
                        _angleDim.trueOnFirstStep = true;
                    }
                }
                else if (_angleDim.trueOnFirstStep && !_angleDim.trueOnSecoundStep && !_angleDim.isDone && p0MousePosOnScreen != Mouse.current.position.ReadValue() && isCreating)
                {
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out hit, Mathf.Infinity);
                    _angleDim.pointB = hit.point;

                    if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.XZ)
                        _angleDim.pointBProxy = new Vector3(_angleDim.pointB.x, _angleDim.pointA.y, _angleDim.pointB.z);
                    else if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.XY)
                        _angleDim.pointBProxy = new Vector3(_angleDim.pointB.x, _angleDim.pointB.y, _angleDim.pointA.z);
                    else if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.ZY)
                        _angleDim.pointBProxy = new Vector3(_angleDim.pointA.x, _angleDim.pointB.y, _angleDim.pointB.z);
                    else _angleDim.pointBProxy = _angleDim.pointB;

                    _angleDim.UpdateDimension(_angleDim.pointA, _angleDim.pointBProxy, _angleDim.pointCProxy, _angleDim.linesThickness, _angleDim.textSize,
                        _angleDim.arcScale, _angleDim.normalOffset,
                        _angleDim.textOffsetFromCenter, _angleDim.textOffsetIfNotFit, _angleDim.numberColor, _angleDim.mainColor, _angleDim.arcColor,
                        _angleDim.cameraTransform, _angleDim.mainParent, _angleDim.arcMat, _angleDim.mainLinesMat, _angleDim.angleMeasurementPlane,
                        internalFuncs.LengthUnitCalculator(this));

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        _angleDim.numberGO.GetComponent<BoxCollider>().enabled = false; // disable box collider to avoid detect any point of number as target point

                        Funcs.SetActiveAllChilds(_angleDim.gameObject.transform, true);
                        _angleDim.arcParentGO.transform.position = _angleDim.pointBProxy;
                        _angleDim.objectB = hit.transform.gameObject;
                        _angleDim.objectBTransformGO.transform.position = hit.transform.position;
                        _angleDim.objectBTransformGO.transform.rotation = hit.transform.rotation;
                        _angleDim.objectBTransformGO.transform.localScale = hit.transform.localScale;
                        _angleDim.pointBTransformGO.transform.position = _angleDim.pointB;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().angleDimComponentsList.Add(_angleDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                        }
                        else
                        {
                            var angleDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            angleDimComp.angleDimComponentsList.Add(_angleDim);
                        }

                        p0MousePosOnScreen = Mouse.current.position.ReadValue();
                        _angleDim.trueOnSecoundStep = true;
                    }
                }
                else if (_angleDim.trueOnFirstStep && _angleDim.trueOnSecoundStep && !_angleDim.isDone && p0MousePosOnScreen != Mouse.current.position.ReadValue() && isCreating)
                {
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out hit, Mathf.Infinity);
                    _angleDim.pointC = hit.point;

                    if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.XZ)
                        _angleDim.pointCProxy = new Vector3(_angleDim.pointC.x, _angleDim.pointA.y, _angleDim.pointC.z);
                    else if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.XY)
                        _angleDim.pointCProxy = new Vector3(_angleDim.pointC.x, _angleDim.pointC.y, _angleDim.pointA.z);
                    else if (_angleDim.angleMeasurementPlane == Funcs.AngleMeasurmentPlane.ZY)
                        _angleDim.pointCProxy = new Vector3(_angleDim.pointA.x, _angleDim.pointC.y, _angleDim.pointC.z);
                    else
                        _angleDim.pointCProxy = _angleDim.pointC;

                    _angleDim.UpdateDimension(_angleDim.pointA, _angleDim.pointBProxy, _angleDim.pointCProxy, _angleDim.linesThickness, _angleDim.textSize,
                        _angleDim.arcScale, _angleDim.normalOffset,
                        _angleDim.textOffsetFromCenter, _angleDim.textOffsetIfNotFit, _angleDim.numberColor, _angleDim.mainColor, _angleDim.arcColor,
                        _angleDim.cameraTransform, _angleDim.mainParent, _angleDim.arcMat, _angleDim.mainLinesMat, _angleDim.angleMeasurementPlane,
                        internalFuncs.LengthUnitCalculator(this));

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        _angleDim.plane = new Plane(_angleDim.pointA, _angleDim.pointBProxy, _angleDim.pointCProxy); // cut the space by a plane from main points(A,B,C). plane is only required for Free angle dimension.

                        _angleDim.objectC = hit.transform.gameObject;
                        _angleDim.objectCTransformGO.transform.position = hit.transform.position;
                        _angleDim.objectCTransformGO.transform.rotation = hit.transform.rotation;
                        _angleDim.objectCTransformGO.transform.localScale = hit.transform.localScale;
                        _angleDim.pointCTransformGO.transform.position = _angleDim.pointC;

                        if (!hit.transform.gameObject.TryGetComponent<EzDynamicTargetObject>(out EzDynamicTargetObject EzDynamicTarget))
                        {
                            hit.transform.gameObject.AddComponent<EzDynamicTargetObject>().angleDimComponentsList.Add(_angleDim);
                            hit.transform.gameObject.GetComponent<EzDynamicTargetObject>().starterScript = this;
                        }
                        else
                        {
                            var angleDimComp = hit.transform.gameObject.GetComponent<EzDynamicTargetObject>();
                            angleDimComp.angleDimComponentsList.Add(_angleDim);
                        }

                        _angleDim.numberGO.GetComponent<BoxCollider>().enabled = true; // it was disable to avoid detection as target while creating.
                        _angleDim.trueOnFirstStep = false;
                        _angleDim.trueOnSecoundStep = false;
                        _angleDim.isDone = true;
                        isCreating = false; // end of drawing of the dimension.
                    }
                }
                yield return null;
            }
        }
        IEnumerator CreateAreaMeasure(LinearAreaMeasure _AreaMeasurment)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 hitNormal = Vector3.up;
            while (!_AreaMeasurment.isDone)
            {
                if (_AreaMeasurment.drawMode)
                {
                    RaycastHit hit;
                    Ray ray = rendererCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        GameObject handle;

                        if (Mouse.current.leftButton.wasPressedThisFrame && isCreating && hit.transform != null)
                        {
                            if (areaHandlesPrefab == null)
                                handle = new GameObject("Handle");
                            else
                            {
                                handle = Instantiate(areaHandlesPrefab);
                                handle.transform.localScale *= areaHandlesScale;
                            }

                            handle.gameObject.name = "Handle";
                            handle.transform.parent = _AreaMeasurment.transform;
                            handle.transform.position = hit.point;
                            var dynamicTarget = handle.AddComponent<EzDynamicTargetObject>();
                            dynamicTarget.areaComponentsList.Add(_AreaMeasurment);
                            dynamicTarget.areaMeasureRoot = _AreaMeasurment;
                            dynamicTarget.starterScript = this;
                            _AreaMeasurment.handlesList.Add(handle);


                            if (_AreaMeasurment.points.Count == 1 || _AreaMeasurment.handlesList.Count == 1)
                            {
                                hitNormal = _AreaMeasurment.hitNormal = hit.normal;
                                rotation = Quaternion.LookRotation(hitNormal, Vector3.up);
                            }

                            handle.transform.rotation = rotation;
                        }
                    }

                    if (Mouse.current.rightButton.wasPressedThisFrame && isCreating)
                    {
                        if (enableAreaBorderLine)
                            _AreaMeasurment.enableBorderLine = true;

                        _AreaMeasurment.handlesParent.transform.rotation = rotation;

                        _AreaMeasurment.DrawArea(_AreaMeasurment.points, this, areaMeasurementPlane, areaLocalYOffset, areaBorderLocalYOffset, areaTextLocalYOffset,
                        areaSurfaceMaterial, areaBorderMaterial, enableAreaBorderLine, areaBorderLineThickness, textSize, cameraTransform,
                        areaNumberPositionOffset, numberColor, areaBorderColor, areaSurfaceColor);
                        _AreaMeasurment.allowDrawSurface = true;
                        _AreaMeasurment.drawMode = false;
                        isCreating = false;
                        _AreaMeasurment.isDone = true;
                    }
                }
                yield return null;
            }

        }

        public void EzPointToPointDimension()
        {
            if (!isCreating)
            {
                isCreating = true;  // this bool will set to false after draw the dimension.
                SelectionList.Clear(); // deselect selected dimensions
                Funcs.UpdateAll(this, DimensionsList, SelectionList);
                GameObject EzPointToPointDimensionGO = new GameObject("EzPointToPointDimension");
                EzPointToPointDimensionGO.transform.parent = this.transform;
                var p2PDim = EzPointToPointDimensionGO.AddComponent<PointToPointDimension>();

                if (createDimensionCort != null)
                    StopCoroutine(createDimensionCort);

                createDimensionCort = StartCoroutine(CreatePointToPointDimension(p2PDim));

                DimensionsList.Add(EzPointToPointDimensionGO.gameObject); // add the parent GO to the list.
            }
        }
        public void EzLinearDimension()
        {
            if (!isCreating)
            {
                isCreating = true;  // this bool will set to false after draw the dimension.
                SelectionList.Clear();
                Funcs.UpdateAll(this, DimensionsList, SelectionList);
                GameObject EzLinearDimensionGO = new GameObject("EzLinearDimension");
                EzLinearDimensionGO.transform.parent = this.transform;
                var linDim = EzLinearDimensionGO.AddComponent<LinearDimension>();

                if (createDimensionCort != null)
                    StopCoroutine(createDimensionCort);

                createDimensionCort = StartCoroutine(CreateLinearDimension(linDim));

                DimensionsList.Add(EzLinearDimensionGO.gameObject); // add the parent GO to the list.
            }
        }
        public void EzAlignedDimension()
        {
            if (!isCreating)
            {
                isCreating = true;  // this bool will set to false after draw the dimension.
                SelectionList.Clear();
                Funcs.UpdateAll(this, DimensionsList, SelectionList);
                GameObject EzAlignedDimensionGO = new GameObject("EzAlignedDimension");
                EzAlignedDimensionGO.transform.parent = this.transform;
                var alignDim = EzAlignedDimensionGO.AddComponent<AlignedDimension>();

                if (createDimensionCort != null)
                    StopCoroutine(createDimensionCort);

                createDimensionCort = StartCoroutine(CreateAlignedDimension(alignDim));

                DimensionsList.Add(EzAlignedDimensionGO.gameObject); // add the parent GO to the list.
            }
        }
        public void EzAngledDimension()
        {
            if (!isCreating)
            {
                isCreating = true;  // this bool will set to false after draw the dimension.
                SelectionList.Clear();
                Funcs.UpdateAll(this, DimensionsList, SelectionList);
                GameObject EzAngleDimensionGO = new GameObject("EzAngleDimension");
                EzAngleDimensionGO.transform.parent = this.transform;
                var angleDim = EzAngleDimensionGO.AddComponent<AngleDimension>();

                if (createDimensionCort != null)
                    StopCoroutine(createDimensionCort);

                createDimensionCort = StartCoroutine(CreateAngleDimension(angleDim));

                DimensionsList.Add(EzAngleDimensionGO.gameObject); // add the parent GO to the list.
            }
        }
        public void EzAreaMeasure()
        {
            if (!isCreating)
            {
                isCreating = true;  // this bool will set to false after draw the dimension.
                SelectionList.Clear();
                Funcs.UpdateAll(this, DimensionsList, SelectionList);
                GameObject EzAreaMeasureGO = new GameObject("EzAreaMeasure");
                EzAreaMeasureGO.transform.parent = this.transform;
                var AreaMeasurment = EzAreaMeasureGO.AddComponent<LinearAreaMeasure>();

                if (createDimensionCort != null)
                    StopCoroutine(createDimensionCort);

                createDimensionCort = StartCoroutine(CreateAreaMeasure(AreaMeasurment));

                DimensionsList.Add(EzAreaMeasureGO.gameObject); // add the parent GO to the list.
            }
        }
        public void HideSelectedDimensions()
        {
            Funcs.HideSelectedDimensions(SelectionList);
            SelectionList.Clear();
        }
        public void UnhideAll()
        {
            Funcs.UnhideAllDimensions(this, DimensionsList);
            Funcs.UpdateAll(this, DimensionsList, SelectionList);
        }
        public void DeleteSelectedDimensions()
        {
            Funcs.DeleteDimensions(DimensionsList, SelectionList);
        }
        public void UpdateAll()
        {
            Funcs.UpdateAll(this, DimensionsList, SelectionList);
        }
    }

}
