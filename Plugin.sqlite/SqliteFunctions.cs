using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace Plugin.sqlite
{
    [SQLiteFunction(Name = "md5", Arguments = 1, FuncType = FunctionType.Scalar)]
    public class Md5Function : SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            MD5 md5 = MD5.Create();
            byte[] bkey = md5.ComputeHash(Encoding.UTF8.GetBytes(args[0].ToString()));
            return StringTool.EncodeHex(bkey);
        }
    }
}
