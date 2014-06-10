using Gamify.Sdk;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class SessionPlayerSetup : ISessionPlayerSetup
    {
        public void GetPlayerReady(GameAcceptedRequestObject gameAcceptedRequest, ISessionGamePlayerBase gamePlayer)
        {
            this.GetPlayerReady(gameAcceptedRequest.AdditionalInformation, gamePlayer);
        }

        public void GetPlayerReady(CreateGameRequestObject createGameRequest, ISessionGamePlayerBase gamePlayer)
        {
            this.GetPlayerReady(createGameRequest.AdditionalInformation, gamePlayer);
        }

        private void GetPlayerReady(string additionalInformation, ISessionGamePlayerBase gamePlayer)
        {
            var playerNumber = new Number(additionalInformation);

            (gamePlayer as GuessMyNumberPlayer).AssignNumber(playerNumber);
        }
    }
}
