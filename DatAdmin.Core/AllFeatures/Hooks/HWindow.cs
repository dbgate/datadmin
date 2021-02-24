using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HWindow
    {
        public static event Action ChangedContentWindows;
        public static event Action ChangedContent;

        public static void CallChangedContentWindows()
        {
            if (ChangedContentWindows != null) ChangedContentWindows();
        }

        public static void CallChangedContent()
        {
            if (ChangedContent != null) ChangedContent();
        }
    }
}
