using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DatAdmin
{
    public static class RegistryKeyExtension
    {
        public static string GetKeyValue(this RegistryKey key, string subbkeyname)
        {
            string value = null;
            RegistryKey subkey = key.OpenSubKey(subbkeyname);
            if (subkey != null)
            {
                value = subkey.GetValue(null) as string;
                subkey.Close();
            }
            return value;
        }
    } 
}
