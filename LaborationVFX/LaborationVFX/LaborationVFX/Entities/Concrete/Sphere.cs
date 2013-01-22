using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabVFXLib.Geometry;
using Microsoft.Xna.Framework.Graphics;
using LabVFXLib.Effects;
using Microsoft.Xna.Framework;

namespace LaborationVFX.Entities.Concrete
{
    public class Sphere : AbstractEntity
    {
        private Effect effect;
        private Model model;
        private TextureCube skyboxTexture;

        public Sphere(GraphicsDevice device, Model model, Effect effect, TextureCube skyboxTexture)
            : base(device, Vector3.Zero, Quaternion.Identity, 1f) {
            this.Position = new Vector3(-2f, 1f, 0f);
            this.effect = effect;
            this.model = model;
            this.skyboxTexture = skyboxTexture;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                    effect.Parameters["World"].SetValue(world * mesh.ParentBone.Transform);
                    effect.Parameters["View"].SetValue(view);
                    effect.Parameters["Projection"].SetValue(projection);
                    effect.Parameters["SkyboxTexture"].SetValue(skyboxTexture);
                    effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    effect.Parameters["WorldInverseTranspose"].SetValue(
                                            Matrix.Transpose(Matrix.Invert(world * mesh.ParentBone.Transform)));
                }
                mesh.Draw();
            }
        }
    }
}
