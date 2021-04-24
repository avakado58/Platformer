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
            StartLevelOne();
            

        }
        static void StartLevelOne()
        {
            using (var levelOne = new MainLevel(new int[,] {
               { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } }))
            {
                levelOne.Run();
                if(levelOne.mainCharcter.flagLose==false)
                {
                    StartLevelTwo();
                }
            }
           
        }

        static void StartLevelTwo()
        {
            using (var levelTwo = new MainLevel(new int[,] {
               { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
               { 1, 5, 1, 3, 5, 1, 1, 2, 1, 5 },
               { 0, 0, 0, 1, 2, 1, 1, 2, 0, 4 },
               { 0, 1, 3, 0, 2, 0, 0, 2, 0, 3 },
               { 5, 0, 0, 0, 1, 1, 1, 2, 0, 1 },
               { 0, 0, 0, 1, 0, 0, 0, 2, 0, 1 },
               { 6, 0, 1, 3, 4, 0, 5, 2, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } }))
            {
                levelTwo.Run();

            }

        }
    }
#endif
}
