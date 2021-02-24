using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace DatAdmin
{
    public static class XmlDataTool
    {
        public static bool CustomValueToStringOverride(Type type, object val, out string res)
        {
            if (val is DateTimeEx)
            {
                res = ((DateTimeEx)val).ToStringNormalized();
                return true;
            }
            else if (val is ISqlDialect)
            {
                var dialect = val as ISqlDialect;
                if (!dialect.Version.IsMax) res = dialect.DialectName + ":" + dialect.Version.ToString();
                else res = dialect.DialectName;
                return true;
            }
            res = "";
            return false;
        }

        public static bool CustomValueFromStringOverride(Type type, string sval, out object res)
        {
            if (type == typeof(DateTimeEx))
            {
                res = DateTimeEx.ParseNormalized(sval);
                return true;
            }
            else if (type == typeof(ISqlDialect))
            {
                if (sval.Contains(":"))
                {
                    string[] ar = sval.Split(new char[] { ':' }, 2);
                    var holder = DialectAddonType.Instance.FindHolder(ar[0]);
                    if (holder == null)
                    {
                        res = null;
                    }
                    else
                    {
                        ISqlDialect dial = (ISqlDialect)holder.CreateInstance();
                        dial.SetVersion(new SqlServerVersion(ar[1]));
                        res = dial;
                    }
                    return true;
                }
                else
                {
                    ISqlDialect dial = (ISqlDialect)DialectAddonType.Instance.FindHolder(sval).CreateInstance();
                    res = dial;
                    return true;
                }
            }
            res = null;
            return false;
        }


        public static void WriteQueueToXml(IDataQueue queue, Stream fw, string dbName, string tableName)
        {
            DataTable table = ConnTools.DataTableFromStructure(queue.GetRowFormat);
            table.TableName = tableName;

            List<string> fldnames = new List<string>();
            foreach (IColumnStructure col in queue.GetRowFormat.Columns)
            {
                fldnames.Add(col.ColumnName);
            }

            XmlWriter xw = XmlTextWriter.Create(fw, new XmlWriterSettings { Encoding = Encoding.UTF8, CheckCharacters = false });
            //XmlTextWriter xw = new XmlTextWriter(fw, Encoding.UTF8);
            //xw.Settings = new XmlWriterSettings { CheckCharacters = false };

            xw.WriteStartDocument();
            xw.WriteStartElement(dbName);
            // mono has bug in writing schema
            if (!Core.IsMono) table.WriteXmlSchema(xw);
            List<string> ids = new List<string>();
            foreach (IColumnStructure col in queue.GetRowFormat.Columns) ids.Add(XmlTool.NormalizeIdentifier(col.ColumnName));
            try
            {
                while (!queue.IsEof)
                {
                    IBedRecord row = queue.GetRecord();
                    xw.WriteStartElement(tableName);
                    for (int i = 0; i < row.FieldCount; i++)
                    {
                        row.ReadValue(i);
                        var type = row.GetFieldType();
                        if (type == TypeStorage.Null) continue;
                        xw.WriteElementString(ids[i], XmlTool.ObjectToString(row.GetValue(i)));
                    }
                    xw.WriteEndElement();
                }
            }
            finally
            {
                queue.CloseReading();
            }
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Flush();
        }

        public static DataTable ReadXmlToQueue(XmlReader reader, IDataQueue queue, string tableTagName)
        {
            try
            {
                reader.MoveToContent();
                reader.Read();
                reader.MoveToContent();

                DataTable structTable = null;
                if (reader.LocalName != tableTagName)
                {
                    structTable = new DataTable();
                    structTable.ReadXmlSchema(reader);
                }

                Dictionary<string, int> colPos = new Dictionary<string, int>();
                ITableStructure rowFormat;
                if (structTable != null)
                {
                    foreach (DataColumn col in structTable.Columns)
                    {
                        colPos[XmlTool.NormalizeIdentifier(col.ColumnName)] = col.Ordinal;
                    }
                    rowFormat = structTable.Columns.GetTableStructure(null);
                }
                else
                {
                    rowFormat = queue.PutRowFormat;
                    int index = 0;
                    foreach (var col in rowFormat.Columns)
                    {
                        colPos[XmlTool.NormalizeIdentifier(col.ColumnName)] = index;
                        index++;
                    }
                }
                while (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.LocalName != tableTagName) throw new XmlFormatError(String.Format("DAE-00301 Bad xml, expected tag {0}, found {1}", tableTagName, reader.LocalName));
                    reader.Read();
                    reader.MoveToContent();
                    object[] values = new object[rowFormat.Columns.Count];
                    for (int i = 0; i < values.Length; i++) values[i] = DBNull.Value;
                    while (reader.NodeType == XmlNodeType.Element)
                    {
                        string colname = reader.LocalName;
                        int pos = colPos[colname];
                        bool wasdata = false;

                        Type dataType = structTable != null ? structTable.Columns[pos].DataType : rowFormat.Columns[pos].DataType.DotNetType;
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            string data = reader.Value;
                            values[pos] = XmlTool.StringToObject_DataXml(dataType, data);
                            reader.Read();
                            wasdata = true;
                        }
                        if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == colname)
                        { // skip end of element
                            reader.Read();
                        }
                        else
                        {
                            // error, do not throw, it is error if .NET parser
                        }
                        //if (reader.NodeType != XmlNodeType.EndElement) throw new XmlFormatError("Bad XML, expected end of tag");
                        //if (reader.LocalName != colname) throw new XmlFormatError(String.Format("Bad xml, expected tag {0}, found {1}", colname, reader.LocalName));
                        //reader.Read();

                        if (!wasdata) values[pos] = XmlTool.StringToObject_DataXml(dataType, "");
                    }
                    queue.PutRecord(new ArrayDataRecord(rowFormat, values));
                    if (reader.NodeType == XmlNodeType.EndElement) reader.Read();
                }
                return structTable;
            }
            finally
            {
                queue.PutEof();
            }
        }
    }
}
