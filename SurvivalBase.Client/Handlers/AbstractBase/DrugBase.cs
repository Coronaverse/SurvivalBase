namespace CRP.SurvivalBase.Client.Handlers.Base
{
	public abstract class DrugBase
	{
		public string ItemName { get; protected set; }
		public double AddictionBase { get; protected set; }
		public int ConsumptionBase { get; protected set; }

		protected DrugBase(string _item, float _addiction, int _consumption)
		{
			this.ItemName = _item;
			this.AddictionBase = _addiction;
			this.ConsumptionBase = _consumption;
		}

		public abstract void Handler();
		public abstract void Consume(int amount);
		public abstract void Treatment();
	}
}
