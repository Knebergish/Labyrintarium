using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TestOpenGL.Controls
{
    class Control
    {
        public BoolEventDelegate ChangeEnabledControl;
        private bool isEnabledControl;

        DataTable actionsControlDataTable;
        //-------------


        public Control()
        {
            actionsControlDataTable = new DataTable();
            actionsControlDataTable.Columns.Add("keyChar", typeof(char));
            actionsControlDataTable.Columns.Add("additionalKey", typeof(AdditionalKey));
            actionsControlDataTable.Columns.Add("action", typeof(Action));

            IsEnabledControl = false;

            //Func<List<RenderObject>> fu = new Func<List<RenderObject>>(() => { return new List<RenderObject>(); });
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
                        uv();
                        break;
                    }

            IsEnabledControl = true;
        }
    }
}
