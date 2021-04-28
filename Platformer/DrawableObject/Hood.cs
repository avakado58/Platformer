using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    class Hood:DrawableGameComponent
    {
        SpriteFont spriteFont;
        Vector2 position;
        public int Score { get; set; }
        public int Lives { get; set; }
        public TimeSpan Time { get; set; }
        public Hood(Game game, SpriteFont spriteFont, Vector2 position):base(game)
        {
            this.spriteFont = spriteFont;
            this.position = position;

        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.DrawString(spriteFont, $"Количество очков {Score}", position, Color.Black);
            spriteBatch.DrawString(spriteFont, $"Количество жизней {Lives}", new Vector2(position.X, position.Y+17), Color.Black);
            spriteBatch.DrawString(spriteFont, $"Прошло времени {Time}", new Vector2(position.X, position.Y + 34), Color.Black);
        }

    }
}
