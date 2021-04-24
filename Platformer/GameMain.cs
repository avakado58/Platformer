using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Platformer
{
    class GameMain : Game
    {
        LevelOne levelOne;
        LevelTwo levelTwo;
        public GameMain()
        {
            levelOne = new LevelOne(new int[,] {
               { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } });

            levelTwo = new LevelTwo(new int[,] {
               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } });
        }
        protected override void Initialize()
        {
            levelOne.IInitialize();
        }
        protected override void LoadContent()
        {
            levelOne.ILoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            levelOne.IUpdate(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            levelOne.Idraw(gameTime);
        }
    }
}
