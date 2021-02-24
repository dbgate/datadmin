using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class ProcessAbortException : Exception
    {
        public ProcessAbortException(string message) : base(message) { }
    }

    //public enum ProcessStateHint { Started, OkFinished, ErrorFinished }

    public interface IProgressInfo : ILogger
    {
        //void LogMessage(MessageData msg);
        void SetCurWork(string title);
        // throws ProcessAbortException if ContinueOnErrors flag is false
        void RaiseError(string error);
        void SetCloseOnFinish(int severity, bool close);
        //void SetProcessStateHint(ProcessStateHint state);
    }

}
