using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LabVFXLib
{
    public class Utils
    {
        public static Vector3 Vector4toVector3(Vector4 input)
        {
            Vector3 output = Vector3.Zero;
            output.X = input.X;
            output.Y = input.Y;
            output.Z = input.Z;

            return output;
        }

        public static Vector4 Vector3toVector4(Vector3 input, float inputWeight)
        {
            Vector4 output = Vector4.Zero;
            output.X = input.X;
            output.Y = input.Y;
            output.Z = input.Z;

            output.W = inputWeight;

            return output;
        }
    }
}
