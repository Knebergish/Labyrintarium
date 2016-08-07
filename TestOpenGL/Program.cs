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
            mainForm = f;
            string path = "D:\\Материя\\Я великий программист\\#Лабиринтариум# разработка\\TestOpenGL\\TestOpenGL\\bin\\x86\\Debug";
            //string path = Directory.GetCurrentDirectory();
            DBIO = new DataBaseIO(path);
            TA = new TexturesAssistant(path);
            OB = new ObjectsBuilder(DBIO, TA);

            // Подгрузка уровня тут не нужна. Но при инициализации прицела при инициализации камеры для Painter Sight начинает свою отрисовку, а в FormAssistant в MapEditorForm в ContentControl идёт обращение опять же к координатам Sight.
            //TODO: Исправить.
            L = new Level(1, 1, new int[5] { 1, 1, 1, 1, 1 });

            P = new Painter();
            DA = new DecalsAssistant();
            P.Camera = new Camera(10, 10);
            Log = new Logger(); Log.LoggerListBox = mainForm.logListBox; Log.QuestLabel = mainForm.questLabel;

            Triggers.currentTriggers = new Triggers();
            GCycle = new GameCycle();
            
            C = new Controls.Control();
            FA = new FormsAssistant();

            Func<int, double, Testo> ftesto = (a, b) => new Testo(a, b);
            Func<double, Testo> ftp = ftesto.Partial(2);
        }
    }

    class Testo
    {
        public Testo(int a, double b)
        {

        }
    }
}
