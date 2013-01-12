using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LaborationVFX.Components.Input;

namespace LaborationVFX.Components
{
    public class FlyingCamera
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        private float TurnSpeed = 0.002f;
        private float MoveSpeed = 0.02f;

        public FlyingCamera()
        {
            this.Position = new Vector3(0, 0, 0);
            this.Rotation = Quaternion.Identity;

            this.Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.ToRadians(180));

        }


        public void PerformAction(ActionType action, float elapsedTime)
        {

            float turningSpeed = (this.TurnSpeed * elapsedTime);
            float moveDistance = (this.MoveSpeed * elapsedTime);


            float leftRightRot = 0;
            float upDownRot = 0;
            float yawRot = 0;

            switch (action)
            {
                case ActionType.Up:
                    Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), this.Rotation);
                    this.Position += forward * moveDistance;
                    break;
                case ActionType.Down:
                    Vector3 backward = Vector3.Transform(new Vector3(0, 0, 1), this.Rotation);
                    this.Position -= backward * moveDistance;
                    break;
                case ActionType.Left:
                    Vector3 left = Vector3.Transform(new Vector3(1, 0, 0), this.Rotation);
                    this.Position += left * moveDistance;
                    break;
                case ActionType.Right:
                    Vector3 right = Vector3.Transform(new Vector3(1, 0, 0), this.Rotation);
                    this.Position -= right * moveDistance;
                    break;
                case ActionType.IncreaseSpeed:
                    Vector3 up = Vector3.Transform(new Vector3(0, 1, 0), this.Rotation);
                    this.Position += up * moveDistance;
                    break;
                case ActionType.DecreaseSpeed:
                    Vector3 down = Vector3.Transform(new Vector3(0, 1, 0), this.Rotation);
                    this.Position -= down * moveDistance;
                    break;
                case ActionType.PitchUp:
                    upDownRot -= turningSpeed;
                    break;
                case ActionType.PitchDown:
                    upDownRot += turningSpeed;
                    break;
                case ActionType.RollLeft:
                    leftRightRot += turningSpeed;
                    break;
                case ActionType.RollRight:
                    leftRightRot -= turningSpeed;
                    break;
                case ActionType.YawLeft:
                    yawRot += turningSpeed;
                    break;
                case ActionType.YawRight:
                    yawRot -= turningSpeed;
                    break;
            }

            Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), leftRightRot)
                * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), upDownRot)
                * Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), yawRot);

            this.Rotation *= additionalRot;
        }
    }
}
