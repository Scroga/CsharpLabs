using GLFW;
using OpenGLTutorial_01.GameLoop;
using OpenGLTutorial_01.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGLTutorial_01.OpenGL.GL;

namespace OpenGLTutorial_01
{
    class TestGame : Game
    {
        public TestGame(int initialWindowWidth, int initialWindowHeight, string? initialWindowTile) : base(initialWindowWidth, initialWindowHeight, initialWindowTile)
        {
        }

        protected override void Initalize()
        {

        }

        protected override void LoadContent()
        {

        }
        protected override void Update()
        {

        }

        protected override void Render()
        {
            glClearColor(MathF.Sin(GameTime.TotalElapsedSeconds), 0, 0, 1);
            glClear(GL_COLOR_BUFFER_BIT);

            Glfw.SwapBuffers(DisplayManager.Window);
        }
    }
}
