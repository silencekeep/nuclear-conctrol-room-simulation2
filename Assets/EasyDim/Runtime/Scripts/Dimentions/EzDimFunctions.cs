using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using EzDimension.dims;

namespace EzDimension
{
    public class Funcs
    {
        static int hoverState;
        static GameObject lastHoveredGO;

        public enum MeasurementDirection { X, Y, Z }
        public enum OffsetDirection { X, Y, Z }
        public enum MeasurementPlane { XZ, XY, ZY, Free }
        public enum AngleMeasurmentPlane { XZ, XY, ZY, Free }
        public enum MeasurmentType { Length, Area, Volume }

        public static void UpdateArea(EzDimStarter _starterScript, LinearAreaMeasure _areaMeasureToUpdate)
        {
            List<Vector2> points = _areaMeasureToUpdate.points;
            Vector2 positionOffset;
            Funcs.MeasurementPlane plane;
            bool enableBorderLine;
            float localYOffset;
            float borderYOffset;
            float textYOffset;
            float borderThickness;
            float textSize;
            Material surfaceMaterial;
            Material borderMaterial;
            Color numberColor, borderColor, surfaceColor;
            Transform cameraTransform;

            if (_areaMeasureToUpdate.isIndividual)
            {
                positionOffset = _areaMeasureToUpdate.positionOffset;
                enableBorderLine = _areaMeasureToUpdate.enableBorderLine;
                localYOffset = _areaMeasureToUpdate.localYOffset;
                borderYOffset = _areaMeasureToUpdate.borderLocalYOffset;
                textYOffset = _areaMeasureToUpdate.textLocalYOffset;
                borderThickness = _areaMeasureToUpdate.borderThickness;
                textSize = _areaMeasureToUpdate.textSize;
                surfaceMaterial = _areaMeasureToUpdate.surfaceMaterial;
                borderMaterial = _areaMeasureToUpdate.BorderMaterial;
                cameraTransform = _areaMeasureToUpdate.cameraTransform;
                numberColor = _areaMeasureToUpdate.numberColor;
                borderColor = _areaMeasureToUpdate.borderColor;
                surfaceColor = _areaMeasureToUpdate.surfaceColor;
            }
            else
            {
                positionOffset = _starterScript.areaNumberPositionOffset;
                enableBorderLine = _starterScript.enableAreaBorderLine;
                localYOffset = _starterScript.areaLocalYOffset;
                borderYOffset = _starterScript.areaBorderLocalYOffset;
                textYOffset = _starterScript.areaTextLocalYOffset;
                borderThickness = _starterScript.areaBorderLineThickness;
                textSize = _starterScript.textSize;
                surfaceMaterial = _starterScript.areaSurfaceMaterial;
                borderMaterial = _starterScript.areaBorderMaterial;
                cameraTransform = _starterScript.cameraTransform;
                numberColor = _starterScript.numberColor;
                borderColor = _starterScript.areaBorderColor;
                surfaceColor = _starterScript.areaSurfaceColor;
            }

            plane = _areaMeasureToUpdate.measurementPlane;

            if (_areaMeasureToUpdate.handlesList.Count > 0)
            {
                _areaMeasureToUpdate.DrawArea(points, _starterScript, plane, localYOffset, borderYOffset, textYOffset, surfaceMaterial, borderMaterial,
                    enableBorderLine, borderThickness, textSize, cameraTransform, positionOffset, numberColor, borderColor, surfaceColor);
            }
        }
        public static void UpdateAngle(EzDimStarter _starterScript, AngleDimension _dimensionToUpdate)
        {
            var angleDim = _dimensionToUpdate;

            if (angleDim.objectA != null)
            {
                angleDim.objectATransformGO.transform.position = angleDim.objectA.transform.position;
                angleDim.objectATransformGO.transform.rotation = angleDim.objectA.transform.rotation;
                angleDim.objectATransformGO.transform.localScale = angleDim.objectA.transform.localScale;
            }

            if (angleDim.objectB != null)
            {
                angleDim.objectBTransformGO.transform.position = angleDim.objectB.transform.position;
                angleDim.objectBTransformGO.transform.rotation = angleDim.objectB.transform.rotation;
                angleDim.objectBTransformGO.transform.localScale = angleDim.objectB.transform.localScale;
            }

            if (angleDim.objectC != null)
            {
                angleDim.objectCTransformGO.transform.position = angleDim.objectC.transform.position;
                angleDim.objectCTransformGO.transform.rotation = angleDim.objectC.transform.rotation;
                angleDim.objectCTransformGO.transform.localScale = angleDim.objectC.transform.localScale;
            }

            Vector3 pointAPos;
            Vector3 pointBPos;
            Vector3 pointCPos;

            if (angleDim.isIndividual)
            {
                if (angleDim.isDynamic)
                {
                    pointAPos = angleDim.pointATransformGO.transform.position;
                    pointBPos = angleDim.pointBTransformGO.transform.position;
                    pointCPos = angleDim.pointCTransformGO.transform.position;
                }
                else
                {
                    pointAPos = angleDim.pointA;
                    pointBPos = angleDim.pointB;
                    pointCPos = angleDim.pointC;
                }
            }
            else
            {
                if (_starterScript.isDynamic)
                {
                    pointAPos = angleDim.pointATransformGO.transform.position;
                    pointBPos = angleDim.pointBTransformGO.transform.position;
                    pointCPos = angleDim.pointCTransformGO.transform.position;
                }
                else
                {
                    pointAPos = angleDim.pointA;
                    pointBPos = angleDim.pointB;
                    pointCPos = angleDim.pointC;
                }
            }

            float linesThickness;
            float textSize;
            float angleDimTextOffset;
            float arcScale;
            float normalOffset;
            Vector2 offsetWhenNotFit;
            Color numberColor;
            Color mainColor;
            Color arcColor;
            Material mainLineMat;
            GameObject mainParent;
            Transform cameraTransform;
            Funcs.AngleMeasurmentPlane angleMeasurmentPlane;

            if (angleDim.isIndividual)
            {
                linesThickness = angleDim.linesThickness;
                textSize = angleDim.textSize;
                arcScale = angleDim.arcScale;
                normalOffset = angleDim.normalOffset;
                angleDimTextOffset = angleDim.textOffsetFromCenter;
                offsetWhenNotFit = angleDim.textOffsetIfNotFit;
                numberColor = angleDim.numberColor;
                mainColor = angleDim.mainColor;
                arcColor = angleDim.arcColor;
                mainLineMat = angleDim.mainLinesMat;
                cameraTransform = angleDim.cameraTransform;
            }
            else
            {
                linesThickness = _starterScript.linesThickness;
                textSize = _starterScript.textSize;
                arcScale = _starterScript.arcScale;
                normalOffset = _starterScript.hitNormalOffset;
                angleDimTextOffset = _starterScript.angleDimTextOffsetFromCenter;
                offsetWhenNotFit = _starterScript.textOffsetWhenNotFit;
                numberColor = _starterScript.numberColor;
                mainColor = _starterScript.mainColor;
                arcColor = _starterScript.arcColor;
                mainLineMat = _starterScript.mainLineMaterial;
                cameraTransform = _starterScript.cameraTransform;
            }

            angleMeasurmentPlane = angleDim.angleMeasurementPlane;
            var arcMaterial = angleDim.arcMat;
            mainParent = angleDim.mainParent;

            angleDim.UpdateDimension(pointAPos, pointBPos, pointCPos, linesThickness, textSize, arcScale, normalOffset,
                angleDimTextOffset, offsetWhenNotFit, numberColor, mainColor,
               arcColor, cameraTransform, mainParent, arcMaterial, mainLineMat, angleMeasurmentPlane, internalFuncs.LengthUnitCalculator(_starterScript));
        }
        public static void UpdateLength(EzDimStarter _starterScript, PointToPointDimension _dimensionToUpdate)
        {
            var p2P = _dimensionToUpdate;

            if (p2P.objectA != null)
            {
                p2P.objectATransformGO.transform.position = p2P.objectA.transform.position;
                p2P.objectATransformGO.transform.rotation = p2P.objectA.transform.rotation;
                p2P.objectATransformGO.transform.localScale = p2P.objectA.transform.localScale;
            }

            if (p2P.objectB != null)
            {
                p2P.objectBTransformGO.transform.position = p2P.objectB.transform.position;
                p2P.objectBTransformGO.transform.rotation = p2P.objectB.transform.rotation;
                p2P.objectBTransformGO.transform.localScale = p2P.objectB.transform.localScale;
            }

            Vector3 pointAPos;
            Vector3 pointBPos;

            if (p2P.isIndividual)
            {
                if (p2P.isDynamic)
                {
                    pointAPos = p2P.pointATransformGO.transform.position;
                    pointBPos = p2P.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = p2P.pointA;
                    pointBPos = p2P.pointB;
                }
            }
            else
            {
                if (_starterScript.isDynamic)
                {
                    pointAPos = p2P.pointATransformGO.transform.position;
                    pointBPos = p2P.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = p2P.pointA;
                    pointBPos = p2P.pointB;
                }
            }


            float linesThickness;
            float arrowHeight;
            float textSize;
            float textOffset;
            float textLocalYOffset;
            Color numberColor;
            Color mainColor;
            Color arrowColor;
            Material mainMaterial;
            Material arrowMaterial;
            Transform cameraTransform;
            float normalOffset;

            if (p2P.isIndividual)
            {
                linesThickness = p2P.lineThickness;
                arrowHeight = p2P.arrowHeight;
                textSize = p2P.textSize;
                textOffset = p2P.textOffset;
                textLocalYOffset = p2P.textTowardsCameraOffset;
                numberColor = p2P.numberColor;
                mainColor = p2P.mainColor;
                arrowColor = p2P.arrowColor;
                mainMaterial = p2P.mainLineMat;
                arrowMaterial = p2P.arrowMat;
                cameraTransform = p2P.cameraTransform;
                normalOffset = p2P.NormalOffset;
            }
            else
            {
                linesThickness = _starterScript.linesThickness;
                arrowHeight = _starterScript.arrowHeight;
                textSize = _starterScript.textSize;
                textOffset = _starterScript.textOffset;
                textLocalYOffset = _starterScript.textTowardsCameraOffset;
                numberColor = _starterScript.numberColor;
                mainColor = _starterScript.mainColor;
                arrowColor = _starterScript.arrowColor;
                mainMaterial = _starterScript.mainLineMaterial;
                arrowMaterial = _starterScript.arrowMaterial;
                cameraTransform = _starterScript.cameraTransform;
                normalOffset = _starterScript.hitNormalOffset;
            }

            p2P.UpdateDimension(pointAPos, pointBPos, linesThickness, textSize, textOffset, numberColor, mainColor, arrowColor, mainMaterial, arrowMaterial,
                cameraTransform, p2P.gameObject, arrowHeight, internalFuncs.LengthUnitCalculator(_starterScript), textLocalYOffset,
                normalOffset);
        }
        public static void UpdateLength(EzDimStarter _starterScript, LinearDimension _dimensionToUpdate)
        {
            var linDim = _dimensionToUpdate;

            if (linDim.objectA != null)
            {
                linDim.objectATransformGO.transform.position = linDim.objectA.transform.position;
                linDim.objectATransformGO.transform.rotation = linDim.objectA.transform.rotation;
                linDim.objectATransformGO.transform.localScale = linDim.objectA.transform.localScale;
            }

            if (linDim.objectB != null)
            {
                linDim.objectBTransformGO.transform.position = linDim.objectB.transform.position;
                linDim.objectBTransformGO.transform.rotation = linDim.objectB.transform.rotation;
                linDim.objectBTransformGO.transform.localScale = linDim.objectB.transform.localScale;
            }

            Vector3 pointAPos;
            Vector3 pointBPos;


            if (linDim.isIndividual)
            {
                if (linDim.isDynamic)
                {
                    pointAPos = linDim.pointATransformGO.transform.position;
                    pointBPos = linDim.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = linDim.pointA;
                    pointBPos = linDim.pointB;
                }
            }
            else
            {
                if (_starterScript.isDynamic)
                {
                    pointAPos = linDim.pointATransformGO.transform.position;
                    pointBPos = linDim.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = linDim.pointA;
                    pointBPos = linDim.pointB;
                }
            }

            float linesThickness;
            float secondaryLinesThickness;
            float secondaryLinesExtend;
            float offsetDistance;
            float arrowHeight;
            float textSize;
            float textLocalYOffset;
            float textOffset;
            bool autoTextPosition;
            bool flipTextPosition;
            Color numberColor;
            Color mainColor;
            Color secondaryColor;
            Color arrowColor;
            Material mainMat;
            Material secondaryMat;
            Material arrowMat;
            Transform cameraTransform;
            float normalOffset;

            if (linDim.isIndividual)
            {
                linesThickness = linDim.mainLineThickness;
                secondaryLinesThickness = linDim.secondaryLinesThickness;
                secondaryLinesExtend = linDim.secondaryLinesExtend;
                arrowHeight = linDim.arrowHeight;
                textSize = linDim.textSize;
                textLocalYOffset = linDim.textTowardsCameraOffset;
                textOffset = linDim.textOffset;
                autoTextPosition = linDim.autoTextPosition;
                flipTextPosition = linDim.flipTextPosition;
                numberColor = linDim.numberColor;
                mainColor = linDim.mainColor;
                secondaryColor = linDim.secondaryColor;
                arrowColor = linDim.arrowColor;
                mainMat = linDim.mainLineMat;
                secondaryMat = linDim.secondaryLinesMat;
                arrowMat = linDim.arrowMat;
                cameraTransform = linDim.cameraTransform;
                normalOffset = linDim.NormalOffset;
            }
            else
            {
                linesThickness = _starterScript.linesThickness;
                secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                arrowHeight = _starterScript.arrowHeight;
                textSize = _starterScript.textSize;
                textLocalYOffset = _starterScript.textTowardsCameraOffset;
                textOffset = _starterScript.textOffset;
                autoTextPosition = _starterScript.autoTextPosition;
                flipTextPosition = _starterScript.flipTextPosition;
                numberColor = _starterScript.numberColor;
                mainColor = _starterScript.mainColor;
                secondaryColor = _starterScript.secondaryColor;
                arrowColor = _starterScript.arrowColor;
                mainMat = _starterScript.mainLineMaterial;
                secondaryMat = _starterScript.secondaryLinesMaterial;
                arrowMat = _starterScript.arrowMaterial;
                cameraTransform = _starterScript.cameraTransform;
                normalOffset = _starterScript.hitNormalOffset;
            }

            offsetDistance = linDim.offsetDistance;

            linDim.UpdateDimension(pointAPos, pointBPos, linesThickness, secondaryLinesThickness, secondaryLinesExtend, textSize, textOffset, mainMat,
                secondaryMat, arrowMat, numberColor, mainColor, secondaryColor, arrowColor, cameraTransform, linDim.transform.gameObject, arrowHeight,
                linDim.measurementDirection, linDim.offsetDirection, offsetDistance, autoTextPosition, flipTextPosition,
                internalFuncs.LengthUnitCalculator(_starterScript), textLocalYOffset, normalOffset);
        }
        public static void UpdateLength(EzDimStarter _starterScript, AlignedDimension _dimensionToUpdate)
        {
            var alignDim = _dimensionToUpdate;

            if (alignDim.objectA != null)
            {
                alignDim.objectATransformGO.transform.position = alignDim.objectA.transform.position;
                alignDim.objectATransformGO.transform.rotation = alignDim.objectA.transform.rotation;
                alignDim.objectATransformGO.transform.localScale = alignDim.objectA.transform.localScale;
            }

            if (alignDim.objectB != null)
            {
                alignDim.objectBTransformGO.transform.position = alignDim.objectB.transform.position;
                alignDim.objectBTransformGO.transform.rotation = alignDim.objectB.transform.rotation;
                alignDim.objectBTransformGO.transform.localScale = alignDim.objectB.transform.localScale;
            }

            Vector3 pointAPos;
            Vector3 pointBPos;

            if (alignDim.isIndividual)
            {
                if (alignDim.isDynamic)
                {
                    pointAPos = alignDim.pointATransformGO.transform.position;
                    pointBPos = alignDim.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = alignDim.pointA;
                    pointBPos = alignDim.pointB;
                }
            }
            else
            {
                if (_starterScript.isDynamic)
                {
                    pointAPos = alignDim.pointATransformGO.transform.position;
                    pointBPos = alignDim.pointBTransformGO.transform.position;
                }
                else
                {
                    pointAPos = alignDim.pointA;
                    pointBPos = alignDim.pointB;
                }
            }

            float mainLineThickness;
            float secondaryLinesThickness;
            float secondaryLinesExtend;
            float textSize;
            float textLocalYOffset;
            float textOffset;
            float arrowHeight;
            float offsetDistance;
            bool autoTextPosition;
            bool flipTextPosition;
            Color numberColor;
            Color mainColor;
            Color secondaryColor;
            Color arrowColor;
            Material mainMat;
            Material secondaryMat;
            Material arrowMat;
            float normalOffset;

            Funcs.MeasurementPlane measurementPlane;
            Transform cameraTransform;

            if (alignDim.isIndividual)
            {
                mainLineThickness = alignDim.mainLineThickness;
                secondaryLinesThickness = alignDim.secondaryLinesThickness;
                secondaryLinesExtend = alignDim.secondaryLinesExtend;
                textSize = alignDim.textSize;
                textLocalYOffset = alignDim.textTowardsCameraOffset;
                textOffset = alignDim.textOffset;
                numberColor = alignDim.numberColor;
                mainColor = alignDim.mainColor;
                secondaryColor = alignDim.secondaryColor;
                arrowColor = alignDim.arrowColor;
                mainMat = alignDim.mainLineMat;
                secondaryMat = alignDim.secondaryLinesMat;
                arrowMat = alignDim.arrowMat;
                arrowHeight = alignDim.arrowHeight;
                cameraTransform = alignDim.cameraTransform;
                autoTextPosition = alignDim.autoTextPosition;
                flipTextPosition = alignDim.flipTextPosition;
                normalOffset = alignDim.NormalOffset;
            }
            else
            {
                mainLineThickness = _starterScript.linesThickness;
                secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                textSize = _starterScript.textSize;
                textLocalYOffset = _starterScript.textTowardsCameraOffset;
                textOffset = _starterScript.textOffset;
                numberColor = _starterScript.numberColor;
                mainColor = _starterScript.mainColor;
                secondaryColor = _starterScript.secondaryColor;
                arrowColor = _starterScript.arrowColor;
                mainMat = _starterScript.mainLineMaterial;
                secondaryMat = _starterScript.secondaryLinesMaterial;
                arrowMat = _starterScript.arrowMaterial;
                arrowHeight = _starterScript.arrowHeight;
                cameraTransform = _starterScript.cameraTransform;
                autoTextPosition = _starterScript.autoTextPosition;
                flipTextPosition = _starterScript.flipTextPosition;
                normalOffset = _starterScript.hitNormalOffset;
            }

            var mainParent = alignDim.mainParent;
            measurementPlane = alignDim.measurementPlane;
            offsetDistance = alignDim.offsetDistance;

            alignDim.UpdateDimension(pointAPos, pointBPos, mainLineThickness, secondaryLinesThickness, secondaryLinesExtend, textSize, textOffset, numberColor,
                mainColor, secondaryColor, arrowColor, mainMat, secondaryMat, arrowMat, cameraTransform, mainParent, arrowHeight, measurementPlane,
                offsetDistance, internalFuncs.LengthUnitCalculator(_starterScript), autoTextPosition, flipTextPosition, textLocalYOffset, normalOffset);
        }
        public static void UpdateAll(EzDimStarter _starterScript, List<GameObject> _dimensionsList, List<GameObject> _selectionList)
        {
            foreach (GameObject dim in _dimensionsList)
            {
                if (dim.TryGetComponent(out PointToPointDimension p2P))
                {
                    Vector3 _pointAOldPos = p2P.pointATransformGO.transform.position;
                    Vector3 _pointBOldPos = p2P.pointBTransformGO.transform.position;

                    float linesThickness;
                    float arrowHeight;
                    float textSize;
                    float textOffset;
                    float textLocalYOffset;
                    Color numberColor;
                    Color mainColor;
                    Color arrowColor;
                    Material mainMaterial;
                    Material arrowMaterial;
                    Transform cameraTransform;
                    float normalOffset;

                    if (p2P.isIndividual)
                    {
                        linesThickness = p2P.lineThickness;
                        arrowHeight = p2P.arrowHeight;
                        textSize = p2P.textSize;
                        textOffset = p2P.textOffset;
                        textLocalYOffset = p2P.textTowardsCameraOffset;
                        numberColor = p2P.numberColor;
                        mainColor = p2P.mainColor;
                        arrowColor = p2P.arrowColor;
                        mainMaterial = p2P.mainLineMat;
                        arrowMaterial = p2P.arrowMat;
                        cameraTransform = p2P.cameraTransform;
                        normalOffset = p2P.NormalOffset;
                    }
                    else
                    {
                        linesThickness = _starterScript.linesThickness;
                        arrowHeight = _starterScript.arrowHeight;
                        textSize = _starterScript.textSize;
                        textOffset = _starterScript.textOffset;
                        textLocalYOffset = _starterScript.textTowardsCameraOffset;
                        numberColor = _starterScript.numberColor;
                        mainColor = _starterScript.mainColor;
                        arrowColor = _starterScript.arrowColor;
                        mainMaterial = _starterScript.mainLineMaterial;
                        arrowMaterial = _starterScript.arrowMaterial;
                        cameraTransform = _starterScript.cameraTransform;
                        normalOffset = _starterScript.hitNormalOffset;
                    }

                    p2P.UpdateDimension(_pointAOldPos, _pointBOldPos, linesThickness, textSize, textOffset, numberColor, mainColor, arrowColor,
                        mainMaterial, arrowMaterial, cameraTransform, p2P.gameObject, arrowHeight, internalFuncs.LengthUnitCalculator(_starterScript),
                        textLocalYOffset, normalOffset);
                }
                else if (dim.TryGetComponent(out LinearDimension linDim))
                {
                    Vector3 _pointAOldPos = linDim.pointATransformGO.transform.position;
                    Vector3 _pointBOldPos = linDim.pointBTransformGO.transform.position;

                    float linesThickness;
                    float secondaryLinesThickness;
                    float secondaryLinesExtend;
                    float offsetDistance;
                    float arrowHeight;
                    float textSize;
                    float textLocalYOffset;
                    float textOffset;
                    bool autoTextPosition;
                    bool flipTextPosition;
                    Color numberColor;
                    Color mainColor;
                    Color secondaryColor;
                    Color arrowColor;
                    Material mainMat;
                    Material secondaryMat;
                    Material arrowMat;
                    Transform cameraTransform;
                    float normalOffset;

                    if (linDim.isIndividual)
                    {
                        linesThickness = linDim.mainLineThickness;
                        secondaryLinesThickness = linDim.secondaryLinesThickness;
                        secondaryLinesExtend = linDim.secondaryLinesExtend;
                        arrowHeight = linDim.arrowHeight;
                        textSize = linDim.textSize;
                        textLocalYOffset = linDim.textTowardsCameraOffset;
                        textOffset = linDim.textOffset;
                        autoTextPosition = linDim.autoTextPosition;
                        flipTextPosition = linDim.flipTextPosition;
                        numberColor = linDim.numberColor;
                        mainColor = linDim.mainColor;
                        secondaryColor = linDim.secondaryColor;
                        arrowColor = linDim.arrowColor;
                        mainMat = linDim.mainLineMat;
                        secondaryMat = linDim.secondaryLinesMat;
                        arrowMat = linDim.arrowMat;
                        cameraTransform = linDim.cameraTransform;
                        normalOffset = linDim.NormalOffset;
                    }
                    else
                    {
                        linesThickness = _starterScript.linesThickness;
                        secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                        secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                        arrowHeight = _starterScript.arrowHeight;
                        textSize = _starterScript.textSize;
                        textLocalYOffset = _starterScript.textTowardsCameraOffset;
                        textOffset = _starterScript.textOffset;
                        autoTextPosition = _starterScript.autoTextPosition;
                        flipTextPosition = _starterScript.flipTextPosition;
                        numberColor = _starterScript.numberColor;
                        mainColor = _starterScript.mainColor;
                        secondaryColor = _starterScript.secondaryColor;
                        arrowColor = _starterScript.arrowColor;
                        mainMat = _starterScript.mainLineMaterial;
                        secondaryMat = _starterScript.secondaryLinesMaterial;
                        arrowMat = _starterScript.arrowMaterial;
                        cameraTransform = _starterScript.cameraTransform;
                        normalOffset = _starterScript.hitNormalOffset;
                    }
                    offsetDistance = linDim.offsetDistance;

                    linDim.UpdateDimension(_pointAOldPos, _pointBOldPos, linesThickness, secondaryLinesThickness, secondaryLinesExtend, textSize, textOffset,
                        mainMat, secondaryMat, arrowMat, numberColor, mainColor, secondaryColor, arrowColor, cameraTransform, linDim.gameObject, arrowHeight,
                        linDim.measurementDirection, linDim.offsetDirection, offsetDistance, autoTextPosition, flipTextPosition,
                        internalFuncs.LengthUnitCalculator(_starterScript), textLocalYOffset, normalOffset);
                }
                else if (dim.TryGetComponent(out AlignedDimension alignDim))
                {
                    float mainLineThickness;
                    float secondaryLinesThickness;
                    float secondaryLinesExtend;
                    float textSize;
                    float textLocalYOffset;
                    float textOffset;
                    float arrowHeight;
                    float offsetDistance;
                    bool autoTextPosition;
                    bool flipTextPosition;
                    Color numberColor;
                    Color mainColor;
                    Color secondaryColor;
                    Color arrowColor;
                    Material mainMat;
                    Material secondaryMat;
                    Material arrowMat;
                    Funcs.MeasurementPlane measurementPlane;
                    Transform cameraTransform;
                    float normalOffset;

                    if (alignDim.isIndividual)
                    {
                        mainLineThickness = alignDim.mainLineThickness;
                        secondaryLinesThickness = alignDim.secondaryLinesThickness;
                        secondaryLinesExtend = alignDim.secondaryLinesExtend;
                        textSize = alignDim.textSize;
                        textLocalYOffset = alignDim.textTowardsCameraOffset;
                        textOffset = alignDim.textOffset;
                        numberColor = alignDim.numberColor;
                        mainColor = alignDim.mainColor;
                        secondaryColor = alignDim.secondaryColor;
                        arrowColor = alignDim.arrowColor;
                        mainMat = alignDim.mainLineMat;
                        secondaryMat = alignDim.secondaryLinesMat;
                        arrowMat = alignDim.arrowMat;
                        arrowHeight = alignDim.arrowHeight;
                        cameraTransform = alignDim.cameraTransform;
                        autoTextPosition = alignDim.autoTextPosition;
                        flipTextPosition = alignDim.flipTextPosition;
                        normalOffset = alignDim.NormalOffset;
                    }
                    else
                    {
                        mainLineThickness = _starterScript.linesThickness;
                        secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                        secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                        textSize = _starterScript.textSize;
                        textLocalYOffset = _starterScript.textTowardsCameraOffset;
                        textOffset = _starterScript.textOffset;
                        numberColor = _starterScript.numberColor;
                        mainColor = _starterScript.mainColor;
                        secondaryColor = _starterScript.secondaryColor;
                        arrowColor = _starterScript.arrowColor;
                        mainMat = _starterScript.mainLineMaterial;
                        secondaryMat = _starterScript.secondaryLinesMaterial;
                        arrowMat = _starterScript.arrowMaterial;
                        arrowHeight = _starterScript.arrowHeight;
                        cameraTransform = _starterScript.cameraTransform;
                        autoTextPosition = _starterScript.autoTextPosition;
                        flipTextPosition = _starterScript.flipTextPosition;
                        normalOffset = _starterScript.hitNormalOffset;
                    }

                    var _pointAOldPos = alignDim.pointATransformGO.transform.position;
                    var _pointBOldPos = alignDim.pointBTransformGO.transform.position;
                    var mainParent = alignDim.mainParent;
                    offsetDistance = alignDim.offsetDistance;
                    measurementPlane = alignDim.measurementPlane;

                    alignDim.UpdateDimension(_pointAOldPos, _pointBOldPos, mainLineThickness, secondaryLinesThickness, secondaryLinesExtend, textSize,
                        textOffset, numberColor, mainColor, secondaryColor, arrowColor, mainMat, secondaryMat, arrowMat, cameraTransform, mainParent,
                        arrowHeight, measurementPlane, offsetDistance, internalFuncs.LengthUnitCalculator(_starterScript), autoTextPosition,
                        flipTextPosition, textLocalYOffset, normalOffset);
                }
                else if (dim.TryGetComponent(out AngleDimension angleDim))
                {
                    float linesThickness;
                    float textSize;
                    float arcScale = angleDim.arcScale;
                    float normalOffset = angleDim.normalOffset;
                    float angleDimTextOffset;
                    Vector2 offsetWhenNotFit;
                    Color numberColor;
                    Color mainColor;
                    Color arcColor;
                    Material arcMat;
                    Material mainLineMat;
                    GameObject mainParent;
                    Transform cameraTransform;
                    Funcs.AngleMeasurmentPlane angleMeasurmentPlane;

                    if (angleDim.isIndividual)
                    {
                        linesThickness = angleDim.linesThickness;
                        textSize = angleDim.textSize;
                        arcScale = angleDim.arcScale;
                        normalOffset = angleDim.normalOffset;
                        angleDimTextOffset = angleDim.textOffsetFromCenter;
                        offsetWhenNotFit = angleDim.textOffsetIfNotFit;
                        numberColor = angleDim.numberColor;
                        mainColor = angleDim.mainColor;
                        arcColor = angleDim.arcColor;
                        cameraTransform = angleDim.cameraTransform;
                        mainLineMat = angleDim.mainLinesMat;
                    }
                    else
                    {
                        linesThickness = _starterScript.linesThickness;
                        textSize = _starterScript.textSize;
                        arcScale = _starterScript.arcScale;
                        normalOffset = _starterScript.hitNormalOffset;
                        angleDimTextOffset = _starterScript.angleDimTextOffsetFromCenter;
                        offsetWhenNotFit = _starterScript.textOffsetWhenNotFit;
                        numberColor = _starterScript.numberColor;
                        mainColor = _starterScript.mainColor;
                        arcColor = _starterScript.arcColor;
                        cameraTransform = _starterScript.cameraTransform;
                        mainLineMat = _starterScript.mainLineMaterial;
                    }

                    var _pointAOldPos = angleDim.pointATransformGO.transform.position;
                    var _pointBOldPos = angleDim.pointBTransformGO.transform.position;
                    var _pointCOldPos = angleDim.pointCTransformGO.transform.position;
                    arcMat = angleDim.arcMat;
                    angleMeasurmentPlane = angleDim.angleMeasurementPlane;
                    mainParent = angleDim.mainParent;

                    angleDim.UpdateDimension(_pointAOldPos, _pointBOldPos, _pointCOldPos, linesThickness, textSize, arcScale, normalOffset,
                        angleDimTextOffset, offsetWhenNotFit, numberColor, mainColor,
                        arcColor, cameraTransform, mainParent, arcMat, mainLineMat, angleMeasurmentPlane, internalFuncs.LengthUnitCalculator(_starterScript));
                }
                else if (dim.TryGetComponent(out LinearAreaMeasure area))
                {
                    UpdateArea(_starterScript, area);
                }
            }

            HighlightSelectedDims(_starterScript, _selectionList);
        }
        public static void UpdateValues(EzDimStarter _starterScript, List<GameObject> _selectionList, GameObject _dimensionParent)
        {
            if (_dimensionParent.TryGetComponent(out PointToPointDimension p2P))
            {
                Vector3 _pointAOldPos = p2P.pointATransformGO.transform.position;
                Vector3 _pointBOldPos = p2P.pointBTransformGO.transform.position;

                float linesThickness;
                float arrowHeight;
                float textSize;
                float textOffset;
                float textLocalYOffset;
                Color numberColor;
                Color mainColor;
                Color arrowColor;
                Material mainMaterial;
                Material arrowMaterial;
                Transform cameraTransform;
                float normalOffset;

                if (p2P.isIndividual)
                {
                    linesThickness = p2P.lineThickness;
                    arrowHeight = p2P.arrowHeight;
                    textSize = p2P.textSize;
                    textOffset = p2P.textOffset;
                    textLocalYOffset = p2P.textTowardsCameraOffset;
                    numberColor = p2P.numberColor;
                    mainColor = p2P.mainColor;
                    arrowColor = p2P.arrowColor;
                    mainMaterial = p2P.mainLineMat;
                    arrowMaterial = p2P.arrowMat;
                    cameraTransform = p2P.cameraTransform;
                    normalOffset = p2P.NormalOffset;
                }
                else
                {
                    linesThickness = _starterScript.linesThickness;
                    arrowHeight = _starterScript.arrowHeight;
                    textSize = _starterScript.textSize;
                    textOffset = _starterScript.textOffset;
                    textLocalYOffset = _starterScript.textTowardsCameraOffset;
                    numberColor = _starterScript.numberColor;
                    mainColor = _starterScript.mainColor;
                    arrowColor = _starterScript.arrowColor;
                    mainMaterial = _starterScript.mainLineMaterial;
                    arrowMaterial = _starterScript.arrowMaterial;
                    cameraTransform = _starterScript.cameraTransform;
                    normalOffset = _starterScript.hitNormalOffset;
                }

                p2P.GetComponent<PointToPointDimension>().UpdateDimension(_pointAOldPos, _pointBOldPos, linesThickness, textSize,
                    textOffset, numberColor, mainColor, arrowColor, mainMaterial, arrowMaterial, cameraTransform, p2P.gameObject,
                    arrowHeight, internalFuncs.LengthUnitCalculator(_starterScript), textLocalYOffset, normalOffset);
            }
            else if (_dimensionParent.TryGetComponent(out LinearDimension linDim))
            {
                Vector3 _pointAOldPos = linDim.pointATransformGO.transform.position;
                Vector3 _pointBOldPos = linDim.pointBTransformGO.transform.position;

                float linesThickness;
                float secondaryLinesThickness;
                float secondaryLinesExtend;
                float offsetDistance;
                float arrowHeight;
                float textSize;
                float textLocalYOffset;
                float textOffset;
                bool autoTextPosition;
                bool flipTextPosition;
                Color numberColor;
                Color mainColor;
                Color secondaryColor;
                Color arrowColor;
                Material mainMat;
                Material secondaryMat;
                Material arrowMat;
                Transform cameraTransform;
                float normalOffset;

                if (linDim.isIndividual)
                {
                    linesThickness = linDim.mainLineThickness;
                    secondaryLinesThickness = linDim.secondaryLinesThickness;
                    secondaryLinesExtend = linDim.secondaryLinesExtend;
                    arrowHeight = linDim.arrowHeight;
                    textSize = linDim.textSize;
                    textOffset = linDim.textOffset;
                    textLocalYOffset = linDim.textTowardsCameraOffset;
                    autoTextPosition = linDim.autoTextPosition;
                    flipTextPosition = linDim.flipTextPosition;
                    numberColor = linDim.numberColor;
                    mainColor = linDim.mainColor;
                    secondaryColor = linDim.secondaryColor;
                    arrowColor = linDim.arrowColor;
                    mainMat = linDim.mainLineMat;
                    secondaryMat = linDim.secondaryLinesMat;
                    arrowMat = linDim.arrowMat;
                    cameraTransform = linDim.cameraTransform;
                    normalOffset = linDim.NormalOffset;
                }
                else
                {
                    linesThickness = _starterScript.linesThickness;
                    secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                    secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                    arrowHeight = _starterScript.arrowHeight;
                    textSize = _starterScript.textSize;
                    textLocalYOffset = _starterScript.textTowardsCameraOffset;
                    textOffset = _starterScript.textOffset;
                    autoTextPosition = _starterScript.autoTextPosition;
                    flipTextPosition = _starterScript.flipTextPosition;
                    numberColor = _starterScript.numberColor;
                    mainColor = _starterScript.mainColor;
                    secondaryColor = _starterScript.secondaryColor;
                    arrowColor = _starterScript.arrowColor;
                    mainMat = _starterScript.mainLineMaterial;
                    secondaryMat = _starterScript.secondaryLinesMaterial;
                    arrowMat = _starterScript.arrowMaterial;
                    cameraTransform = _starterScript.cameraTransform;
                    normalOffset = _starterScript.hitNormalOffset;
                }
                offsetDistance = linDim.offsetDistance;

                linDim.UpdateDimension(_pointAOldPos, _pointBOldPos, linesThickness, secondaryLinesThickness, secondaryLinesExtend, textSize,
                   textOffset, mainMat, secondaryMat, arrowMat, numberColor, mainColor, secondaryColor, arrowColor, cameraTransform, linDim.gameObject,
                   arrowHeight, linDim.measurementDirection, linDim.offsetDirection, offsetDistance, autoTextPosition, flipTextPosition,
                   internalFuncs.LengthUnitCalculator(_starterScript), textLocalYOffset, normalOffset);
            }
            else if (_dimensionParent.TryGetComponent(out AlignedDimension alignDim))
            {
                float mainLineThickness;
                float secondaryLinesThickness;
                float secondaryLinesExtend;
                float textSize;
                float textLocalYOffset;
                float textOffset;
                float arrowHeight;
                float offsetDistance;
                bool autoTextPosition;
                bool flipTextPosition;
                Color numberColor;
                Color mainColor;
                Color secondaryColor;
                Color arrowColor;
                Material mainMat;
                Material secondaryMat;
                Material arrowMat;
                float normalOffset;

                Funcs.MeasurementPlane measurementPlane;
                Transform cameraTransform;

                if (alignDim.isIndividual)
                {
                    mainLineThickness = alignDim.mainLineThickness;
                    secondaryLinesThickness = alignDim.secondaryLinesThickness;
                    secondaryLinesExtend = alignDim.secondaryLinesExtend;
                    textSize = alignDim.textSize;
                    textLocalYOffset = alignDim.textTowardsCameraOffset;
                    textOffset = alignDim.textOffset;
                    numberColor = alignDim.numberColor;
                    mainColor = alignDim.mainColor;
                    secondaryColor = alignDim.secondaryColor;
                    arrowColor = alignDim.arrowColor;
                    mainMat = alignDim.mainLineMat;
                    secondaryMat = alignDim.secondaryLinesMat;
                    arrowMat = alignDim.arrowMat;
                    arrowHeight = alignDim.arrowHeight;
                    cameraTransform = alignDim.cameraTransform;
                    autoTextPosition = alignDim.autoTextPosition;
                    flipTextPosition = alignDim.flipTextPosition;
                    normalOffset = alignDim.NormalOffset;
                }
                else
                {
                    mainLineThickness = _starterScript.linesThickness;
                    secondaryLinesThickness = _starterScript.secondaryLinesThickness;
                    secondaryLinesExtend = _starterScript.secondaryLinesExtend;
                    textSize = _starterScript.textSize;
                    textLocalYOffset = _starterScript.textTowardsCameraOffset;
                    textOffset = _starterScript.textOffset;
                    numberColor = _starterScript.numberColor;
                    mainColor = _starterScript.mainColor;
                    secondaryColor = _starterScript.secondaryColor;
                    arrowColor = _starterScript.arrowColor;
                    mainMat = _starterScript.mainLineMaterial;
                    secondaryMat = _starterScript.secondaryLinesMaterial;
                    arrowMat = _starterScript.arrowMaterial;
                    arrowHeight = _starterScript.arrowHeight;
                    cameraTransform = _starterScript.cameraTransform;
                    autoTextPosition = _starterScript.autoTextPosition;
                    flipTextPosition = _starterScript.flipTextPosition;
                    normalOffset = _starterScript.hitNormalOffset;
                }

                var _pointAOldPos = alignDim.pointATransformGO.transform.position;
                var _pointBOldPos = alignDim.pointBTransformGO.transform.position;
                var mainParent = alignDim.mainParent;
                measurementPlane = alignDim.measurementPlane;
                offsetDistance = alignDim.offsetDistance;

                alignDim.UpdateDimension(_pointAOldPos, _pointBOldPos, mainLineThickness, secondaryLinesThickness, secondaryLinesExtend, textSize,
                    textOffset, numberColor, mainColor, secondaryColor, arrowColor, mainMat, secondaryMat, arrowMat, cameraTransform,
                    mainParent, arrowHeight, measurementPlane, offsetDistance, internalFuncs.LengthUnitCalculator(_starterScript)
                    , autoTextPosition, flipTextPosition, textLocalYOffset, normalOffset);
            }
            else if (_dimensionParent.TryGetComponent(out AngleDimension angleDim))
            {
                float linesThickness;
                float textSize;
                float arcScale = angleDim.arcScale;
                float normalOffset = angleDim.normalOffset;
                float angleDimTextOffset;
                Vector2 offsetWhenNotFit;
                Color numberColor;
                Color mainColor;
                Color arcColor;
                Material mainMat;
                Material arcMat;
                GameObject mainParent;
                Transform cameraTransform;
                Funcs.AngleMeasurmentPlane angleMeasurmentPlane;

                if (angleDim.isIndividual)
                {
                    linesThickness = angleDim.linesThickness;
                    textSize = angleDim.textSize;
                    arcScale = angleDim.arcScale;
                    normalOffset = angleDim.normalOffset;
                    angleDimTextOffset = angleDim.textOffsetFromCenter;
                    offsetWhenNotFit = angleDim.textOffsetIfNotFit;
                    numberColor = angleDim.numberColor;
                    mainColor = angleDim.mainColor;
                    arcColor = angleDim.arcColor;
                    mainMat = angleDim.mainLinesMat;
                    arcMat = angleDim.arcMat;
                    cameraTransform = angleDim.cameraTransform;
                }
                else
                {
                    linesThickness = _starterScript.linesThickness;
                    textSize = _starterScript.textSize;
                    arcScale = _starterScript.arcScale;
                    normalOffset = _starterScript.hitNormalOffset;
                    angleDimTextOffset = _starterScript.angleDimTextOffsetFromCenter;
                    offsetWhenNotFit = _starterScript.textOffsetWhenNotFit;
                    numberColor = _starterScript.arcColor;
                    mainColor = _starterScript.mainColor;
                    arcColor = _starterScript.arcColor;
                    mainMat = _starterScript.mainLineMaterial;
                    arcMat = _starterScript.arcMaterial;
                    cameraTransform = _starterScript.cameraTransform;
                }

                var _pointAOldPos = angleDim.pointATransformGO.transform.position;
                var _pointBOldPos = angleDim.pointBTransformGO.transform.position;
                var _pointCOldPos = angleDim.pointCTransformGO.transform.position;
                angleMeasurmentPlane = angleDim.angleMeasurementPlane;
                mainParent = angleDim.mainParent;


                angleDim.UpdateDimension(_pointAOldPos, _pointBOldPos, _pointCOldPos, linesThickness, textSize, arcScale, normalOffset, angleDimTextOffset, offsetWhenNotFit,
                   numberColor, mainColor, arcColor, cameraTransform, mainParent, arcMat, mainMat, angleMeasurmentPlane, internalFuncs.LengthUnitCalculator(_starterScript));
            }
            HighlightSelectedDims(_starterScript, _selectionList);
        }
        public static void UpdateNumbersRotation(EzDimStarter _starterScript, List<GameObject> _dimensionsList, Transform _cameraTransform)
        {
            if (_dimensionsList.Count != 0)
            {
                foreach (GameObject dim in _dimensionsList)
                {
                    if (dim.TryGetComponent(out PointToPointDimension p2PDimParent) && p2PDimParent.gameObject.activeSelf == true)
                    {
                        float linesThickness;
                        float textOffset;
                        float textLocalYOffset;
                        float normalOffset;
                        var pointA = p2PDimParent.pointATransformGO.transform.position;
                        var pointB = p2PDimParent.pointBTransformGO.transform.position;

                        if (p2PDimParent.isIndividual)
                        {
                            linesThickness = p2PDimParent.lineThickness;
                            textOffset = p2PDimParent.textOffset;
                            textLocalYOffset = p2PDimParent.textTowardsCameraOffset;
                            normalOffset = p2PDimParent.NormalOffset;
                        }
                        else
                        {
                            linesThickness = _starterScript.linesThickness;
                            textOffset = _starterScript.textOffset;
                            textLocalYOffset = _starterScript.textTowardsCameraOffset;
                            normalOffset = _starterScript.hitNormalOffset;
                        }

                        p2PDimParent.UpdateTextTransform(pointA, pointB, _cameraTransform, textOffset, linesThickness, textLocalYOffset, normalOffset);
                    }
                    else if (dim.TryGetComponent(out LinearDimension linDimParent) && linDimParent.gameObject.activeSelf == true)
                    {
                        float mainLineThickness;
                        float textLocalYOffset;
                        float textOffset;
                        float arrowHeight;
                        bool autoTextPosition;
                        bool flipTextPosition;
                        float normalOffset;

                        if (linDimParent.isIndividual)
                        {
                            mainLineThickness = linDimParent.mainLineThickness;
                            arrowHeight = linDimParent.arrowHeight;
                            textLocalYOffset = linDimParent.textTowardsCameraOffset;
                            textOffset = linDimParent.textOffset;
                            autoTextPosition = linDimParent.autoTextPosition;
                            flipTextPosition = linDimParent.flipTextPosition;
                            normalOffset = linDimParent.NormalOffset;
                        }
                        else
                        {
                            mainLineThickness = _starterScript.linesThickness;
                            arrowHeight = _starterScript.arrowHeight;
                            textLocalYOffset = _starterScript.textTowardsCameraOffset;
                            textOffset = _starterScript.textOffset;
                            autoTextPosition = _starterScript.autoTextPosition;
                            flipTextPosition = _starterScript.flipTextPosition;
                            normalOffset = _starterScript.hitNormalOffset;
                        }

                        linDimParent.UpdateTextTransform(_cameraTransform, textOffset, mainLineThickness, arrowHeight, autoTextPosition,
                            flipTextPosition, textLocalYOffset, normalOffset);
                    }
                    else if (dim.TryGetComponent(out AlignedDimension alignDimParent) && alignDimParent.gameObject.activeSelf == true)
                    {

                        float mainLineThickness;
                        float textLocalYOffset;
                        float textOffset;
                        float arrowHeight;
                        bool autoTextPosition;
                        bool flipTextPosition;
                        float normalOffset;

                        if (alignDimParent.isIndividual)
                        {
                            mainLineThickness = alignDimParent.mainLineThickness;
                            textLocalYOffset = alignDimParent.textTowardsCameraOffset;
                            textOffset = alignDimParent.textOffset;
                            arrowHeight = alignDimParent.arrowHeight;
                            autoTextPosition = alignDimParent.autoTextPosition;
                            flipTextPosition = alignDimParent.flipTextPosition;
                            normalOffset = alignDimParent.NormalOffset;
                        }
                        else
                        {
                            mainLineThickness = _starterScript.linesThickness;
                            textLocalYOffset = _starterScript.textTowardsCameraOffset;
                            textOffset = _starterScript.textOffset;
                            arrowHeight = _starterScript.arrowHeight;
                            autoTextPosition = _starterScript.autoTextPosition;
                            flipTextPosition = _starterScript.flipTextPosition;
                            normalOffset = _starterScript.hitNormalOffset;
                        }


                        alignDimParent.UpdateTextTransform(_cameraTransform, textOffset, mainLineThickness, arrowHeight, autoTextPosition,
                            flipTextPosition, textLocalYOffset, normalOffset);
                    }
                    else if (dim.TryGetComponent(out AngleDimension angleDimParent) && angleDimParent.gameObject.activeSelf == true) // text is not camera based. you can delete this part
                    {

                    }
                    else if (dim.TryGetComponent(out LinearAreaMeasure areaMeasureParent) && areaMeasureParent.gameObject.activeSelf == true)
                    {
                        Transform cameraTransform;
                        float textOffset;
                        Vector2 positionOffset;

                        var meshCenter = areaMeasureParent.meshCenter;
                        var measurementPlane = areaMeasureParent.measurementPlane;

                        if (areaMeasureParent.isIndividual)
                        {
                            cameraTransform = areaMeasureParent.cameraTransform;
                            textOffset = areaMeasureParent.textLocalYOffset;
                            positionOffset = areaMeasureParent.positionOffset;
                        }
                        else
                        {
                            cameraTransform = _starterScript.cameraTransform;
                            textOffset = _starterScript.areaTextLocalYOffset;
                            positionOffset = _starterScript.areaNumberPositionOffset;
                        }
                        areaMeasureParent.UpdateNumberPosAndRot(meshCenter, cameraTransform, measurementPlane, textOffset, positionOffset);
                    }
                }
            }
        }
        public static void SelectDimension(EzDimStarter starterScript, List<GameObject> dimensionsList, List<GameObject> selectionList, RaycastHit _hit)
        {
            if (!starterScript.isCreating)
            {
                RaycastHit hit = _hit;

                if (hit.transform.gameObject.name == "EzDimensionDistanceNumber" || hit.transform.gameObject.name == "EzDimensionAreaNumber")
                {
                    if (hit.transform.parent.parent.TryGetComponent(out PointToPointDimension p2P))
                    {
                        var root = p2P.gameObject;

                        if (selectionList.Contains(root))
                        {
                            selectionList.Remove(root);
                        }
                        else
                            selectionList.Add(root);
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out LinearDimension linDim))
                    {
                        var root = linDim.gameObject;

                        if (selectionList.Contains(root))
                        {
                            selectionList.Remove(root);
                        }
                        else
                            selectionList.Add(root);
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out AlignedDimension alignDim))
                    {
                        var root = alignDim.gameObject;

                        if (selectionList.Contains(root))
                        {
                            selectionList.Remove(root);
                        }
                        else
                            selectionList.Add(root);
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out AngleDimension angleDim))
                    {
                        var root = angleDim.gameObject;

                        if (selectionList.Contains(root))
                        {
                            selectionList.Remove(root);
                        }
                        else
                            selectionList.Add(root);
                    }
                    if (hit.transform.parent.parent.TryGetComponent(out LinearAreaMeasure area))
                    {
                        var root = area.gameObject;

                        if (selectionList.Contains(root))
                        {
                            selectionList.Remove(root);
                        }
                        else
                            selectionList.Add(root);
                    }

