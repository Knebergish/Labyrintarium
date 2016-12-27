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

	    private static ILowLevelLibraryble lll;
	    private static DataBaseIO dbio;
	    private static TexturesAssistant ta;
	    private static ObjectsBuilder ob;
	    private static IRenderManager renderManager;
	    private static Logger log;
	    private static FormsAssistant fa;
	    private static GameCycle gCycle;
	    private static Sight sight;

	    private static WorldData worldData;

	    private static event ADelegate<WorldData> changeWorldDataEvent;
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

        public static string Path => path;

	    public static ILowLevelLibraryble LLL => lll;

	    public static DataBaseIO DBIO => dbio;

	    public static TexturesAssistant TA => ta;

	    public static ObjectsBuilder OB => ob;

	    public static IRenderManager RenderManager => renderManager;

	    public static Logger Log => log;

	    public static FormsAssistant FA => fa;

	    public static GameCycle GCycle => gCycle;

	    public static Sight Sight => sight;

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
