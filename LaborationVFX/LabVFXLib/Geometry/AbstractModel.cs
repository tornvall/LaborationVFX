using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LabVFXLib.Geometry {
    public abstract class AbstractModel {
        #region Fields
        protected Matrix _world;
        protected Quaternion _rotation;
        #endregion

        #region Properties
        public Vector3 Position { get; set; }
        public Quaternion Rotation {
            get {
                return this._rotation;
            }
            set {
                this._rotation = value;
                this._rotation.Normalize();
            }
        }

        public Boolean IsVisible { get; set; }
        #endregion

        protected AbstractModel() {
            _world = Matrix.Identity;
            _rotation = Quaternion.Identity;

            IsVisible = true;
        }

        public abstract void Draw(Transparency transparency);
    }
}
