using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    class Logger
    {
        //TODO: изменить на получение (функтора?).
        System.Windows.Forms.ListBox loggerListBox;
        //-------------


        public Logger(System.Windows.Forms.ListBox loggerListBox)
        {
            this.loggerListBox = loggerListBox;
        }
        //=============


        public void Log(string message)
        {
            loggerListBox.Invoke(new Func<int>(() => 
            { 
                loggerListBox.Items.Add(message);
                loggerListBox.SelectedIndex = loggerListBox.Items.Count - 1;
                return 0;
            }));
        }
    }
}
