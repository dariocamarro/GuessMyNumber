using GuessMyNumber.Core.Interfaces;
using Gamify.Sdk;
using Gamify.Sdk.Setup.Definition;
using Gamify.Sdk.Services;
using Gamify.Sdk.Contracts.Requests;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberMoveHandler : IMoveHandler
    {
        public IGameMoveResponse Handle(MoveRequestObject moveRequest, IMoveService moveService)
        {
            var number = new Number(moveRequest.MoveInformation);
            var move = new GuessMyNumberMove(number);
            var moveResponse = moveService.Handle<INumber, IAttemptResult>(moveRequest.SessionName, moveRequest.PlayerName, move);

            return moveResponse;
        }
    }
}
