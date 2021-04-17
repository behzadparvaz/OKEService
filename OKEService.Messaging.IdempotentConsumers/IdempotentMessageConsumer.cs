using OKEService.Core.ApplicationServices.Commands;
using OKEService.Utilities.Configurations;
using OKEService.Utilities.Services.MessageBus;
using OKEService.Utilities.Services.Serializers;
using System;
using System.Collections.Generic;
using OKEService.Core.Domain.Events;
using System.Linq;

namespace OKEService.Messaging.IdempotentConsumers
{
    public class IdempotentMessageConsumer : IMessageConsumer
    {
        private readonly OKEServiceConfigurationOptions _hamoonConfigurations;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMessageInboxItemRepository _messageInboxItemRepository;
        private readonly Dictionary<string, string> _messageTypeMap = new Dictionary<string, string>();
        public IdempotentMessageConsumer(OKEServiceConfigurationOptions hamoonConfigurations, IEventDispatcher eventDispatcher, IJsonSerializer jsonSerializer, ICommandDispatcher commandDispatcher, IMessageInboxItemRepository messageInboxItemRepository)
        {
            _hamoonConfigurations = hamoonConfigurations;
            _eventDispatcher = eventDispatcher;
            _jsonSerializer = jsonSerializer;
            _commandDispatcher = commandDispatcher;
            _messageInboxItemRepository = messageInboxItemRepository;
            LoadMessageMap();
        }

        private void LoadMessageMap()
        {
            if (_hamoonConfigurations?.Messageconsumer?.Commands?.Any() == true)
            {
                foreach (var item in _hamoonConfigurations?.Messageconsumer?.Commands)
                {
                    _messageTypeMap.Add($"{_hamoonConfigurations.ServiceId}.{item.CommandName}", item.MapToClass);
                }
            }
            if (_hamoonConfigurations?.Messageconsumer?.Events?.Any() == true)
            {
                foreach (var eventPublisher in _hamoonConfigurations?.Messageconsumer?.Events)
                {
                    foreach (var @event in eventPublisher?.EventData)
                    {
                        _messageTypeMap.Add($"{eventPublisher.FromServiceId}.{@event.EventName}", @event.MapToClass);

                    }
                }
            }
        }

        public void ConsumeCommand(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var eventType = Type.GetType(mapToClass);
                dynamic command = _jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                _commandDispatcher.Send(command);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }

        public void ConsumeEvent(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var eventType = Type.GetType(mapToClass);
                dynamic @event = _jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                _eventDispatcher.PublishDomainEventAsync(@event);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }
    }
}
