using System;

namespace SolidTest.Controls
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }


}