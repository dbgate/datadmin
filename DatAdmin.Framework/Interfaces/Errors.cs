using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class AnyError : Exception
    {
        public AnyError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ExpectedError : AnyError
    {
        public ExpectedError(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ExpectedError(string message)
            : base(message, null)
        {
        }
    }

    public abstract class UnexpectedError : AnyError
    {
        public UnexpectedError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class InternalError : UnexpectedError
    {
        public InternalError(string message) : this(message, null) { }

        public InternalError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class SerializeNotSupportedException : ExpectedError
    {
        public SerializeNotSupportedException(string errcode)
            : base(errcode + " " + Texts.Get("s_serialize_is_not_supported"), null)
        {
        }
    }

    public class WaitAbortError : ExpectedError
    {
        public WaitAbortError()
            : base(Texts.Get("s_operation_canceled_by_user"), null)
        {
        }
    }

    public class OperationCanceledError : ExpectedError
    {
        public OperationCanceledError()
            : base(Texts.Get("s_operation_canceled_by_user"), null)
        {
        }
    }

    public class CheckConfigError : ExpectedError
    {
        public CheckConfigError(string message)
            : base(message, null)
        {
        }
    }

    public class NotImplementedError : UnexpectedError
    {
        public NotImplementedError(string errcode)
            : base(errcode + " " + Texts.Get("s_operation_not_implemented"), null)
        {
        }
    }

    public class DeleteError : ExpectedError
    {
        public DeleteError(string fn, Exception inner)
            : base("File cannot be deleted:" + fn, inner)
        {
        }
    }

    public class QueueClosedError : InternalError
    {
        public QueueClosedError(string errcode)
            : base(errcode + " Queue closed, cannot perform operations on it")
        {
        }

        public QueueClosedError(string errcode, Exception inner)
            : base(errcode + " Queue closed, cannot perform operations on it", inner)
        {
        }
    }

    public class BadSettingsError : ExpectedError
    {
        string m_cfgpath;
        public BadSettingsError(string message, string cfgpath)
            : base(message, null)
        {
            m_cfgpath = cfgpath;
        }

        public string ConfigPath { get { return m_cfgpath; } }
    }

    public class HttpApiError : ExpectedError
    {
        public HttpApiError(string message)
            : base(message, null)
        {
        }
    }

    public class RegistryError : InternalError
    {
        public RegistryError(string msg)
            : base(msg)
        {
        }
    }

    public class MissingFeatureError : ExpectedError
    {
        public MissingFeatureError(string feature)
            : base(Texts.Get("s_missing$feature", "feature", FeatureAddonType.GetTitle(feature)), null)
        {
        }
    }
}
