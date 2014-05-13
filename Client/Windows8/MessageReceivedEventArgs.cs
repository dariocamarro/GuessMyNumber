using System;

namespace GuessMyNumber.Client.Windows8
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageReceivedEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
