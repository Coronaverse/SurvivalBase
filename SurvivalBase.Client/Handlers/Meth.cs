using CRP.SurvivalBase.Client.Handlers.Base;
using static CitizenFX.Core.Native.API;
using System;

namespace CRP.SurvivalBase.Client.Handlers
{
	class Meth : DrugBase
	{
		private bool IsPlayerHigh = false;
		private int HandlerCount = 0;

		public Meth() : base("Meth", 0.25f, 100) { }

		public override void Handler()
		{
			#region Meth Effects
			if (Global.ActiveSurvive.Meth > 0)
			{
				RestorePlayerStamina(PlayerPedId(), 1.0f);

				if (!IsPlayerHigh)
				{
					IsPlayerHigh = true;
					SetTimecycleModifier("spectator5");
					SetPedMotionBlur(PlayerPedId(), true);
				}
			}
			else
			{
				if (IsPlayerHigh)
				{
					IsPlayerHigh = false;

					ClearTimecycleModifier();
					ResetScenarioTypesEnabled();
				}

				if (Global.ActiveSurvive.Meth < 0)
				{
					// TODO Negative Meth addiction effects
				}
			}
			#endregion

			Global.ActiveSurvive.Meth -= 5;

			if ((Global.ActiveSurvive.MethAddict <= 20) && (Global.ActiveSurvive.Meth < 0)) Global.ActiveSurvive.Meth = 0;

			HandlerCount++;
			if (HandlerCount >= 6)
			{
				Global.ActiveSurvive.MethAddict--;
				HandlerCount = 0;
			}
		}

		public override void Consume(int amount)
		{
			throw new System.NotImplementedException();
		}

		public override void Treatment()
		{
			throw new System.NotImplementedException();
		}
	}
}
