using System;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTest.Core.Async	
{
	public interface IMessageDispatcher
	{
		Task DispatchAsync<TMessage>(TMessage message) where TMessage : IMessage;
	}
}