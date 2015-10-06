using Autofac;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTest.Core.Async
{
	public class MessageDispatcher : IMessageDispatcher
	{
		private readonly ILifetimeScope _lifetimeScope;

		public MessageDispatcher(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;
		}

		public Task DispatchAsync<TMessage>(TMessage message) where TMessage : IMessage
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			var handler = _lifetimeScope.Resolve<IMessageHandler<TMessage>>();

			if (handler == null)
			{
				throw new InvalidOperationException("The handler for '" + typeof(TMessage) + "'is not registered");
			}
			return handler.ExecuteAsync(message);
		}
	}
}