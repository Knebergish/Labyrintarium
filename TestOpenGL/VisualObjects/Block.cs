namespace TestOpenGL.VisualObjects
{
    class Block: VisualObject
    {
        //Проходимость (для сущностей)
        public bool passableness { get; set; }
        //Прозрачность (для видимости за объектом)
        public bool transparency { get; set; }
        //Проницаемость (для атаки сквозь такой объект)
        public bool permeability { get; set; }

        public Block()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();
            id = -1;
            //texture.imageId = -1;
            texture.textureId = -1;
            //NumberTexture = -1;
            visualObjectInfo.name = "";
            visualObjectInfo.description = "";
            passableness = true;
            transparency = true;
            permeability = true;
        }

        /*~Block()
        {
            System.Windows.Forms.MessageBox.Show("Потрачено");
        }*/
    }
}
