using System;
using System.IO;
using System.Windows.Forms;

using TestOpenGL.Logic;
using TestOpenGL.DataIO;
using TestOpenGL.Forms;
using TestOpenGL.OutInfo;
using TestOpenGL.Renders;
using TestOpenGL.World;

namespace TestOpenGL
{
    class GlobalData
    {
        static string path = "D:\\Материя\\Я великий программист\\#Лабиринтариум# разработка\\TestOpenGL\\TestOpenGL\\bin\\x86\\Debug";
        //string path = Directory.GetCurrentDirectory();

        static ILowLevelLibraryble lll;// = new OpenGLLibrary();
        static DataBaseIO dbio;// = new DataBaseIO(path);
        static TexturesAssistant ta;// = new TexturesAssistant(path);
        static ObjectsBuilder ob;// = new ObjectsBuilder(dbio, ta);
        static IRenderManager renderManager;// = new Painter();
        static Logger log;// = new Logger();
        static FormsAssistant fa;// = new FormsAssistant();
        static GameCycle gCycle;// = new GameCycle();

        static WorldData worldData;// = new WorldData();

        
        //-------------


        public static void Initialize()
        { 
            lll = new OpenGLLibrary();
            dbio = new DataBaseIO(path);
            ta = new TexturesAssistant(path);
            ob = new ObjectsBuilder(dbio, ta);
            renderManager = new Painter();
            log = new Logger(); //Log.LoggerListBox = mainForm.logListBox; Log.QuestLabel = mainForm.questLabel;
            fa = new FormsAssistant();
            gCycle = new GameCycle();

            WorldData = new WorldData();

            
        }

        public static WorldData WorldData
        {
            get { return worldData; }
            set
            {
                //gCycle.StopStep();
                //renderManager.StopRender();
                //worldData.Camera.ChangeSizeEvent -= () => { GlobalData.LLL.SettingVisibleAreaSize(GlobalData.WorldData.Camera.Width, GlobalData.WorldData.Camera.Height); };
                worldData = value;
                worldData.Camera.ChangeSizeEvent += () => 
                {
                    GlobalData.LLL.SettingVisibleAreaSize();
                };
                
                //gCycle.StartStep();
                //renderManager.StartRender();
                // worldData.RenderManager.StopRender();
                //Log.LoggerListBox = mainForm.logListBox; Log.QuestLabel = mainForm.questLabel;
                //Cam.ChangeSizeEvent += LLL.SettingVisibleAreaSize;
            }
        }
        public static ILowLevelLibraryble LLL
        { get { return lll; } }
        public static DataBaseIO DBIO
        { get { return dbio; } }
        public static TexturesAssistant TA
        { get { return ta; } }
        public static ObjectsBuilder OB
        { get { return ob; } }
        public static IRenderManager RenderManager
        { get { return renderManager; } }
        public static Logger Log
        { get { return log; } }
        public static FormsAssistant FA
        { get { return fa; } }
        public static GameCycle GCycle
        { get { return gCycle; } }
    }
}
