using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    /// <summary>
    /// auxiliary class, disposes also calling command, when disposing reader
    /// </summary>
    public class CommandDataReader : DataReaderProxy
    {
        IDisposable[] m_disposeList;
        public CommandDataReader(IDataReader src, params IDisposable[] disposeList)
            : base(src)
        {
            m_disposeList = disposeList;
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var d in m_disposeList)
            {
                if (d != null) d.Dispose();
            }
        }
    }
}
