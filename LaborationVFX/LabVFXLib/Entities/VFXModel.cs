using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using LabVFXLib.Effects;

namespace LabVFXLib.Geometry {
    public class VFXModel : AbstractModel {
        private List<AccessMesh> _translucentMeshes;
        private List<AccessMesh> _opaqueMeshes;

        private List<Boolean> _isVisible;
        private List<Boolean> _isDoubleSided;
        private Matrix[] _meshTransform;
        private VFXEffect _effect;
        private GraphicsDevice _device;

        public VFXModel(GraphicsDevice device, Model model, VFXEffect effect):base(model) {
            _effect = effect;
            _device = device;
            _translucentMeshes = new List<AccessMesh>();
            _opaqueMeshes = new List<AccessMesh>();
            _isVisible = new List<Boolean>();
            _isDoubleSided = new List<Boolean>();

            _meshTransform = new Matrix[_model.Meshes.Count];

            this.SetupModel();
            this.SetupEffect();
        }

        private void SetupModel() {
            for(int i = 0; i < _meshTransform.Length; i++) {
                _isVisible.Add(true);
                _isDoubleSided.Add(false);
                _meshTransform[i] = Matrix.Identity;
            }
            for(int id = 0; id < _model.Meshes.Count; id++) {
                ModelMesh mesh = _model.Meshes[id];
                foreach(ModelMeshPart part in mesh.MeshParts) {
                    if(PartIsTranslucent(part)) {
                        _translucentMeshes.Add(new AccessMesh(id, mesh));
                        break;
                    }
                }
            }
        }

        private void SetupEffect() {
            foreach(ModelMesh modelMesh in _model.Meshes) {
                foreach(ModelMeshPart modelMeshPart in modelMesh.MeshParts) {
                    if(modelMeshPart.Effect is BasicEffect) {
                        BasicEffect basicEffect = (BasicEffect)modelMeshPart.Effect ;
                        modelMeshPart.Effect = _effect.Clone();
                        VFXEffect modelEffect = (VFXEffect)modelMeshPart.Effect;

                        modelEffect.Alpha = basicEffect.Alpha;
                        modelEffect.DiffuseColor = basicEffect.DiffuseColor;
                        modelEffect.SpecularColor = basicEffect.SpecularColor;
                        modelEffect.SpecularPower = basicEffect.SpecularPower;
                        if(basicEffect.Texture != null)
                            modelEffect.DiffuseTexture = basicEffect.Texture;
                    } else {
                        modelMeshPart.Effect = (Effect)_effect;
                    }
                }
            }
        }

        private bool PartIsTranslucent(ModelMeshPart modelMeshPart) {
            bool result = false;

            if(modelMeshPart.Effect is BasicEffect){
                if((double)((BasicEffect)modelMeshPart.Effect).Alpha < 1.0)   //|| (double)((VFXEffect)modelMeshPart.Effect).Alpha < 1.0
                 result = true;
            }
            return result;
        }

        public override void Draw(Matrix view, Matrix projection) {
            if(_visible) {
                //Draw all opaque
                _device.BlendState = BlendState.Opaque;
                _device.DepthStencilState = DepthStencilState.Default;
                this.DrawMeshes(_opaqueMeshes, view, projection);

                //Draw all translucent
                _device.BlendState = BlendState.AlphaBlend;
                _device.DepthStencilState = DepthStencilState.DepthRead;
                this.DrawMeshes(_translucentMeshes, view, projection);
            }
        }

        private void DrawMeshes(List<AccessMesh> meshes,Matrix view, Matrix projection) {
            foreach(AccessMesh am in meshes) {
                if(_isVisible.ElementAt(am.MeshId)) {
                    if(_isDoubleSided.ElementAt(am.MeshId)) {
                        _device.RasterizerState = RasterizerState.CullNone;
                        foreach(ModelMeshPart part in am.Mesh.MeshParts)
                            this.DrawMeshPart(am, part, view, projection);
                        _device.RasterizerState = RasterizerState.CullCounterClockwise;
                    } else {
                        foreach(ModelMeshPart part in am.Mesh.MeshParts)
                            this.DrawMeshPart(am, part, view, projection);
                    }
                }
            }
        }


        private void DrawMeshPart(AccessMesh am, ModelMeshPart modelMeshPart,Matrix view, Matrix projection) {
            if(modelMeshPart.Effect is IEffectMatrices) {
                IEffectMatrices effectMatrices = modelMeshPart.Effect as IEffectMatrices;
                effectMatrices.Projection = projection;
                effectMatrices.View = view;
                effectMatrices.World = _meshTransform[am.MeshId]
                    * (_model.Meshes)[am.MeshId].ParentBone.Transform
                    * Matrix.CreateTranslation(_position);
            }

            _device.SetVertexBuffer(modelMeshPart.VertexBuffer);
            _device.Indices = modelMeshPart.IndexBuffer;
            foreach(EffectPass effectPass in modelMeshPart.Effect.CurrentTechnique.Passes) {
                effectPass.Apply();
                _device.DrawIndexedPrimitives(PrimitiveType.TriangleList, modelMeshPart.VertexOffset,
                    modelMeshPart.StartIndex, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount);
            }
        }

        private struct AccessMesh {
            public int MeshId;
            public ModelMesh Mesh;            

            public AccessMesh(int meshId, ModelMesh mesh) {
                this.MeshId = meshId;
                Mesh = mesh;
            }
        }

        public void SetLighting(VFXEffect baseEffect) {
            foreach(ModelMesh modelMesh in _model.Meshes) {
                foreach(Effect effect in modelMesh.Effects) {
                    if(typeof(IEffectFog).Equals(effect)) {
                        IEffectFog fogEffect = (IEffectFog)effect;
                        fogEffect.FogColor = baseEffect.FogColor;
                        fogEffect.FogEnabled = baseEffect.FogEnabled;
                        fogEffect.FogStart = baseEffect.FogStart;
                        fogEffect.FogEnd = baseEffect.FogEnd;
                    }
                    if(typeof(IEffectLights).Equals(effect)) {
                        IEffectLights lightEffect = (IEffectLights)effect;
                        lightEffect.AmbientLightColor = baseEffect.AmbientLightColor;
                        lightEffect.LightingEnabled = true;
                        lightEffect.DirectionalLight0.Direction = baseEffect.DirectionalLight0.Direction;
                        lightEffect.DirectionalLight0.DiffuseColor = baseEffect.DirectionalLight0.DiffuseColor;
                        lightEffect.DirectionalLight0.Enabled = true;
                        lightEffect.DirectionalLight0.SpecularColor = baseEffect.DirectionalLight0.SpecularColor;                       
                        
                    }
                }
            }
        }
    }
}
