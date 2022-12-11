using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Domain
{
	public abstract class AggregateRoot
	{
		public string Id { get; set; } = string.Empty;

		private int version = -1;

		List<BaseEvent> changes = new List<BaseEvent>();

		public int GetVesion()
		{
			return version;
		}

		public void SetVersion(int version)
		{
			this.version = version;
		}

		public List<BaseEvent> GetUncommitedChanges()
		{
			return changes;
		}

		public void MarkChangesAsCommited()
		{
			changes.Clear();
		}

		public void ApplyChange(BaseEvent @event, bool isNewEvent)
		{
			try
			{
				var eventClass = @event.GetType();
				var method = GetType().GetMethod("Apply", new[] { eventClass });

				method.Invoke(this, new object[] { @event });
			}
			catch (Exception ex)
			{

			}
			finally
			{
				if (isNewEvent)
				{
					changes.Add(@event);
				}
			}
		}

		public void RaiseEvent(BaseEvent @event)
		{
			ApplyChange(@event, true);
		}

		public void ReplyEvents(IEnumerable<BaseEvent> events)
		{
			foreach (var evt in events)
			{
				ApplyChange(evt, false);
			}
		}
	}
}

