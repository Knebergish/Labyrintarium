using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

using TestOpenGL.Forms;
using TestOpenGL.PhisicalObjects;
using TestOpenGL.PhisicalObjects.ChieldsBeing;

namespace TestOpenGL.Forms
{
    partial class MapEditorForm : Form
    {
        Layer currentLayer;
        List<int> idList;


        public MapEditorForm()
        {
            InitializeComponent();
            idList = new List<int>();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            UpdateForm();



        }

        public void UpdateForm()
        {
            contentControl1.SetSight(GlobalData.Sight);
            UpdateLayerComboBox();
        }

        void UpdateLayerComboBox()
        {
            layerComboBox.Items.Clear();
            //foreach (string s in Enum.GetNames(typeof(Layer)))
            for (int i = 0; i < 3; i++)
                layerComboBox.Items.Add(Enum.GetName(typeof(Layer), (Layer)i));
            layerComboBox.SelectedIndex = 0;
        }
        void UpdateDepthComboBox()
        {
            depthComboBox.Items.Clear();
            for (int i = 0; i < GlobalData.WorldData.Level.GetDepthLayer(currentLayer); i++)
                depthComboBox.Items.Add(i.ToString());
            depthComboBox.SelectedIndex = 0;
        }
        void UpdateDataListView()
        {
            idList.Clear();
            dataListView.Items.Clear();
            DataTable dataTable = GlobalData.DBIO.ExecuteSQL($"SELECT * FROM {/*Enum.GetName(typeof(Layer), (Layer)layerComboBox.SelectedIndex)*/currentLayer.ToString()}s");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (int.Parse(dataTable.Rows[i]["idGraphicObject"].ToString()) == 0)
                    continue;

                idList.Add(int.Parse(dataTable.Rows[i]["id"].ToString()));
                dataListView.Items.Add(dataTable.Rows[i]["name"].ToString());
            }
        }

        
        private void UpdateContentListBox()
        {
            switch ((Layer)layerComboBox.SelectedIndex)
            {
                case Layer.Background:
                    FillContentListBox<Background>();
                    break;

                case Layer.Block:
                    FillContentListBox<Block>();
                    break;

                case Layer.Being:
                    FillContentListBox<Being>();
                    break;
            }
        }
        private void FillContentListBox<T>() where T : PhisicalObject, IInfoble
        {
            contentListBox.Items.Clear();
            foreach (T b in GlobalData.WorldData.Level.GetMap<T>().GetCellObject(new Coord(
                GlobalData.Sight.Coord.X,
                GlobalData.Sight.Coord.Y
                )))
                contentListBox.Items.Add(b.PartLayer + ". " + b.ObjectInfo.Name);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(dataListView.SelectedIndices.Count > 0)
                switch ((Layer)layerComboBox.SelectedIndex)
                {
                    case Layer.Background:
                        GlobalData.OB.GetBackground(idList[dataListView.SelectedIndices[0]]).Spawn
                            (
                                depthComboBox.SelectedIndex,
                                new Coord
                                (
                                GlobalData.Sight.Coord.X,
                                GlobalData.Sight.Coord.Y
                                )
                            );
                        break;

                    case Layer.Block:
                        GlobalData.OB.GetBlock(idList[dataListView.SelectedIndices[0]]).Spawn
                            (
                                depthComboBox.SelectedIndex,
                                new Coord
                                (
                                GlobalData.Sight.Coord.X,
                                GlobalData.Sight.Coord.Y
                                )
                            );
                        break;

                    case Layer.Being:
                        new Bot(GlobalData.OB.GetBeing(idList[dataListView.SelectedIndices[0]]), null).Spawn
                            (
                                0,
                                new Coord
                                (
                                GlobalData.Sight.Coord.X,
                                GlobalData.Sight.Coord.Y
                                )
                            );
                        break;

                    /*case Layer.Decal:
                        GlobalData.OB.GetDecal(massId[dataListView.SelectedIndices[0]]).Spawn
                            (
                                depthComboBox.SelectedIndex,
                                new Coord
                                (
                                GlobalData.Sight.Coord.X,
                                GlobalData.Sight.Coord.Y
                                )
                            );
                        break;*/
                }

            UpdateContentListBox();
            contentControl1.ReloadContents();
        }
        private void removeButton_Click(object sender, EventArgs e)
        {
            switch ((Layer)layerComboBox.SelectedIndex)
            {
                case Layer.Background:
                    GlobalData.WorldData.Level.GetMap<Background>().GetObject
                        (
                            depthComboBox.SelectedIndex,
                            new Coord
                            (
                            GlobalData.Sight.Coord.X,
                            GlobalData.Sight.Coord.Y
                            )
                        )?.Despawn();
                    break;

                case Layer.Block:
                    GlobalData.WorldData.Level.GetMap<Block>().GetObject
                        (
                            depthComboBox.SelectedIndex,
                            new Coord
                            (
                            GlobalData.Sight.Coord.X,
                            GlobalData.Sight.Coord.Y
                            )
                        )?.Despawn();
                    break;

                case Layer.Being:
                    GlobalData.WorldData.Level.GetMap<Being>().GetObject
                        (
                            0,
                            new Coord
                            (
                            GlobalData.Sight.Coord.X,
                            GlobalData.Sight.Coord.Y
                            )
                        )?.Despawn();
                    break;
            }

            UpdateContentListBox();
            contentControl1.ReloadContents();
        }

        private void dataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataListView.SelectedIndices.Count == 0)
                return;

            DataTable DT = GlobalData.DBIO.ExecuteSQL("SELECT * FROM " + (currentLayer.ToString() + "s WHERE " + currentLayer.ToString() + "s.id=" + idList[dataListView.SelectedIndices[0]].ToString()));
            label3.Text = DT.Rows[0][1].ToString();
            label4.Text = DT.Rows[0][2].ToString();
        }
        private void layerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLayer = (Layer)layerComboBox.SelectedIndex;
            UpdateDepthComboBox();
            UpdateContentListBox();
            UpdateDataListView();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            //closeForm();
        }
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            GlobalData.WorldData.Control.ProcessingKeyPress(e);
            UpdateContentListBox();
        }
    }
}
