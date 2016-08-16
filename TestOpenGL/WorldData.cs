using System;
using System.IO;
using System.Windows.Forms;

using TestOpenGL.Logic;
using TestOpenGL.DataIO;
using TestOpenGL.Forms;
using TestOpenGL.OutInfo;
using TestOpenGL.Renders;
using TestOpenGL.World;
using TestOpenGL.PhisicalObjects.ChieldsBeing;


namespace TestOpenGL
{
    class WorldData
    {
        Level level;
        RendereableObjectsContainer rendereableObjectsContainer;
        DecalsAssistant decalsAssistant;
        Controls.Control control;

        Camera camera;

        Gamer gamer;
        //-------------


        public WorldData()
        {
            level = new Level(30, 30, new int[5] { 4, 4, 1, 3, 1 });
            rendereableObjectsContainer = new RendereableObjectsContainer();
            decalsAssistant = new DecalsAssistant();
            control = new Controls.Control();
            camera = new Camera(10, 10);

        }

        public Level Level
        {
            get { return level; }
            //set { level = value; }
        }
        public DecalsAssistant DecalsAssistant
        { get { return decalsAssistant; } }
        public Controls.Control Control
        { get { return control; } }
        public Camera Camera
        { get { return camera; } }
        public Gamer Gamer
        { get { return gamer; } }
        public RendereableObjectsContainer RendereableObjectsContainer
        { get { return rendereableObjectsContainer; } }

    }
}
