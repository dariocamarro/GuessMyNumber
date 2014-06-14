using Gamify.Sdk.Contracts.Notifications;

namespace GuessMyNumber.Core.Game
{
    public class GuessMyNumberPlayerHistoryItemObject : IPlayerHistoryItem
    {
        public string Number { get; set; }

        public int Goods { get; set; }

        public int Regulars { get; set; }

        public int Bads { get; set; }
    }
}
