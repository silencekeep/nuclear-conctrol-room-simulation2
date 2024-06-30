using System.Collections.Generic;
using UnityEngine;
using EzDimension;
using EzDimension.dims;
using TMPro;
using EarcutNet;

namespace EzDimension.dims
{
    public class LinearAreaMeasure : MonoBehaviour
    {
        [internalFuncs.ReadOnly] public string workingPlane;
        [Header("Always Individual :")]
        [Space]
        public EzDimStarter valuesReference;
        [Space]
        public bool cameraBaseNumberInXYDir;
        public bool rotateText180LocalY;
        public bool rotateText180LocalZ;
        public bool rotateText180LocalX;
        [Header("____________________________________________________________________________________________________________________________________________")]
        [Space]
        [Header("Global & Individual :")]
        [Space]
        [Header("—— Parameters :")]
        [Space]
        public bool isIndividual;
        public float textSize;
        public float localYOffset;
        public float textLocalYOffset;
        public float borderLocalYOffset;
        public float borderThickness;
        [Space]
        public Vector2 positionOffset;
        [Space]
        public bool enableBorderLine = false;
        [Space]
        [Header("—— Colors :")]
        [Space]
        public Color numberColor = new Color(0, 0, 0, 1);
        public Color borderColor = new Color(0, 0, 0, 1);
        public Color surfaceColor = new Color(0, 0, 0, 1);
        [Space]
        public Color hoveredTint = new Color(0, 0.3f, 0.5f, 1);
        public Color selectedTint = new Color(0, 0.7f, 1, 1);
        public Color hoveredOnSelectedTint = new Color(0.5f, 0.8f, 1, 1);
        [Space]
        [Header("—— Materials :")]
        [Space]
        public Material surfaceMaterial;
        public Material BorderMaterial;

        [HideInInspector]
        public GameObject handlesParent;
        [HideInInspector]
        public Transform cameraTransform;
        [HideInInspector]
        public List<Vector2> points = new List<Vector2>();
        [HideInInspector]
        public List<GameObject> handlesList = new List<GameObject>();
        [HideInInspector]
        public float area;
        [HideInInspector]
        public GameObject borderLineGO;
        [HideInInspector]
        public LineRenderer borderLine;
        [HideInInspector]
        public Vector3 meshCenter;
        [HideInInspector]
        public bool allowDrawSurface;
        [HideInInspector]
        public Vector3 hitNormal;
        [HideInInspector]
        public Funcs.MeasurementPlane measurementPlane;
        [HideInInspector]
        public bool isDone;
        [HideInInspector]
        public bool drawMode = true;
        [HideInInspector]

        Quaternion HandlesParentRotation;
        Vector2 numberBoundsSize;
        float tempPointHeight;
        string unitTextShortForm;
        bool flipMesh;
        TextMeshPro number;
        GameObject borderLineParent;
        GameObject numberGO;
        GameObject numberParent;
        GameObject meshGO;
        BoxCollider numberCol;

        private void Awake()
        {
            numberGO = new GameObject("EzDimensionAreaNumber");
            numberParent = new GameObject("NumberParent");
            handlesParent = new GameObject("HandlesParent");
            borderLineParent = new GameObject("BorderLineParent");
            borderLineGO = new GameObject("borderLineGO");
            meshGO = new GameObject("MeshGO");
            borderLineParent.transform.parent = this.transform;
            borderLineGO.transform.parent = borderLineParent.transform;
            borderLineGO.SetActive(false);
            meshGO.transform.parent = this.transform;
            handlesParent.transform.parent = this.transform;
            numberParent.transform.parent = this.transform;
            numberGO.transform.parent = numberParent.transform;
            number = numberGO.AddComponent<TextMeshPro>();
            meshGO.AddComponent<MeshFilter>();
            meshGO.AddComponent<MeshRenderer>();
            borderLine = borderLineGO.AddComponent<LineRenderer>();
            numberCol = numberGO.gameObject.AddComponent<BoxCollider>();

            // value reference :
            valuesReference = this.GetComponentInParent<EzDimStarter>();
            cameraTransform = valuesReference.cameraTransform;
            measurementPlane = valuesReference.measurementPlane;
            measurementPlane = valuesReference.areaMeasurementPlane;
            localYOffset = valuesReference.areaLocalYOffset;
            textLocalYOffset = valuesReference.areaTextLocalYOffset;
            borderLocalYOffset = valuesReference.areaBorderLocalYOffset;
            textSize = valuesReference.textSize;
            borderThickness = valuesReference.areaBorderLineThickness;
            surfaceMaterial = new Material(valuesReference.areaSurfaceMaterial);
            BorderMaterial = new Material(valuesReference.areaBorderMaterial);
            workingPlane = valuesReference.areaMeasurementPlane.ToString();
            numberColor = valuesReference.numberColor;
            borderColor = valuesReference.areaBorderColor;
            surfaceColor = valuesReference.areaSurfaceColor;
            hoveredTint = valuesReference.hoveredTint;
            selectedTint = valuesReference.selectedTint;
            hoveredOnSelectedTint = valuesReference.hoveredOnSelectedTint;
            enableBorderLine = valuesReference.enableAreaBorderLine;
        }

