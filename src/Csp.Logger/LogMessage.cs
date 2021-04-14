﻿using System;

namespace Csp.Logger
{
    public class LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }

        public LogMessage(DateTimeOffset offset,string msg)
        {
            Timestamp = offset;
            Message = msg;
        }
    }
}
