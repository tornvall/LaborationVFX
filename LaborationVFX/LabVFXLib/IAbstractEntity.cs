using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LabVFXLib
{
    public interface IAbstractEntity
    {
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(BasicEffect effect, Matrix parentWorld);
    }
}
