using System;

namespace Platformer
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        public static int Levels { get; set; }
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
            {
                game.Run();
            }
        }
        

    }
#endif
}
