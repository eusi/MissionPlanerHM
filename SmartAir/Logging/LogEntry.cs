using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

public class LogEntry
{

    private string _message;

    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }
    private Level _level;

    public Level Level
    {
        get { return _level; }
        set { _level = value; }
    }
    private string _stackTrace;

    public string StackTrace
    {
        get { return _stackTrace; }
        set { _stackTrace = value; }
    }
    private string _modul;

    public string Modul
    {
        get { return _modul; }
        set { _modul = value; }
    }


}
