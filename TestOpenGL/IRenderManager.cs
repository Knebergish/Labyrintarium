using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.Renders;


namespace TestOpenGL
{
    interface IRenderManager
    {
        void SetCamera(Camera camera);
        void SetMaxFPS(int maxFPS);

        void AddRenderObject(IRenderable renderObject);
        void RemoveRenderObject(IRenderable renderObject);

        void StartRender();
        void StopRender();

        int ActualFPS { get; }
        event ADelegate<int> ChangeActualFPSEvent;

        int MaxFPS { get; }
    }
}
