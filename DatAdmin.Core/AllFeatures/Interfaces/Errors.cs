using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace DatAdmin
{

    public class ConnectionFailedError : ExpectedError
    {
        public ConnectionFailedError(string errcode, Exception inner)
            : base(errcode + " " + Texts.Get("s_connect_failed_with$error", "error", inner.Message), inner)
        {
        }
    }


    public class DatabaseNotAccessibleError : ExpectedError
    {
        public DatabaseNotAccessibleError(string dbname, Exception inner)
            : base(Texts.Get("s_database_not_accessible$database", "database", dbname) + "\n" + inner.Message, inner)
        {
        }
    }


    public class DbAnalyseError : UnexpectedError
    {
        public DbAnalyseError(string message)
            : base(message, null)
        {
        }
    }

    public class CannotChangeTablePropertyError : ExpectedError
    {
        public CannotChangeTablePropertyError(string errcode)
            : base(errcode + " " + Texts.Get("s_cannot_change_table_property"), null)
        {
        }
    }

    public class AlterNotPossibleError : ExpectedError
    {
        public AlterNotPossibleError()
            : base(Texts.Get("s_alter_not_possible"), null)
        {
        }
    }

    public class XmlFormatError : ExpectedError
    {
        public XmlFormatError(string message)
            : base(message, null)
        {
        }
    }

    public class InvalidInputError : ExpectedError
    {
        public InvalidInputError(string message)
            : base(message, null)
        {
        }
    }

    public class DataError : ExpectedError
    {
        public DataError(string errcode, Exception err)
            : base(errcode + " " + Texts.Get("s_data_error") + ":" + err.Message, err)
        {

        }
    }

    public class DatabaseErrorItem
    {
        public string Message;
        /// <summary>
        /// line number, where error occured, numbered from 0
        /// </summary>
        public int LineNumber;
        public int ErrorCode;
        public string Procedure;
    }

    public class DatabaseError : ExpectedError
    {
        public List<DatabaseErrorItem> Items = new List<DatabaseErrorItem>();

        public int ErrorCode;

        public DatabaseError(Exception inner)
            : base(inner.Message, inner)
        {
        }
    }

    public class ArchiveError : ExpectedError
    {
        public ArchiveError(string message)
            : base(message, null)
        {
        }
    }

    public class InconsistentTableStructureError : ExpectedError
    {
        public InconsistentTableStructureError(string message)
            : base(message, null)
        {
        }
    }

    public class CommandLineError : ExpectedError
    {
        public CommandLineError(string msg)
            : base(msg, null)
        {
        }
    }

    public class RowTransformError : ExpectedError
    {
        public RowTransformError(string message)
            : base(message, null)
        {
        }
    }

    public class BedTableError : InternalError
    {
        public BedTableError(string msg)
            : base(msg)
        { }
    }

    public class BadBedRowStateError : InternalError
    {
        public BadBedRowStateError(string errcode, BedRowState expected, BedRowState found)
            : base(errcode + " Bad BED row state, expected:" + expected.ToString() + "; found:" + found.ToString())
        {
        }
    }

    public class ParseError : ExpectedError
    {
        public ParseError(string msg)
            : base(msg, null)
        {
        }
    }

    public class GenerateSqlError : UnexpectedError
    {
        public GenerateSqlError(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }

    public class SyntaxNotSupportedError : GenerateSqlError
    {
        public SyntaxNotSupportedError(string msg)
            : base(msg, null)
        {
        }
    }

    public class ProcessFailedError : ExpectedError
    {
        Process m_process;
        public ProcessFailedError(Process process)
            : base("Process failed", null)
        {
            m_process = process;
        }

        public override string ToString()
        {
            return String.Format("Process {0} failed\n", m_process);
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("Process {0} failed\n", m_process);
            //foreach (var msg in m_process.GetMessages())
            //{
            //    sb.AppendFormat("{0} {1} {2}\n", msg.Severity, msg.Category, msg.Message);
            //    if (!String.IsNullOrEmpty(msg.Detail)) sb.AppendFormat("  DETAIL:{0}\n", msg.Detail);
            //}
            //return sb.ToString();
        }
    }

    public class IncorrectObjectReferenceError : ExpectedError
    {
        public IncorrectObjectReferenceError(string errcode, string objtype, string objname)
            : base(errcode + " " + Texts.Get("s_incorrect_object_reference$objtype$objname", "objtype", objtype, "objname", objname), null)
        {
        }
    }

    public class BulkCopyInputError : ExpectedError
    {
        public BulkCopyInputError(string errcode, Exception inner)
            : base(errcode + " " + inner.Message, inner)
        {
        }
    }

    public class DataConversionError : ExpectedError
    {
        public DataConversionError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DataParseError : DataConversionError
    {
        public DataParseError(string data, TypeStorage type)
            : base(Texts.Get("s_cannot_convert$text$type", "text", data, "type", type), null)
        {
        }
    }

    public class ConnectionBrokenError : ExpectedError
    {
        public ConnectionBrokenError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ApiError : ExpectedError
    {
        public ApiError(string message)
            : base(message, null)
        {
        }
    }

    public class UnknownVirtualPathError : ExpectedError
    {
        public UnknownVirtualPathError(string virtualPath)
            : base("Virtual path cannot be parsed:" + virtualPath, null)
        {
        }
    }

    public class IncorrectEmailError : ExpectedError
    {
        static Regex m_emailRegex = new Regex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.IgnoreCase);

        public IncorrectEmailError(string errcode, string email)
            : base(errcode + " " + Texts.Get("s_incorrect$email", "email", email), null)
        {
        }

        public static bool IsValid(string email)
        {
            return m_emailRegex.Match(email).Success;
        }
    }

    public class ValueNotSpecifiedError : ExpectedError
    {
        public ValueNotSpecifiedError(string errcode, string value)
            : base(errcode + " " + Texts.Get("s_value_not_specified$value", "value", Texts.Get(value)), null)
        {
        }
    }

    public class UserInputSyntaxError : ExpectedError
    {
        public UserInputSyntaxError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DashboardAllreadyDesignedError : ExpectedError
    {
        public DashboardAllreadyDesignedError(DockPanelDashboard dashboard)
            : base(Texts.Get("s_dashboard_is_allready_designed$file", "file", Path.GetFileName(dashboard.AddonFileName)), null)
        {
        }
    }
}
