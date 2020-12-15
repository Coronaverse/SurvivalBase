using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using JetBrains.Annotations;
using NFive.SDK.Client.Commands;
using NFive.SDK.Client.Communications;
using NFive.SDK.Client.Events;
using NFive.SDK.Client.Interface;
using NFive.SDK.Client.Services;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Models.Player;
using CRP.SurvivalBase.Client.Handlers;
using CRP.SurvivalBase.Shared;
using CRP.SurvivalBase.Client.Models;

namespace CRP.SurvivalBase.Client
{
	[PublicAPI]
	public class SurvivalBaseService : Service
	{
		public static SurvivalBaseService Instance;
		private List<Handlers.Base.ConsumableBase> ConsumableInstances = new List<Handlers.Base.ConsumableBase>();
		private List<Handlers.Base.DrugBase> DrugInstances = new List<Handlers.Base.DrugBase>();

		public SurvivalBaseService(ILogger logger, ITickManager ticks, ICommunicationManager comms, ICommandManager commands, IOverlayManager overlay, User user) :
			base(logger, ticks, comms, commands, overlay, user)
		{
			Instance = this;
		}

		public override Task Started()
		{
			Comms.Event(ExternalEvents.IdentityLogin).FromClient().OnRequest<Character>((e, c) => IdentityLogin(e, c));
			Comms.Event(ExternalEvents.ConsumeItem).FromClient().OnRequest<string, int>((e, item, amount) => ConsumeItem(e, item, amount));

			Initialize_Handlers();

			this.Ticks.On(HandlerTick);
			this.Ticks.On(UpdaterTick);

			return Task.FromResult(0);
		}

		#region Tick Handlers
		private async Task HandlerTick()
		{
			await Delay(TimeSpan.FromSeconds(10));

			if (Global.ActiveSurvive != null)
			{
				foreach (Handlers.Base.ConsumableBase _inst in ConsumableInstances)
				{
					try
					{
						if (_inst == null) throw new Exception("Instance is null");

						this.Logger.Debug("not null instance");
						_inst.Handler();
					}
					catch (Exception ex)
					{
						this.Logger.Debug(ex.ToString());
					}
				}

				foreach (Handlers.Base.DrugBase _inst in DrugInstances)
				{
					try
					{
						if (_inst == null) throw new Exception("Instance is null");

						this.Logger.Debug("not null instance");
						_inst.Handler();
					}
					catch (Exception ex)
					{
						this.Logger.Debug(ex.ToString());
					}
				}
			}
		}

		private async Task UpdaterTick()
		{
			await Delay(TimeSpan.FromSeconds(30));

			if (Global.ActiveSurvive != null) this.Comms.Event(SurvivalEvents.SyncSurvivalObject).ToServer().Emit(Global.ActiveSurvive);
		}
		#endregion
		#region Private Methods
		private async void IdentityLogin(ICommunicationMessage e, Character c)
		{
			Global.LoggedInCharacter = c;
			Global.ActiveSurvive = await this.Comms.Event(SurvivalEvents.GetSurvivalObject).ToServer().Request<Survive>(c.CharacterId);
		}

		private void ConsumeItem(ICommunicationMessage e, string item, int amount)
		{
			foreach (Handlers.Base.ConsumableBase _inst in ConsumableInstances) if (_inst.ItemName == item) _inst.Consume(amount);
			foreach (Handlers.Base.DrugBase _inst in DrugInstances) if (_inst.ItemName == item) _inst.Consume(amount);
		}
		#endregion
		#region Handler Class Instantiation
		private void Initialize_Handlers()
		{
			ConsumableInstances.Add(new Water());

			DrugInstances.Add(new Alcohol());
			DrugInstances.Add(new Cocaine());
			DrugInstances.Add(new Meth());
		}
		#endregion
		#region Public Methods
		public Survive GetActiveSurvive()
		{
			return Global.ActiveSurvive;
		}
		#endregion
	}
}
