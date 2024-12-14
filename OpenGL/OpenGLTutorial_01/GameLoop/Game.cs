using GLFW;
using OpenGLTutorial_01.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTutorial_01.GameLoop
{
    abstract class Game
    {
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string? InitialWindowTile { get; set; }

        public Game(int initialWindowWidth, int initialWindowHeight, string? initialWindowTile)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTile = initialWindowTile;
        }

        public void Run() 
        {
            Initalize();
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTile!);

            LoadContent();

            while (!Glfw.WindowShouldClose(DisplayManager.Window)) 
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSeconds;
                GameTime.TotalElapsedSeconds = (float)Glfw.Time; 

                Update();
                Glfw.PollEvents();
                Render();
            }
            DisplayManager.CloseWindow();
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();
        protected abstract void Update();
        protected abstract void Render();
    }
}