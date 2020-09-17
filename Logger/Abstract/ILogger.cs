using System;
using System.Collections.Generic;
using System.Text;

namespace Log.Abstract
{
    public interface ILogger
    {
        void Log(string Message);
        void Log(string Message, Exception exception);
    }
}
