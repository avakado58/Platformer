using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    class BaseDrawObj:DrawableGameComponent
    {
        Texture2D textureObj;
        SoundEffect effect;
        public Vector2 position;
        public Rectangle rectangleSpriteSize;
        public BaseDrawObj(Game game,ref Texture2D textureObj, Vector2 position, Rectangle rectangleSpriteSize, SoundEffect effect) :base(game)
        {
            this.textureObj = textureObj;
            this.position = position * 64;
            this.rectangleSpriteSize = rectangleSpriteSize;
            this.effect = effect;
        }
        public void PlaySoundEffect()
        {
            effect.Play();
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
