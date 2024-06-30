using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzDimension;
using EzDimension.dims;

namespace EzDimension.dims
{
    public class EzDynamicTargetObject : MonoBehaviour
    {
        public EzDimStarter starterScript;
        public LinearAreaMeasure areaMeasureRoot;
        public List<PointToPointDimension> p2PDimComponentsList = new List<PointToPointDimension>();
        public List<LinearDimension> linDimComponentsList = new List<LinearDimension>();
        public List<AlignedDimension> alignDimComponentsList = new List<AlignedDimension>();
        public List<AngleDimension> angleDimComponentsList = new List<AngleDimension>();
        public List<LinearAreaMeasure> areaComponentsList = new List<LinearAreaMeasure>();

        private void Update()
        {
            if (this.transform.hasChanged)
            {
                if (p2PDimComponentsList.Count > 0)
                {
                    for (int i = p2PDimComponentsList.Count - 1; i >= 0; i--)
                    {
                        if (p2PDimComponentsList[i] == null)
                        {
                            p2PDimComponentsList.Remove(p2PDimComponentsList[i]);
                        }
                    }
                    foreach (PointToPointDimension dim in p2PDimComponentsList)
                    {
                        Funcs.UpdateLength(starterScript, dim);
                    }
                }

                if (linDimComponentsList.Count > 0)
                {
                    for (int i = linDimComponentsList.Count - 1; i >= 0; i--)
                    {
                        if (linDimComponentsList[i] == null)
                        {
                            linDimComponentsList.Remove(linDimComponentsList[i]);
                        }
                    }
                    foreach (LinearDimension dim in linDimComponentsList)
                    {
                        Funcs.UpdateLength(starterScript, dim);
                    }
                }

                if (alignDimComponentsList.Count > 0)
                {
                    for (int i = alignDimComponentsList.Count - 1; i >= 0; i--)
                    {
                        if (alignDimComponentsList[i] == null)
                        {
                            alignDimComponentsList.Remove(alignDimComponentsList[i]);
                        }
                    }
                    foreach (AlignedDimension dim in alignDimComponentsList)
                    {
                        Funcs.UpdateLength(starterScript, dim);
                    }
                }

                if (angleDimComponentsList.Count > 0)
                {
                    for (int i = angleDimComponentsList.Count - 1; i >= 0; i--)
                    {
                        if (angleDimComponentsList[i] == null)
                        {
                            angleDimComponentsList.Remove(angleDimComponentsList[i]);
                        }
                    }
                    foreach (AngleDimension dim in angleDimComponentsList)
                    {
                        Funcs.UpdateAngle(starterScript, dim);
                    }
                }

                if (areaComponentsList.Count > 0)
                {
                    if (areaMeasureRoot != null && areaMeasureRoot.allowDrawSurface)
                        Funcs.UpdateArea(starterScript, areaMeasureRoot);
                }

                Funcs.HighlightSelectedDims(starterScript, starterScript.SelectionList);

                if (starterScript.hit.transform != null)
                {
                    Funcs.IsHoveredOnDimension(starterScript, starterScript.SelectionList, starterScript.hit);
                }


            }
            this.transform.hasChanged = false;
        }
    }
}