                    UpdateAll(starterScript, dimensionsList, selectionList);
                    //HighlightSelectedDimensions(starterScript, selectionList);
                }
                else
                {
                    selectionList.Clear();
                    UpdateAll(starterScript, dimensionsList, selectionList);
                }


            }
        }
        public static void HighlightSelectedDims(EzDimStarter _starterScript, List<GameObject> _selectionList)
        {
            if (!_starterScript.isCreating)
            {
                foreach (GameObject dim in _selectionList)
                {
                    if (dim.TryGetComponent(out PointToPointDimension p2P))
                    {
                        LineRenderer[] lr = dim.GetComponentsInChildren<LineRenderer>();
                        TextMeshPro[] tm = dim.GetComponentsInChildren<TextMeshPro>();
                        MeshRenderer[] mr = dim.GetComponentsInChildren<MeshRenderer>();

                        if (p2P.isIndividual)
                        {
                            lr[0].sharedMaterial.color = Color.Lerp(p2P.mainColor, p2P.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(p2P.numberColor, p2P.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(p2P.arrowColor, p2P.selectedTint, 0.5f);
                        }
                        else
                        {
                            lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.selectedTint, 0.5f);
                        }
                    }
                    else if (dim.TryGetComponent(out LinearDimension linDim))
                    {
                        LineRenderer[] lr = dim.GetComponentsInChildren<LineRenderer>();
                        TextMeshPro[] tm = dim.GetComponentsInChildren<TextMeshPro>();
                        MeshRenderer[] mr = dim.GetComponentsInChildren<MeshRenderer>();

                        if (linDim.isIndividual)
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(linDim.mainColor, linDim.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(linDim.numberColor, linDim.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(linDim.arrowColor, linDim.selectedTint, 0.5f);
                        }
                        else
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.selectedTint, 0.5f);
                        }

                    }
                    else if (dim.TryGetComponent(out AlignedDimension alignDim))
                    {
                        LineRenderer[] lr = dim.GetComponentsInChildren<LineRenderer>();
                        TextMeshPro[] tm = dim.GetComponentsInChildren<TextMeshPro>();
                        MeshRenderer[] mr = dim.GetComponentsInChildren<MeshRenderer>();

                        if (alignDim.isIndividual)
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(alignDim.mainColor, alignDim.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(alignDim.numberColor, alignDim.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(alignDim.arrowColor, alignDim.selectedTint, 0.5f);
                        }
                        else
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.selectedTint, 0.5f);
                            mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.selectedTint, 0.5f);
                        }

                    }
                    else if (dim.TryGetComponent(out AngleDimension angleDim))
                    {
                        LineRenderer[] lr = dim.GetComponentsInChildren<LineRenderer>();
                        TextMeshPro[] tm = dim.GetComponentsInChildren<TextMeshPro>();
                        MeshRenderer[] mr = dim.GetComponentsInChildren<MeshRenderer>();

                        if (angleDim.isIndividual)
                        {
                            lr[0].sharedMaterial.color = Color.Lerp(angleDim.mainColor, angleDim.selectedTint, 0.5f);
                            lr[1].sharedMaterial.color = Color.Lerp(angleDim.mainColor, angleDim.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(angleDim.numberColor, angleDim.selectedTint, 0.5f);
                            mr[0].sharedMaterial.color = Color.Lerp(angleDim.arcColor, angleDim.selectedTint, 0.5f);
                        }
                        else
                        {
                            lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.selectedTint, 0.5f);
                            lr[1].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.selectedTint, 0.5f);
                            tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.selectedTint, 0.5f);
                            mr[0].sharedMaterial.color = Color.Lerp(_starterScript.arcColor, _starterScript.selectedTint, 0.5f);
                        }
                    }
                    if (dim.TryGetComponent(out LinearAreaMeasure area))
                    {
                        LineRenderer[] lr = dim.GetComponentsInChildren<LineRenderer>();
                        // TextMeshPro[] tm = dim.GetComponentsInChildren<TextMeshPro>();
                        MeshRenderer[] mr = dim.GetComponentsInChildren<MeshRenderer>();

                        if (area.isIndividual)
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(area.borderColor, area.selectedTint, 0.5f);
                            //  foreach (TextMeshPro item in tm)
                            //      item.color = Color.Lerp(area.numberColor, area.selectedTint, 0.5f);
                            foreach (MeshRenderer item in mr)
                                if (item.gameObject.name == "MeshGO")
                                    item.sharedMaterial.color = Color.Lerp(area.surfaceColor, area.selectedTint, 0.5f);
                        }
                        else
                        {
                            foreach (LineRenderer item in lr)
                                item.sharedMaterial.color = Color.Lerp(_starterScript.areaBorderColor, _starterScript.selectedTint, 0.5f);
                            //  foreach (TextMeshPro item in tm)
                            //      item.color = Color.Lerp(_starterScript.numberColor, _starterScript.selectedTint, 0.5f);
                            foreach (MeshRenderer item in mr)
                                if (item.gameObject.name == "MeshGO")
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.areaSurfaceColor, _starterScript.selectedTint, 0.5f);
                        }
                    }
                }
            }
        }
        public static GameObject IsHoveredOnDimension(EzDimStarter _starterScript, List<GameObject> _selectionList, RaycastHit _hit)
        {
            GameObject DimensionRoot = null;

            if (!_starterScript.isCreating)
            {
                RaycastHit hit = _hit;

                if (hit.transform.gameObject.name == "EzDimensionDistanceNumber" || hit.transform.gameObject.name == "EzDimensionAreaNumber")  // if hovered on any active dimension number.
                {
                    if (hit.transform.parent.parent.TryGetComponent(out PointToPointDimension p2P)) // if hovered on a pointToPoint dimension:
                    {
                        DimensionRoot = p2P.gameObject;
                        lastHoveredGO = DimensionRoot;
                        hoverState = 1; // hoverState helps to know the lastSelectionGO type. 1 = pointToPoint, 2=linear, 3=aligned, 4=angle , 5=area.

                        if (_selectionList.Contains(DimensionRoot)) // if it was selected:
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (p2P.isIndividual) // if isIndividual true, read value from local settings
                            {

                                lr[0].sharedMaterial.color = Color.Lerp(p2P.mainColor, p2P.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(p2P.numberColor, p2P.hoveredOnSelectedTint, 0.5f).linear;
                                mr[1].sharedMaterial.color = Color.Lerp(p2P.arrowColor, p2P.hoveredOnSelectedTint, 0.5f);
                            }
                            else  // if isIndividual false, read value from starterScript settings
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredOnSelectedTint, 0.5f).linear;
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                            }

                        }
                        else // if it wasn't selected:
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (p2P.isIndividual) // local settings:
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(p2P.mainColor, p2P.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(p2P.numberColor, p2P.hoveredTint, 0.5f).linear;
                                mr[1].sharedMaterial.color = Color.Lerp(p2P.arrowColor, p2P.hoveredTint, 0.5f);
                            }
                            else // starterScript settings:
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredTint, 0.5f).linear;
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredTint, 0.5f);
                            }

                        }
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out LinearDimension linDim)) // if hovered on a linear dimension:
                    {
                        DimensionRoot = linDim.gameObject;
                        lastHoveredGO = DimensionRoot;
                        hoverState = 2;

                        if (_selectionList.Contains(DimensionRoot))
                        {

                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (linDim.isIndividual)
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(linDim.mainColor, linDim.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(linDim.numberColor, linDim.hoveredOnSelectedTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(linDim.arrowColor, linDim.hoveredOnSelectedTint, 0.5f);
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                            }

                        }
                        else
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (linDim.isIndividual)
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(linDim.mainColor, linDim.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(linDim.numberColor, linDim.hoveredTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(linDim.arrowColor, linDim.hoveredTint, 0.5f);
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredTint, 0.5f);
                            }

                        }
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out AlignedDimension AlgDim)) // if hovered on a aligned dimension:
                    {
                        DimensionRoot = AlgDim.gameObject;
                        lastHoveredGO = DimensionRoot;
                        hoverState = 3;

                        if (_selectionList.Contains(DimensionRoot))
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (AlgDim.isIndividual)
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(AlgDim.mainColor, AlgDim.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(AlgDim.numberColor, AlgDim.hoveredOnSelectedTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(AlgDim.arrowColor, AlgDim.hoveredOnSelectedTint, 0.5f);
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                            }

                        }
                        else
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (AlgDim.isIndividual)
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(AlgDim.mainColor, AlgDim.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(AlgDim.numberColor, AlgDim.hoveredTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(AlgDim.arrowColor, AlgDim.hoveredTint, 0.5f);
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredTint, 0.5f);
                                mr[1].sharedMaterial.color = Color.Lerp(_starterScript.arrowColor, _starterScript.hoveredTint, 0.5f);
                            }

                        }
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out AngleDimension AngDim)) // if hovered on a angle dimension:
                    {
                        DimensionRoot = AngDim.gameObject;
                        lastHoveredGO = DimensionRoot;
                        hoverState = 4;

                        if (_selectionList.Contains(DimensionRoot))
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (AngDim.isIndividual)
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(AngDim.mainColor, AngDim.hoveredOnSelectedTint, 0.5f);
                                //lr[1].sharedMaterial.color = Color.Lerp(AngDim.mainColor, AngDim.hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(AngDim.numberColor, AngDim.hoveredOnSelectedTint, 0.5f);
                                mr[0].sharedMaterial.color = Color.Lerp(AngDim.arcColor, AngDim.hoveredOnSelectedTint, 0.5f);
                            }
                            else
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                //lr[1].sharedMaterial.color = Color.Lerp(_mainColor, _hoveredOnSelectedTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                mr[0].sharedMaterial.color = Color.Lerp(_starterScript.arcColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                            }

                        }
                        else
                        {

                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (AngDim.isIndividual)
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(AngDim.mainColor, AngDim.hoveredTint, 0.5f);
                                //lr[1].sharedMaterial.color = Color.Lerp(AngDim.mainColor, AngDim.hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(AngDim.numberColor, AngDim.hoveredTint, 0.5f);
                                mr[0].sharedMaterial.color = Color.Lerp(AngDim.arcColor, AngDim.hoveredTint, 0.5f);
                            }
                            else
                            {
                                lr[0].sharedMaterial.color = Color.Lerp(_starterScript.mainColor, _starterScript.hoveredTint, 0.5f);
                                //lr[1].sharedMaterial.color = Color.Lerp(_mainColor, _hoveredTint, 0.5f);
                                tm[0].color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredTint, 0.5f);
                                mr[0].sharedMaterial.color = Color.Lerp(_starterScript.arcColor, _starterScript.hoveredTint, 0.5f);
                            }
                        }
                    }
                    else if (hit.transform.parent.parent.TryGetComponent(out LinearAreaMeasure area)) // if hovered on a area dimension:
                    {
                        DimensionRoot = area.gameObject;
                        lastHoveredGO = DimensionRoot;
                        hoverState = 5; // hoverState helps to know the lastSelectionGO type. 1 = pointToPoint, 2=linear, 3=aligned, 4=angle , 5=area.

                        if (_selectionList.Contains(DimensionRoot)) // if it was selected:
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (area.isIndividual) // if isIndividual true, read value from local settings
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(area.borderColor, area.hoveredOnSelectedTint, 0.5f);
                                foreach (TextMeshPro item in tm)
                                    item.color = Color.Lerp(area.numberColor, area.hoveredOnSelectedTint, 0.5f).linear;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = Color.Lerp(area.surfaceColor, area.hoveredOnSelectedTint, 0.5f);
                            }
                            else  // if isIndividual false, read value from starterScript settings
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.areaBorderColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                                foreach (TextMeshPro item in tm)
                                    item.color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredOnSelectedTint, 0.5f).linear;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = Color.Lerp(_starterScript.areaSurfaceColor, _starterScript.hoveredOnSelectedTint, 0.5f);
                            }

                        }
                        else // if it wasn't selected:
                        {
                            LineRenderer[] lr = DimensionRoot.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = DimensionRoot.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = DimensionRoot.GetComponentsInChildren<MeshRenderer>();

                            if (area.isIndividual) // local settings:
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(area.borderColor, area.hoveredTint, 0.5f);
                                foreach (TextMeshPro item in tm)
                                    item.color = Color.Lerp(area.numberColor, area.hoveredTint, 0.5f).linear;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = Color.Lerp(area.surfaceColor, area.hoveredTint, 0.5f);
                            }
                            else // starterScript settings:
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = Color.Lerp(_starterScript.areaBorderColor, _starterScript.hoveredTint, 0.5f);
                                foreach (TextMeshPro item in tm)
                                    item.color = Color.Lerp(_starterScript.numberColor, _starterScript.hoveredTint, 0.5f).linear;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = Color.Lerp(_starterScript.areaSurfaceColor, _starterScript.hoveredTint, 0.5f);
                            }
                        }
                    }
                }
                else // if mouse was not hovered any selected dimension
                {
                    if (lastHoveredGO != null) // if mouse was hovered on any dimension
                    {
                        if (hoverState == 1) // mouse was hovered on a point-to-point dimension
                        {
                            LineRenderer[] lr = lastHoveredGO.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = lastHoveredGO.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = lastHoveredGO.GetComponentsInChildren<MeshRenderer>();

                            if (lastHoveredGO.GetComponent<PointToPointDimension>().isIndividual)
                            {
                                var p2PDim = lastHoveredGO.GetComponent<PointToPointDimension>();

                                lr[0].sharedMaterial.color = p2PDim.mainColor;
                                tm[0].color = p2PDim.numberColor;
                                mr[1].sharedMaterial.color = p2PDim.arrowColor;
                            }
                            else
                            {
                                lr[0].sharedMaterial.color = _starterScript.mainColor;
                                tm[0].color = _starterScript.numberColor;
                                mr[1].sharedMaterial.color = _starterScript.arrowColor;
                            }

                        }
                        else if (hoverState == 2) // mouse was hovered on a linear dimension
                        {
                            LineRenderer[] lr = lastHoveredGO.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = lastHoveredGO.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = lastHoveredGO.GetComponentsInChildren<MeshRenderer>();

                            if (lastHoveredGO.GetComponent<LinearDimension>().isIndividual)
                            {
                                var linDim = lastHoveredGO.GetComponent<LinearDimension>();

                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = linDim.mainColor;
                                tm[0].color = linDim.numberColor;
                                mr[1].sharedMaterial.color = linDim.arrowColor;
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = _starterScript.mainColor;
                                tm[0].color = _starterScript.numberColor;
                                mr[1].sharedMaterial.color = _starterScript.arrowColor;
                            }
                        }
                        else if (hoverState == 3) // mouse was hovered on an aligned dimension
                        {
                            LineRenderer[] lr = lastHoveredGO.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = lastHoveredGO.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = lastHoveredGO.GetComponentsInChildren<MeshRenderer>();

                            if (lastHoveredGO.GetComponent<AlignedDimension>().isIndividual)
                            {
                                var alignDim = lastHoveredGO.GetComponent<AlignedDimension>();

                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = alignDim.mainColor;
                                tm[0].color = alignDim.numberColor;
                                mr[1].sharedMaterial.color = alignDim.arrowColor;
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = _starterScript.mainColor;
                                tm[0].color = _starterScript.numberColor;
                                mr[1].sharedMaterial.color = _starterScript.arrowColor;
                            }
                        }
                        else if (hoverState == 4) // mouse was hovered on an angle dimension
                        {
                            LineRenderer[] lr = lastHoveredGO.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = lastHoveredGO.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = lastHoveredGO.GetComponentsInChildren<MeshRenderer>();

                            if (lastHoveredGO.GetComponent<AngleDimension>().isIndividual)
                            {
                                var angDim = lastHoveredGO.GetComponent<AngleDimension>();

                                lr[0].sharedMaterial.color = angDim.mainColor;
                                tm[0].color = angDim.numberColor;
                                mr[0].sharedMaterial.color = angDim.arcColor;

                            }
                            else
                            {
                                lr[0].sharedMaterial.color = _starterScript.mainColor;
                                tm[0].color = _starterScript.numberColor;
                                mr[0].sharedMaterial.color = _starterScript.arcColor;
                            }
                        }
                        else if (hoverState == 5) // mouse was hovered on an area dimension
                        {
                            LineRenderer[] lr = lastHoveredGO.GetComponentsInChildren<LineRenderer>();
                            TextMeshPro[] tm = lastHoveredGO.GetComponentsInChildren<TextMeshPro>();
                            MeshRenderer[] mr = lastHoveredGO.GetComponentsInChildren<MeshRenderer>();

                            if (lastHoveredGO.GetComponent<LinearAreaMeasure>().isIndividual)
                            {
                                var area = lastHoveredGO.GetComponent<LinearAreaMeasure>();

                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = area.borderColor;
                                foreach (TextMeshPro item in tm)
                                    item.color = area.numberColor;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = area.surfaceColor;
                            }
                            else
                            {
                                foreach (LineRenderer item in lr)
                                    item.sharedMaterial.color = _starterScript.areaBorderColor;
                                foreach (TextMeshPro item in tm)
                                    item.color = _starterScript.numberColor;
                                foreach (MeshRenderer item in mr)
                                    if (item.gameObject.name == "MeshGO")
                                        item.sharedMaterial.color = _starterScript.areaSurfaceColor;
                            }
                        }

                        HighlightSelectedDims(_starterScript, _selectionList);
                        lastHoveredGO = null;
                    }
                }
            }
            return DimensionRoot;
        }
        public static void SetActiveAllChilds(Transform transform, bool value)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);
            }
        }
        public static void HideSelectedDimensions(List<GameObject> selectionList)
        {
            if (selectionList.Count > 0)
            {
                foreach (GameObject dim in selectionList)
                {
                    dim.SetActive(false);
                }
                selectionList.Clear();
            }
        }
        public static void UnhideAllDimensions(EzDimStarter starterScript, List<GameObject> dimensionsList)
        {
            foreach (GameObject dim in dimensionsList)
            {
                dim.SetActive(true);
                UpdateValues(starterScript, dimensionsList, dim);
            }
        }
        public static void DeleteDimensions(List<GameObject> dimensionsList, List<GameObject> selectionList)
        {
            foreach (GameObject dim in selectionList)
            {
                dimensionsList.Remove(dim);
                GameObject.Destroy(dim);
            }
            selectionList.Clear();
        }
    }

    public class internalFuncs
    {
        public enum Units { Millimetre, centimeter, meter, inch, foot, yard, custom }
        public enum numberOfDecimal { none, one, two, three, four, five, six }
        public static float LengthUnitCalculator(EzDimStarter _starterScript)
        {
            switch (_starterScript.unit)
            {
                case Units.Millimetre: return 1000;

                case Units.centimeter: return 100;

                case Units.meter: return 1;

                case Units.inch: return 39.3700787408f;

                case Units.foot: return 3.28084f;

                case Units.yard: return 1.0936132983f;

                case Units.custom: return _starterScript.customUnitMultiplier;

                default: return 1;
            }
        }
        public static float AreaUnitCalculator(EzDimStarter _starterScript)
        {
            switch (_starterScript.unit)
            {
                case Units.Millimetre: return 1000000;

                case Units.centimeter: return 10000;

                case Units.meter: return 1;

                case Units.inch: return 1550.0031f;

                case Units.foot: return 10.7639f;

                case Units.yard: return 1.19599f;

                case Units.custom: return _starterScript.customUnitMultiplier;

                default: return 1;
            }
        }
        public static string DecimalCalculator(EzDimStarter _starterScript)
        {
            switch (_starterScript.numberOfDecimal)
            {
                case numberOfDecimal.none: return "0";
                case numberOfDecimal.one: return "0.0";
                case numberOfDecimal.two: return "0.00";
                case numberOfDecimal.three: return "0.000";
                case numberOfDecimal.four: return "0.0000";
                case numberOfDecimal.five: return "0.00000";
                case numberOfDecimal.six: return "0.000000";

                default: return "";
            }

        }
        public static string GetUnitTextShortForm(EzDimStarter _starterScript)
        {
            switch (_starterScript.unit)
            {
                case Units.Millimetre: return "mm";

                case Units.centimeter: return "cm";

                case Units.meter: return "m";

                case Units.inch: return "in";

                case Units.foot: return "ft";

                case Units.yard: return "yd";

                case Units.custom: return _starterScript.customUnitName;

                default: return "";
            }
        }
        public static Vector3 AngleBisectorPoint(Vector3 _pointA, Vector3 _pointB, Vector3 _pointC)
        {
            Vector3 directionA = (_pointA - _pointB).normalized;
            Vector3 directionB = (_pointC - _pointB).normalized;
            Vector3 bisector = (directionA + directionB).normalized;

            return bisector;
        }
        public static Color HighlightColor(Color _Color, float _DiffrencePercentage)
        {
            float newDiffrencePercentage;

            newDiffrencePercentage = Remap(_DiffrencePercentage, 0, 100, 0, 1);

            Color result = new Color();
            if (_Color.r <= 1 - newDiffrencePercentage && _Color.b <= 1 - newDiffrencePercentage && _Color.g <= 1 - newDiffrencePercentage)
                result = _Color += new Color(newDiffrencePercentage, newDiffrencePercentage, newDiffrencePercentage, 0);
            else if (_Color.r >= 1 - newDiffrencePercentage && _Color.b >= 1 - newDiffrencePercentage && _Color.g >= 1 - newDiffrencePercentage)
                result = _Color -= new Color(newDiffrencePercentage, newDiffrencePercentage, newDiffrencePercentage, 0);
            else
            {
                float highestColor;

                if (_Color.r >= _Color.g)
                    highestColor = _Color.r;
                else
                    highestColor = _Color.g;

                if (highestColor <= _Color.b)
                    highestColor = _Color.b;

                if (highestColor >= 1 - newDiffrencePercentage)
                    result = _Color -= new Color(newDiffrencePercentage, newDiffrencePercentage, newDiffrencePercentage, 0);
            }

            return result;
        }
        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static bool FlipNormal(Vector3 normal, Funcs.MeasurementPlane measurmentPlane)
        {
            if (measurmentPlane == Funcs.MeasurementPlane.XZ)
            {
                return normal.y < 0;
            }
            else if (measurmentPlane == Funcs.MeasurementPlane.XY)
            {
                return normal.z > 0;
            }
            else if (measurmentPlane == Funcs.MeasurementPlane.ZY)
            {
                return normal.x > 0;
            }

            return false;
        }
        public static Mesh TriangulatePoints(Vector2[] points)
        {
            points = GetCWPoints(points);
            Mesh mesh = new Mesh();
            int index = 1;
            List<int> pointsIndex = new List<int>();
            List<int> triangles = new List<int>();

            for (int i = 0; i < points.Length; i++)
            {
                pointsIndex.Add(i);
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                int i0 = pointsIndex[index - 1];
                int i1 = pointsIndex[index];
                int i2;

                if (index == pointsIndex.Count - 1)
                    i2 = pointsIndex[0];
                else
                    i2 = pointsIndex[index + 1];

                Vector2 p0 = points[i0];
                Vector2 p1 = points[i1];
                Vector2 p2 = points[i2];

                if (ValidateTriangle(p0, p1, p2, points, out bool isInTriangle, out Vector2 innerPoint))
                {
                    triangles.Add(i0);
                    triangles.Add(i1);
                    triangles.Add(i2);

                    pointsIndex.RemoveAt(index);
                }
                else
                {
                    if (isInTriangle)
                    {
                        int innerPointIndex = System.Array.IndexOf(points, innerPoint);
                        triangles.Add(i0);
                        triangles.Add(i1);
                        triangles.Add(innerPointIndex);
                        // 
                        triangles.Add(innerPointIndex);
                        triangles.Add(i1);
                        triangles.Add(i2);

                        pointsIndex.RemoveAt(index);
                        //pointsIndex.RemoveAt(index+1);

                        //i++;

                    }
                    else
                        index++;
                }
            }

            mesh.vertices = ConvertToVec3(points);
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
        public static float Area(Mesh m)
        {
            Vector3[] mVertices = m.vertices;
            Vector3 result = Vector3.zero;
            for (int p = mVertices.Length - 1, q = 0; q < mVertices.Length; p = q++)
            {
                result += Vector3.Cross(mVertices[q], mVertices[p]);
            }
            result *= 0.5f;
            return result.magnitude;
        }
        static bool ValidateTriangle(Vector2 p0, Vector2 p1, Vector2 p2, Vector2[] points, out bool isInTriangle, out Vector2 innerPoint)
        {
            isInTriangle = false;
            innerPoint = Vector2.zero;
            Vector2 directionA = p0 - p1;
            Vector2 directionB = p2 - p1;
            float angle = Vector2.SignedAngle(directionA, directionB);

            if (angle <= 0)
                return false;

            foreach (Vector2 p in points)
            {
                if (IsInTriangle(p, p0, p1, p2))
                {
                    isInTriangle = true;
                    innerPoint = p;
                    return false;
                }
            }

            return true;
        }
        static bool IsInTriangle(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            var a = .5f * (-p1.y * p2.x + p0.y * (-p1.x + p2.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y);
            var sign = a < 0 ? -1 : 1;
            var s = (p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y) * sign;
            var t = (p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y) * sign;

            return s > 0 && t > 0 && (s + t) < 2 * a * sign;
        }
        static Vector2[] GetCWPoints(Vector2[] input)
        {
            if (IsCW(input))
                return input;

            Vector2[] result = new Vector2[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[input.Length - i - 1];
            }

            return result;
        }
        static bool IsCW(Vector2[] points)
        {
            if (points.Length < 2)
                return false;

            float sum = 0;

            for (int i = 0; i < points.Length; i++)
            {
                if (i < points.Length - 1)
                    sum += (points[i + 1].x - points[i].x) * (points[i + 1].y + points[i].y);
                else
                    sum += (points[0].x - points[i].x) * (points[0].y + points[i].y);
            }

            return sum >= 0;
        }
        static Vector3[] ConvertToVec3(Vector2[] input)
        {
            Vector3[] result = new Vector3[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = new Vector3(input[i].x, 0, input[i].y);
            }

            return result;
        }
        public class ReadOnlyAttribute : PropertyAttribute
        {

        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
        public class ReadOnlyDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property,
                                                    GUIContent label)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            public override void OnGUI(Rect position,
                                       SerializedProperty property,
                                       GUIContent label)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
        }
#endif
    }
}

