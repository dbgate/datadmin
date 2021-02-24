using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class IOTool2
    {
        public static void CheckOutputFileName(ref string filename)
        {
            if (filename.IsEmpty()) throw new CheckConfigError("DAE-00299 " + Texts.Get("s_output_file_is_not_defined"));
            string dir = Path.GetDirectoryName(filename);
            if (dir.IsEmpty())
            {
                filename = Path.Combine(Core.DataDirectory, filename);
            }
            else
            {
                if (!Directory.Exists(dir))
                {
                    throw new CheckConfigError("DAE-00300 " + Texts.Get("s_output_directory_does_not_exist"));
                }
            }
        }
    }
}
