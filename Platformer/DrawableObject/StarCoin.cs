using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Platformer
{
    class StarCoin : BaseDrawObj
    {
        public StarCoin(Game game, ref Texture2D texturePlatform, Vector2 position, Rectangle rectangleSpriteSize) : base(game, ref texturePlatform, position, rectangleSpriteSize)
        {

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
            base.Draw(gameTime);
        }
    }
}
