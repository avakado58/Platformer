using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    enum Direction
    {
        UpDown = 0,
        Jamp = 1,
        Left = 2,
        Right = 3
    }
    class MainCharcter:DrawableGameComponent
    {
        public Vector2 Position;
        protected Texture2D textureMainCharacter;
        protected Rectangle upDownFrameOfTextureMC;//MC - MainCharacter
        protected Rectangle jampFrameOfTextureMC;
        protected Rectangle[] leftFrameOfTextureMC;
        protected Rectangle[] rightFrameOfTextureMC;
        public Rectangle sizeOfSprite;
        public int IterRightFrame { get; set; } = 0;
        public int IterLeftFrame { get; set; } = 0;
        public Direction walkingDirection;
        public MainCharcter(Game game,ref Texture2D texture, Vector2 beginPosition) :base(game)
        {
            Position = beginPosition * 64;
            textureMainCharacter = texture;
            walkingDirection = Direction.Right;
            RectangleInitialize();
        }
        protected virtual void RectangleInitialize()
        {
            sizeOfSprite = new Rectangle(0, 0, 30, 64);
            upDownFrameOfTextureMC = new Rectangle(105,0,30, 64);
            jampFrameOfTextureMC = new Rectangle(57, 67, 30, 64);
            leftFrameOfTextureMC = new Rectangle[] {
                new Rectangle(57,191,30,64),
                new Rectangle(0,192,30,64),
                new Rectangle(114,192,30,64)
            };
            rightFrameOfTextureMC = new Rectangle[] {
                new Rectangle(57,67,30,64),
                new Rectangle(0,68,30,64),
                new Rectangle(114,68,30,64)
            };
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
            switch (walkingDirection)
            {
                case Direction.UpDown:
                    spriteBatch.Draw(textureMainCharacter, Position, upDownFrameOfTextureMC, Color.White);
                    break;
                case Direction.Jamp:
                    spriteBatch.Draw(textureMainCharacter, Position, jampFrameOfTextureMC, Color.White);
                    break;
                case Direction.Left:
                    spriteBatch.Draw(textureMainCharacter, Position, leftFrameOfTextureMC[IterLeftFrame], Color.White);
                    break;
                case Direction.Right:
                    spriteBatch.Draw(textureMainCharacter, Position, rightFrameOfTextureMC[IterRightFrame], Color.White);
                    break;
            }

            base.Draw(gameTime);
        }
    }
    
}
