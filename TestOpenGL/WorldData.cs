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


        public WorldData(
            Level level, 
            RendereableObjectsContainer rendereableObjectsContainer, 
            DecalsAssistant decalsAssistant,
            Controls.Control control,
            Camera camera)
        {
            this.level = level ?? new Level(1, 1, new int[5] { 1, 1, 1, 1, 1 });
            this.rendereableObjectsContainer = rendereableObjectsContainer ?? new RendereableObjectsContainer();
            this.decalsAssistant = decalsAssistant ?? new DecalsAssistant();
            this.control = control ?? new Controls.Control();
            this.camera = camera ?? new Camera(1, 1);
        }

        public Level Level
        {
            get { return level; }
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
