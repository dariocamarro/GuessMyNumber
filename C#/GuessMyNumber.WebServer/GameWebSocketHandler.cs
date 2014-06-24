﻿using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.WebServer
{
    public class GameWebSocketHandler : WebSocketHandler
    {
        private static ICollection<WebSocketHandler> connectedClients;

        private readonly IGameInitializer gameInitializer;
        private readonly ISerializer serializer;

        private IGameService gameService;

        public string UserName { get; private set; }

        static GameWebSocketHandler()
        {
            connectedClients = new WebSocketCollection();
        }

        public GameWebSocketHandler(IGameInitializer gameInitializer, ISerializer serializer)
        {
            this.gameInitializer = gameInitializer;
            this.serializer = serializer;
        }

        public override void OnOpen()
        {
            this.Initialize();

            connectedClients.Add(this);
        }

        public override void OnMessage(string message)
        {
            var gameRequest = this.serializer.Deserialize<GameRequest>(message);

            if (gameRequest.Type != (int)GameRequestType.PlayerConnect && string.IsNullOrEmpty(this.UserName))
            {
                throw new ApplicationException("A connection error occurred. The players must be connected in order to perform other type of requests");
            }

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect && !string.IsNullOrEmpty(this.UserName))
            {
                throw new ApplicationException("A connection error occurred when trying to connect a player already connected");
            }

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect)
            {
                var playerConnectRequest = this.serializer.Deserialize<PlayerConnectRequestObject>(gameRequest.SerializedRequestObject);

                this.UserName = playerConnectRequest.PlayerName;

                this.gameService.Connect(playerConnectRequest.PlayerName, playerConnectRequest.AccessToken);
            }
            else
            {
                this.gameService.Send(message);
            }
        }

        public override void OnClose()
        {
            base.OnClose();

            this.gameService.Disconnect(this.UserName);
            connectedClients.Remove(this);
        }

        private void Initialize()
        {
            var gameDefinition = new GuessMyNumberDefinition();

            this.gameService = gameInitializer.Initialize(gameDefinition);

            this.gameService.Notification += (sender, args) =>
            {
                this.PushMessage(args.Receiver, args.Notification);
            };
        }

        private void PushMessage(string receiver, GameNotification notification)
        {
            var serializedNotification = this.serializer.Serialize(notification);
            var client = connectedClients
                .Cast<GameWebSocketHandler>()
                .FirstOrDefault(c => c.UserName == receiver);

            if (client != null)
            {
                client.Send(serializedNotification);
            }
        }
    }
}