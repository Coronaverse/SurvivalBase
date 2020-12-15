using CRP.SurvivalBase.Client.Handlers.Base;
using static CitizenFX.Core.Native.API;
using System;

namespace CRP.SurvivalBase.Client.Handlers
{
	class Alcohol : DrugBase
	{
		private bool IsPlayerDrunk = false;
		private int HandlerCount = 0;

		public Alcohol() : base("alcohol", 0.25f, 100) { }

		public override void Handler()
		{
			#region Alcohol Effects
			if (Global.ActiveSurvive.Alcohol > 0)
			{
				if (!IsPlayerDrunk)
				{
					IsPlayerDrunk = true;

					string animset = "MOVE_M@DRUNK@VERYDRUNK";					

					while (!HasAnimSetLoaded(animset)) RequestAnimSet(animset);

					SetCamEffect(1);
					SetPedIsDrunk(PlayerPedId(), true);
					SetPedMovementClipset(PlayerPedId(), animset, 1);
					SetPedMotionBlur(PlayerPedId(), true);
				}
			}
			else
			{
				if (IsPlayerDrunk)
				{
					IsPlayerDrunk = false;

					SetCamEffect(0);
					SetPedIsDrunk(PlayerPedId(), false);
					ResetPedMovementClipset(PlayerPedId(), 0);
					SetPedMotionBlur(PlayerPedId(), false);
				}

				if (Global.ActiveSurvive.Alcohol < 0)
				{
					// TODO Negative cocaine addiction effects
				}
			}
			#endregion

			Global.ActiveSurvive.Alcohol -= 5;
			if ((Global.ActiveSurvive.AlcoholAddict <= 20) && (Global.ActiveSurvive.Alcohol < 0)) Global.ActiveSurvive.Alcohol = 0;

			HandlerCount++;
			if (HandlerCount >= 6)
			{
				Global.ActiveSurvive.AlcoholAddict--;
				HandlerCount = 0;
			}
		}

		public override void Consume(int amount)
		{
			TaskStartScenarioInPlace(PlayerPedId(), "WORLD_HUMAN_SMOKING_POT", 0, true); // TODO Replace with drinking animation

			#region Addiction
			if (Global.ActiveSurvive.AlcoholAddict > 0)
			{
				Global.ActiveSurvive.AlcoholAddict += Convert.ToInt32(Global.ActiveSurvive.AlcoholAddict * this.AddictionBase) * amount;
			}
			else
			{
				Global.ActiveSurvive.AlcoholAddict = 10 * amount;
			}
			#endregion

			#region Consumption
			if (Global.ActiveSurvive.Alcohol >= 0)
			{
				Global.ActiveSurvive.Alcohol += this.ConsumptionBase * amount;
			}
			else if (Global.ActiveSurvive.Alcohol < 0)
			{
				Global.ActiveSurvive.Alcohol += Convert.ToInt32(this.ConsumptionBase * 0.5) * amount;
			}
			#endregion
		}

		public override void Treatment()
		{
			throw new System.NotImplementedException();
		}
	}
}
