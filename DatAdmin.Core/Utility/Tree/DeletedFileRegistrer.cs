using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class DeletedFileRegistrer
    {
        public static string DeletedVisibleFile = Path.Combine(Core.ConfigDirectory, "delvis.txt");
        public static string DeletedInvisibleFile = Path.Combine(Core.ConfigDirectory, "delinvis.txt");

        public static HashSetEx<string> DeletedVisible = new HashSetEx<string>();
        public static HashSetEx<string> DeletedInvisible = new HashSetEx<string>();

        public static void AddPath(string path, bool fileVisible)
        {
            if (fileVisible) _AddFile(path, DeletedVisible, DeletedVisibleFile);
            else _AddFile(path, DeletedInvisible, DeletedInvisibleFile);
        }

        private static void _AddFile(string path, HashSetEx<string> deleted, string indexFile)
        {
            var files = new HashSetEx<string>(IOTool.LoadLines(indexFile));
            files.Add(path);
            IOTool.SaveLines(indexFile, files);
            deleted.Add(path.ToLower());
        }

        public static void Initialize()
        {
            _Initialize(DeletedVisibleFile, DeletedVisible);
            _Initialize(DeletedInvisibleFile, DeletedInvisible);
        }

        private static void _Initialize(string indexFile, HashSetEx<string> files)
        {
            foreach (string line in IOTool.LoadLines(indexFile))
            {
                try
                {
                    if (File.Exists(line)) File.Delete(line);
                    if (Directory.Exists(line)) Directory.Delete(line, true);
                }
                catch
                {
                    files.Add(line.ToLower());
                }
            }
            IOTool.SaveLines(indexFile, files);
        }
    }
}
