using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using EzDimension;
using UnityEditor;

namespace EzDimension.dims
{
    public class AngleDimension : MonoBehaviour
    {
        [Header("Always Individual :")]
        [Space]
        public EzDimStarter valuesReference;
        [Space]
        [Space]
        public Funcs.AngleMeasurmentPlane angleMeasurementPlane;
        public bool rotateTextBy180;
        public bool reverseTextPosIfNotFit;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global & Individual :")]
        [Space]
        [Header("—— Parameters :")]
        [Space]
        public bool isIndividual = false;
        public bool isDynamic = true;
        [Space]
        public float linesThickness = 1f;
        public float textSize = 1f;
        public float arcScale;
        public float textOffsetFromCenter = 0.5f;
        public float normalOffset;
        [Space]
        public Vector2 textOffsetIfNotFit;
        [Space]
        [Header("—— Colors :")]
        [Space]
        public Color numberColor = new Color(0, 0, 0, 1);
        public Color mainColor = new Color(0, 0, 0, 1);
        public Color arcColor = new Color(0, 0, 0, 1);
        [Space]
        public Color hoveredTint = new Color(0, 0.3f, 0.5f, 1);
        public Color selectedTint = new Color(0, 0.7f, 1, 1);
        public Color hoveredOnSelectedTint = new Color(0.5f, 0.8f, 1, 1);
        [Space]
        [Header("—— Materials :")]
        [Space]
        public Material mainLinesMat;
        public Material arcMat;

        [HideInInspector]
        public Vector3 pointA = Vector3.zero;
        [HideInInspector]
        public Vector3 pointB = Vector3.zero;
        [HideInInspector]
        public Vector3 pointC = Vector3.zero;
        [HideInInspector]
        public GameObject mainParent;
        [HideInInspector]
        public Transform cameraTransform;

        [HideInInspector]
        public bool trueOnFirstStep = false;
        [HideInInspector]
        public bool trueOnSecoundStep = false;
        [HideInInspector]
        public bool isDone = false;
        [HideInInspector]
        public GameObject lineAGO;
        [HideInInspector]
        public GameObject lineBGO;
        [HideInInspector]
        public Vector3 pointBProxy;
        [HideInInspector]
        public Vector3 pointCProxy;
        [HideInInspector]
        public GameObject arcParentGO;
        [HideInInspector]
        public Plane plane;
        [HideInInspector]
        public GameObject numberGO;
        [HideInInspector]
        public Vector3 firstPointHitNormal;

        float maxWidth;
        float textDistance;

        // dynamic length :
        GameObject dynamicTransforms;
        [HideInInspector]
        public GameObject objectATransformGO;
        [HideInInspector]
        public GameObject objectBTransformGO;
        [HideInInspector]
        public GameObject objectCTransformGO;
        [HideInInspector]
        public GameObject pointATransformGO;
        [HideInInspector]
        public GameObject pointBTransformGO;
        [HideInInspector]
        public GameObject pointCTransformGO;
        [HideInInspector]
        public GameObject objectA;
        [HideInInspector]
        public GameObject objectB;
        [HideInInspector]
        public GameObject objectC;

        // create and update:
        float unitMultiplier;
        LineRenderer lineA;
        LineRenderer lineB;
        GameObject arcGO;
        GameObject numberParentGO;
        BoxCollider numberCol;
        TextMeshPro number;
        Vector3 pointOnBisector;
        Vector2 numberBoundsSize;
        Vector2 oldMousePos;
        float degree = 0;
        bool signedAngleMoreThanZero;
        // end create and update

