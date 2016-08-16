using TestOpenGL.Renders;


namespace TestOpenGL
{
    interface ILowLevelLibraryble
    {
        Texture LoadTextureFromFile(string path);
        void DrawCell(Texture texture, int xCoord, int yCoord);
        void ClearScreen();
        void RedrawScreed();
        void SettingVisibleAreaSize();
    }
}
