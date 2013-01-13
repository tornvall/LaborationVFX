using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LabVFXLib.Geometry {
    public class VFXModel : Model{
        private List<MeshAccess> translucentMeshes = new List<MeshAccess>();
        private List<MeshAccess> opaqueMeshes = new List<MeshAccess>();



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
