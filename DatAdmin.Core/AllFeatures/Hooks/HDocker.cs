using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HDocker
    {
        public static event Action<IDocker> ClosedDocker;

        public static void CallClosedDocker(IDocker docker)
        {
            if (ClosedDocker != null) ClosedDocker(docker);
        }
    }
}
