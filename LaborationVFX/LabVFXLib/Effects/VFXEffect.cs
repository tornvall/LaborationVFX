using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Effects {
    public class VFXEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog{
        #region Fields
        private FogProperties _fog;
        private float _alpha;
        private float _specularPower;
        private EffectParameter _diffuseColor;
        private EffectParameter _specularColor;
        private Texture2D _diffuseTexture;
        private EffectParameter _projection;
        private EffectParameter _view;
        private EffectParameter _world;
        private DirectionalLight _directionalLight;
        private Vector3 _ambientLightColor;
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
        public Vector4 DiffuseColor {
            get {
                return _diffuseColor.GetValueVector4();
            }
            set {
                _diffuseColor.SetValue(value);
                //_diffuseColor = value;
                //if(this.Parameters["DiffuseColor"] != null)
                  //  this.Parameters["DiffuseColor"].SetValue(new Vector4(_diffuseColor, _alpha));
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
        public Vector4 SpecularColor {
            get {
                return _specularColor.GetValueVector4();
            }
            set {
                _specularColor.SetValue(value);
                /*if(this.Parameters["SpecularColor"] != null)
                    this.Parameters["SpecularColor"].SetValue(_specularColor);*/
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
                if(this.Parameters["DiffuseTextureEnabled"] != null) {
                    if(_diffuseTexture != null)
                        this.Parameters["DiffuseTextureEnabled"].SetValue(true);
                    else
                        this.Parameters["DiffuseTextureEnabled"].SetValue(false);
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
        public Vector3 AmbientLightColor {
            get {
                return _ambientLightColor;
            }
            set {
                _ambientLightColor =(value);
                //if(this.Parameters["AmbientLightColor"] != null)
                  //  this.Parameters["AmbientLightColor"].SetValue(_ambientLightColor);
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
                return true;
            }
            set {
            }
        }
        #endregion

        public VFXEffect(Effect cloneSource)
            : base(cloneSource) {
            _projection = this.Parameters["Projection"];
            _view = this.Parameters["View"];
            _world = this.Parameters["World"];
            _directionalLight = new DirectionalLight(
                this.Parameters["DirectionalLightDirection"],
                this.Parameters["DirectionalLightDiffuseColor"],
                this.Parameters["DirectionalLightSpecularColor"],
                (DirectionalLight)null);
            _directionalLight.Enabled = true;            
            this.Parameters["DiffuseColor"].SetValue(Vector4.Zero);
            this.Parameters["SpecularColor"].SetValue(Vector4.Zero);
            _diffuseColor = this.Parameters["DiffuseColor"];
            _specularColor = this.Parameters["SpecularColor"];
            //this.Parameters["AmbientColor"].SetValue(Vector4.Zero);
            _ambientLightColor = Utils.Vector4toVector3(this.Parameters["AmbientColor"].GetValueVector4());
        }

        public override Effect Clone() {
            return (Effect)new VFXEffect((Effect)this) {
                DiffuseColor = _diffuseColor.GetValueVector4(),
                SpecularColor = _specularColor.GetValueVector4(),
                SpecularPower = _specularPower,
                AmbientLightColor = _ambientLightColor,                                       
                DirectionalLight0 = _directionalLight,
            };            
        }              
    }
}
