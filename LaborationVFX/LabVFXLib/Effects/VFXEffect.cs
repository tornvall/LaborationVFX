using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    class VFXEffect : Effect {
        private FogProperties _fog;



        public Vector3 FogColor {
            get {
                return this._fog.Color;
            }
            set {
                this._fog.Color = value;
                if(this.Parameters["FogColor"] == null)
                    return;
                else
                    this.Parameters["FogColor"].SetValue(this._fog.Color);
            }
        }
    }
}
