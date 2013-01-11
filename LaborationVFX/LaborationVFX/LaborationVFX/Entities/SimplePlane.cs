using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LaborationVFX.Entities {
    public class SimplePlane : AbstractEntity {
        protected VertexPositionColor[] vertices;
        protected int[] indices;

        private VertexBuffer vertexBuffer { get; set; }
        private IndexBuffer indexBuffer { get; set; }

        public virtual void Update() {
            this.World =
                Matrix.Identity
                * Matrix.CreateScale(this.Scale)
                * Matrix.CreateFromQuaternion(this.Rotation)
                * Matrix.CreateTranslation(this.Position);
        }

        public SimplePlane(GraphicsDevice graphicsDevice, Vector3 pos, Quaternion rot, float scale)
            : base (graphicsDevice, pos, rot, scale) {
            Initialize();

            this.vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), this.vertices.Length, BufferUsage.None);
            this.indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), this.indices.Length, BufferUsage.None);

            this.vertexBuffer.SetData<VertexPositionColor>(this.vertices);
            this.indexBuffer.SetData<int>(this.indices);

            this.World = 
                Matrix.Identity
                * Matrix.CreateScale(scale)
                * Matrix.CreateFromQuaternion(this.Rotation)
                * Matrix.CreateTranslation(this.Position);
        }

        private void Initialize()
        {
            vertices = new VertexPositionColor[4];
            //6 vertices make up 2 triangles which make up our rectangle
            indices = new int[6];

            //Go through each row
            Color color = Color.Black;

            // Top left
            vertices[0] = new VertexPositionColor(new Vector3(-10, 10, 0), color);
            indices[0] = 0;

            // Top right
            vertices[1] = new VertexPositionColor(new Vector3(10, 10, 0), color);
            indices[1] = 1;
            indices[3] = 3;

            // Bottom Left
            vertices[2] = new VertexPositionColor(new Vector3(-10, -10, 0), color);
            indices[2] = 2;
            indices[5] = 5;

            // Bottom right
            vertices[3] = new VertexPositionColor(new Vector3(10, -10, 0), color);
            indices[4] = 4;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(ref BasicEffect effect, ref Matrix parentWorld)
        {
            Matrix newWorld = this.World * parentWorld;
            effect.World = newWorld;
            effect.CurrentTechnique.Passes[0].Apply();

            this.Device.SetVertexBuffer(this.vertexBuffer);
            this.Device.Indices = this.indexBuffer;
            this.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vertices.Length, 0, this.indices.Length / 3);
        }
    }
}

