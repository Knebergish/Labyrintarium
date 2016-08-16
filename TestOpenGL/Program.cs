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
    
    static class Program
    {
        //public static ExceptionAssistant exceptionAssistant;
        /*public static ILowLevelLibraryble LLL;
        
        public static ObjectsBuilder OB;
        public static TexturesAssistant TA;
        public static DataBaseIO DBIO;
        public static Level L;
        public static IRenderManager P;
        public static Camera Cam;
        public static DecalsAssistant DA;
        public static Logger Log;
        public static GameCycle GCycle;
        
        public static Controls.Control C;
        public static FormsAssistant FA;*/

        //public static GlobalData GlobalData;
        public static LoadForm loadForm;
        public static MainForm mainForm;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoadForm());
        }
        
        /// <summary>
        /// Инициализация игровых классов.
        /// </summary>
        /// <param name="f">Ссылка на форму с компонентом вывода изображения.</param>
        public static void InitApp(LoadForm f)
        {
            System.Threading.Thread.CurrentThread.Name = "MainThread";

            loadForm = f;
            mainForm = new MainForm();
            GlobalData.Initialize();

            Triggers.currentTriggers = new Triggers();

            
        }

        public static object MainThreadInvoke(Delegate method)
        {
            return loadForm.Invoke(method);
        }
    }
}
