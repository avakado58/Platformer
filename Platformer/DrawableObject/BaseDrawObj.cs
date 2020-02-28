using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class BaseDrawObj:DrawableGameComponent
    {
        Texture2D textureObj;
        Vector2 position;
        public BaseDrawObj(Game game,ref Texture2D textureObj, Vector2 position):base(game)
        {
            this.textureObj = textureObj;
            this.position = position * 64;

        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Draw(textureObj, position, Color.White);
            base.Draw(gameTime);
        }
    }
}
