using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    public class VFXEffect : Effect {
        private FogProperties _fog;
        private float _alpha;

        public VFXEffect(Effect clone):base(clone)
        {
        }

        public float Alpha {
            get {
                return _alpha;
            }
            set {
                _alpha = value;
            }
        }

        public Vector3 FogColor {
            get {
                return _fog.Color;
            }
            set {
                _fog.Color = value;
                if(this.Parameters["FogColor"] != null){
                    this.Parameters["FogColor"].SetValue(_fog.Color);
                }
            }
        }

        public bool FogEnabled {
            get {
                return _fog.Enabled;
            }
            set {
                _fog.Enabled = value;
                if(this.Parameters["FogEnabled"] != null) {
                    if(_fog.Enabled)
                        this.Parameters["FogEnabled"].SetValue(1f);
                    else
                        this.Parameters["FogEnabled"].SetValue(0f);
                }
            }
        }

        public float FogEnd {
            get {
                return _fog.End;
            }
            set {
                _fog.End = value;
                if(this.Parameters["FogEnd"] != null) {                 
                    this.Parameters["FogEnd"].SetValue(_fog.End);
                }
            }
        }

        public float FogStart {
            get {
                return _fog.Start;
            }
            set {
                _fog.Start = value;
                if(this.Parameters["FogStart"] != null) {
                    this.Parameters["FogStart"].SetValue(_fog.Start);
                }
            }
        }
    }
}
