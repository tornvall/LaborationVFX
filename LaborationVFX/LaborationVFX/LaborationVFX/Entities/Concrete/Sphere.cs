using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabVFXLib.Geometry;
using Microsoft.Xna.Framework.Graphics;
using LabVFXLib.Effects;

namespace LaborationVFX.Entities.Concrete
{
    public class Sphere : VFXModel
    {
        public Sphere(GraphicsDevice device, Model model, VFXEffect effect)
            : base(device, model, effect) {
        }
    }
}
