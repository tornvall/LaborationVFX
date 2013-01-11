using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LaborationVFX.Entities
{
    public abstract class AbstractEntity
    {
        #region Fields and Properties

        private List<AbstractEntity> children;
        public List<AbstractEntity> Children
        {
            get {
                if (children == null)
                    return new List<AbstractEntity>();
                return children;
            }
            set { children = value; }
        }

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

        public virtual void LoadContent(ContentManager content)
        {
            foreach (AbstractEntity entity in Children)
            {
                entity.LoadContent(content);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (AbstractEntity entity in Children) {
                entity.Update(gameTime);
            }
        }

        public virtual void Draw(ref BasicEffect effect, ref Matrix parentWorld)
        {
            foreach (AbstractEntity entity in Children)
            {
                entity.Draw(ref effect, ref parentWorld);
            }
        }
    }
}