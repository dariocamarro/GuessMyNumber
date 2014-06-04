using Gamify.Contracts.Requests;
using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class MoveResultNotificationFactory : IMoveResultNotificationFactory
    {
        public IMoveResultNotificationObject Create(MoveRequestObject moveRequest, IGameMoveResponse moveResponse)
        {
            var moveResponseObject = moveResponse.MoveResponseObject as IAttemptResult;

            return new MoveResultNotificationObject
            {
                SessionName = moveRequest.SessionName,
                PlayerName = moveRequest.PlayerName,
                Number = moveRequest.MoveInformation,
                Goods = moveResponseObject.Goods,
                Regulars = moveResponseObject.Regulars,
                Bads = moveResponseObject.Bads
            };
        }
    }
}
