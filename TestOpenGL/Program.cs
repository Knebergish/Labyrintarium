using System;
using System.IO;
using System.Windows.Forms;

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
        public static MainForm mainForm;
        public static ObjectsBuilder OB;
        public static TexturesAssistant TA;
        public static DataBaseIO DBIO;
        public static Level L;
        public static Painter P;
        public static DecalsAssistant DA;
        public static Logger Log;
        public static GameCycle GCycle;
        
        public static Controls.Control C;
        public static FormsAssistant FA;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }

        /// <summary>
        /// Инициализация игровых классов.
        /// </summary>
        /// <param name="f">Ссылка на форму с компонентом вывода изображения.</param>
        public static void InitApp(MainForm f)
        {
            //exceptionAssistant = new ExceptionAssistant();
            mainForm = f;
            DBIO = new DataBaseIO(Directory.GetCurrentDirectory());
            TA = new TexturesAssistant(Directory.GetCurrentDirectory());
            OB = new ObjectsBuilder(DBIO, TA);
            L = new Level(30, 30, new int[5] { 4, 4, 1, 4, 1 });
            P = new Painter();
            DA = new DecalsAssistant();
            P.Camera = new Camera(10, 10);
            Log = new Logger(); Log.LoggerListBox = mainForm.logListBox; Log.QuestLabel = mainForm.questLabel;

            Triggers.currentTriggers = new Triggers();
            GCycle = new GameCycle();
            
            C = new Controls.Control();
            FA = new FormsAssistant();
        }
    }
}
