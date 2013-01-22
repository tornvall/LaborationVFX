using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabVFXLib.Geometry;
using Microsoft.Xna.Framework.Graphics;
using LabVFXLib.Effects;

namespace LaborationVFX.Entities.Concrete
{
    public class Snowplow :VFXModel
    {
        public Snowplow(GraphicsDevice device, Model model, VFXEffect effect):base(device,model,effect)
        {
            _isDoubleSided["Circle"] = true;
            _isDoubleSided["Circle.004"] = true;
            _isDoubleSided["Circle.003"] = true;
        }
    }
}
