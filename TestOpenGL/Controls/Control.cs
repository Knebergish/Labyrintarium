using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Threading;

namespace TestOpenGL.Controls
{
    class Control
    {
        public BoolEventDelegate ChangeEnabledControl;
        private bool isEnabledControl;

        DataTable actionsControlDataTable;

        Thread testThread;
        public ManualResetEvent mre;
        //-------------


        public Control()
        {
            actionsControlDataTable = new DataTable();
            actionsControlDataTable.Columns.Add("keyChar", typeof(char));
            actionsControlDataTable.Columns.Add("additionalKey", typeof(AdditionalKey));
            actionsControlDataTable.Columns.Add("action", typeof(Action));

            IsEnabledControl = false;

            mre = new ManualResetEvent(false);
            testThread = new Thread(Test);
            testThread.Name = "ControlThread";
            testThread.Start();
        }

        void Test()
        {
            while(true)
            {
                mre.WaitOne();
                lock (Program.mainForm.keyList)
                {
                    while (Program.mainForm.keyList.Count > 0)
                    {

                        ProcessingKeyPress(Program.mainForm.keyList[0]);
                        Program.mainForm.keyList.RemoveAt(0);

                    }
                }
                mre.Reset();
            }
        }

        public bool IsEnabledControl
        {
            get { return isEnabledControl; }
            set
            {
                isEnabledControl = value;

                ChangeEnabledControl?.Invoke(isEnabledControl);
            }
        }
        //=============


        public void AddNewActionControl(char c, AdditionalKey ak, Action ved)
        {
            for (int i = 0; i < actionsControlDataTable.Rows.Count; i++)
                if ((char)actionsControlDataTable.Rows[i][0] == char.ToLower(c))
                    if ((AdditionalKey)actionsControlDataTable.Rows[i][1] == ak)
                    {
                        actionsControlDataTable.Rows[i][2] = ved;
                        return;
                    }

            actionsControlDataTable.Rows.Add(c, ak, ved);
        }

        public void ClearAllActionsControl()
        {
            actionsControlDataTable.Rows.Clear();
        }

        public void ProcessingKeyPress(KeyEventArgs kea)
        {
            if (!IsEnabledControl) 
                return;
            
            IsEnabledControl = false;

            AdditionalKey ak = kea.Shift ? AdditionalKey.Shift :
                kea.Control ? AdditionalKey.Ctrl :
                kea.Alt ? AdditionalKey.Alt : 
                AdditionalKey.None;
            Action uv;
            char key = char.ToLower((char)kea.KeyCode);

            for (int i = 0; i < actionsControlDataTable.Rows.Count; i++)
                if ((char)actionsControlDataTable.Rows[i][0] == char.ToLower((char)kea.KeyCode))
                    if ((AdditionalKey)actionsControlDataTable.Rows[i][1] == ak)
                    {
                        uv = (Action)actionsControlDataTable.Rows[i][2];
                        Program.MainThreadInvoke(uv);
                        break;
                    }

            IsEnabledControl = true;
        }
    }
}
