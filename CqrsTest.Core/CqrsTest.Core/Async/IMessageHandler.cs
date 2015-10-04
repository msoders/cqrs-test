using System;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTest.Core.Async
{
	public interface IMessageHandler<in TMessage> where TMessage : IMessage
	{
		Task<MessageResult> ExecuteAsync(TMessage message);
	}
}