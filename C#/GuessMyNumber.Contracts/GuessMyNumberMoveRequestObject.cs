using Gamify.Contracts.Requests;

namespace GuessMyNumber.Contracts
{
    public class GuessMyNumberMoveRequestObject : IRequestObject
    {
        public string SessionId { get; set; }

        public string PlayerName { get; set; }

        public string Number { get; set; }
    }
}
