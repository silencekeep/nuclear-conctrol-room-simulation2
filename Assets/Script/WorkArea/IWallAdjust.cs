using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CNCC.WorkArea
{
    public interface IWallAdjust
    {
        void SetWallScale(double length, double width);
        void SetWallPosition(double length,double width);
    }
}