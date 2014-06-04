using Gamify.Sdk;
using Gamify.Sdk.Data.Entities;
using Gamify.Sdk.Services;
using GuessMyNumber.Core.Interfaces;
using System;

namespace GuessMyNumber.Core.Game
{
    public class GuessMyNumberPlayer : SessionGamePlayer<INumber, IAttemptResult>
    {
        private readonly INumberComparer numberComparer;

        public INumber Number { get; private set; }

        public GuessMyNumberPlayer(IGamePlayer information, ISessionHistoryService<INumber, IAttemptResult> sessionHistoryService)
            : base(sessionHistoryService)
        {
            this.numberComparer = new NumberComparer();
            this.Information = information;
        }

        public void AssignNumber(INumber number)
        {
            if (this.Number != default(INumber))
            {
                var errorMessage = string.Format("The number of Player {0} can't be changed once assigned", this.Information.UserName);

                throw new ApplicationException(errorMessage);
            }

            this.Number = number;
        }

        public override ISessionHistory<INumber, IAttemptResult> GetHistory()
        {
            if (string.IsNullOrEmpty(this.SessionName))
            {
                var errorMessage = string.Format("The player {0} has not been assigned to the session yet", this.Information.UserName);

                throw new ApplicationException(errorMessage);
            }

            return this.sessionHistoryService.GetBySessionPlayer(this.SessionName, this.Information.UserName);
        }

        public override IGameMoveResponse<IAttemptResult> ProcessMove(IGameMove<INumber> move)
        {
            if (this.Number == default(INumber))
            {
                var errorMessage = string.Format("The player {0} is not ready because it doesn't have a number assigned", this.Information.UserName);

                throw new ApplicationException(errorMessage);
            }

            var triedNumber = move.MoveObject;
            var result = this.numberComparer.Compare(this.Number, triedNumber);

            var sessionHistoryItem = new SessionHistoryItem<INumber, IAttemptResult>(triedNumber, result);

            this.sessionHistoryService.Add(this.SessionName, this.Information.UserName, sessionHistoryItem);

            return new GuessMyNumberResponse(result);
        }
    }
}
