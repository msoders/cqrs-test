using Autofac;
using CqrsTest.Core;
using CqrsTest.Core.Async;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTest.Core.Tests
{
	[TestFixture]
	public class MessageDispatcherTests
	{
		private class TestMessage : IMessage
		{
			public int Value { get; set; }
		}

		private class TestMessageHandler : IMessageHandler<TestMessage>
		{
			public Action<int> Callback { get; set; }

			public Task<MessageResult> ExecuteAsync(TestMessage message)
			{
				Callback(message.Value);
				return Task.FromResult(MessageResult.None);
			}
		}

		[Test]
		public async Task MesssageDispatcher_TestMessageValue_Test()
		{
			var container = new ContainerBuilder();
			var callback = new Action<int>(res => { Assert.That(res, Is.EqualTo(1)); });
			container.RegisterType<TestMessageHandler>()
				.As<IMessageHandler<TestMessage>>()
				.OnActivating(x => x.Instance.Callback = callback);

			var b = container.Build();
			using (var lifetimeScope = b.BeginLifetimeScope())
			{
				var dispatcher = new MessageDispatcher(lifetimeScope);
				await dispatcher.DispatchAsync<TestMessage>(new TestMessage { Value = 1 });
			}
		}
	}
}