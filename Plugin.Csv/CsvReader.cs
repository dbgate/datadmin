using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//namespace Plugin.Csv
//{
//    public class CsvReader
//    {
//        const int DEFAULT_BUFFER_SIZE = 0x1000;
//        Stream m_stream;
//        char[] m_buffer = new char[DEFAULT_BUFFER_SIZE];
//        int m_bufpos;
//        int m_bufsize;
//        bool m_eof;

//        char m_separator;

//        public CsvReader(Stream stream)
//        {
//            m_stream = stream;
//        }
//        void ReadBuffer()
//        {
//            m_bufsize = m_stream.Read(m_bufsize, 0, m_buffer.Length);
//            if (m_bufsize <= 0) m_eof = true;
//        }
//        IEnumerable<object[]> GetLines()
//        {
//        }
//    }
//}
