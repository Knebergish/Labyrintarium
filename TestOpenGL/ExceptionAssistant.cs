using System;
using System.Windows.Forms;

namespace TestOpenGL
{
    class ExceptionAssistant
    {
        static public void NewException(Exception exception)
        {
            MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Application.Exit();
        }
    }
}
