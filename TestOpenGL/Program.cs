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
            L = new Level(10, 10, 4);
            P = new Painter(new Camera(10, 10));
            Log = new Logger(f.listBox1);
            
            GCycle = new GameCycle();
            C = new Control();
            FA = new FormsAssistant();
        }
    }
}

// Доработан класс Battle до работоспособного состояния.
// Переработана система хождения сущностей (чтоб убило эту чёртову многопоточность)
// ПРоблема со сравнение разноразмерных координат. (Возможно разделить Coord на Coord2 и Coord3
//Я так понимаю, главная проблема тут во мне.
// Нужно больше глобальной стандартизации. +
//Чёртовы координаты.
//Чёртовы типы визуальных объектов.
//Семь бед - один ответ.

//Я охреневаю от себя.
//Понял, что всё гавно, переделал половину проги за день. 
//Говна стало меньше. Я аж сам охреневаю.
// В VisualObject пошаманить с свойством C (проверка на пустоту новых координат). +
//Гавно. Я переписал массу кода, получилось охрененно, всё неожиданно работает. Я испугался и боюсь прогать дальше.
//Переместить sight из GameCycle в Level
//Form3 наглядно показывает - ещё работать и работать над унификацией, убиранием switch

//Ох. Столько всего произошло...
// Добавлен индикатор включенности управления на Form1 (верхний левый угол)
//Да тут всё переделано... Но я постараюсь выделить самое основное.
// Открытие побочных форм теперь не вызывает остановку игрового цикла.
// Доработана Form3
// Улучшена система ходов сущностей в GameCycle.
// Самое главное - унифицированы карты визуальных объектов в MapVisualObject.
//Подумать над Camera.

// Добавлен индикатор фпс на Form1 и его активная регулировка в Painter.
// Объект класса Sight перенесён из GameCycle в Camera.
// TextureAssistant теперь независим от Painter.
// Painter теперь может запускать отрисовку, независимо от GameCycle.