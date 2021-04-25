using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer
{
    class GameMain : Game
    {
        enum GameState
        {
            Menu,
            LevelOne,
            LevelTwo,
            WinOfGame,
            LoseOFGame
        }

        GameState state;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D textureMainCharacter;
        Texture2D textureEnemy;
        Texture2D textureCoin;
        Texture2D textureFon;
        Texture2D texturePlatform;
        Texture2D textureStairs;
        Texture2D textureHeart;
        Rectangle rectangleSpriteSize;
        protected int[,] levelOne;
        protected int[,] levelTwo;
        public MainCharcter mainCharcter;
        bool flagLoadLevelOne;
        bool flagLoadLevelTwo;
        int countLevelWin;
        public GameMain()
        {
            state = GameState.LevelOne;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rectangleSpriteSize = new Rectangle(0, 0, 64, 64);
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 35);
            flagLoadLevelOne = false;
            flagLoadLevelTwo = false;
            countLevelWin = 0;
            levelOne = new int[,] {
               { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            levelTwo = new int[,] {
               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            
        }



        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureEnemy = Content.Load<Texture2D>("Enemy");
            textureCoin = Content.Load<Texture2D>("StarCoin");
            textureFon = Content.Load<Texture2D>("fon");
            texturePlatform = Content.Load<Texture2D>("platform");
            textureStairs = Content.Load<Texture2D>("stairs");
            textureHeart = Content.Load<Texture2D>("heart");
            textureMainCharacter = Content.Load<Texture2D>("main-character-walk-frame");
            Services.AddService(typeof(SpriteBatch), spriteBatch);
        }
        void AddSprite(int [,] level)
        {
            int a = 0, b = 0, enemy = 0;
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    switch (level[i, j])
                    {
                        case 1:
                            Components.Add(new Platform(this, ref texturePlatform, new Vector2(j, i), rectangleSpriteSize));
                            break;
                        case 2:
                            Components.Add(new Stairs(this, ref textureStairs, new Vector2(j, i), rectangleSpriteSize));
                            break;
                        case 3:
                            Components.Add(new Heart(this, ref textureHeart, new Vector2(j, i), rectangleSpriteSize));
                            break;
                        case 4:
                            Components.Add(new StarCoin(this, ref textureCoin, new Vector2(j, i), rectangleSpriteSize));
                            break;
                        case 5:
                            Components.Add(new Enemy(this, ref textureEnemy, new Vector2(j, i), rectangleSpriteSize));
                            enemy++;
                            break;
                        case 6:
                            a = j;
                            b = i;
                            break;

                    }

                }
            }
            mainCharcter = new MainCharcter(this, ref textureMainCharacter, new Vector2(a, b), enemy);
            mainCharcter.WonGame += MainCharcter_WonGame;
            mainCharcter.LoseGame += MainCharcter_LoseGame;
            Components.Add(mainCharcter);
        }
        private void MainCharcter_WonGame(string stateMC)
        {
            state = GameState.LevelTwo;
            countLevelWin++;
        }
        private void MainCharcter_LoseGame(string stateMC)
        {
            state = GameState.LoseOFGame;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (countLevelWin == 2)
                state = GameState.WinOfGame;
            switch(state)
            {
                case GameState.Menu:

                    break;
                case GameState.LevelOne:
                    if(!flagLoadLevelOne)
                    {
                        Components.Clear();
                        AddSprite(levelOne);
                        flagLoadLevelOne = true;
                    }
                    break;
                case GameState.LevelTwo:
                    if (!flagLoadLevelTwo)
                    {
                        Components.Clear();
                        AddSprite(levelTwo);
                        flagLoadLevelTwo = true;
                    }
                    break;
                case GameState.LoseOFGame:
                    Components.Clear();
                    flagLoadLevelOne = false;
                    flagLoadLevelTwo = false;

                    break;
                case GameState.WinOfGame:
                    Components.Clear();
                    flagLoadLevelOne = false;
                    flagLoadLevelTwo = false;
                    break;
            }
            base.Update(gameTime);
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:

                    break;
                case GameState.LevelOne:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
                    base.Draw(gameTime);
                    spriteBatch.End();

                    break;
                case GameState.LevelTwo:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
                    base.Draw(gameTime);
                    spriteBatch.End();
                    break;
                case GameState.LoseOFGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Lose"), new Vector2(0, 0), Color.White);
                    base.Draw(gameTime);
                    spriteBatch.End();
                    break;
                case GameState.WinOfGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Win"), new Vector2(0, 0), Color.White);
                    base.Draw(gameTime);
                    spriteBatch.End();
                    break;
            }
        }
    }
}
