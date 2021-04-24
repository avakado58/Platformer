using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Platformer
{
    class LevelOne:MainLevel
    {
       public LevelOne (int[,] level):base(level)
        {
            
        }
        public void IInitialize()
        {
            this.Initialize();
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        public void IUpdate(GameTime gameTime)
        {
            Update(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void ILoadContent()
        {
            LoadContent();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Idraw(GameTime gameTime)
        {
            Draw(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
