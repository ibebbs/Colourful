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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Colourful.Implementation;

#if (NET40 || NET35)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;
#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful
{
    /// <summary>
    /// RGB color without specified <see cref="IRGBWorkingSpace">working space</see>
    /// </summary>
    public abstract class RGBColorBase : IColorVector
    {
        #region Constructor

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        internal RGBColorBase(double r, double g, double b)
        {
            R = r.CheckRange(0, 1);
            G = g.CheckRange(0, 1);
            B = b.CheckRange(0, 1);
        }

         /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions (range from 0 to 1)</param>
        internal RGBColorBase(Vector vector)
            : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// Red
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        public double R { get; private set; }

        /// <summary>
        /// Green
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G")]
        public double G { get; private set; }

        /// <summary>
        /// Blue
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        public double B { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector
        {
            get { return new[] { R, G, B }; }
        }

        #endregion

        #region Equality

        protected bool Equals(RGBColorBase other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBColorBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RGBColorBase left, RGBColorBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RGBColorBase left, RGBColorBase right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}