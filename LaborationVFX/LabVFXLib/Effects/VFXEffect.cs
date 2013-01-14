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

        public bool FogEnabled {
            get {
                return this._fog.Enabled;
            }
            set {
                this._fog.Enabled = value;
                if(this.Parameters["FogEnabled"] == null)
                    return;
                this.Parameters["FogEnabled"].SetValue(this._fog.Enabled ? 1f : 0.0f);
            }
        }

        public float FogEnd {
            get {
                return this._fog.End;
            }
            set {
                this._fog.End = value;
                if(this.Parameters["FogEnd"] == null)
                    return;
                this.Parameters["FogEnd"].SetValue(this._fog.End);
            }
        }

        public float FogStart {
            get {
                return this._fog.Start;
            }
            set {
                this._fog.Start = value;
                if(this.Parameters["FogStart"] == null)
                    return;
                this.Parameters["FogStart"].SetValue(this._fog.Start);
            }
        }
    }
}
