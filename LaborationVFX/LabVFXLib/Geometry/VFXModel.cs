using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LabVFXLib.Geometry {
    public class VFXModel : AbstractModel{
        private List<MeshAccess> _translucentMeshes;
        private List<MeshAccess> _opaqueMeshes;

        private List<Boolean> isVisible;
        private List<Boolean> isDoubleSided;
        

        public VFXModel(Model model) {
            _translucentMeshes = new List<MeshAccess>();
            _opaqueMeshes = new List<MeshAccess>();

        }

        public override void Draw(Transparency transparency) {
            if(transparency == Transparency.Translucent) {
                DrawMeshes(_translucentMeshes);
            } else {
                DrawMeshes(_opaqueMeshes);
            }
        }

        private void DrawMeshes(List<MeshAccess> meshes) {

        }

        private struct MeshAccess {
            public int MeshId;
            public ModelMesh Mesh;            

            public MeshAccess(int meshId, ModelMesh mesh) {
                this.MeshId = meshId;
                Mesh = mesh;
            }
        }
    }
}