        private void Awake()
        {
            lineAGO = new GameObject("Line A");
            lineBGO = new GameObject("Line B");
            lineA = lineAGO.gameObject.AddComponent<LineRenderer>();
            lineB = lineBGO.gameObject.AddComponent<LineRenderer>();
            numberGO = new GameObject("EzDimensionDistanceNumber");
            arcParentGO = new GameObject("Arc Parent");
            arcGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
            arcGO.GetComponent<MeshCollider>().enabled = false;
            arcGO.name = "Arc";
            numberParentGO = new GameObject("NumberParent");
            mainParent = this.gameObject;


            // value reference
            valuesReference = GetComponentInParent<EzDimStarter>();
            arcScale = valuesReference.arcScale;
            unitMultiplier = valuesReference.customUnitMultiplier;
            angleMeasurementPlane = valuesReference.angleMeasurmentPlane;
            linesThickness = valuesReference.linesThickness;
            textSize = valuesReference.textSize;
            textOffsetFromCenter = valuesReference.angleDimTextOffsetFromCenter;
            textOffsetIfNotFit = valuesReference.textOffsetWhenNotFit;
            cameraTransform = valuesReference.cameraTransform;
            normalOffset = valuesReference.angleDimHitNormalOffset;
            // materials :
            mainLinesMat = new Material(valuesReference.mainLineMaterial);
            arcMat = new Material(valuesReference.arcMaterial);
            // colors :
            numberColor = valuesReference.numberColor;
            mainColor = valuesReference.mainColor;
            arcColor = valuesReference.arcColor;
            hoveredTint = valuesReference.hoveredTint;
            selectedTint = valuesReference.selectedTint;
            hoveredOnSelectedTint = valuesReference.hoveredOnSelectedTint;
        }

        void Start()
        {
            CreateDimension(pointA, pointB, pointC, mainParent);
            Funcs.SetActiveAllChilds(this.transform, false);
        }

        void CreateDimension(Vector3 _pointA, Vector3 _pointB, Vector3 _pointC, GameObject _mainParent)
        {
            // lines :
            lineAGO.transform.parent = _mainParent.transform;
            lineBGO.transform.parent = _mainParent.transform;
            // number :
            numberGO.transform.parent = numberParentGO.transform;
            arcGO.transform.parent = arcParentGO.transform;
            arcParentGO.transform.parent = _mainParent.transform;
            numberParentGO.transform.parent = _mainParent.transform;
            number = numberGO.AddComponent<TextMeshPro>();
            numberCol = numberGO.gameObject.AddComponent<BoxCollider>();
            arcGO.GetComponent<MeshRenderer>().material = arcMat;
            // dynamic objects :
            dynamicTransforms = new GameObject("dynamicTransforms");
            objectATransformGO = new GameObject("objectATransformGO");
            objectBTransformGO = new GameObject("objectBTransformGO");
            objectCTransformGO = new GameObject("objectCTransformGO");
            pointATransformGO = new GameObject("pointATransformGO");
            pointBTransformGO = new GameObject("pointBTransformGO");
            pointCTransformGO = new GameObject("pointCTransformGO");
            dynamicTransforms.transform.parent = this.transform;
            objectATransformGO.transform.parent = dynamicTransforms.transform;
            objectBTransformGO.transform.parent = dynamicTransforms.transform;
            objectCTransformGO.transform.parent = dynamicTransforms.transform;
            pointATransformGO.transform.parent = objectATransformGO.transform;
            pointBTransformGO.transform.parent = objectBTransformGO.transform;
            pointCTransformGO.transform.parent = objectCTransformGO.transform;


            // turn off shadow
            // lines
            lineA.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineA.receiveShadows = false;
            lineB.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineB.receiveShadows = false;

            // arrows
            arcGO.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            arcGO.GetComponent<MeshRenderer>().receiveShadows = false;

        }

