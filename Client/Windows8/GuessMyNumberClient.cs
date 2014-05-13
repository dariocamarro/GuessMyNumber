using System;
using System.Threading;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace GuessMyNumber.Client.Windows8
{
    public class GuessMyNumberClient : IGuessMyNumberClient
    {
        private readonly Uri guessMyNumberServerUri;
        private MessageWebSocket guessMyNumberWebSocketClient;
        private DataWriter guessMyNumberMessageWriter;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public bool IsInitialized
        {
            get
            {
                return this.guessMyNumberWebSocketClient != null;
            }
        }

        public GuessMyNumberClient(string guessMyNumberServerUri)
        {
            this.guessMyNumberServerUri = new Uri(guessMyNumberServerUri);
        }

        public async void Initialize()
        {
            var webSocketClient = this.guessMyNumberWebSocketClient;

            if (this.IsInitialized)
            {
                return;
            }

            webSocketClient = new MessageWebSocket();
            
            webSocketClient.Control.MessageType = SocketMessageType.Utf8;
            webSocketClient.MessageReceived += (sender, args) =>
            {
                this.ReceiveMessage(args);
            };
            webSocketClient.Closed += (sender, args) =>
            {
                this.CloseConnection(args);
            };

            await webSocketClient.ConnectAsync(this.guessMyNumberServerUri);

            this.guessMyNumberWebSocketClient = webSocketClient;
            this.guessMyNumberMessageWriter = new DataWriter(this.guessMyNumberWebSocketClient.OutputStream);
        }

        public void Send(string message)
        {
            if (!this.IsInitialized)
            {
                throw new Exception("The client is not initialized");
            }

            this.guessMyNumberMessageWriter.WriteString(message);
        }

        private void ReceiveMessage(MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (var reader = args.GetDataReader())
                {
                    reader.UnicodeEncoding = UnicodeEncoding.Utf8;

                    var message = reader.ReadString(reader.UnconsumedBufferLength);

                    if (this.MessageReceived != null)
                    {
                        this.MessageReceived(this, new MessageReceivedEventArgs(message));
                    }
                }
            }
            catch (Exception ex)
            {
                var status = WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add your specific error-handling code here.
            }
        }

        private void CloseConnection(WebSocketClosedEventArgs args)
        {
            // You can add code to log or display the code and reason
            // for the closure (stored in args.Code and args.Reason)

            var webSocketClient = Interlocked.Exchange(ref this.guessMyNumberWebSocketClient, null);

            if (webSocketClient != null)
            {
                webSocketClient.Dispose();
                webSocketClient = null;
            }
        }
    }
}
