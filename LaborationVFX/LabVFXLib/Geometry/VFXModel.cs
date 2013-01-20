using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using LabVFXLib.Effects;

namespace LabVFXLib.Geometry {
    public class VFXModel : AbstractModel{
        private List<AccessMesh> _translucentMeshes;
        private List<AccessMesh> _opaqueMeshes;

        private List<Boolean> _isVisible;
        private List<Boolean> _isDoubleSided;
        private Matrix[] _meshTransform;
        

        public VFXModel(Model model):base(model) {
            _translucentMeshes = new List<AccessMesh>();
            _opaqueMeshes = new List<AccessMesh>();
            _isVisible = new List<Boolean>();
            _isDoubleSided = new List<Boolean>();

            _meshTransform = new Matrix[_model.Meshes.Count];

        }

        private void SetupModel() {
            for(int i = 0; i < _meshTransform.Length; i++) {
                _isVisible.Add(true);
                _meshTransform[i] = Matrix.Identity;
            }
            for(int id = 0; id < _model.Meshes.Count; id++) {
                ModelMesh mesh = _model.Meshes[id];
                foreach(ModelMeshPart part in mesh.MeshParts) {
                    if(PartIsTranslucent(part)) {
                        this._translucentMeshes.Add(new AccessMesh(id, mesh));
                        break;
                    }
                }
            }
        }

        private bool PartIsTranslucent(ModelMeshPart part) {
            bool result = false;

            if(part.Effect is BasicEffect){
             if((double)((BasicEffect)part.Effect).Alpha < 1.0 || (double)((VFXEffect)part.Effect).Alpha < 1.0)   
                 result = true;
            }
            return result;
        }

        public override void Draw(Transparency transparency) {
            if(transparency == Transparency.Translucent) {
                DrawMeshes(_translucentMeshes);
            } else {
                DrawMeshes(_opaqueMeshes);
            }
        }

        private void DrawMeshes(List<AccessMesh> meshes) {

        }

        private struct AccessMesh {
            public int MeshId;
            public ModelMesh Mesh;            

            public AccessMesh(int meshId, ModelMesh mesh) {
                this.MeshId = meshId;
                Mesh = mesh;
            }
        }
    }
}
