using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.Renders;


using System.Threading;


namespace TestOpenGL
{
    interface IRenderManager
    {
        void SetMaxFPS(int maxFPS);

        void StartRender();
        void StopRender();

        int ActualFPS { get; }
        event ADelegate<int> ChangeActualFPSEvent;

        int MaxFPS { get; }

        ManualResetEvent IsStopRender { get; }
    }
}
