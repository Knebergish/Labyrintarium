using System.Data;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.DataIO
{
    class ObjectsAssistant
    {
        private DataTable objectsDataTable;

        public ObjectsAssistant()
        {
            objectsDataTable = new DataTable();
            objectsDataTable.Columns.Add("type", typeof(string));
            objectsDataTable.Columns.Add("objectId", typeof(int));
            objectsDataTable.Columns.Add("object", typeof(VisualObject));
        }

        public T GetObject<T>(int index) where T : VisualObject
        {
            T obj = FindObject<T>(index);

            if (obj != null)
                return obj;

            obj = (T)Program.OB.GetVO<T>(index);

            SaveObject<T>(obj);

            return obj;
        }

        T FindObject<T>(int index) where T : VisualObject
        {
            for (int i = 0; i < objectsDataTable.Rows.Count; i++)
            {
                if (objectsDataTable.Rows[i]["type"].ToString() == nameof(T))
                {
                    if (objectsDataTable.Rows[i]["objectId"].ToString() == index.ToString())
                    {
                        return (T)objectsDataTable.Rows[i]["object"];
                    }
                }
            }
            return null;
        }

        void SaveObject<T>(T obj) where T : VisualObject
        {
            objectsDataTable.Rows.Add(nameof(T), obj.Id, obj);
        }

    }
}
