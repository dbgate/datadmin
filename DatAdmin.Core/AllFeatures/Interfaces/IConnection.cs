using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace DatAdmin
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message)
            : base(message)
        {
        }
    }

    public class InfoMessageEventArgs : EventArgs
    {
        public InfoMessageEventArgs(string message, string source, int code)
        {
            Message = message;
            Source = source;
            Code = code;
        }
        public readonly string Message;
        public readonly string Source;
        public readonly int Code;
    }

    public delegate void InfoMessageDelegate(object sender, InfoMessageEventArgs e);

    public delegate void PhysicalConnectionDelegate(IPhysicalConnection conn);
    public delegate void PhysicalConnectionPropertiesDelegate(IPhysicalConnection conn, IDictionary<string, object> properties);

    public class TableDataSetProperties
    {
        public string FilterSqlCondition;
        // sql condition without user input; used when query with FilterSqlCondition returns error
        // to determine, whether error is in user input
        public string FilterSqlConditionNoUserInput;
        // order in format col1 ASC/DESC, col2 ASC/DESC
        public string SortOrder;
        // if null, use default perspective
        public TablePerspective Perspective;
    }

    public class TablePageProperties : TableDataSetProperties
    {
        public int? Start;
        public int? Count;
    }


    /// <summary>
    /// if DBConnection implemens this interface, it can send scripts consisting of morec commands
    /// </summary>
    public interface IScriptAcceptConnection
    {
        /// <summary>
        /// sends batch of commands
        /// </summary>
        /// <param name="commands"></param>
        void SendScript(IEnumerable<string> commands);
        /// <summary>
        /// if this returns false, it behaves uequally as if class doesn't implement IScriptAcceptConnection
        /// </summary>
        bool SendScriptSupported { get; }
    }

    /// <summary>
    /// if DBConnection implemens this interface, it can send commands contained binary data
    /// commands are encoded to string using BinaryCommandEncoding 
    /// usually is used iso-8859-1 (latin1), it preserves all binary data
    /// </summary>
    public interface IBinaryCommandConnection
    {
        Encoding BinaryCommandEncoding { get; set; }
    }

    /// <summary>
    /// if DBConnection implemens this interface, it has no state and all initializing SQL code
    /// must be defined in SessionInitScript
    /// </summary>
    public interface IStatelessConnection
    {
        string SessionInitScript { get; set; }
    }
}
