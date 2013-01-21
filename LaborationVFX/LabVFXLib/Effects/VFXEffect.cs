using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    public class VFXEffect : Effect, IEffectFog, IEffectMatrices, IEffectLights {
        #region Fields
        private FogProperties _fog;
        private float _alpha;
        private float _specularPower;
        private Vector3 _diffuseColor;
        private Vector3 _specularColor;
        private Texture2D _diffuseTexture;
        private EffectParameter _projection;
        private EffectParameter _view;
        private EffectParameter _world;
        private DirectionalLight _directionalLight;
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
        public Matrix Projection {
            get {
                return _projection.GetValueMatrix();
            }
            set {
                if(_projection != null)
                    _projection.SetValue(value);
            }
        }
        public Matrix View {
            get {
                return _view.GetValueMatrix();
            }
            set {
                if(_view != null)
                    _view.SetValue(value);
            }
        }
        public Matrix World {
            get {
                return _world.GetValueMatrix();
            }
            set {
                if(_world != null)
                    _world.SetValue(value);
            }
        }
        #endregion

        public VFXEffect(Effect cloneSource)
            : base(cloneSource) {
            _projection = this.Parameters["Projection"];
            _view = this.Parameters["View"];
            _world = this.Parameters["World"];
        }

        public override Effect Clone() {
            return (Effect)new VFXEffect((Effect)this) {
                DiffuseColor = _diffuseColor,
                SpecularColor = _specularColor,
                SpecularPower = _specularPower
            };
        }


        public Vector3 AmbientLightColor {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public DirectionalLight DirectionalLight0 {
            get {
                return _directionalLight;
            }
            set {
                _directionalLight.DiffuseColor = value.DiffuseColor;
                _directionalLight.Direction = value.Direction;
                _directionalLight.Enabled = value.Enabled;
                _directionalLight.SpecularColor = value.SpecularColor;
            }
        }

        public DirectionalLight DirectionalLight1 {
            get { throw new NotImplementedException(); }
        }

        public DirectionalLight DirectionalLight2 {
            get { throw new NotImplementedException(); }
        }

        public void EnableDefaultLighting() {
            throw new NotImplementedException();
        }

        public bool LightingEnabled {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

      
    }
}
