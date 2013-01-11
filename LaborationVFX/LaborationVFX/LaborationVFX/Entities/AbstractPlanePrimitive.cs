using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LaborationVFX.Entities
{
    public class AbstractPlanePrimitive : AbstractPrimitive
    {
        public AbstractPlanePrimitive(GraphicsDevice graphicsDevice, Vector3 position, Quaternion rotation, float scale)
            : base(graphicsDevice, position, rotation, scale)
        {
        }
    }
}
