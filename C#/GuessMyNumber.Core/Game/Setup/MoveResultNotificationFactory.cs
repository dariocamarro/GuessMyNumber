using GuessMyNumber.Core.Interfaces;
using Gamify.Sdk.Setup.Definition;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk;

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
