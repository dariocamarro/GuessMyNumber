using Gamify.Contracts.Requests;

namespace GuessMyNumber.Contracts.Requests
{
    public class MoveRequestObject : IRequestObject
    {
        public string SessionId { get; set; }

        public string PlayerName { get; set; }

        public string Number { get; set; }
    }
}
