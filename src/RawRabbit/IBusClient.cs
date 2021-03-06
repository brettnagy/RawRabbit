﻿using System;
using System.Threading.Tasks;
using RawRabbit.Configuration.Publish;
using RawRabbit.Configuration.Request;
using RawRabbit.Configuration.Respond;
using RawRabbit.Configuration.Subscribe;
using RawRabbit.Context;

namespace RawRabbit
{
	public interface IBusClient<out TMessageContext> : IDisposable where TMessageContext : IMessageContext
	{
		void SubscribeAsync<T>(Func<T, TMessageContext, Task> subscribeMethod, Action<ISubscriptionConfigurationBuilder> configuration = null);

		Task PublishAsync<T>(T message = default(T), Guid globalMessageId = new Guid(), Action<IPublishConfigurationBuilder> configuration = null);

		void RespondAsync<TRequest, TResponse>(Func<TRequest, TMessageContext, Task<TResponse>> onMessage, Action<IResponderConfigurationBuilder> configuration = null);

		Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message = default(TRequest), Guid globalMessageId = new Guid(), Action<IRequestConfigurationBuilder> configuration = null);
	}
}
