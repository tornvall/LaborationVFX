﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaborationVFX.Entities
{
    public class AbstractEntity
    {
        #region Fields and Properties

        private GraphicsDevice device;
        public GraphicsDevice Device
        {
            get { return device; }
        }

        private Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            protected set { position = value; }
        }

        private Quaternion rotation;
        public Quaternion Rotation
        {
            get { return rotation; }
            protected set { rotation = value; }
        }

        private float scale;
        public float Scale
        {
            get { return scale; }
            protected set { scale = value; }
        }

        private Matrix world;
        public Matrix World
        {
            get { return world; }
            protected set { world = value; }
        }

        #endregion

        public AbstractEntity(GraphicsDevice graphicsDevice, Vector3 position, Quaternion rotation, float scale)
        {
            this.device = graphicsDevice;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}