using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;

namespace DatAdmin
{
    public interface IDumpLoaderConfig
    {
    }

    public interface IDumpLoader
    {
        DbConnection Connection { get; set; }
        DbTransaction Transaction { get; set; }
        IProgressInfo ProgressInfo { get; set; }
        IDumpLoaderConfig Config { get; set; }
        void Run(Stream fr);
        void Run(TextReader reader);
    }

    public interface IQuerySplitter
    {
        bool AllowGoSeparator { get; set; }
        bool StripComments { get; set; }
        IEnumerable<SplitQueryItem> Run(TextReader reader);
    }

    public struct SplitQueryItem
    {
        public string Data;
        public string Delimiter;
        public int StartLine;
        public int Length
        {
            get
            {
                int res = 0;
                if (Data != null) res += Data.Length;
                if (Delimiter != null) res += Delimiter.Length;
                return res;
            }
        }
        public override string ToString()
        {
            return Data;
        }
        public static implicit operator string(SplitQueryItem value)
        {
            return value.Data;
        }
    }
}
