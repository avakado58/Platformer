using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainLevel : Game
    {
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
        protected int[,] level;
        public MainCharcter mainCharcter;
        public MainLevel(int [,] level)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.level = level; 
            rectangleSpriteSize = new Rectangle(0, 0, 64, 64);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureEnemy = Content.Load<Texture2D>("Enemy");
            textureCoin = Content.Load<Texture2D>("StarCoin");
            textureFon = Content.Load<Texture2D>("fon");
            texturePlatform = Content.Load<Texture2D>("platform");
            textureStairs = Content.Load<Texture2D>("stairs");
            textureHeart = Content.Load<Texture2D>("heart");
            textureMainCharacter=Content.Load<Texture2D>("main-character-walk-frame");
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            AddSprite();

        }
        void AddSprite()
        {
            int a = 0, b = 0, enemy = 0;
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    switch(level[i,j])
                    {
                        case 1:
                            Components.Add(new Platform(this, ref texturePlatform, new Vector2(j,i ), rectangleSpriteSize));
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
            Components.Add(mainCharcter);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("Fon"), new Vector2(0, 0), Color.White);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
