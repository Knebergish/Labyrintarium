using System.IO;

using TestOpenGL.DataIO;
using TestOpenGL.Forms;
using TestOpenGL.OutInfo;
using TestOpenGL.Renders;
using TestOpenGL.World;

namespace TestOpenGL
{
    class GlobalData
    {
#if DEBUG
        static string path = "D:\\Материя\\Я великий программист\\#Лабиринтариум# разработка\\TestOpenGL\\TestOpenGL\\bin\\x86\\Release";
#else
        static string path = Directory.GetCurrentDirectory();
#endif

        static ILowLevelLibraryble lll;
        static DataBaseIO dbio;
        static TexturesAssistant ta;
        static ObjectsBuilder ob;
        static IRenderManager renderManager;
        static Logger log;
        static FormsAssistant fa;
        static GameCycle gCycle;
        static Sight sight;

        static WorldData worldData;

        static event ADelegate<WorldData> changeWorldDataEvent;
        //-------------


        public static void Initialize()
        { 
            lll = new OpenGLLibrary();
            dbio = new DataBaseIO(path);
            ta = new TexturesAssistant(path);
            ob = new ObjectsBuilder(dbio, ta);
            renderManager = new Painter();
            log = new Logger();
            fa = new FormsAssistant();
            gCycle = new GameCycle();
            //sight = new Sight();
            

            //WorldData = new WorldData();
        }

        public static string Path
        { get { return path; } }

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
        public static Sight Sight
        { get { return sight; } }

        public static WorldData WorldData
        {
            get { return worldData; }
            set
            {
                if (worldData != null)
                {
                    worldData.Camera.ChangeSizeEvent -= () => { LLL.SettingVisibleAreaSize(); };
                    worldData.Camera.ChangeSizeEvent -= sight.Check;
                    worldData.Camera.ChangePositionEvent -= sight.Check;
                }
                changeWorldDataEvent -= (WorldData worldData) => sight.Check();

                worldData = value;

                if (worldData != null)
                {
                    sight = new Sight();
                    worldData.Camera.ChangeSizeEvent += () => { LLL.SettingVisibleAreaSize(); };
                    worldData.Camera.ChangeSizeEvent += sight.Check;
                    worldData.Camera.ChangePositionEvent += sight.Check;
                    changeWorldDataEvent += (WorldData worldData) => sight.Check();
                    sight.Spawn(0, worldData?.Gamer?.Coord ?? new Coord(0, 0));
                }
                
                changeWorldDataEvent?.Invoke(worldData);
            }
        }

        public static event ADelegate<WorldData> ChangeWorldDataEvent
        {
            add { changeWorldDataEvent += value; }
            remove { changeWorldDataEvent -= value; }
        }
    }
}
