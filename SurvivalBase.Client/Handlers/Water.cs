using System;
using CRP.SurvivalBase.Client.Handlers.Base;

namespace CRP.SurvivalBase.Client.Handlers
{
	class Water : ConsumableBase
	{
		public Water() : base("water", 0, 25, TimeSpan.FromMinutes(1)) { }

		public override void Consume(int amount)
		{
			// TODO drinking animation
			Global.ActiveSurvive.Water += this.ThirstValue;
		}

		public override void Handler()
		{
			
		}
	}
}
