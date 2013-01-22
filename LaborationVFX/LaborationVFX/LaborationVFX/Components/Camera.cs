using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaborationVFX.Components
{
    public class Camera
    {
        private GraphicsDevice device;
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 UpDirection { get; set; }
        public Matrix ViewMatrix { get; set; }
        public Matrix ProjectionMatrix { get; set; }

        public float AspectRatio { get; set; }
        public float nearPlaneDistance { get; set; }
        public float farPlaneDistance { get; set; }

        public Camera(GraphicsDevice graphics)
        {
            this.device = graphics;

            //Settings
            this.AspectRatio = device.DisplayMode.AspectRatio;
            this.Position = new Vector3(3, 3, 5);
            this.Rotation = Quaternion.Identity;
            this.nearPlaneDistance = 0.1f;
            this.farPlaneDistance = 10000f;

            this.ViewMatrix = Matrix.CreateLookAt(this.Position, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, this.AspectRatio, this.nearPlaneDistance, this.farPlaneDistance);
        }

        public void Update(Vector3 pos, Quaternion rot)
        {
            this.Rotation = Quaternion.Lerp(this.Rotation, rot, 0.1f);

            Vector3 campos = new Vector3(0, 0, -1f);
            campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(this.Rotation));
            campos += pos;

            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(this.Rotation));

            this.Position = campos;
            this.UpDirection = camup;

            this.ViewMatrix = Matrix.CreateLookAt(this.Position, pos, this.UpDirection);
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, this.AspectRatio, this.nearPlaneDistance, this.farPlaneDistance);
        }

        //public void Update(AbstractEntity entity)
        //{
        //    this.Rotation = Quaternion.Lerp(this.Rotation, entity.GetRotation(), 0.1f);

        //    Vector3 campos = new Vector3(0, 2f, -4f);
        //    campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(this.Rotation));
        //    campos += entity.GetPosition();

        //    Vector3 camup = new Vector3(0, 1, 0);
        //    camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(this.Rotation));

        //    this.Position = campos;
        //    this.UpDirection = camup;

        //    this.ViewMatrix = Matrix.CreateLookAt(this.Position, entity.GetPosition(), this.UpDirection);
        //    this.ViewProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
        //        MathHelper.PiOver4, this.AspectRatio, this.nearPlaneDistance, this.farPlaneDistance);
        //}
    }
}
