﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    public class VFXEffect : Effect {
        #region Fields
        private FogProperties _fog;
        private float _alpha;
        private float _specularPower;
        private Vector3 _diffuseColor;
        private Vector3 _specularColor;
        private Texture2D _diffuseTexture;

        #endregion

        #region Properties
        public float Alpha {
            get {
                return _alpha;
            }
            set {
                _alpha = value;
            }
        }
        public Vector3 DiffuseColor {
            get {
                return _diffuseColor;
            }
            set {
                _diffuseColor = value;
                if(this.Parameters["DiffuseColor"] != null)
                    this.Parameters["DiffuseColor"].SetValue(new Vector4(_diffuseColor, _alpha));
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

        public float SpecularPower {
            get {
                return _specularPower;
            }
            set {
                _specularPower = value;
                if(this.Parameters["SpecularPower"] != null)                    
                    this.Parameters["SpecularPower"].SetValue(_specularPower);
            }
        }


        public Vector3 SpecularColor {
            get {
                return _specularColor;
            }
            set {
                _specularColor = value;
                if(this.Parameters["SpecularColor"] != null)
                    this.Parameters["SpecularColor"].SetValue(_specularColor);
            }
        }

        public Texture2D DiffuseTexture {
            get {
                return _diffuseTexture;
            }
            set {
                _diffuseTexture = value;
                if(this.Parameters["DiffuseTexture"] != null)
                    this.Parameters["DiffuseTexture"].SetValue((Texture)_diffuseTexture);
                if(this.Parameters["UseDiffuseTexture"] != null) {
                    if(_diffuseTexture != null)
                        this.Parameters["UseDiffuseTexture"].SetValue(true);
                    else
                        this.Parameters["UseDiffuseTexture"].SetValue(false);
                }
            }
        }
        #endregion

        public VFXEffect(Effect clone)
            : base(clone) {
        }

        public override Effect Clone() {
            return (Effect)new VFXEffect((Effect)this) {
                DiffuseColor = _diffuseColor,
                SpecularColor = _specularColor,
                SpecularPower = _specularPower
            };
        }
    }
}
