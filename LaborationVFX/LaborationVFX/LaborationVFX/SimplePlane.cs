using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LaborationVFX {
    public class SimplePlane {
        protected GraphicsDevice device;
        protected VertexPositionColor[] vertices;
        protected int[] indices;

        private VertexBuffer vertexBuffer { get; set; }
        private IndexBuffer indexBuffer { get; set; }

        public Quaternion Rotation;
        protected Vector3 Position;
        protected float Scale;

        private Matrix world;

        public virtual void Update() {
            this.world =
                Matrix.Identity
                * Matrix.CreateScale(this.Scale)
                * Matrix.CreateFromQuaternion(this.Rotation)
                * Matrix.CreateTranslation(this.Position);
        }

        public SimplePlane(GraphicsDevice graphicsDevice, Vector3 pos, Quaternion rot, float scale) {
            this.device = graphicsDevice;
            initializeGround();

            this.vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), this.vertices.Length, BufferUsage.None);
            this.indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), this.indices.Length, BufferUsage.None);

            this.vertexBuffer.SetData<VertexPositionColor>(this.vertices);
            this.indexBuffer.SetData<int>(this.indices);

            this.Position = pos;
            this.Rotation = rot;
            this.Scale = scale;

            this.world = 
                Matrix.Identity
                * Matrix.CreateScale(scale)
                * Matrix.CreateFromQuaternion(this.Rotation)
                * Matrix.CreateTranslation(this.Position);
        }

        public void Draw(ref BasicEffect effect, ref Matrix parentWorld) {
            Matrix newWorld = this.world * parentWorld;
            effect.World = newWorld;
            effect.CurrentTechnique.Passes[0].Apply();

            this.device.SetVertexBuffer(this.vertexBuffer);
            this.device.Indices = this.indexBuffer;
            this.device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.vertices.Length, 0, this.indices.Length / 3);
          
            
        }
    

            private void initializeGround() {
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
        }



}

