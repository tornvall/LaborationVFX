using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LaborationVFX.Entities
{
    public class Ground : AbstractEntity
    {
        private Texture2D texture;
        private VertexPositionColorTexture[] vertices;
        private int[] indices;

        private VertexBuffer vertexBuffer { get; set; }
        private IndexBuffer indexBuffer { get; set; }

        public Ground(GraphicsDevice graphicsDevice, Vector3 position, Quaternion rotation, float scale)
            : base(graphicsDevice, position, rotation, scale)
        {
            this.World =
                Matrix.Identity
                * Matrix.CreateScale(scale)
                * Matrix.CreateFromQuaternion(this.Rotation)
                * Matrix.CreateTranslation(this.Position);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Entities/ground-setts");
            Initialize();

            this.vertexBuffer = new VertexBuffer(Device, typeof(VertexPositionColorTexture), this.vertices.Length, BufferUsage.None);
            this.indexBuffer = new IndexBuffer(Device, typeof(int), this.indices.Length, BufferUsage.None);

            this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices);
            this.indexBuffer.SetData<int>(this.indices);
            
            base.LoadContent(content);
        }

        private void Initialize() {
            vertices = new VertexPositionColorTexture[4];
            //6 vertices make up 2 triangles which make up our rectangle
            indices = new int[6];

            //Go through each row
            Color color = Color.Black;

            // Top left
            vertices[0] = new VertexPositionColorTexture(new Vector3(-10, 0, -10), color , new Vector2(0,0));
            indices[0] = 0;

            // Top right
            vertices[1] = new VertexPositionColorTexture(new Vector3(10, 0, -10), color, new Vector2(1, 0));
            indices[1] = 1;
            indices[3] = 1;

            // Bottom Left
            vertices[2] = new VertexPositionColorTexture(new Vector3(-10, 0, 10), color, new Vector2(0,1));
            indices[2] = 2;
            indices[5] = 2;

            // Bottom right
            vertices[3] = new VertexPositionColorTexture(new Vector3(10, 0, 10), color, new Vector2(1, 1));
            indices[4] = 3;
        }

        public override void Update(GameTime gameTime)
        {
            //this.World =
            //    Matrix.Identity
            //    * Matrix.CreateScale(this.Scale)
            //    * Matrix.CreateFromQuaternion(this.Rotation)
            //    * Matrix.CreateTranslation(this.Position);

            base.Update(gameTime);
        }

        public override void Draw(BasicEffect effect, Matrix parentWorld)
        {
            Matrix newWorld = this.World * parentWorld;
            effect.World = newWorld;
            effect.Texture = texture;
            effect.TextureEnabled = true;
            //effect.VertexColorEnabled = true;
            effect.CurrentTechnique.Passes[0].Apply();

            this.Device.SetVertexBuffer(this.vertexBuffer);
            this.Device.Indices = this.indexBuffer;
            this.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vertices.Length, 0, this.indices.Length / 3);            

            base.Draw(effect, parentWorld);
        }
    }
}
