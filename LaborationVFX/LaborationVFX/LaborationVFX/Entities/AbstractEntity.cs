using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LabVFXLib;

namespace LaborationVFX.Entities
{
    public abstract class AbstractEntity : IAbstractEntity
    {
        #region Fields
        private List<AbstractEntity> children;     
        private GraphicsDevice device;       
        private Vector3 position;       
        private Quaternion rotation;        
        private float scale;       
        private Matrix world;     
        #endregion

        #region Properties
        public List<AbstractEntity> Children {
            get {
                if(children == null)
                    return new List<AbstractEntity>();
                return children;
            }
            set { children = value; }
        }
        public GraphicsDevice Device {
            get { return device; }
        }
        public Vector3 Position {
            get { return position; }
            protected set { position = value; }
        }
        public Quaternion Rotation {
            get { return rotation; }
            protected set { rotation = value; }
        }
        public float Scale {
            get { return scale; }
            protected set { scale = value; }
        }
        public Matrix World {
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

        public virtual void Draw(BasicEffect effect, Matrix parentWorld)
        {
            foreach (AbstractEntity entity in Children)
            {
                entity.Draw(effect, parentWorld);
            }
        }
    }
}