using System;
using SnowPro.Shared.Contracts;

namespace AuthorizationService.Infrastructure.MessageBroker
{
    public interface IMessageService 
    {
        void Publish<T>(T message);
        void PublishUser(UserDto user);
    }
}

