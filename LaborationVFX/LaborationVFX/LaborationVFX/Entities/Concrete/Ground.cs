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

        public Ground(GraphicsDevice graphicsDevice, Vector3 position, Quaternion rotation, float scale)
            : base(graphicsDevice, position, rotation, scale)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Entities/ground-setts");

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(ref BasicEffect effect, ref Matrix parentWorld)
        {
            base.Draw(ref effect, ref parentWorld);
        }
    }
}
