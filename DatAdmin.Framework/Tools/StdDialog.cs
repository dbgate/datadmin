using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public static class StdDialog
    {
        public static void ShowError(string error)
        {
            if (Framework.IsGUI)
            {
                MessageBox.Show(Texts.Get(error), VersionInfo.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Console.Error.WriteLine("(Error):" + Texts.Get(error));
            }
        }
        public static void ShowInfo(string info)
        {
            if (Framework.IsGUI)
            {
                MessageBox.Show(Texts.Get(info), VersionInfo.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.Error.WriteLine("(Info):" + Texts.Get(info));
            }
        }
        public static bool ReallyDeleteFile(object fn)
        {
            return MessageBox.Show(Texts.Get("s_really_delete$file", "file", fn), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static bool ReallyOverwriteFile(object fn)
        {
            return MessageBox.Show(Texts.Get("s_file_exists_overwrite$file", "file", fn), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        }

        public static bool YesNoDialog(string text, params string[] parval)
        {
            string label = Texts.Get(text, parval);
            return DialogResult.Yes == MessageBox.Show(label, VersionInfo.ProgramTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static bool CheckAbsoluteOutputFileName(string fn, string extensions)
        {
            try
            {
                if (!Path.IsPathRooted(fn))
                {
                    StdDialog.ShowError("s_please_enter_full_path_to_file");
                    return false;
                }
                string dir = Path.GetDirectoryName(fn);
                if (!Directory.Exists(dir))
                {
                    StdDialog.ShowError("s_output_directory_does_not_exist");
                    return false;
                }
                if (!IOTool.FileHasOneOfExtension(fn, extensions))
                {
                    StdDialog.ShowError(Texts.Get("s_incorrect_file$extension", "extension", extensions.ToLower()));
                    return false;
                }
            }
            catch (Exception err)
            {
                StdDialog.ShowError(err.Message);
                return false;
            }
            if (File.Exists(fn) && !StdDialog.ReallyOverwriteFile(fn)) return false;
            return true;
        }
    }
}