        public void updatePointsList
            (List<Vector2> _points, Funcs.MeasurementPlane _measurmentPlane)
        {
            if (handlesList.Count > 0)
            {
                _points.Clear();

                foreach (GameObject handle in handlesList)
                {
                    handle.transform.parent = handlesParent.transform;
                }

                if (_measurmentPlane == Funcs.MeasurementPlane.Free)
                {
                    HandlesParentRotation = handlesParent.transform.rotation;
                    handlesParent.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    tempPointHeight = handlesList[0].transform.position.y;
                }

                foreach (GameObject handle in handlesList)
                {
                    if (_measurmentPlane == Funcs.MeasurementPlane.XZ || _measurmentPlane == Funcs.MeasurementPlane.Free)
                        _points.Add(new Vector2(handle.transform.position.x, handle.transform.position.z));
                    else if (_measurmentPlane == Funcs.MeasurementPlane.XY)
                        _points.Add(new Vector2(handle.transform.position.x, handle.transform.position.y));
                    else if (measurementPlane == Funcs.MeasurementPlane.ZY)
                        _points.Add(new Vector2(handle.transform.position.y, handle.transform.position.z));
                }
            }
        }

        public void UpdateBorderLine(List<Vector2> _points, bool _enableBorderLine, float _borderThickness,
            Funcs.MeasurementPlane _measurementPlane, Material _borderMaterial, float _localYOffset, float _borderLocalYOffset,
            Color _borderColor
            )
        {
            float finalYOffset = 0;

            if (_enableBorderLine)
            {
                borderLineGO.SetActive(true);
                borderLine.positionCount = _points.Count;
                borderLine.loop = true;
                borderLine.startWidth = _borderThickness;
                borderLine.material = _borderMaterial;
                borderLine.useWorldSpace = false;
                borderLine.alignment = LineAlignment.TransformZ;
                borderLine.material.color = _borderColor;

                for (int i = 0; i < _points.Count; i++)
                {
                    if (_measurementPlane == Funcs.MeasurementPlane.XZ)
                    {
                        if (!flipMesh)
                            finalYOffset = -_localYOffset - _borderLocalYOffset;
                        else
                            finalYOffset = _localYOffset + _borderLocalYOffset;

                        borderLineParent.transform.eulerAngles = new Vector3(90, 0, 0);
                        borderLine.SetPosition(i, new Vector3(_points[i].x, _points[i].y, -handlesList[0].transform.position.y + finalYOffset));
                    }
                    else if (_measurementPlane == Funcs.MeasurementPlane.XY)
                    {
                        if (!flipMesh)
                            finalYOffset = -_localYOffset - _borderLocalYOffset;
                        else
                            finalYOffset = _localYOffset + _borderLocalYOffset;

                        borderLine.SetPosition(i, new Vector3(_points[i].x, _points[i].y, handlesList[0].transform.position.z + finalYOffset));
                    }
                    else if (_measurementPlane == Funcs.MeasurementPlane.ZY)
                    {
                        if (!flipMesh)
                            finalYOffset = -_localYOffset - _borderLocalYOffset;
                        else
                            finalYOffset = _localYOffset + _borderLocalYOffset;

                        borderLineParent.transform.eulerAngles = new Vector3(0, 90, 90);
                        borderLine.SetPosition(i, new Vector3(_points[i].x, _points[i].y, handlesList[0].transform.position.x + finalYOffset));
                    }
                    else if (_measurementPlane == Funcs.MeasurementPlane.Free)
                    {
                        finalYOffset = _localYOffset + _borderLocalYOffset;

                        borderLine.SetPosition(i, new Vector3(_points[i].x, _points[i].y, 0));

                        borderLineGO.transform.localEulerAngles = new Vector3(90, 0, 0);
                        borderLineGO.transform.localPosition = new Vector3(0, tempPointHeight + finalYOffset, 0);
                        borderLineParent.transform.eulerAngles = new Vector3(90 + handlesParent.transform.eulerAngles.x, handlesParent.transform.eulerAngles.y, handlesParent.transform.eulerAngles.z);
                    }
                }
            }
            else
                borderLineGO.SetActive(false);

        }

