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
        protected Rectangle forTextureMC;
        public Rectangle  rectangleSpriteSize;
        private readonly Rectangle bounds;
        public int IterRightFrame { get; set; } = 0;
        public int IterLeftFrame { get; set; } = 0;
        public Direction walkingDirection;
        private readonly float speed = 2;
        private readonly float gravity = 0.8f;//Сила тяжести 
        private readonly float acceleration = 0.03f;//Ускорения свободного падения
        private float grAcc = 0;//Скорость падения
        private readonly float jump = 70;
        public int Lives { get; private set; } = 2;
        public int Scores { get;private set; } = 0;
        private int enemy;

        public MainCharcter(Game game, ref Texture2D texture, Vector2 beginPosition, int enemy) : base(game)
        {

            Position = beginPosition * 64;
            textureMainCharacter = texture;
            walkingDirection = Direction.Right;
            RectangleInitialize();
            this.enemy = enemy;
            bounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }
        protected virtual void RectangleInitialize()
        {
            rectangleSpriteSize = new Rectangle(0, 0, 32, 32);
            forTextureMC = rectangleSpriteSize;
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
       
        void MoveUp(float speed)
        {
            Position.Y -= speed;
        }
        void MoveDown(float speed)
        {
            Position.Y += speed;
        }
        void MoveLeft(float speed)
        {
            Position.X -= speed;
        }
        void MoveRight(float speed)
        {
            Position.X += speed;
        }
        void CheckBounds()
        {
            if(Position.X< bounds.Left)
            {
                Position.X = bounds.Left;
            }
            if(Position.X+rectangleSpriteSize.Width> bounds.Right)
            {
                Position.X = bounds.Right - rectangleSpriteSize.Width;
            }
            if(Position.Y<bounds.Top)
            {
                Position.Y = bounds.Top;
            }
            if(Position.Y+rectangleSpriteSize.Height>bounds.Bottom)
            {
                Position.Y = bounds.Bottom - rectangleSpriteSize.Height;
            }
        }
        bool IsPlatformBotom()
        {
            Platform platform;
            bool result = false; 
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if(Game.Components[i] is Platform )
                {
                    platform = (Platform)Game.Components[i];
                    if (this.Position.X + rectangleSpriteSize.Width > platform.position.X &&
                        this.Position.X < platform.position.X + platform.rectangleSpriteSize.Width &&
                        this.Position.Y + 1 + rectangleSpriteSize.Height + 1 > platform.position.Y &&
                        this.Position.Y + 1 < platform.position.Y + platform.rectangleSpriteSize.Height)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        bool IsCollideWithObject(BaseDrawObj obj)
        {
            return (this.Position.X + this.rectangleSpriteSize.Width > obj.position.X &&
                this.Position.X < obj.position.X + obj.rectangleSpriteSize.Width &&
                this.Position.Y + this.rectangleSpriteSize.Height > obj.position.Y &&
                this.Position.Y < obj.position.Y + obj.rectangleSpriteSize.Height);
        }
        bool IsCollideWithStairs()
        {
            Stairs stairs;
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Stairs)
                {
                    stairs = (Stairs)Game.Components[i];
                    if (this.Position.X + this.rectangleSpriteSize.Width + 1 > stairs.position.X &&
                        this.Position.X + 1 < stairs.position.X + stairs.rectangleSpriteSize.Width &&
                        this.Position.Y + this.rectangleSpriteSize.Height + 1 > stairs.position.Y &&
                        this.Position.Y + 1 < stairs.position.Y + stairs.rectangleSpriteSize.Height)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        bool IsCollideWithPlatform()
        {
            
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Platform)
                {
                    
                    if(IsCollideWithObject((BaseDrawObj)Game.Components[i]))
                    {
                        return true;
                    }


                }
            }
            return false;
        }
        void Move()
        {
            KeyboardState state = Keyboard.GetState();
            if(state.IsKeyDown(Keys.W))
            {
                if(IsPlatformBotom()==true&&IsCollideWithStairs()==false)
                {
                    MoveUp(jump);
                    while (IsCollideWithPlatform())
                    {
                        MoveDown(speed / 10);
                    }
                }
                if(IsCollideWithStairs()==true)
                {
                    MoveUp(speed);
                    while (IsCollideWithPlatform())
                    {
                        MoveDown(speed / 10);
                    }
                }
            }
            if (state.IsKeyDown(Keys.S))
            {
                MoveDown(speed);
                while(IsCollideWithPlatform())
                {
                    MoveUp(speed / 10);
                }
            }
            if (state.IsKeyDown(Keys.A))
            {
                MoveLeft(speed);
                while (IsCollideWithPlatform())
                {
                    MoveRight(speed / 10);
                }
            }
            if (state.IsKeyDown(Keys.D))
            {
                MoveRight(speed);
                while (IsCollideWithPlatform())
                {
                    MoveLeft(speed / 10);
                }
            }
        }
                                            
        void IsCollideWithAny()
        {

            BaseDrawObj FindObj = null;
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is StarCoin)
                {
                    if(IsCollideWithObject((BaseDrawObj)Game.Components[i]))
                    {
                        FindObj = (BaseDrawObj)Game.Components[i];
                    }
                   
                    
                }
            }
            if (FindObj != null)
            {
                Scores += 10;
                Game.Components.Remove(FindObj);
                FindObj.Dispose();
                FindObj = null;

            }
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Heart)
                {
                    if (IsCollideWithObject((BaseDrawObj)Game.Components[i]))
                    {
                        FindObj = (BaseDrawObj)Game.Components[i];
                    }
                    

                }
            }
            if (FindObj != null)
            {
                Lives++;
                Game.Components.Remove(FindObj);
                FindObj.Dispose();
                FindObj = null;

            }
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Enemy)
                {
                    if (IsCollideWithObject((BaseDrawObj)Game.Components[i]))
                    {
                        FindObj = (BaseDrawObj)Game.Components[i];
                    }
                    

                }
            }
            if (FindObj != null)
            {
                Lives--;
                FindObj.Dispose();
                FindObj = null;

            }



        }
        void GotoDown()
        {
            if(IsCollideWithStairs()==false)
            {
                if(this.grAcc==0)
                {
                    grAcc = acceleration;
                }
                MoveDown(grAcc);
                while (IsCollideWithPlatform())
                {
                    MoveUp(speed / 10);
                }
                grAcc += acceleration;
                if(IsPlatformBotom())
                {
                    grAcc = 0;
                }
                if(IsCollideWithStairs())
                {
                    grAcc = 0;
                }
            }
        }
        void IskillEnemy()
        {
            BaseDrawObj FindObj = null;
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is Enemy)
                {
                    FindObj = (BaseDrawObj)Game.Components[i];
                    if (this.Position.X + this.rectangleSpriteSize.Width > FindObj.position.X &&
                    this.Position.X < FindObj.position.X + FindObj.rectangleSpriteSize.Width &&
                    this.Position.Y + this.rectangleSpriteSize.Height < FindObj.position.Y &&
                    this.Position.Y < FindObj.position.Y + FindObj.rectangleSpriteSize.Height && (FindObj.position.Y - this.Position.Y) < 35)
                    {
                        
                        
                    }
                    else
                    {
                        FindObj = null;
                    }

                    if (FindObj != null)
                    {
                        Scores += 10;
                        enemy--;
                        Game.Components.Remove(FindObj);
                        FindObj.Dispose();
                        FindObj = null;

                    }

                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GotoDown();
            Move();
            CheckBounds();
            IskillEnemy();
            IsCollideWithAny();


            Game.Window.Title = $"Количество очков = {Scores} Количество жизней {Lives}";
            if(Lives<0)
            {
                Game.Window.Title = "Вы проиграли";
                this.Dispose();
            }
            if(enemy==0)
            {
                Game.Window.Title = "Вы выиграли";
            }
            
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
                    spriteBatch.Draw(textureMainCharacter, Position, forTextureMC, Color.White);
                    break;
            }

            base.Draw(gameTime);
        }
    }
    
}
