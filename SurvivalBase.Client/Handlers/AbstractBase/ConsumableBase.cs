using System;

namespace CRP.SurvivalBase.Client.Handlers.Base
{
	public abstract class ConsumableBase
	{
		public string ItemName { get; protected set; }
		public int NutritionValue { get; protected set; }
		public int ThirstValue { get; protected set; }
		public TimeSpan Delay { get; protected set; }

		protected ConsumableBase(string _item, int _nutrition, int _thirst, TimeSpan _delay)
		{
			this.ItemName = _item;
			this.NutritionValue = _nutrition;
			this.ThirstValue = _thirst;
			this.Delay = _delay;
		}

		public abstract void Consume(int amount);

		public abstract void Handler();
	}
}
