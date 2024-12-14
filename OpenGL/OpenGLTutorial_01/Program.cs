using System;
using GLFW;
using OpenGLTutorial_01.GameLoop;
using static OpenGLTutorial_01.OpenGL.GL;

namespace OpenGLTutorial_01;

class Program 
{
    static void Main(string[] args) 
    {
        Game game = new TestGame(800, 600, "Test game!");
        game.Run();
    }
}