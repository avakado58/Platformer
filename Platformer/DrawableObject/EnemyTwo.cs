using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Ghostbusters
{
    class EnemyTwo:Enemy
    {
        
        int direction;
        Random random;
        Rectangle bounds;
        
        public EnemyTwo(Game game, ref Texture2D texture, Vector2 position, Rectangle rectangleSpriteSize, SoundEffect effect)
            : base(game, ref texture, position, rectangleSpriteSize, effect)
        {

            bounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
            random = new Random();
            direction = random.Next(-5, 5);
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            base.position.X += direction;
            if(IsCollideWithPlatform())
            {
                direction *= -1;
            }
            if(CheckBounds())
            {
                direction *= -1;
            }
        }
        bool CheckBounds()
        {
            if (base.position.X < bounds.Left)
            {
                base.position.X = bounds.Left;
                return true;
            }
            if (base.position.X + rectangleSpriteSize.Width > bounds.Right)
            {
                base.position.X = bounds.Right - rectangleSpriteSize.Width;
                return true;
            }
            return false;
        }
            bool IsCollideWithObject(BaseDrawObj obj)
        {
            return (position.X + this.rectangleSpriteSize.Width > obj.position.X &&
                position.X < obj.position.X + obj.rectangleSpriteSize.Width &&
                position.Y + this.rectangleSpriteSize.Height > obj.position.Y &&
                position.Y < obj.position.Y + obj.rectangleSpriteSize.Height);
        }
        bool IsCollideWithPlatform()
        {
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Platform)
                {
                    if (IsCollideWithObject((BaseDrawObj)Game.Components[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
       
    }
}
