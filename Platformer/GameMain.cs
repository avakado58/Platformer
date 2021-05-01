using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Diagnostics;

namespace Ghostbusters
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
        Texture2D textureButton;
        SpriteFont spriteFont;
        Rectangle rectangleSpriteSize;
        Button butStart;
        Button butExit;
        Button butBackToMenu;
        Song songMenu;
        Song songLevelOne;
        Song songLevelTwo;
        SoundEffect effectLose;
        SoundEffect effectStartlLevel;
        SoundEffect effectWinGame;
        Hood hood;
        TimeSpan timeLevelOne;
        TimeSpan timeLevelTwo;
        protected int[,] levelOne;
        protected int[,] levelTwo;
        public MainCharcter mainCharcter;
        bool flagLoadLevelOne;
        bool flagLoadLevelTwo;
        bool flagLoadMenu;
        bool flagWinGame;
        bool flagLoseGame;
        int countLevelWin;
        Stopwatch stopWatch;

        public GameMain()
        {
            state = GameState.Menu;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rectangleSpriteSize = new Rectangle(0, 0, 64, 64);
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 35);
            flagLoadLevelOne = false;
            flagLoadLevelTwo = false;
            flagLoadMenu = false;
            flagWinGame = false;
            flagLoseGame = false;
            countLevelWin = 0;
            levelOne = new int[,] {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
               { 0, 0, 1, 3, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 },
               { 4, 0, 0, 1, 2, 1, 1, 2, 0, 4, 0, 0, 0, 5, 1, 2, 0, 0, 0, 4 },
               { 1, 5, 3, 0, 2, 0, 0, 2, 0, 3, 0, 0, 0, 1, 1, 1, 2, 1, 1, 0 },
               { 0, 1, 0, 0, 1, 1, 1, 2, 0, 1, 2, 0, 0, 0, 0, 0, 2, 0, 0, 5 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1, 2, 0, 0, 0, 0, 0, 2, 0, 0, 1 },
               { 6, 0, 1, 3, 4, 5, 0, 2, 0, 1, 2, 0, 5, 1, 3, 0, 2, 0, 5, 3 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            levelTwo = new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 4, 3 },
               { 4, 5, 0, 0, 1, 0, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 2, 1, 1 },
               { 1, 1, 1, 1, 1, 1, 0, 5, 0, 2, 3, 0, 1, 1, 1, 2, 1, 2, 0, 0 },
               { 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 1, 1, 1, 1, 0, 2, 0, 2, 0, 0 },
               { 0, 0, 0, 4, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 1, 1, 1, 0 },
               { 0, 0, 3, 1, 0, 0, 0, 0, 0, 2, 0, 3, 0, 0, 0, 2, 0, 0, 0, 4 },
               { 6, 0, 1, 1, 3, 5, 0, 0, 0, 2, 0, 0, 5, 0, 1, 1, 0, 5, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            stopWatch = new Stopwatch();
            
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
            spriteFont = Content.Load<SpriteFont>("MenuFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureEnemy = Content.Load<Texture2D>("Enemy");
            textureCoin = Content.Load<Texture2D>("StarCoin");
            textureFon = Content.Load<Texture2D>("fon");
            texturePlatform = Content.Load<Texture2D>("platform");
            textureStairs = Content.Load<Texture2D>("stairs");
            textureHeart = Content.Load<Texture2D>("heart");
            textureMainCharacter = Content.Load<Texture2D>("main-character-walk-frame");
            textureButton = Content.Load<Texture2D>("button");
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            songMenu = Content.Load<Song>("songMenu");
            effectLose = Content.Load<SoundEffect>("LoseE");
            effectStartlLevel = Content.Load<SoundEffect>("LevelStart");
            songLevelOne = Content.Load<Song>("fonMusicLevelOne");
            songLevelTwo = Content.Load<Song>("fonMusicLevelTwo");
            effectWinGame = Content.Load<SoundEffect>("effectWinGame");
            hood = new Hood(this, Content.Load<SpriteFont>("fontHood"), new Vector2(0,0));
            hood.Lives = 2;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5F;
            
            butBackToMenu = new Button(this, textureButton, spriteFont, new Vector2(225, 400), "В МЕНЮ");
            butBackToMenu.Clik += ButBackToMenu_Clik;
        }
        void LoadLevel(int [,] level)
        {
            int a = 0, b = 0, enemy = 0;
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    switch (level[i, j])
                    {
                        case 1:
                            Components.Add(new Platform(this, ref texturePlatform, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("Star")));
                            break;
                        case 2:
                            Components.Add(new Stairs(this, ref textureStairs, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("Star")));
                            break;
                        case 3:
                            Components.Add(new Heart(this, ref textureHeart, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("HeartE")));
                            break;
                        case 4:
                            Components.Add(new StarCoin(this, ref textureCoin, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("Star")));
                            break;
                        case 5:
                            if(state == GameState.LevelOne)
                            {
                                Components.Add(new Enemy(this, ref textureEnemy, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("Damage")));
                                enemy++;
                            }
                            if (state == GameState.LevelTwo)
                            {
                                Components.Add(new EnemyTwo(this, ref textureEnemy, new Vector2(j, i), rectangleSpriteSize, Content.Load<SoundEffect>("Damage")));
                                enemy++;
                            }

                            break;
                        case 6:
                            a = j;
                            b = i;
                            break;

                    }

                }
            }
            mainCharcter = new MainCharcter(this, ref textureMainCharacter, new Vector2(a, b), enemy);
            mainCharcter.Lives = hood.Lives;
            mainCharcter.Scores = hood.Score;
            mainCharcter.WonLevel += MainCharcter_WonLevel;
            mainCharcter.LoseGame += MainCharcter_LoseGame;
            Components.Add(mainCharcter);
            Components.Add(hood);
        }



        // Format and display the TimeSpan value.
        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //    ts.Hours, ts.Minutes, ts.Seconds,
        //    ts.Milliseconds / 10);
        private void MainCharcter_WonLevel(string stateMC)
        {
            
            
            MediaPlayer.Play(songLevelTwo);
            if(!flagLoadLevelTwo)
            {
                timeLevelOne = hood.Time;
            }
            
            stopWatch.Restart();
            state = GameState.LevelTwo;
            countLevelWin++;
        }
        private void MainCharcter_LoseGame(string stateMC)
        {
            state = GameState.LoseOFGame;
            effectLose.Play();
        }
        private void ButExit_Clik()
        {
            Exit();
        }
        private void ButStart_Clik()
        {
            state = GameState.LevelOne;
            MediaPlayer.Stop();
            //effectStartlLevel.Play();
            MediaPlayer.Play(songLevelOne);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (countLevelWin == 2)
            {
                state = GameState.WinOfGame;
                
            }
                
            switch(state)
            {
                case GameState.Menu:
                    
                    this.IsMouseVisible = true;
                    flagWinGame = false;
                    flagLoseGame = false;
                    if (!flagLoadMenu)
                    {
                        MediaPlayer.Play(songMenu);
                        Components.Clear();
                        butStart = new Button(this, textureButton, spriteFont, new Vector2(225, 100), "  СТАРТ");
                        butExit = new Button(this, textureButton, spriteFont, new Vector2(225, 200), " ВЫХОД");
                        butStart.Clik += ButStart_Clik;
                        butExit.Clik += ButExit_Clik;
                        Components.Add(butStart);
                        Components.Add(butExit);
                        flagLoadMenu = true;
                        hood.Score = 0;
                        hood.Lives = 2;
                    }
                    break;
                case GameState.LevelOne:


                    if (!flagLoadLevelOne)
                    {
                        this.IsMouseVisible = false;
                        graphics.PreferredBackBufferWidth = 1280;
                        graphics.PreferredBackBufferHeight = 576;
                        graphics.ApplyChanges();
                        Components.Clear();
                        LoadLevel(levelOne);
                        flagLoadMenu = false;
                        flagLoadLevelOne = true;
                        stopWatch.Restart();
                    }
                    hood.Score = mainCharcter.Scores;
                    hood.Lives = mainCharcter.Lives;

                    hood.Time = stopWatch.Elapsed;
                    break;
                case GameState.LevelTwo:

                    if (!flagLoadLevelTwo)
                    {
                        graphics.PreferredBackBufferWidth = 1280;
                        graphics.PreferredBackBufferHeight = 576;
                        graphics.ApplyChanges();
                        this.IsMouseVisible = false;
                        Components.Clear();
                        LoadLevel(levelTwo);
                        flagLoadLevelTwo = true;
                        
                    }
                    hood.Score = mainCharcter.Scores;
                    hood.Lives = mainCharcter.Lives;
                    hood.Time = stopWatch.Elapsed;
                    break;
                case GameState.LoseOFGame:
                    if(!flagLoseGame)
                    {

                        MediaPlayer.Stop();
                        graphics.PreferredBackBufferWidth = 640;
                        graphics.PreferredBackBufferHeight = 512;
                        graphics.ApplyChanges();
                        Components.Clear();
                        Components.Add(butBackToMenu);
                        IsMouseVisible = true;
                        flagLoadLevelOne = false;
                        flagLoadLevelTwo = false;
                        flagLoseGame = true;
                        
                    }


                    break;
                case GameState.WinOfGame:
                    if(!flagWinGame)
                    {
                       
                        MediaPlayer.Stop();
                        graphics.PreferredBackBufferWidth = 640;
                        graphics.PreferredBackBufferHeight = 512;
                        graphics.ApplyChanges();
                        Components.Clear();
                        Components.Add(butBackToMenu);
                        IsMouseVisible = true;
                        effectWinGame.Play();
                        flagLoadLevelOne = false;
                        flagLoadLevelTwo = false;
                        flagWinGame = true;
                        
                        stopWatch.Stop();
                        
                    }
                    timeLevelTwo = hood.Time;
                    break;
            }
            base.Update(gameTime);
        }

        private void ButBackToMenu_Clik()
        {
            state = GameState.Menu;
            flagLoadMenu = false;
            countLevelWin = 0;
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
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
                    
                    base.Draw(gameTime);
                    spriteBatch.End();
                    break;
                case GameState.LevelOne:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();

                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(640, 0), Color.White);
                    base.Draw(gameTime);
                    spriteBatch.End();

                    break;
                case GameState.LevelTwo:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.Begin();

                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(640, 0), Color.White);
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
                    spriteBatch.DrawString(Content.Load<SpriteFont>("fontHood"),$"Время прохождения первого уровня {timeLevelOne}", new Vector2(50, 200), Color.Black);
                    spriteBatch.DrawString(Content.Load<SpriteFont>("fontHood"), $"Время прохождения второго уровня {timeLevelTwo}", new Vector2(50, 300), Color.Black);
                    base.Draw(gameTime);
                    spriteBatch.End();
                    break;
            }
        }
    }
}
