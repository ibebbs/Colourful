﻿#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChabColor"/> to <see cref="LabColor"/>.
    /// </summary>
    public class LChabToLabConverter : IColorConversion<LChabColor, LabColor>
    {
        public LabColor Convert(LChabColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            double L = input.L, C = input.C, hDegrees = input.h;
            double hRadians = Angle.DegreeToRadian(hDegrees);

            double a = C * Math.Cos(hRadians);
            double b = C * Math.Sin(hRadians);

            var output = new LabColor(L, a, b, input.WhitePoint);
            return output;
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public static bool operator ==(LChabToLabConverter left, LChabToLabConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LChabToLabConverter left, LChabToLabConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}