        public void DrawArea(List<Vector2> _points, EzDimStarter _starterScript, Funcs.MeasurementPlane _measurementPlane,
            float _localYOffset, float _borderLocalYOffset, float _textLocalYOffset, Material _surfaceMaterial, Material _borderMaterial,
            bool _enableBorderLine, float _borderThickness, float _textSize, Transform _cameraTransform, Vector2 _positionOffset,
            Color _numberColor, Color _borderColor, Color _surfaceColor
            )
        {
            updatePointsList(_points, _measurementPlane);
            float offset = 0f;

            if (measurementPlane == Funcs.MeasurementPlane.XZ || measurementPlane == Funcs.MeasurementPlane.Free)
                offset = handlesList[0].transform.position.y;
            else if (measurementPlane == Funcs.MeasurementPlane.XY)
                offset = handlesList[0].transform.position.z;
            else if (measurementPlane == Funcs.MeasurementPlane.ZY)
                offset = handlesList[0].transform.position.x;

            flipMesh = internalFuncs.FlipNormal(hitNormal, measurementPlane);
            float finalOffset = flipMesh ? _localYOffset : -_localYOffset;
            if (measurementPlane == Funcs.MeasurementPlane.XZ || measurementPlane == Funcs.MeasurementPlane.Free)
                finalOffset = -finalOffset;
            Mesh mesh = Earcut.triangulate(
                meshGO.gameObject.GetComponent<MeshFilter>().sharedMesh,
                _points,
                measurementPlane,
                offset + finalOffset,
                flipMesh
                );
            meshGO.GetComponent<MeshFilter>().sharedMesh = mesh;
            meshGO.GetComponent<MeshRenderer>().material = surfaceMaterial;
            meshGO.GetComponent<MeshRenderer>().material.color = _surfaceColor;

            if (mesh != null)
                area = internalFuncs.Area(mesh);

            meshCenter = mesh.bounds.center;

            if (measurementPlane == Funcs.MeasurementPlane.Free)
            {
                if (!isDone)
                    meshGO.transform.parent = handlesParent.transform;

                handlesParent.transform.rotation = HandlesParentRotation;
                meshGO.transform.parent = this.transform;
                foreach (GameObject handle in handlesList)
                {
                    handle.transform.hasChanged = false;
                }
            }

            UpdateBorderLine(_points, _enableBorderLine, _borderThickness, _measurementPlane, _borderMaterial, _localYOffset,
                _borderLocalYOffset, _borderColor);
            UpdateAreaNumber(_starterScript, area, internalFuncs.AreaUnitCalculator(_starterScript), meshCenter, _textSize, _cameraTransform
                , _measurementPlane, _textLocalYOffset, _positionOffset, _numberColor
                );
        }

