using System;
using System.Collections.Generic;
using System.Windows.Forms;

using TestOpenGL.OutInfo;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.Forms.Controls
{
    partial class ContentControl : UserControl
    {
        Sight sight;

        List<ObjectInfo> listObjectsInfo;

        public ContentControl()
        {
            listObjectsInfo = new List<ObjectInfo>();

            InitializeComponent();
        }

        public void SetSight(Sight sight)
        {
            if (this.sight != null)
                this.sight.EventSight.EventSightChangeCoord -= ReloadContents;

            this.sight = sight;
            if (this.sight != null)
                sight.EventSight.EventSightChangeCoord += ReloadContents;
            //TODO: УПР. Исправить.
            ReloadContents();
        }

        public void ReloadContents()
        {
            contentList.SelectedIndex = -1;
            listObjectsInfo.Clear();
            contentList.Items.Clear();

            if (sight == null)
                return;

            foreach (PhisicalObject go in Program.L.GetMap<Background>().GetCellObject(sight.Coord))
                listObjectsInfo.Add(go.ObjectInfo);
            foreach (PhisicalObject go in Program.L.GetMap<Block>().GetCellObject(sight.Coord))
                listObjectsInfo.Add(go.ObjectInfo);
            foreach (PhisicalObject go in Program.L.GetMap<Being>().GetCellObject(sight.Coord))
                listObjectsInfo.Add(go.ObjectInfo);

            foreach (ObjectInfo oi in listObjectsInfo)
                contentList.Items.Add(oi.Name);
        }

        private void ContentControl_Load(object sender, EventArgs e)
        {
            ContentControl_Resize(sender, e);
        }

        private void ContentControl_Resize(object sender, EventArgs e)
        {
            infoControl.Top = 5;
            infoControl.Left = contentList.Width + 10;
            infoControl.Width = Width - contentList.Width - 10;
            infoControl.Height = Height - 10;
            
            contentList.Height = Height - 25;
        }

        private void contentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            infoControl.SetInfo(
                contentList.SelectedIndex == -1
                ? null
                : listObjectsInfo[contentList.SelectedIndex]
                );
        }
    }
}
