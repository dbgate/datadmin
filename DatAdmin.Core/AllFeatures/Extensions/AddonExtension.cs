using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class AddonExtension
    {
        public static T CloneUsingXml<T>(this T obj)
            where T : IAddonInstance
        {
            var doc = XmlTool.CreateDocument("Addon");
            obj.SaveToXml(doc.DocumentElement);
            return (T)obj.AddonType.LoadAddon(doc.DocumentElement);
        }

        public static void RedirectToCfgDirectory(this IFileBasedAddonInstance addon)
        {
            if (addon.IsInLibDirectory())
            {
                addon.AddonFileName = Path.Combine(addon.AddonType.GetDirectory(), Path.GetFileName(addon.AddonFileName));
            }
        }

        public static bool IsInLibDirectory(this IFileBasedAddonInstance addon)
        {
            return IOTool.FileIsInDirectory(addon.AddonFileName, Core.LibDirectory);
        }

        public static bool IsInCfgDirectory(this IFileBasedAddonInstance addon)
        {
            return IOTool.FileIsInDirectory(addon.AddonFileName, Core.ConfigDirectory);
        }

        public static bool HasLibVariant(this IFileBasedAddonInstance addon)
        {
            string fn = Path.GetFileName(addon.AddonFileName);
            return File.Exists(Path.Combine(addon.AddonType.GetLibDirectory(), fn));
        }

        public static void RedirectToLibDirectory(this IFileBasedAddonInstance addon)
        {
            string fn = Path.GetFileName(addon.AddonFileName);
            addon.AddonFileName = Path.Combine(addon.AddonType.GetLibDirectory(), fn);
        }
    }
}
