using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    class VFXEffect : Effect {
        private FogProperties fog;



        public Vector3 FogColor {
            get {
                return this.fog.Color;
            }
            set {
                this.fog.Color = value;
                if(this.Parameters["FogColor"] == null)
                    return;
                else
                    this.Parameters["FogColor"].SetValue(this.fog.Color);
            }
        }
    }
}