        void UpdateAreaNumber(EzDimStarter _starterScript, float _area, float _unitMultiplier, Vector3 _numberCenter, float _textSize,
            Transform _cameraTransform, Funcs.MeasurementPlane _measurementPlane, float _textLocalYOffset, Vector2 _positionOffset
            , Color _numberColor)
        {
            if (_starterScript.showUnitAfterNumber)
                unitTextShortForm = internalFuncs.GetUnitTextShortForm(valuesReference);
            else
                unitTextShortForm = "";

            if (_starterScript.showUnitAfterNumber)
                unitTextShortForm = internalFuncs.GetUnitTextShortForm(valuesReference);
            else
                unitTextShortForm = "";

            number.text = ((_area * _unitMultiplier).ToString(internalFuncs.DecimalCalculator(_starterScript)) + " " + unitTextShortForm);
            number.fontSize = _textSize;
            number.enableWordWrapping = false;
            number.color = _numberColor;

            number.horizontalAlignment = HorizontalAlignmentOptions.Center;
            number.verticalAlignment = VerticalAlignmentOptions.Middle;
            numberBoundsSize = new Vector2(number.preferredWidth, number.preferredHeight);
            number.rectTransform.sizeDelta = numberBoundsSize;
            UpdateNumberPosAndRot(_numberCenter, _cameraTransform, _measurementPlane, _textLocalYOffset, _positionOffset);
        }

        public void UpdateNumberPosAndRot(Vector3 _numberCenter, Transform _cameraTransform, Funcs.MeasurementPlane _measurementPlane,
            float _textLocalYOffset, Vector2 _positionOffset)
        {
            float finalLocalYOffset;

            if (_measurementPlane == Funcs.MeasurementPlane.XZ)
            {
                if (!flipMesh)
                    finalLocalYOffset = _textLocalYOffset;
                else
                    finalLocalYOffset = -_textLocalYOffset;

                numberParent.transform.position = _numberCenter + new Vector3(_positionOffset.x, finalLocalYOffset, _positionOffset.y);
                Vector3 cameraDirection = _numberCenter - new Vector3(_cameraTransform.position.x, finalLocalYOffset, _cameraTransform.position.z);

                if (cameraBaseNumberInXYDir)
                    numberGO.transform.rotation = Quaternion.LookRotation(Vector3.down, cameraDirection);
                else
                    numberGO.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
            }
            else if (_measurementPlane == Funcs.MeasurementPlane.XY)
            {
                if (!flipMesh)
                {
                    finalLocalYOffset = -_textLocalYOffset;
                    numberParent.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    finalLocalYOffset = _textLocalYOffset;
                    numberParent.transform.eulerAngles = new Vector3(0, 180, 0);
                }

                numberParent.transform.position = _numberCenter + new Vector3(_positionOffset.x, _positionOffset.y, finalLocalYOffset);
            }
            else if (_measurementPlane == Funcs.MeasurementPlane.ZY)
            {
                if (!flipMesh)
                {
                    finalLocalYOffset = -_textLocalYOffset;
                    numberParent.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    finalLocalYOffset = _textLocalYOffset;
                    numberParent.transform.eulerAngles = new Vector3(0, -90, 0);
                }

                numberParent.transform.position = _numberCenter + new Vector3(finalLocalYOffset, _positionOffset.x, _positionOffset.y);
            }
            else if (_measurementPlane == Funcs.MeasurementPlane.Free)
            {
                numberGO.transform.localEulerAngles = new Vector3(90, 0, 0);
                numberParent.transform.localEulerAngles = new Vector3(handlesParent.transform.eulerAngles.x + 90, handlesParent.transform.eulerAngles.y, handlesParent.transform.eulerAngles.z);
                numberGO.transform.localPosition = _numberCenter + new Vector3(_positionOffset.x, _textLocalYOffset, _positionOffset.y);

                if (numberGO.transform.up.y < 0) // align up direction of the number
                    numberGO.transform.localEulerAngles += new Vector3(0, 0, 180);
            }

            if (rotateText180LocalY)
                numberGO.transform.localEulerAngles += new Vector3(0, 180, 0);
            if (rotateText180LocalZ)
                numberGO.transform.localEulerAngles += new Vector3(0, 0, 180);
            if (rotateText180LocalX)
                numberGO.transform.localEulerAngles += new Vector3(180, 0, 0);

            numberCol.size = new Vector3(number.bounds.size.x, number.bounds.size.y, 0.001f);
        }
    }
}