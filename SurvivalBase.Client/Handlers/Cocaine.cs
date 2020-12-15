using CRP.SurvivalBase.Client.Handlers.Base;
using static CitizenFX.Core.Native.API;
using System;

namespace CRP.SurvivalBase.Client.Handlers
{
	class Cocaine : DrugBase
	{
		private bool IsPlayerHigh = false;
		private int HandlerCount = 0;

		public Cocaine() : base("cocaine", 0.25f, 100) { }

		public override void Handler()
		{
			#region Cocaine Effects
			if (Global.ActiveSurvive.Cocaine > 0)
			{
				if (!IsPlayerHigh)
				{
					IsPlayerHigh = true;
					SetRunSprintMultiplierForPlayer(PlayerPedId(), (float)1.49);
					SetTimecycleModifier("spectator5");
					SetPedMotionBlur(PlayerPedId(), true);
				}
			}
			else
			{
				if (IsPlayerHigh)
				{
					IsPlayerHigh = false;

					SetRunSprintMultiplierForPlayer(PlayerPedId(), (float)1.00);
					ClearTimecycleModifier();
					ResetScenarioTypesEnabled();
				}

				if (Global.ActiveSurvive.Cocaine < 0)
				{
					// TODO Negative cocaine addiction effects
				}
			}
			#endregion

			Global.ActiveSurvive.Cocaine -= 5;
			if ((Global.ActiveSurvive.CocaineAddict <= 20) && (Global.ActiveSurvive.Cocaine < 0)) Global.ActiveSurvive.Cocaine = 0;

			HandlerCount++;
			if (HandlerCount >= 6)
			{
				Global.ActiveSurvive.CocaineAddict--;
				HandlerCount = 0;
			}
		}

		public override void Consume(int amount)
		{
			TaskStartScenarioInPlace(PlayerPedId(), "WORLD_HUMAN_SMOKING_POT", 0, true); // TODO Replace with snorting animation https://forum.cfx.re/t/looking-for-snorting-coke-animation/434332	

			#region Addiction
			if (Global.ActiveSurvive.CocaineAddict > 0)
			{
				Global.ActiveSurvive.CocaineAddict += Convert.ToInt32(Global.ActiveSurvive.CocaineAddict * this.AddictionBase) * amount;
			}
			else
			{
				Global.ActiveSurvive.CocaineAddict = 10 * amount;
			}
			#endregion

			#region Consumption
			if (Global.ActiveSurvive.Cocaine >= 0)
			{
				Global.ActiveSurvive.Cocaine += this.ConsumptionBase * amount;
			}
			else if (Global.ActiveSurvive.Cocaine < 0)
			{
				Global.ActiveSurvive.Cocaine += Convert.ToInt32(this.ConsumptionBase * 0.5) * amount;
			}
			#endregion
		}

		public override void Treatment()
		{
			throw new System.NotImplementedException();
		}
	}
}
