using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Button: DrawableGameComponent
    {
        Texture2D textureFon;
        Vector2 position;
        Rectangle rectangleSize;
        Color colorFont;
        SpriteFont spriteFont;
        MouseState mouseState;
        public delegate void EventClik();
        public event EventClik Clik;
        int timeCount;
        string text;
        public Button(Game game, Texture2D textureFon,SpriteFont spriteFont, Vector2 position, string text):base(game)
        {
            this.textureFon = textureFon;
            this.position = position;
            this.rectangleSize.X = (int)position.X;
            this.rectangleSize.Y = (int)position.Y;
            this.rectangleSize.Width = textureFon.Width;
            this.rectangleSize.Height = textureFon.Height;
            this.spriteFont = spriteFont;
            this.text = text;
        }
        protected bool IsMouseClollide()
        {
            return (this.position.X + this.rectangleSize.Width > mouseState.X &&
                this.position.X < mouseState.X &&
                this.position.Y + this.rectangleSize.Height > mouseState.Y &&
                this.position.Y < mouseState.Y);
        }
        public override void Update(GameTime gameTime)
        {
            //rectangleSize.
            mouseState = Mouse.GetState();
            if(IsMouseClollide())
            {
                Mouse.SetCursor(MouseCursor.Hand);
                if(mouseState.LeftButton==ButtonState.Pressed)
                {
                    Clik?.Invoke();
                }
            }
            else
            {
                Mouse.SetCursor(MouseCursor.Arrow);
            }
            
            colorFont = Color.FromNonPremultiplied(66, 66, 66, timeCount % 256);
            
            timeCount += 7;
            base.Update(gameTime);
            
        }
        public override void Draw(GameTime gameTime)
        {
            
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(textureFon, position, Color.White);
            spriteBatch.DrawString(spriteFont, text, new Vector2(position.X+10,position.Y+5), colorFont);
            base.Draw(gameTime);

        }
    }
}
