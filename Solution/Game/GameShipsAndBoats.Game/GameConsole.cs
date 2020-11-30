﻿using System;

namespace PT_Console_App_ShipsAndBoatsGame
{
    public static class GameConsole
    {        
        public static void SetConsoleProperties()
        {
            Console.Title = "Ships&BoatsGame";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetWindowSize(33, 50);
            Console.SetWindowPosition(0, 0);
            Console.SetBufferSize(33, 50);

            Console.CursorVisible = false;            
        }
    }
}