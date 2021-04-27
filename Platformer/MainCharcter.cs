using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    enum Direction
    {
        UpDown = 0,
        Jamp = 1,
        Left = 2,
        Right = 3
    }
    public class MainCharcter:DrawableGameComponent
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
        public Color color;
        public int IterRightFrame { get; set; } = 0;
        public int IterLeftFrame { get; set; } = 0;
        Direction walkingDirection;
        private readonly float speed = 4;
        private readonly float gravity = 0.8f;//Сила тяжести 
        private readonly float acceleration = 0.3f;//Ускорения свободного падения
        private float grAcc = 2;//Скорость падения
        private readonly float jump = 70;
        public int Lives { get; private set; } = 2;
        public int Scores { get;private set; } = 0;
        private int enemy;
        TimerCallback tm;
        Timer timer;
        bool flagEventWin;
        public delegate void EventStateChange(string stateMC);
        SoundEffect effectWinLevel;
        public event EventStateChange NextLevel;
        public event EventStateChange WonLevel;
        public event EventStateChange LoseGame;

        public MainCharcter(Game game, ref Texture2D texture, Vector2 beginPosition, int enemy) : base(game)
        {

            Position = beginPosition * 64;
            textureMainCharacter = texture;
            walkingDirection = Direction.Right;
            RectangleInitialize();
            this.enemy = enemy;
            bounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
            flagEventWin = false;
            color = Color.White;
            effectWinLevel = game.Content.Load<SoundEffect>("LevelStart");

        }
        protected virtual void RectangleInitialize()
        {
            rectangleSpriteSize = new Rectangle(0, 0, 30, 50);
            forTextureMC = rectangleSpriteSize;
            upDownFrameOfTextureMC = new Rectangle(105, 14, 30, 50);
            jampFrameOfTextureMC = new Rectangle(60, 77, 30, 50);
            leftFrameOfTextureMC = new Rectangle[] {
                 new Rectangle(10,207,30,50),
                new Rectangle(57,207,30,50),
                new Rectangle(105,207,30,50)
            };
            rightFrameOfTextureMC = new Rectangle[] {
                new Rectangle(10,77,30,50),
                new Rectangle(60,77,30,50),
                new Rectangle(108,77,30,50)
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
                    walkingDirection = Direction.Jamp;
                }
                
                if(IsCollideWithStairs()==true)
                {
                    MoveUp(speed);
                    while (IsCollideWithPlatform())
                    {
                        MoveDown(speed / 10);
                    }
                    walkingDirection = Direction.UpDown;
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
                walkingDirection = Direction.Left;
                IterLeftFrame++;
                if (IterLeftFrame == 3)
                    IterLeftFrame = 0;

            }
            if (state.IsKeyDown(Keys.D))
            {
                MoveRight(speed);
                while (IsCollideWithPlatform())
                {
                    MoveLeft(speed / 10);
                }
                IterRightFrame++;
                if (IterRightFrame == 3)
                    IterRightFrame = 0;
                walkingDirection = Direction.Right;
            }
        }
                                            
        void IsCollideWithAny()//добавить звук при столкновении с объектами 
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
                FindObj.PlaySoundEffect();
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
                FindObj.PlaySoundEffect();
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
                FindObj.PlaySoundEffect();
                color = Color.Red;
                if(walkingDirection == Direction.Right)
                {
                    Position.X -= 32;
                }
                if(walkingDirection==Direction.Left)
                {
                    Position.X += 32;
                }
                if(walkingDirection==Direction.UpDown)
                {
                    Position.Y += 32;
                }
                if (walkingDirection == Direction.Jamp)
                {
                    Position.Y += 32;
                }
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
                    this.Position.Y < FindObj.position.Y + FindObj.rectangleSpriteSize.Height && (FindObj.position.Y - this.Position.Y) < 53)
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
                        GC.Collect();

                    }

                }
            }
        }
        public bool flagLose { private set; get; } = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            color = Color.White;
            GotoDown();
            Move();
            CheckBounds();
            IskillEnemy();
            IsCollideWithAny();

            if (!flagLose&&!flagEventWin)
            {
                Game.Window.Title = $"Количество очков = {Scores} Количество жизней {Lives}";
            }
            
            if(Lives<0&&!flagLose)
            {
                flagLose = true;
                Game.Window.Title = "Вы проиграли";
                timer = new Timer((obj)=> { LoseGame?.Invoke($"Количество очков {Scores} "); }, 0,200, 0);
                this.Dispose();
            }
            if(enemy==0&&!flagEventWin)
            {
                effectWinLevel.Play();
                Game.Window.Title = "Вы прошли этот уровень";
                flagEventWin = true;
                timer = new Timer((obj) => { WonLevel?.Invoke($"Количество очков {Scores} "); }, 0, 2000, 0);
                
            }
            
        }
        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            switch (walkingDirection)
            {
                case Direction.UpDown:
                    spriteBatch.Draw(textureMainCharacter, Position, upDownFrameOfTextureMC, color);
                    break;
                case Direction.Jamp:
                    spriteBatch.Draw(textureMainCharacter, Position, jampFrameOfTextureMC, color);
                    break;
                case Direction.Left:
                    spriteBatch.Draw(textureMainCharacter, Position, leftFrameOfTextureMC[IterLeftFrame], color);

                    break;
                case Direction.Right:
                    spriteBatch.Draw(textureMainCharacter, Position, rightFrameOfTextureMC[IterRightFrame], color);
                    break;
            }

            base.Draw(gameTime);
        }
    }
    
}
