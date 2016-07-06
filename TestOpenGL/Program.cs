using System;
using System.IO;
using System.Windows.Forms;

using TestOpenGL.DataIO;
using TestOpenGL.Logic;

namespace TestOpenGL
{
    static class Program
    {
        public static Form1 mainForm;
        public static ObjectsBuilder OB;
        public static TexturesAssistant TA;
        public static DataBaseIO DBIO;
        public static Level L;
        public static Painter P;
        public static Logger Log;
        public static GameCycle GCycle;
        public static Control C;
        public static FormsAssistant FA;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
        

        /// <summary>
        /// Инициализация игровых классов.
        /// </summary>
        /// <param name="f">Ссылка на форму с компонентом вывода изображения.</param>
        public static void InitApp(Form1 f)
        {
            mainForm = f;
            DBIO = new DataBaseIO(Directory.GetCurrentDirectory());
            TA = new TexturesAssistant(Directory.GetCurrentDirectory());
            OB = new ObjectsBuilder(DBIO, TA);
            L = new Level(30, 30, 4);
            P = new Painter(new Camera(10, 10));
            Log = new Logger(f.logListBox);
            
            GCycle = new GameCycle();
            C = new Control();
            FA = new FormsAssistant();
        }
    }
}

// Отменены изменения в MapVisualObject: IEnumerable оказался неудобным для использования, а выигрыш в производительности был ничтожен.
// Внесены изменения в систему текстурирования(TextureAssistant): имя файла теперь задаётся не номером, а строкой.
// Добавлены классы: UsedBlock (абстрактный класс блоков, поддерживающий использование), ActionBlock (наследник UsedBlock, класс блоков с заданным действием использования), Dorr (наследник UsedBlock, класс двери).
// В класс Being добавлено действие Use - использование используемых блоков под прицелом.
// Добавлен шейдер отрисовки надетых предметов.
// Изменён метод Render для исправления $%`~&?#@ ошибки.