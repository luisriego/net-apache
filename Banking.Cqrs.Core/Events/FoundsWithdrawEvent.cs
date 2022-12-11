namespace Banking.Cqrs.Core.Events
{
	public class FoundsWithdrawEvent : BaseEvent
	{
		public double Amount { get; set; }

		public FoundsWithdrawEvent(string id) : base(id)
        {
        }
	}
}

