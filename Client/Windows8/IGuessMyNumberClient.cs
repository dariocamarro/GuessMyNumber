using System;

namespace GuessMyNumber.Client.Windows8
{
    public interface IGuessMyNumberClient
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        bool IsInitialized { get; }

        void Initialize();

        void Send(string message);
    }
}
