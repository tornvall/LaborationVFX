using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabVFXLib.Geometry {
    public abstract class AbstractModel {
        #region Fields
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Boolean _visible;
        protected Model _model;
        #endregion

        #region Properties
        public Vector3 Position {
            get { return _position; }
            set { _position = value; }
        }
        public Quaternion Rotation {
            get {
                return this._rotation;
            }
            set {
                this._rotation = value;
            }
        }
        public Boolean Visible {
            get { return _visible; }
            set { _visible = value; }
        }
        #endregion

        protected AbstractModel(Model model) {
            _position = Vector3.Zero;
            _rotation = Quaternion.Identity;
            _model = model;
            _visible = true;
        }

        public abstract void Draw(Transparency transparency, Matrix view, Matrix projection);
    }
}
