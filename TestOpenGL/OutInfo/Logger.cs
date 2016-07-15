using System;
using System.Windows.Forms;

namespace TestOpenGL.OutInfo
{
    class Logger
    {
        //TODO: изменить на получение (функтора?).
        ListBox loggerListBox;
        Label questLabel;
        //-------------


        public Logger()
        { }

        public ListBox LoggerListBox
        { set { loggerListBox = value; } }

        public Label QuestLabel
        { set { questLabel = value; } }
        //=============


        public void Log(string message)
        {
            loggerListBox?.Invoke(new Action(() => 
            { 
                loggerListBox.Items.Add(message);
                loggerListBox.SelectedIndex = loggerListBox.Items.Count - 1;
            }));
        }

        public void SetCurrentQuest(string questText)
        {
            questLabel?.Invoke(new Action(() =>
            {
                questLabel.Text = questText;
            }));
        }
    }
}