        public void UpdateDimension(Vector3 _pointA, Vector3 _pointB, Vector3 _pointC, float _lineTickness, float _textSize, float _arcScale, float _normalOffset,
            float _angleDimensionTextOffset, Vector2 _textOffsetWhenNotFit, Color _numberColor, Color _mainColor, Color _arcColor,
            Transform _cameraTransform, GameObject _mainParent, Material _arcMaterial, Material _mainMaterial,
            Funcs.AngleMeasurmentPlane _angleMeasurementPlae, float _unitMultiplier)
        {
            // materials :
            lineA.material = _mainMaterial;
            lineB.material = lineA.material;
            arcGO.GetComponent<MeshRenderer>().material = _arcMaterial;
            // colors :
            lineA.sharedMaterial.color = _mainColor;
            number.color = _numberColor.linear;
            _arcMaterial.color = _arcColor;

            arcGO.transform.localScale = new Vector3(_arcScale, _arcScale, _arcScale);
            numberParentGO.transform.position = pointBProxy;

            if (_angleMeasurementPlae == Funcs.AngleMeasurmentPlane.XZ) // __________________XZ__________________
            {
                pointBProxy = new Vector3(_pointB.x, _pointA.y, _pointB.z);
                pointCProxy = new Vector3(_pointC.x, _pointA.y, _pointC.z);
                arcParentGO.transform.position = pointBProxy;

                degree = Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.up);
                if (degree >= 0)
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.up);
                    arcGO.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    _arcMaterial.SetFloat("_Degree", 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.up));
                    degree = 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.up);
                    signedAngleMoreThanZero = true;
                }
                else
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.up);
                    arcGO.transform.localEulerAngles = new Vector3(90, 0, 180);
                    _arcMaterial.SetFloat("_Degree", 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.up));
                    degree = 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.up);
                    signedAngleMoreThanZero = false;
                }


                pointOnBisector = internalFuncs.AngleBisectorPoint(_pointA, pointBProxy, pointCProxy) * _angleDimensionTextOffset;
                textDistance = (pointBProxy - (pointBProxy + pointOnBisector)).magnitude - (number.bounds.size.y / 2);
                maxWidth = textDistance * Mathf.Tan((degree / 2) * Mathf.Deg2Rad) * 2;
            }
            else if (_angleMeasurementPlae == Funcs.AngleMeasurmentPlane.XY) // __________________XY__________________
            {
                pointBProxy = new Vector3(_pointB.x, _pointB.y, _pointA.z);
                pointCProxy = new Vector3(_pointC.x, _pointC.y, _pointA.z);
                arcParentGO.transform.position = pointBProxy;

                degree = Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.forward);
                if (degree >= 0)
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.forward);
                    arcGO.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    _arcMaterial.SetFloat("_Degree", 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.forward));
                    degree = 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.forward);
                    signedAngleMoreThanZero = true;
                }
                else
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.forward);
                    arcGO.transform.localEulerAngles = new Vector3(90, 180, 0);
                    _arcMaterial.SetFloat("_Degree", 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.forward));
                    degree = 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.forward);
                    signedAngleMoreThanZero = false;
                }

                // calculate the space between two lines where the text is placed.
                pointOnBisector = internalFuncs.AngleBisectorPoint(_pointA, pointBProxy, pointCProxy) * _angleDimensionTextOffset;
                textDistance = (pointBProxy - (pointBProxy + pointOnBisector)).magnitude - (number.bounds.size.y / 2);
                maxWidth = textDistance * Mathf.Tan((degree / 2) * Mathf.Deg2Rad) * 2;
            }
            else if (_angleMeasurementPlae == Funcs.AngleMeasurmentPlane.ZY) // __________________ZY__________________
            {
                pointBProxy = new Vector3(_pointA.x, _pointB.y, _pointB.z);
                pointCProxy = new Vector3(_pointA.x, _pointC.y, _pointC.z);
                arcParentGO.transform.position = pointBProxy;

                degree = Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right);

                if (degree >= 0)
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.right);
                    arcGO.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    _arcMaterial.SetFloat("_Degree", 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right));
                    degree = 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right);
                    signedAngleMoreThanZero = true;
                }
                else
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.right);
                    arcGO.transform.localEulerAngles = new Vector3(90, 0, 180);
                    _arcMaterial.SetFloat("_Degree", 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right));
                    degree = 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right);
                    signedAngleMoreThanZero = false;
                }

                // calculate the space between two lines where the text is placed.
                pointOnBisector = internalFuncs.AngleBisectorPoint(_pointA, pointBProxy, pointCProxy) * _angleDimensionTextOffset;
                textDistance = (pointBProxy - (pointBProxy + pointOnBisector)).magnitude - (number.bounds.size.y / 2);
                maxWidth = textDistance * Mathf.Tan((degree / 2) * Mathf.Deg2Rad) * 2;
            }
            else //__________________Free__________________
            {
                pointBProxy = _pointB;
                pointCProxy = _pointC;

                if (degree >= 0)
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.right);
                    arcGO.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    _arcMaterial.SetFloat("_Degree", 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right));
                    degree = 180 - Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right);
                    signedAngleMoreThanZero = true;
                }
                else
                {
                    arcParentGO.transform.LookAt(_pointA, Vector3.right);
                    arcGO.transform.localEulerAngles = new Vector3(90, 0, 180);
                    _arcMaterial.SetFloat("_Degree", 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right));
                    degree = 180 + Vector3.SignedAngle(pointBProxy - _pointA, pointCProxy - pointBProxy, Vector3.right);
                    signedAngleMoreThanZero = false;
                }

                // calculate the space between two lines where the text is placed.
                pointOnBisector = internalFuncs.AngleBisectorPoint(_pointA, pointBProxy, pointCProxy) * _angleDimensionTextOffset;
                textDistance = (pointBProxy - (pointBProxy + pointOnBisector)).magnitude - (number.bounds.size.y / 2);
                maxWidth = textDistance * Mathf.Tan((degree / 2) * Mathf.Deg2Rad) * 2;
            }


            UpdateTextAndIndicator(_pointA, pointBProxy, pointCProxy, _arcMaterial, _textOffsetWhenNotFit, _cameraTransform, _angleMeasurementPlae, _normalOffset,
                _angleDimensionTextOffset);

            // number :
            number.text = (degree).ToString(internalFuncs.DecimalCalculator(valuesReference));
            number.fontSize = _textSize;
            number.enableWordWrapping = false;
            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;
            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);

            // lines :
            _lineTickness = Mathf.Abs(_lineTickness);
            lineA.startWidth = _lineTickness;
            lineB.startWidth = _lineTickness;
            lineA.SetPosition(0, _pointA + firstPointHitNormal * _normalOffset);
            lineA.SetPosition(1, pointBProxy + firstPointHitNormal * _normalOffset);
            lineB.SetPosition(0, pointBProxy + firstPointHitNormal * _normalOffset);
            lineB.SetPosition(1, pointCProxy + firstPointHitNormal * _normalOffset);
        }

        public void UpdateTextAndIndicator(Vector3 _pointA, Vector3 _pointBproxy, Vector3 _pointCProxy, Material _arcMaterial,
            Vector2 _textOffsetWhenNotFit, Transform _cameraTransform, Funcs.AngleMeasurmentPlane _angleMeasurementPlane,
            float _normalOffset, float _angleDimensionTextOffset)
        {
            plane.Set3Points(_pointA, _pointBproxy, _pointCProxy);
            arcParentGO.transform.position = _pointBproxy;
            arcParentGO.transform.rotation = Quaternion.LookRotation(plane.normal, (_pointBproxy - _pointA).normalized);
            arcGO.transform.localPosition = Vector3.zero;
            arcGO.transform.localEulerAngles = Vector3.zero;
            // temp
            _arcMaterial.SetFloat("_Degree", 180 - Vector3.SignedAngle(_pointBproxy - _pointA, _pointCProxy - _pointBproxy, plane.normal));
            signedAngleMoreThanZero = true;

            degree = 180 - Vector3.SignedAngle(_pointBproxy - _pointA, _pointCProxy - _pointBproxy, plane.normal);

            // calculate the space between two lines where the text is placed.
            pointOnBisector = internalFuncs.AngleBisectorPoint(_pointA, _pointBproxy, _pointCProxy) * _angleDimensionTextOffset;
            textDistance = (_pointBproxy - (_pointBproxy + pointOnBisector)).magnitude - (number.bounds.size.y / 2);
            maxWidth = textDistance * Mathf.Tan((degree / 2) * Mathf.Deg2Rad) * 2;
            //change the number position if angle was tight
            if (number.bounds.size.x >= maxWidth)
            {
                numberGO.transform.position = _pointBproxy;
                numberGO.transform.localEulerAngles = new Vector3(90, 0, -90);
                bool faceToGround = false;
                if (!reverseTextPosIfNotFit)
                {
                    if (_pointA.y <= _pointCProxy.y)
                    {
                        numberParentGO.transform.LookAt(_pointCProxy, plane.normal);
                        numberGO.transform.localPosition = new Vector3(-number.bounds.size.y / 2 - _textOffsetWhenNotFit.x, 0, number.bounds.size.x / 2 + _textOffsetWhenNotFit.y);
                    }
                    else
                    {
                        numberParentGO.transform.LookAt(_pointA, plane.normal);
                        numberGO.transform.localPosition = new Vector3(number.bounds.size.y / 2 + _textOffsetWhenNotFit.x, 0, number.bounds.size.x / 2 + _textOffsetWhenNotFit.y);
                    }
                }
                else
                {
                    if (_pointA.y <= _pointCProxy.y)
                    {
                        numberParentGO.transform.LookAt(_pointA, plane.normal);
                        numberGO.transform.localPosition = new Vector3(number.bounds.size.y / 2 + _textOffsetWhenNotFit.x, 0, number.bounds.size.x / 2 + _textOffsetWhenNotFit.y);
                    }
                    else
                    {
                        numberParentGO.transform.LookAt(_pointCProxy, plane.normal);
                        numberGO.transform.localPosition = new Vector3(-number.bounds.size.y / 2 - _textOffsetWhenNotFit.x, 0, number.bounds.size.x / 2 + _textOffsetWhenNotFit.y);
                    }
                }

                if (numberGO.transform.forward.y >= 0.01) // rotate text when its faced to ground
                {
                    numberGO.transform.localEulerAngles += new Vector3(180, 180, 0);
                    faceToGround = true;
                }

                if (numberGO.transform.up.y < 0.0001) // rotate the text if it was inverted vertically
                {
                    numberGO.transform.localEulerAngles += new Vector3(0, 180, 0);
                }



                if (plane.GetSide(_cameraTransform.position) && !faceToGround) // rotate text when camera went behind and not rotated because it faced to ground
                {

                }
                else if (!plane.GetSide(_cameraTransform.position) && faceToGround)
                {

                }
                else if (!plane.GetSide(_cameraTransform.position) && !faceToGround)
                {
                    numberGO.transform.localEulerAngles += new Vector3(180, 180, 0);
                }
                else
                {
                    numberGO.transform.localEulerAngles -= new Vector3(180, 180, 0);
                }


            }
            else
            {
                if (plane.GetSide(_cameraTransform.position))
                {
                    numberGO.transform.position = _pointBproxy + pointOnBisector;
                    numberGO.transform.rotation = Quaternion.LookRotation(plane.normal * -1, ((_pointBproxy + pointOnBisector * 2) - _pointBproxy).normalized);
                }
                else
                {
                    numberGO.transform.position = _pointBproxy + pointOnBisector;
                    numberGO.transform.rotation = Quaternion.LookRotation(plane.normal, ((_pointBproxy + pointOnBisector) - _pointBproxy).normalized);
                }
            }

            if (_angleMeasurementPlane == Funcs.AngleMeasurmentPlane.XY || _angleMeasurementPlane == Funcs.AngleMeasurmentPlane.ZY)
            {
                if (numberGO.transform.up.y < 0.01) // align up direction of the number
                    numberGO.transform.localEulerAngles += new Vector3(0, 0, 180);
            }

            if (rotateTextBy180)
                numberGO.transform.localEulerAngles += new Vector3(0, 180, 0);

            Vector2 numberBot;
            Vector2 numberUp;
            numberBot = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(numberGO.transform.up * -1);
            numberUp = _cameraTransform.GetComponent<Camera>().WorldToScreenPoint(numberGO.transform.up);

            if (numberBot.y > numberUp.y && _angleMeasurementPlane != Funcs.AngleMeasurmentPlane.Free)
                numberGO.transform.localEulerAngles += new Vector3(0, 180, 0);

            if (numberBot.y > numberUp.y && _angleMeasurementPlane == Funcs.AngleMeasurmentPlane.Free)
            {
                numberGO.transform.localEulerAngles += new Vector3(0, 0, 180);
            }

            // text local Y offset
            numberParentGO.transform.localPosition += firstPointHitNormal * _normalOffset;
            // Arc parent local Y offset 
            arcParentGO.transform.localPosition += firstPointHitNormal * _normalOffset;
            // offset text towards camera
            // number.rectTransform.position += (_cameraTransform.position - numberGO.transform.position).normalized * _textLocalYOffset;
        }

    }
}