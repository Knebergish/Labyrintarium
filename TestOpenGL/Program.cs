using System;
using System.IO;
using System.Windows.Forms;

using TestOpenGL.DataIO;
using TestOpenGL.Forms;
using TestOpenGL.OutInfo;
using TestOpenGL.Renders;
using TestOpenGL.World;
using TestOpenGL.Logic;

namespace TestOpenGL
{
    static class Program
    {
        public static MainForm mainForm;
        public static ObjectsBuilder OB;
        public static TexturesAssistant TA;
        public static DataBaseIO DBIO;
        public static Level L;
        public static Painter P;
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

        static public double GetGlobalDepth(Layer layer, int partLayer, ModifyDepth modifyDepth)
        {
            int layerDepth = Program.L.LengthZ;
            double resultDepth = (int)layer * layerDepth;
            double delta = 0.5;

            switch (modifyDepth)
            {
                case ModifyDepth.None:
                    resultDepth += partLayer;
                    break;

                case ModifyDepth.UnderLayer:
                    resultDepth -= delta;
                    break;

                case ModifyDepth.ToLayer:
                    resultDepth += layerDepth - delta;
                    break;

                case ModifyDepth.UnderPartLayer:
                    resultDepth += partLayer - delta;
                    break;

                case ModifyDepth.ToPartLayer:
                    resultDepth += partLayer + delta;
                    break;
            }

            return resultDepth;
        }

        /// <summary>
        /// Инициализация игровых классов.
        /// </summary>
        /// <param name="f">Ссылка на форму с компонентом вывода изображения.</param>
        public static void InitApp(MainForm f)
        {
            mainForm = f;
            DBIO = new DataBaseIO(Directory.GetCurrentDirectory());
            TA = new TexturesAssistant(Directory.GetCurrentDirectory());
            OB = new ObjectsBuilder(DBIO, TA);
            L = new Level(30, 30, 4);
            P = new Painter(new Camera(10, 10));
            Log = new Logger(); Log.LoggerListBox = mainForm.logListBox; Log.QuestLabel = mainForm.questLabel;

            Triggers.currentTriggers = new Triggers();
            GCycle = new GameCycle();
            C = new Controls.Control();
            FA = new FormsAssistant();
        }
    }
}
