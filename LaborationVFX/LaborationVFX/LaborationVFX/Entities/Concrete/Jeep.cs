using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabVFXLib.Geometry;
using Microsoft.Xna.Framework.Graphics;
using LabVFXLib.Effects;

namespace LaborationVFX.Entities.Concrete
{
    public class Jeep : VFXModel
    {
        public Jeep(GraphicsDevice device, Model model, VFXEffect effect)
            : base(device, model, effect) {
        }
    }
}
